using GruzoMaster.CargoMenu;
using GruzoMaster.Objects;
using GruzoMaster.Objects.Cargo;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster.Companies
{
    public partial class MainMenuCompany : Form
    {
        private List<Company> CompanyActiveList = new List<Company>();
        private MenuAddCompany MenuAddCompany = null;
        private MenuEditDataCompany MenuEditDataCompany = null;
        public MainMenuCompany()
        {
            InitializeComponent();
            if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanMakeExportDataCompany))
            {
                this.экспортДанныхОКомпанииToolStripMenuItem.Enabled = false;
                this.экспортДанныхОКомпанииToolStripMenuItem.Visible = false;
            }
            if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanDeleteCompany))
            {
                this.удалитьКомпаниюToolStripMenuItem.Enabled = false;
                this.удалитьКомпаниюToolStripMenuItem.Visible = false;
            }
            if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanAppendCompany))
            {
                this.добавитьКомпаниюToolStripMenuItem.Enabled = false;
                this.добавитьКомпаниюToolStripMenuItem.Visible = false;
            }
            this.label1.Text = "";
            this.LoadMainMenuCompanyDataBase();
        }
        public async void LoadMainMenuCompanyDataBase()
        {
            try
            {
                DataTable dataTable = await MySQL.QueryRead($"SELECT `Name`,`id` FROM `companies`");
                this.CompanyActiveList = new List<Company>();
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    this.Компании.Items.Clear();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        this.CompanyActiveList.Add(new Company()
                        {
                            IdKey = Convert.ToInt32(row["id"]),
                        });
                        this.Компании.Items.Add(Convert.ToString(row["Name"]));
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("LoadMainMenuCompanyDataBase: " + ex.ToString()); }
        }

        private async void Компании_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.Компании.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите компанию !");
                    return;
                }
                String text = await this.GetCompanyText();
                if (text == null) return;
                this.label1.Text = text;
            }
            catch (Exception ex) { MessageBox.Show("Компании_SelectedIndexChanged: " + ex.ToString()); }
        }
        private async Task<String> GetCompanyText()
        {
            try
            {
                Int32 idKey = this.CompanyActiveList[this.Компании.SelectedIndex].IdKey;
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `companies` WHERE `id`={idKey}");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow selectedCompany = dataTable.Rows[0];
                    Dictionary<PhoneNumber, String> numberCalls = JsonConvert.DeserializeObject<Dictionary<PhoneNumber, String>>(selectedCompany["Contacts"].ToString());
                    String numberPhonesText = "";
                    foreach (KeyValuePair<PhoneNumber, String> number in numberCalls)
                    {
                        numberPhonesText += number.Value;
                        if (numberCalls.Last().Value != number.Value)
                        {
                            numberPhonesText += ", ";
                        }
                    }
                    Dictionary<CompanyBankData, String> bankData = JsonConvert.DeserializeObject<Dictionary<CompanyBankData, String>>(selectedCompany["BankData"].ToString());
                    return $"Инофрмация о компании:" +
                        $"\nНазвание: {Convert.ToString(selectedCompany["Name"])}." +
                        $"\nСтрана: {Company.GetCountryRussianName((Company.CompanyCountry)Convert.ToInt32(selectedCompany["Country"]))}." +
                        $"\nГород: {Convert.ToString(selectedCompany["City"])}." +
                        $"\nВремя добавления в базу: {Convert.ToString(selectedCompany["TimeAdded"])}." +
                        $"\nКонтактные телефоны: {numberPhonesText}." +
                        $"\nПочта: {Convert.ToString(selectedCompany["Email"])}." +
                        $"\nИНН: {bankData[CompanyBankData.INN]}" +
                        $"\nLTD: {bankData[CompanyBankData.LTD]}" +
                        $"\nАдрес банка: {bankData[CompanyBankData.AddressBank]}" +
                        $"\nНазвание банка: {bankData[CompanyBankData.NameOfBank]}" +
                        $"\nНомер банковского счета: {bankData[CompanyBankData.NumberBank]}";
                }
                else
                {
                    MessageBox.Show("Данная компания не была найдена в базе !");
                    this.LoadMainMenuCompanyDataBase();
                }
                return null;
            }
            catch (Exception e) { MessageBox.Show("GetCompanyText: " + e.ToString()); return null; }
        }

        private async void экспортДанныхОКомпанииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanMakeExportDataCompany))
                {
                    MessageBox.Show("У вас нету доступа к этому меню !");
                    return;
                }
                if (this.Компании.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите компанию !");
                    return;
                }
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.Title = "Выберите место для сохранения файла";
                DialogResult result = saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    String transportInfo = await this.GetCompanyText();
                    if (transportInfo == null) return;
                    File.WriteAllText(saveFileDialog.FileName, transportInfo);
                    MessageBox.Show("Вы успешно выгрузили данные о компании в файл.");
                }
            }
            catch (Exception ex) { MessageBox.Show("экспортДанныхОКомпанииToolStripMenuItem_Click: " + ex.ToString()); }
        }
        private async void редактироватьКомпаниюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.MenuEditDataCompany != null)
                {
                    MessageBox.Show("У вас уже есть открытое меню !");
                    return;
                }
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanEditCompany))
                {
                    MessageBox.Show("У вас нету доступа к этому меню !");
                    return;
                }
                if (this.Компании.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите компанию !");
                    return;
                }
                Int32 idKey = this.CompanyActiveList[this.Компании.SelectedIndex].IdKey;
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `companies` WHERE `id`={idKey}");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    this.MenuEditDataCompany = new MenuEditDataCompany(this, new Company()
                    {
                        IdKey = idKey,
                        Name = Convert.ToString(dataTable.Rows[0]["Name"]),
                        City = Convert.ToString(dataTable.Rows[0]["City"]),
                        Email = Convert.ToString(dataTable.Rows[0]["Email"]),
                        Country = (Company.CompanyCountry)Convert.ToInt32(dataTable.Rows[0]["Country"]),
                        PhoneNumbers = JsonConvert.DeserializeObject<Dictionary<PhoneNumber, String>>(dataTable.Rows[0]["Contacts"].ToString()),
                        BankData = JsonConvert.DeserializeObject<Dictionary<CompanyBankData, String>>(dataTable.Rows[0]["BankData"].ToString()),
                    });
                    this.MenuEditDataCompany.FormClosed += MenuEditDataCompany_FormClosed;
                    this.MenuEditDataCompany.Show();
                }

            }
            catch (Exception ex) { MessageBox.Show("редактироватьКомпаниюToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private void MenuEditDataCompany_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.MenuEditDataCompany = null;
        }

        private async void удалитьКомпаниюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanDeleteCompany))
                {
                    MessageBox.Show("У вас нету доступа к этому меню !");
                    return;
                }
                if (this.Компании.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите компанию !");
                    return;
                }
                DialogResult result = MessageBox.Show("Вы уверены что хотите удалить компанию ?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Int32 idKey = this.CompanyActiveList[this.Компании.SelectedIndex].IdKey;
                    DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `companies` WHERE `id`={idKey}");
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        await MySQL.QueryAsync($"DELETE FROM `companies` WHERE `id`={idKey}");
                        MySQL.AddUserLog(User.LoggedUser.Login, $"Удалил компанию с базы данных {Convert.ToString(dataTable.Rows[0]["Name"])} #{idKey}.");
                        this.LoadMainMenuCompanyDataBase();
                        MessageBox.Show("Вы успешно удалили компанию с базы данных.");
                    }
                    else
                    {
                        MessageBox.Show("Данный компанию не был найден в базе данных !");
                        this.LoadMainMenuCompanyDataBase();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("удалитьКомпаниюToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private void добавитьКомпаниюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.MenuAddCompany != null)
                {
                    MessageBox.Show("У вас уже есть открытое меню !");
                    return;
                }
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanAppendCompany))
                {
                    MessageBox.Show("У вас нету доступа к этому меню !");
                    return;
                }
                this.MenuAddCompany = new MenuAddCompany(this);
                this.MenuAddCompany.FormClosed += MenuAddCompany_FormClosed;
                this.MenuAddCompany.Show();
            }
            catch (Exception ex) { MessageBox.Show("добавитьКомпаниюToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private void MenuAddCompany_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.MenuAddCompany = null;
        }
        public async void FilterCargosToExcelWithCharts(DateTime time)
        {
            try
            {
                List<Cargo> cargos = await MainCargoMenu.GetCargoList();
                var filteredCargos = cargos
                    .Where(c => c.CargoParts.Any(x => x.DeliveryDate.Date == time.Date))
                    .ToList();

                if (filteredCargos.Count == 0)
                {
                    MessageBox.Show("Нет заказов на выбранную дату.");
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                    FileName = $"Отчет_по_заказам_{time:dd_MM_yyyy}.xlsx"
                };

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    // --- Общий отчет ---
                    var ws = package.Workbook.Worksheets.Add("Общий отчет");

                    ws.Cells["A1"].Value = $"Отчет по заказам на {time:dd.MM.yyyy}";
                    ws.Cells["A1"].Style.Font.Size = 16;
                    ws.Cells["A1"].Style.Font.Bold = true;

                    var headers = new string[]
                    {
                "Груз", "Описание", "Компания", "Создатель", "Экспедитор",
                "Адрес отправления", "Адрес прибытия", "Сумма, руб", "Статус", "Дедлайн"
                    };

                    for (int i = 0; i < headers.Length; i++)
                    {
                        ws.Cells[3, i + 1].Value = headers[i];
                        ws.Cells[3, i + 1].Style.Font.Bold = true;
                        ws.Cells[3, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        ws.Cells[3, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    }

                    int row = 4;
                    foreach (var cargo in filteredCargos)
                    {
                        ws.Cells[row, 1].Value = cargo.Name;
                        ws.Cells[row, 2].Value = cargo.Description;
                        ws.Cells[row, 3].Value = cargo.CustomerCompany?.Name ?? "Не указано";
                        ws.Cells[row, 4].Value = cargo.CreateUserCargo?.Name ?? "Не указан";
                        ws.Cells[row, 5].Value = cargo.Forwarder?.Name ?? "Не назначен";
                        ws.Cells[row, 6].Value = cargo.AddressFromCargo;
                        ws.Cells[row, 7].Value = cargo.AddressToCargo;
                        ws.Cells[row, 8].Value = cargo.Price;
                        ws.Cells[row, 9].Value = Cargo.GetDeliveryTypeDescription(cargo.DeliveryType);
                        ws.Cells[row, 10].Value = cargo.DeadlineTime.ToString("dd.MM.yyyy");

                        var fillColor = cargo.GetColorByCargoStatus();

                        ws.Cells[row, 1, row, 10].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        ws.Cells[row, 1, row, 10].Style.Fill.BackgroundColor.SetColor(fillColor);

                        row++;
                    }

                    ws.Column(8).Style.Numberformat.Format = "#,##0 руб.";
                    ws.Cells[ws.Dimension.Address].AutoFitColumns();

                    var chart = ws.Drawings.AddChart("chart1", eChartType.ColumnClustered) as ExcelBarChart;
                    chart.Title.Text = "Сумма по грузам";
                    chart.SetPosition(1, 0, 11, 0);
                    chart.SetSize(600, 300);

                    var sumRange = ws.Cells[4, 8, row - 1, 8];
                    var nameRange = ws.Cells[4, 1, row - 1, 1];

                    var series = chart.Series.Add(sumRange, nameRange);
                    series.Header = "Сумма";

                    // --- Отчеты по компаниям ---
                    // Группируем заказы по компаниям
                    var groupedByCompany = filteredCargos
                        .GroupBy(c => c.CustomerCompany?.Name ?? "Без компании");

                    foreach (var group in groupedByCompany)
                    {
                        string companyName = group.Key;

                        // Ограничим длину имени листа (Excel не поддерживает >31 символ)
                        string sheetName = companyName.Length > 31 ? companyName.Substring(0, 31) : companyName;

                        var wsCompany = package.Workbook.Worksheets.Add(sheetName);

                        wsCompany.Cells["A1"].Value = $"Отчет по заказам компании \"{companyName}\" на {time:dd.MM.yyyy}";
                        wsCompany.Cells["A1"].Style.Font.Size = 16;
                        wsCompany.Cells["A1"].Style.Font.Bold = true;

                        // Заголовки
                        for (int i = 0; i < headers.Length; i++)
                        {
                            wsCompany.Cells[3, i + 1].Value = headers[i];
                            wsCompany.Cells[3, i + 1].Style.Font.Bold = true;
                            wsCompany.Cells[3, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            wsCompany.Cells[3, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                        }

                        int r = 4;
                        foreach (var cargo in group)
                        {
                            wsCompany.Cells[r, 1].Value = cargo.Name;
                            wsCompany.Cells[r, 2].Value = cargo.Description;
                            wsCompany.Cells[r, 3].Value = cargo.CustomerCompany?.Name ?? "Не указано";
                            wsCompany.Cells[r, 4].Value = cargo.CreateUserCargo?.Name ?? "Не указан";
                            wsCompany.Cells[r, 5].Value = cargo.Forwarder?.Name ?? "Не назначен";
                            wsCompany.Cells[r, 6].Value = cargo.AddressFromCargo;
                            wsCompany.Cells[r, 7].Value = cargo.AddressToCargo;
                            wsCompany.Cells[r, 8].Value = cargo.Price;
                            wsCompany.Cells[r, 9].Value = Cargo.GetDeliveryTypeDescription(cargo.DeliveryType);
                            wsCompany.Cells[r, 10].Value = cargo.DeadlineTime.ToString("dd.MM.yyyy");

                            var fillColor = cargo.GetColorByCargoStatus();

                            // Покраска всей строки в цвет fillColor
                            wsCompany.Cells[r, 1, r, 10].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            wsCompany.Cells[r, 1, r, 10].Style.Fill.BackgroundColor.SetColor(fillColor);

                            r++;
                        }

                        wsCompany.Column(8).Style.Numberformat.Format = "#,##0 руб.";
                        wsCompany.Cells[wsCompany.Dimension.Address].AutoFitColumns();

                        // График суммы по грузам компании
                        var chartCompany = wsCompany.Drawings.AddChart($"chart_{sheetName}", eChartType.ColumnClustered) as ExcelBarChart;
                        chartCompany.Title.Text = $"Сумма по грузам компании \"{companyName}\"";
                        chartCompany.SetPosition(1, 0, 11, 0);
                        chartCompany.SetSize(600, 300);

                        var sumRangeCompany = wsCompany.Cells[4, 8, r - 1, 8];
                        var nameRangeCompany = wsCompany.Cells[4, 1, r - 1, 1];

                        var seriesCompany = chartCompany.Series.Add(sumRangeCompany, nameRangeCompany);
                        seriesCompany.Header = "Сумма";
                    }

                    var file = new FileInfo(saveFileDialog.FileName);
                    package.SaveAs(file);

                    MessageBox.Show("Отчет успешно сохранён с графиками.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании отчета: " + ex.Message);
            }
        }


        private async void наДатуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompanyFilterDate companyFilterDate = new CompanyFilterDate(this);
            companyFilterDate.Show();
        }
    }
}
