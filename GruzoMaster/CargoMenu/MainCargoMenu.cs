using GruzoMaster.Objects;
using GruzoMaster.Objects.Cargo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace GruzoMaster.CargoMenu
{
    public partial class MainCargoMenu : Form
    {
        public List<Cargo> CargoList { get; set; } = new List<Cargo>();
        private AddCargoMenu AddCargoMenu { get; set; } = null;
        private EditingCargoMenu EditingCargoMenu { get; set; } = null;
        private FilterCargoMenu FilterCargoMenu { get; set; } = null;
        public MainCargoMenu()
        {
            InitializeComponent();
            dataGridView1.UserDeletingRow += DataGridView1_UserDeletingRow;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            LoadCargoMenu(1);
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (EditingCargoMenu != null)
            {
                MessageBox.Show("У вас уже есть открытое меню для изменения данных о грузе !");
                return;
            }
            if (!UserSettings.GetAccessUser(UserSettings.UserSetting.EditingCargoMenu))
            {
                MessageBox.Show("У вас нету доступа к этому меню !");
                return;
            }
            if (e.RowIndex >= 0)
            {
                Int64 cargoId = Convert.ToInt64(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value);
                Cargo cargo = this.CargoList.Find(_ => _.ID == cargoId);
                if (cargo == null)
                {
                    MessageBox.Show("Такой груз не найден !");
                    return;
                }
                this.EditingCargoMenu = new EditingCargoMenu(cargo);
                this.EditingCargoMenu.FormClosed += EditingCargoMenu_FormClosed;
                this.EditingCargoMenu.Show();
            }
        }

        private void EditingCargoMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.EditingCargoMenu = null;
        }

        public async void LoadCargoMenu(Int32 currentPage, Int32 pageSize = 30)
        {
            try
            {
                Int32 offset = (currentPage - 1) * pageSize;
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `cargo` LIMIT {pageSize} OFFSET {offset}");
                CargoList.Clear();
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Int64 idCargo = Convert.ToInt64(row["ID"]);
                        Int32 idCreatorUser = Convert.ToInt32(row["ID_user_creator"]);
                        Int32 idCompany = Convert.ToInt32(row["ID_company"]);
                        Int32 idTrasport = Convert.ToInt32(row["ID_Transport"]);
                        String name = Convert.ToString(row["Name"]);
                        String description = Convert.ToString(row["Description"]);
                        String addressFromCargo = Convert.ToString(row["addressFromCargo"]);
                        String addressToCargo = Convert.ToString(row["AddressToCargo"]);
                        Int32 idDriver = Convert.ToInt32(row["DriverID"]);
                        Int32 price = Convert.ToInt32(row["Price"]);
                        Int32 idForwarder = Convert.ToInt32(row["ForwarderID"]);
                        CargoDeliveryType cargoDeliveryType = (CargoDeliveryType)Convert.ToInt32(row["DeliveryType"]);
                        List<CargoLog> cargoLogs = JsonConvert.DeserializeObject<List<CargoLog>>(row["CargoLogs"].ToString());
                        Company company = null;
                        if (idCompany != -1)
                        {
                            company = await Company.GetCompanyById(idCompany);
                        }
                        Transport transport = null;
                        if (idTrasport != -1)
                        {
                            transport = await Transport.GetTransportById(idTrasport);
                        }
                        Driver driverInfo = null;
                        if (idDriver != -1)
                        {
                            driverInfo = await Driver.GetDriverById(idDriver);
                        }
                        User user = null;
                        if (idCreatorUser != -1)
                        {
                            user = await User.GetUserById(idCreatorUser);
                        }
                        User forwarderUser = null;
                        if (idForwarder != -1)
                        {
                            forwarderUser = await User.GetUserById(idForwarder);
                        }
                        CargoList.Add(new Cargo()
                        {
                            ID = idCargo,
                            CreateUserCargo = user,
                            CustomerCompany = company,
                            Driver = driverInfo,
                            TransportCargo = transport,
                            Price = price,
                            AddressFromCargo = addressFromCargo,
                            AddressToCargo = addressToCargo,
                            DeliveryType = cargoDeliveryType,
                            CargoLogs = cargoLogs,
                            Name = name,
                            Forwarder = forwarderUser,
                            Description = description,
                        });
                    }
                    PopulateDataGridView();
                    this.textBox1.Text = currentPage.ToString();
                }
            }
            catch (Exception e) { MessageBox.Show("LoadCargoMenu: " + e.ToString()); }
        }
        private void PopulateDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            // Add columns
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("CreateUserCargoName", "Имя создателя");
            dataGridView1.Columns.Add("CustomerCompanyName", "Название компании");
            dataGridView1.Columns.Add("DriverName", "Имя водителя");
            dataGridView1.Columns.Add("TransportCargoGovNumber", "Гос. номер транспорта");
            dataGridView1.Columns.Add("TransportCargoModelDescriptionName", "Модель транспорта");
            dataGridView1.Columns.Add("Price", "Цена");
            dataGridView1.Columns.Add("Name", "Название");
            dataGridView1.Columns.Add("DeliveryType", "Тип доставки");
            dataGridView1.Columns.Add("AddressFromCargo", "Адрес отправления");
            dataGridView1.Columns.Add("AddressToCargo", "Адрес доставки");
            dataGridView1.Columns.Add("ForwarderName", "Экспедитор");


            // Add rows
            foreach (var cargo in CargoList)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView1,
                    cargo.ID,
                    cargo.CreateUserCargo?.Name,
                    cargo.CustomerCompany?.Name,
                    cargo.Driver?.FullName,
                    cargo.TransportCargo?.GovNumber,
                    cargo.TransportCargo?.ModelDescriptionName,
                    cargo.Price.ConvertToFormatMoney() + " ₽",
                    cargo.Name,
                    Cargo.GetDeliveryTypeDescription(cargo.DeliveryType),
                    cargo.AddressFromCargo,
                    cargo.AddressToCargo,
                    cargo.Forwarder?.Name
                );

                dataGridView1.Rows.Add(row);
            }
            dataGridView1.ReadOnly = true;

        }
        private void добавитьЗаказToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.AddCargoMenu != null)
            {
                MessageBox.Show("У вас уже есть открытое меню добавления груза !");
                return;
            }
            this.AddCargoMenu = new AddCargoMenu();
            this.AddCargoMenu.FormClosed += AddCargoMenu_FormClosed;
            this.AddCargoMenu.Show();
        }
        private void DataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = true;
        }
        private void AddCargoMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.AddCargoMenu = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.LoadCargoMenu(Convert.ToInt32(this.textBox1.Text) + 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Int32 page = Convert.ToInt32(this.textBox1.Text);
            if (page <= 1)
            {
                MessageBox.Show("Это предыдущая страницы !");
                return;
            }
            this.LoadCargoMenu(page - 1);
        }

        private void выставитьСчетФактуруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Пожалуйста, выберите строку.");
                    return;
                }

                var selectedRow = dataGridView1.SelectedRows[0];

                Int64 cargoId = Convert.ToInt64(selectedRow.Cells["ID"].Value);
                Cargo cargo = this.CargoList.Find(_ => _.ID == cargoId);
                if (cargo == null)
                {
                    MessageBox.Show("Такой груз не найден !");
                    return;
                }

                var companyName = cargo.CustomerCompany.Name;
                var email = cargo.CustomerCompany.Email;
                var city = cargo.CustomerCompany.Country.ToString()  + " " + cargo.CustomerCompany.City;

                Dictionary<CompanyBankData, String> bankData = cargo.CustomerCompany.BankData;

                using (var doc = DocX.Create("СчетФактура.docx"))
                {
                    doc.InsertParagraph("Счет-фактура")
                        .FontSize(20)
                        .Bold()
                        .Alignment = Alignment.center;

                    doc.InsertParagraph($"Компания: {companyName}")
                        .FontSize(12)
                        .SpacingAfter(10);

                    doc.InsertParagraph($"Страна, Город компании: {city}")
                        .FontSize(12)
                        .SpacingAfter(10);

                    doc.InsertParagraph($"ИНН компании: {bankData[CompanyBankData.INN]}")
                        .FontSize(12)
                        .SpacingAfter(10);

                    doc.InsertParagraph($"LTD компании: {bankData[CompanyBankData.LTD]}")
                        .FontSize(12)
                        .SpacingAfter(10);

                    doc.InsertParagraph($"Адрес банка компании: {bankData[CompanyBankData.AddressBank]}")
                        .FontSize(12)
                        .SpacingAfter(10);

                    doc.InsertParagraph($"Название банка компании: {bankData[CompanyBankData.NameOfBank]}")
                        .FontSize(12)
                        .SpacingAfter(10);

                    doc.InsertParagraph($"Номер банковского счета компании: {bankData[CompanyBankData.NumberBank]}")
                        .FontSize(12)
                        .SpacingAfter(10);

                    doc.InsertParagraph($"Адрес почты: {email}")
                        .FontSize(12)
                        .SpacingAfter(10);

                    doc.InsertParagraph("Информация о заказе")
                        .FontSize(14)
                        .Bold()
                        .SpacingAfter(10);

                    var table = doc.AddTable(2, 6);
                    table.Design = TableDesign.LightShadingAccent2;
                    table.Rows[0].Cells[0].Paragraphs.First().Append("Наименование").Bold();
                    table.Rows[0].Cells[1].Paragraphs.First().Append("Сумма").Bold();
                    table.Rows[0].Cells[2].Paragraphs.First().Append("Адрес отправления").Bold();
                    table.Rows[0].Cells[3].Paragraphs.First().Append("Адрес прибытия").Bold();
                    table.Rows[0].Cells[4].Paragraphs.First().Append("Транспорт").Bold();
                    table.Rows[0].Cells[5].Paragraphs.First().Append("Водитель").Bold();

                    table.Rows[1].Cells[0].Paragraphs.First().Append($"Перевозка груза: {cargo.Name}");
                    table.Rows[1].Cells[1].Paragraphs.First().Append(cargo.Price.ConvertToFormatMoney() + " rub");
                    table.Rows[1].Cells[2].Paragraphs.First().Append(cargo.AddressFromCargo);
                    table.Rows[1].Cells[3].Paragraphs.First().Append(cargo.AddressToCargo);
                    table.Rows[1].Cells[4].Paragraphs.First().Append(cargo.TransportCargo.TransportModelName.ToString() + " " + cargo.TransportCargo.ModelDescriptionName + $" [{cargo.TransportCargo.GovNumber}]");
                    table.Rows[1].Cells[5].Paragraphs.First().Append(cargo.Driver.FullName);

                    doc.InsertTable(table);

                    doc.InsertParagraph($"Итого: {cargo.Price.ConvertToFormatMoney()} рублей")
                        .FontSize(12)
                        .SpacingBefore(20);

                    doc.InsertParagraph("Подпись: _____________________")
                        .FontSize(12)
                        .SpacingBefore(50);

                    doc.Save();
                    MessageBox.Show("Счет-фактура создана успешно.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"выставитьСчетФактуруToolStripMenuItem_Click: {ex}");
            }
        }
        private void создатьАктВыполненныхРаботToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Пожалуйста, выберите строку.");
                    return;
                }

                var selectedRow = dataGridView1.SelectedRows[0];

                Int64 cargoId = Convert.ToInt64(selectedRow.Cells["ID"].Value);
                Cargo cargo = this.CargoList.Find(_ => _.ID == cargoId);
                if (cargo == null)
                {
                    MessageBox.Show("Такой груз не найден!");
                    return;
                }

                var companyName = cargo.CustomerCompany.Name;
                var email = cargo.CustomerCompany.Email;
                var city = cargo.CustomerCompany.Country.ToString() + " " + cargo.CustomerCompany.City;

                using (var doc = DocX.Create("АктВыполненныхРабот.docx"))
                {
                    doc.InsertParagraph("Акт выполненных работ")
                        .FontSize(20)
                        .Bold()
                        .Alignment = Alignment.center;

                    doc.InsertParagraph($"Компания: {companyName}")
                        .FontSize(12)
                        .SpacingAfter(10);

                    doc.InsertParagraph($"Страна, Город компании: {city}")
                        .FontSize(12)
                        .SpacingAfter(10);

                    doc.InsertParagraph($"Адрес почты: {email}")
                        .FontSize(12)
                        .SpacingAfter(10);

                    doc.InsertParagraph("Информация о заказе")
                        .FontSize(14)
                        .Bold()
                        .SpacingAfter(10);

                    var table = doc.AddTable(2, 5);
                    table.Design = TableDesign.LightShadingAccent2;
                    table.Rows[0].Cells[0].Paragraphs.First().Append("Наименование").Bold();
                    table.Rows[0].Cells[1].Paragraphs.First().Append("Описание").Bold();
                    table.Rows[0].Cells[2].Paragraphs.First().Append("Адрес отправления").Bold();
                    table.Rows[0].Cells[3].Paragraphs.First().Append("Адрес прибытия").Bold();
                    table.Rows[0].Cells[4].Paragraphs.First().Append("Сумма").Bold();

                    table.Rows[1].Cells[0].Paragraphs.First().Append($"Перевозка груза: {cargo.Name}");
                    table.Rows[1].Cells[1].Paragraphs.First().Append(cargo.Description);
                    table.Rows[1].Cells[2].Paragraphs.First().Append(cargo.AddressFromCargo);
                    table.Rows[1].Cells[3].Paragraphs.First().Append(cargo.AddressToCargo);
                    table.Rows[1].Cells[4].Paragraphs.First().Append(cargo.Price.ConvertToFormatMoney() + " rub");

                    doc.InsertTable(table);

                    doc.InsertParagraph($"Итого: {cargo.Price.ConvertToFormatMoney()} рублей")
                        .FontSize(12)
                        .SpacingBefore(20);

                    doc.InsertParagraph("Все работы выполнены в полном объеме и в срок. Претензий по выполнению работ нет.")
                        .FontSize(12)
                        .SpacingBefore(10);

                    doc.InsertParagraph("Подпись: _____________________")
                        .FontSize(12)
                        .SpacingBefore(50);

                    doc.Save();
                    MessageBox.Show("Акт выполненных работ создан успешно.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"создатьАктВыполненныхРаботToolStripMenuItem_Click: {ex}");
            }
        }

        private void поЭкспедиторамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FilterCargoMenu != null)
                {
                    MessageBox.Show("У вас уже есть открытое это меню !");
                    return;
                }
                this.FilterCargoMenu = new FilterCargoMenu(this);
                this.FilterCargoMenu.FormClosed += FilterCargoMenu_FormClosed;
                this.FilterCargoMenu.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"поЭкспедиторамToolStripMenuItem_Click: {ex}");
            }
        }

        private void FilterCargoMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.FilterCargoMenu = null;
        }
    }
}