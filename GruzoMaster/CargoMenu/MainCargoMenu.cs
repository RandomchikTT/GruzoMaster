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
using System.IO;
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
        public static List<Cargo> CargoList { get; set; } = new List<Cargo>();
        private AddCargoMenu AddCargoMenu { get; set; } = null;
        private EditingCargoMenu EditingCargoMenu { get; set; } = null;
        private FilterCargoMenu FilterCargoMenu { get; set; } = null;
        private User FilteredByForwarder { get; set; } = null;
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
                Cargo cargo = CargoList.Find(_ => _.ID == cargoId);
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
        
        public void FilteredByForwarders(User forrwarder)
        {
            try
            {
                this.FilteredByForwarder = forrwarder;
                this.LoadCargoMenu(1);
            }
            catch (Exception e) { MessageBox.Show("FilteredByForwarders: " + e.ToString()); }
        }
        public static async Task<List<Cargo>> GetCargoList()
        {
            try
            {
                List<Cargo> cargos = new List<Cargo>();
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `cargo`");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Int64 idCargo = Convert.ToInt64(row["ID"]);
                        Int32 idCreatorUser = Convert.ToInt32(row["ID_user_creator"]);
                        Int32 idCompany = Convert.ToInt32(row["ID_company"]);
                        String name = Convert.ToString(row["Name"]);
                        String description = Convert.ToString(row["Description"]);
                        String addressFromCargo = Convert.ToString(row["addressFromCargo"]);
                        DateTime deadLineTime = Convert.ToDateTime(row["DeadlineTime"]);
                        String addressToCargo = Convert.ToString(row["AddressToCargo"]);
                        Int32 price = Convert.ToInt32(row["Price"]);
                        Int32 idForwarder = Convert.ToInt32(row["ForwarderID"]);
                        CargoDeliveryType cargoDeliveryType = (CargoDeliveryType)Convert.ToInt32(row["DeliveryType"]);
                        List<CargoLog> cargoLogs = JsonConvert.DeserializeObject<List<CargoLog>>(row["CargoLogs"].ToString());
                        Company company = null;
                        if (idCompany != -1)
                        {
                            company = await Company.GetCompanyById(idCompany);
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
                        var cargoList = await CargoPart.GetCargoPartsByOrderId(idCargo);
                        cargos.Add(new Cargo()
                        {
                            ID = idCargo,
                            DeadlineTime = deadLineTime,
                            CreateUserCargo = user,
                            CustomerCompany = company,
                            Price = price,
                            AddressFromCargo = addressFromCargo,
                            AddressToCargo = addressToCargo,
                            DeliveryType = cargoDeliveryType,
                            CargoLogs = cargoLogs,
                            Name = name,
                            Forwarder = forwarderUser,
                            Description = description,
                            CargoParts = cargoList,
                        });
                    }
                }
                return cargos;
            }
            catch (Exception e) { MessageBox.Show("GetCargoList: " + e.ToString()); return new List<Cargo>(); }
        }
        public async void LoadCargoMenu(Int32 currentPage, Int32 pageSize = 30)
        {
            try
            {
                Int32 offset = (currentPage - 1) * pageSize;
                DataTable dataTable;
                if (this.FilteredByForwarder != null)
                {
                    dataTable = await MySQL.QueryRead($"SELECT * FROM `cargo` WHERE `ForwarderID`={this.FilteredByForwarder.ID} LIMIT {pageSize} OFFSET {offset}");
                }
                else
                {
                    dataTable = await MySQL.QueryRead($"SELECT * FROM `cargo` LIMIT {pageSize} OFFSET {offset}");
                }
                CargoList.Clear();
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Int64 idCargo = Convert.ToInt64(row["ID"]);
                        Int32 idCreatorUser = Convert.ToInt32(row["ID_user_creator"]);
                        Int32 idCompany = Convert.ToInt32(row["ID_company"]);
                        String name = Convert.ToString(row["Name"]);
                        String description = Convert.ToString(row["Description"]);
                        String addressFromCargo = Convert.ToString(row["addressFromCargo"]);
                        String addressToCargo = Convert.ToString(row["AddressToCargo"]);
                        DateTime deadlineTime = Convert.ToDateTime(row["DeadlineTime"]);
                        Int32 price = Convert.ToInt32(row["Price"]);
                        Int32 idForwarder = Convert.ToInt32(row["ForwarderID"]);
                        CargoDeliveryType cargoDeliveryType = (CargoDeliveryType)Convert.ToInt32(row["DeliveryType"]);
                        List<CargoLog> cargoLogs = JsonConvert.DeserializeObject<List<CargoLog>>(row["CargoLogs"].ToString());
                        Company company = null;
                        if (idCompany != -1)
                        {
                            company = await Company.GetCompanyById(idCompany);
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
                        var cargoList = await CargoPart.GetCargoPartsByOrderId(idCargo);
                        CargoList.Add(new Cargo()
                        {
                            ID = idCargo,
                            DeadlineTime = deadlineTime,
                            CreateUserCargo = user,
                            CustomerCompany = company,
                            Price = price,
                            AddressFromCargo = addressFromCargo,
                            AddressToCargo = addressToCargo,
                            DeliveryType = cargoDeliveryType,
                            CargoLogs = cargoLogs,
                            Name = name,
                            Forwarder = forwarderUser,
                            Description = description,
                            CargoParts = cargoList,
                        });
                    }
                    PopulateDataGridView();
                    this.textBox1.Text = currentPage.ToString();
                }
            }
            catch (Exception e) { MessageBox.Show("LoadCargoMenu: " + e.ToString()); }
        }
        private async void PopulateDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            // Add columns
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("CreateUserCargoName", "Имя создателя");
            dataGridView1.Columns.Add("CustomerCompanyName", "Название компании");
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

        private async void выставитьСчетФактуруToolStripMenuItem_Click(object sender, EventArgs e)
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
                Cargo cargo = CargoList.Find(_ => _.ID == cargoId);
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

                    table.Rows[1].Cells[0].Paragraphs.First().Append($"Перевозка груза: {cargo.Name}");
                    table.Rows[1].Cells[1].Paragraphs.First().Append(cargo.Price.ConvertToFormatMoney() + " rub");
                    table.Rows[1].Cells[2].Paragraphs.First().Append(cargo.AddressFromCargo);
                    table.Rows[1].Cells[3].Paragraphs.First().Append(cargo.AddressToCargo);


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
                Cargo cargo = CargoList.Find(_ => _.ID == cargoId);
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

        private void очиститьФильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FilteredByForwarder != null)
                {
                    this.FilteredByForwarder = null;
                    this.LoadCargoMenu(1);
                    MessageBox.Show("Фильтр по экспедиторам успешно очищен !");
                    return;
                }
            }
            catch (Exception ex) { MessageBox.Show("очиститьФильтрToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private async void заявкуНаПеревозкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Пожалуйста, выберите груз.");
                    return;
                }
                var selectedRow = dataGridView1.SelectedRows[0];
                Int64 cargoId = Convert.ToInt64(selectedRow.Cells["ID"].Value);
                Cargo cargo = CargoList.Find(_ => _.ID == cargoId);
                if (cargo == null)
                {
                    MessageBox.Show("Такой груз не найден !");
                    return;
                }
                string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Образцы", "ЗаявкаНаГруз.docx");
                if (!File.Exists(templatePath))
                {
                    MessageBox.Show("Файл шаблона не найден: " + templatePath);
                    return;
                }
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Title = "Сохранить заявку на груз";
                    saveFileDialog.Filter = "Word документы (*.docx)|*.docx";
                    saveFileDialog.FileName = $"Заявка_{cargo.ID}.docx";

                    if (saveFileDialog.ShowDialog() != DialogResult.OK)
                        return;

                    string savePath = saveFileDialog.FileName;

                    var doc = DocX.Load(templatePath);
                    List<Transport> transports = await Transport.GetTransports();
                    transports = transports.FindAll(_ => cargo.CargoParts.Any(x => x.Transport == _.IdKey));

                    doc.ReplaceText("{{Customer}}", cargo.CustomerCompany?.Name ?? "");
                    doc.ReplaceText("{{Carrier}}", "ООО Малекс");
                    doc.ReplaceText("{{cargo_id}}", cargo.ID.ToString());
                    doc.ReplaceText("{{Route}}", $"{cargo.AddressFromCargo} - {cargo.AddressToCargo}");
                    doc.ReplaceText("{{CarParams}}", "Тентованный, до 20 тонн, объем 90 м³");
                    doc.ReplaceText("{{LoadDate}}", DateTime.Now.ToString("G"));
                    doc.ReplaceText("{{LoadAddress}}", cargo.AddressFromCargo);
                    doc.ReplaceText("{{UnloadAddress}}", cargo.AddressToCargo);
                    doc.ReplaceText("{{CargoInfo}}", $"{cargo.Name}, {cargo.Description}");
                    doc.ReplaceText("{{DeliveryDeadline}}", cargo.DeadlineTime.ToString("G"));
                    doc.ReplaceText("{{PaymentTerms}}", "Оплата в течение 5 банковских дней после предоставления оригиналов закрывающих документов.");
                    doc.ReplaceText("{{FreightCost}}", $"{cargo.Price} руб.");
                    doc.ReplaceText("{{CarNumbers}}", string.Join(", ", transports.Select(_ => _.GovNumber)));

                    doc.SaveAs(savePath);
                    MessageBox.Show($"Заявка успешно сформирована");
                }
            }
            catch (Exception ex) { MessageBox.Show("заявкуНаПеревозкуToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private async void путевойЛистToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Пожалуйста, выберите груз.");
                    return;
                }
                var selectedRow = dataGridView1.SelectedRows[0];
                Int64 cargoId = Convert.ToInt64(selectedRow.Cells["ID"].Value);
                Cargo cargo = CargoList.Find(_ => _.ID == cargoId);
                if (cargo == null)
                {
                    MessageBox.Show("Такой груз не найден !");
                    return;
                }

            }
            catch (Exception ex) { MessageBox.Show("путевойЛистToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private async void договорНаОказаниеУслугToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Пожалуйста, выберите груз.");
                    return;
                }
                var selectedRow = dataGridView1.SelectedRows[0];
                Int64 cargoId = Convert.ToInt64(selectedRow.Cells["ID"].Value);
                Cargo cargo = CargoList.Find(_ => _.ID == cargoId);
                if (cargo == null)
                {
                    MessageBox.Show("Такой груз не найден !");
                    return;
                }
                string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Образцы", "ДоговорНаОказаниеТранспортныхУслуг.docx");
                if (!File.Exists(templatePath))
                {
                    MessageBox.Show("Файл шаблона не найден: " + templatePath);
                    return;
                }
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Title = "Сохранить договор на оказание услуг";
                    saveFileDialog.Filter = "Word документы (*.docx)|*.docx";
                    saveFileDialog.FileName = $"Договор На оказание услуг.docx";

                    if (saveFileDialog.ShowDialog() != DialogResult.OK)
                        return;

                    string savePath = saveFileDialog.FileName;

                    var doc = DocX.Load(templatePath);
                    List<Transport> transports = await Transport.GetTransports();
                    transports = transports.FindAll(_ => cargo.CargoParts.Any(x => x.Transport == _.IdKey));
                    doc.ReplaceText("{{Customer}}", cargo.CustomerCompany?.Name ?? "");
                    doc.ReplaceText("{{cargo_id}}", cargo.ID.ToString());
                    doc.ReplaceText("{{LoadDate}}", DateTime.Now.ToString("G"));
                    doc.ReplaceText("{{LoadAddress}}", cargo.AddressFromCargo);
                    doc.ReplaceText("{{UnloadAddress}}", cargo.AddressToCargo);
                    doc.SaveAs(savePath);
                    MessageBox.Show($"Договор успешно сформирован");
                }
            }
            catch (Exception ex) { MessageBox.Show("договорНаОказаниеУслугToolStripMenuItem_Click: " + ex.ToString()); }
        }
    }
}