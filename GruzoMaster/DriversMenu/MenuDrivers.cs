using GruzoMaster.CargoMenu;
using GruzoMaster.Objects;
using GruzoMaster.Objects.Cargo;
using Newtonsoft.Json;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GruzoMaster.DriversMenu;
using OfficeOpenXml.Style;
using System.Drawing;

namespace GruzoMaster
{
    public partial class MenuDrivers : Form
    {
        private List<Driver> DriverInfoList = new List<Driver>();
        private MenuAddDriver MenuAddDriver = null;
        private MenuChangeDataDriver MenuChangeDataDriver = null;
        public MenuDrivers()
        {
            try
            {
                InitializeComponent();
                this.labelInfoDriver.Text = "";
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanAppendDrivers))
                {
                    this.добавитьВодителяToolStripMenuItem.Visible = false;
                    this.добавитьВодителяToolStripMenuItem.Enabled = false;
                }
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanEditDrivers))
                {
                    this.изменитьДанныеВодителяToolStripMenuItem.Visible = false;
                    this.изменитьДанныеВодителяToolStripMenuItem.Enabled = false;
                }
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanEditDrivers))
                {
                    this.изменитьДанныеВодителяToolStripMenuItem.Visible = false;
                    this.изменитьДанныеВодителяToolStripMenuItem.Enabled = false;
                }
                this.LoadMenu();
            }
            catch (Exception ex) { MessageBox.Show("MenuDrivers: " + ex.ToString()); }
        }
        public async void LoadMenu()
        {
            try
            {
                DataTable result = await MySQL.QueryRead("SELECT `FullName`,`id` FROM `drivers`");
                this.DriverInfoList = new List<Driver>();
                if (result != null && result.Rows.Count > 0)
                {
                    this.Водители.Items.Clear();
                    foreach (DataRow row in result.Rows)
                    {
                        this.DriverInfoList.Add(new Driver()
                        {
                            IdKey = Convert.ToInt32(row["id"]),                    
                        });
                        String[] fullName = Convert.ToString(row["FullName"]).Split(' ');
                        this.Водители.Items.Add($"{fullName[0]} {fullName[1][0]}. {fullName[2][0]}.");
                    }
                }
            }
            catch (Exception e) { MessageBox.Show("LoadMenu: " + e.ToString()); }
        }
        private async void Водители_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String driverInfo = await this.GetDriverInfo();
                if (driverInfo == null) return;
                this.labelInfoDriver.Text = driverInfo;
            }
            catch (Exception ex) { MessageBox.Show("Водители_SelectedIndexChanged: " + ex.ToString()); }
        }
        private async Task<String> GetDriverInfo()
        {
            try
            {
                if (this.Водители.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите водителя !");
                    return null;
                }
                Int32 idKey = this.DriverInfoList[this.Водители.SelectedIndex].IdKey;
                DataTable selectDriver = await MySQL.QueryRead($"SELECT * FROM `drivers` WHERE `id`={idKey}");
                if (selectDriver != null && selectDriver.Rows.Count > 0)
                {
                    DataRow dataRowCollection = selectDriver.Rows[0];
                    List<License> listLicense = JsonConvert.DeserializeObject<List<License>>(dataRowCollection["ListLicenses"].ToString());
                    String licText = "";
                    foreach (License license in listLicense)
                    {
                        licText += license.ToString();
                        if (listLicense.Last() != license)
                        {
                            licText += ", ";
                        }
                    }
                    Dictionary<PhoneNumber, String> numberCalls = JsonConvert.DeserializeObject<Dictionary<PhoneNumber, String>>(dataRowCollection["PhoneNumbers"].ToString());
                    String numberPhonesText = "";
                    foreach (KeyValuePair<PhoneNumber, String> number in numberCalls)
                    {
                        numberPhonesText += number.Value;
                        if (numberCalls.Last().Value != number.Value)
                        {
                            numberPhonesText += ", ";
                        }
                    }
                    return $"Информация о водителе: " +
                            $"\nФИО: {Convert.ToString(dataRowCollection["FullName"])}" +
                            $"\nМед. Справка до: {Convert.ToDateTime(dataRowCollection["MedSpravka"]).ToString("d")}" +
                            $"\nДата рождения: {Convert.ToDateTime(dataRowCollection["DateBirthday"]).ToString("d")}" +
                            $"\nОткрытые Категории: {(licText == "" ? "Не указаны" : licText)}." +
                            $"\nНомера телефонов: {(numberPhonesText == "" ? "Не указаны" : numberPhonesText)}." +
                            $"\nАдрес проживания: {Convert.ToString(dataRowCollection["Address"])}.";
                }
                else
                {
                    MessageBox.Show("Такой водитель не был найден в базе данных, обновите меню !");
                    this.LoadMenu();
                }
                return null;
            }
            catch (Exception ex) { MessageBox.Show("GetDriverInfo: " + ex.ToString()); return null; }
        }
        private async void экспортДанныхВодителяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Водители.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите водителя !");
                    return;
                }
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.Title = "Выберите место для сохранения файла";
                DialogResult result = saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    String driverInfo = await this.GetDriverInfo();
                    if (driverInfo == null) return;
                    File.WriteAllText(saveFileDialog.FileName, driverInfo);
                    MessageBox.Show("Вы успешно выгрузили данные по водителю в файл.");
                }
            }
            catch (Exception ex) { MessageBox.Show("экспортДанныхВодителяToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private void добавитьВодителяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanAppendDrivers))
                {
                    MessageBox.Show("У вас нету доступа к этому пункту !");
                    return;
                }
                if (User.LoggedUser.UserType != UserType.Admin)
                {
                    MessageBox.Show("У вас нету доступа добавлять водителей !");
                    return;
                }
                if (this.MenuAddDriver != null)
                {
                    MessageBox.Show("У вас уже есть открытое меню добавление водителя !");
                    return;
                }
                this.MenuAddDriver = new MenuAddDriver(this);
                this.MenuAddDriver.FormClosed += MenuAddDriver_FormClosed;
                this.MenuAddDriver.Show();
            }
            catch (Exception ex) { MessageBox.Show("добавитьВодителяToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private void MenuAddDriver_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.MenuAddDriver = null;
        }

        private async void удалитьВодителяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanDeleteDrivers))
                {
                    MessageBox.Show("У вас нету доступа к этому пункту !");
                    return;
                }
                if (this.Водители.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите водителя !");
                    return;
                }
                DialogResult result = MessageBox.Show("Вы уверены что хотите удалить водителя ?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Int32 idKey = this.DriverInfoList[this.Водители.SelectedIndex].IdKey;
                    DataTable selectDrivers = await MySQL.QueryRead($"SELECT * FROM `drivers` WHERE `id`={idKey}");
                    if (selectDrivers != null && selectDrivers.Rows.Count > 0)
                    {
                        await MySQL.QueryAsync($"DELETE FROM `drivers` WHERE `id`={idKey}");
                        this.LoadMenu();
                        String fullName = Convert.ToString(selectDrivers.Rows[0]["FullName"]);
                        Int32 idkey = Convert.ToInt32(selectDrivers.Rows[0]["id"]);
                        MySQL.AddUserLog(User.LoggedUser.Login, $"Удалил водителя {fullName} #{idkey}.");
                        MessageBox.Show("Вы успешно удалили водителя с базы данных !");
                    }
                    else
                    {
                        MessageBox.Show("Такой водитель не был найден в базе данных, обновите меню !");
                        this.LoadMenu();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Удалить_Водителя_ToolStripMenuItem_Click: " + ex.ToString()); }
        }
        private async void изменитьДанныеВодителяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Водители.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите водителя !");
                    return;
                }
                if (this.MenuChangeDataDriver != null)
                {
                    MessageBox.Show("У вас уже есть открытое меню изменение данных водителя !");
                    return;
                }
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanEditDrivers))
                {
                    MessageBox.Show("У вас нету доступа к этому пункту !");
                    return;
                }
                Int32 idKey = this.DriverInfoList[this.Водители.SelectedIndex].IdKey;
                DataTable selectDriver = await MySQL.QueryRead($"SELECT * FROM `drivers` WHERE `id`={idKey}");
                if (selectDriver != null && selectDriver.Rows.Count > 0)
                {
                    DataRow dataRowCollection = selectDriver.Rows[this.Водители.SelectedIndex];
                    List<License> listLicense = JsonConvert.DeserializeObject<List<License>>(dataRowCollection["ListLicenses"].ToString());
                    Dictionary<PhoneNumber, String> numberCalls = JsonConvert.DeserializeObject<Dictionary<PhoneNumber, String>>(dataRowCollection["PhoneNumbers"].ToString());
                    this.MenuChangeDataDriver = new MenuChangeDataDriver(this, new Driver()
                    {
                        FullName = Convert.ToString(dataRowCollection["FullName"]),
                        BirthDay = Convert.ToDateTime(dataRowCollection["DateBirthday"]),
                        MedSpavka = Convert.ToDateTime(dataRowCollection["MedSpravka"]),
                        ListLicense = listLicense,
                        PhoneNumbers = numberCalls,
                        SerialPassport = Convert.ToString(dataRowCollection["SerialPassport"]),
                        NumberPassport = Convert.ToString(dataRowCollection["NumberPassport"]),
                        Address = Convert.ToString(dataRowCollection["Address"]),
                        IdKey = Convert.ToInt32(dataRowCollection["id"]),
                    });
                    this.MenuChangeDataDriver.FormClosed += MenuChangeDataDriver_FormClosed;
                    this.MenuChangeDataDriver.Show();
                }
                else
                {
                    MessageBox.Show("Такой водитель не был найден в базе данных, обновите меню !");
                    this.LoadMenu();
                }
            }
            catch (Exception ex) { MessageBox.Show("изменитьДанныеВодителяToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private void MenuChangeDataDriver_FormClosed(object sender, EventArgs e)
        {
            this.MenuChangeDataDriver = null;
        }
        public async void FilterCargosByDriversToExcelWithCharts(DateTime time)
        {
            try
            {
                List<Cargo> cargos = await MainCargoMenu.GetCargoList();
                var allCargoParts = cargos
                    .SelectMany(c => c.CargoParts.Select(cp => new { Cargo = c, Part = cp }))
                    .Where(x => x.Part.DeliveryDate.Date == time.Date && x.Part.DriverID != -1)
                    .ToList();

                if (allCargoParts.Count == 0)
                {
                    MessageBox.Show("Нет грузов на выбранную дату.");
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                    FileName = $"Отчет_по_водителям_{time:dd_MM_yyyy}.xlsx"
                };

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    var ws = package.Workbook.Worksheets.Add("Общий отчет");

                    ws.Cells["A1"].Value = $"Отчет по водителям на {time:dd.MM.yyyy}";
                    ws.Cells["A1"].Style.Font.Size = 16;
                    ws.Cells["A1"].Style.Font.Bold = true;

                    string[] headers =
                    {
                "Груз", "Описание", "Компания", "Водитель",
                "Адрес отправления", "Адрес прибытия", "Сумма, руб", "Статус", "Дата доставки"
            };

                    for (int i = 0; i < headers.Length; i++)
                    {
                        ws.Cells[3, i + 1].Value = headers[i];
                        ws.Cells[3, i + 1].Style.Font.Bold = true;
                        ws.Cells[3, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[3, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }

                    int row = 4;
                    foreach (var item in allCargoParts)
                    {
                        var driver = await Driver.GetDriverById(item.Part.DriverID);
                        string driverName = driver?.FullName ?? "Неизвестно";

                        ws.Cells[row, 1].Value = item.Cargo.Name;
                        ws.Cells[row, 2].Value = item.Cargo.Description;
                        ws.Cells[row, 3].Value = item.Cargo.CustomerCompany?.Name ?? "Не указано";
                        ws.Cells[row, 4].Value = driverName;
                        ws.Cells[row, 5].Value = item.Cargo.AddressFromCargo;
                        ws.Cells[row, 6].Value = item.Cargo.AddressToCargo;
                        ws.Cells[row, 7].Value = item.Cargo.Price;
                        ws.Cells[row, 8].Value = Cargo.GetDeliveryTypeDescription(item.Cargo.DeliveryType);
                        ws.Cells[row, 9].Value = item.Part.DeliveryDate.ToString("dd.MM.yyyy");

                        var fillColor = item.Cargo.GetColorByCargoStatus();
                        ws.Cells[row, 1, row, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[row, 1, row, 9].Style.Fill.BackgroundColor.SetColor(fillColor);

                        row++;
                    }

                    ws.Column(7).Style.Numberformat.Format = "#,##0 руб.";
                    ws.Cells[ws.Dimension.Address].AutoFitColumns();

                    // Группировка по водителям
                    var groupedByDriver = allCargoParts
                        .GroupBy(x => x.Part.DriverID)
                        .ToDictionary(g => g.Key, g => g.ToList());

                    // Данные для круговой диаграммы
                    int chartDataStartRow = row + 2;
                    int chartCol = 13;
                    int currentRow = chartDataStartRow;

                    foreach (var kvp in groupedByDriver)
                    {
                        var driver = await Driver.GetDriverById(kvp.Key);
                        string driverName = driver?.FullName ?? "Неизвестно";

                        decimal total = kvp.Value.Sum(x => x.Cargo.Price);
                        ws.Cells[currentRow, chartCol].Value = driverName;
                        ws.Cells[currentRow, chartCol + 1].Value = total;
                        currentRow++;
                    }

                    var pieChart = ws.Drawings.AddChart("driverPie", eChartType.Pie) as ExcelPieChart;
                    pieChart.Title.Text = "Распределение сумм по водителям";
                    pieChart.SetPosition(1, 0, chartCol + 3, 0);
                    pieChart.SetSize(500, 400);

                    var nameRange = ws.Cells[chartDataStartRow, chartCol, currentRow - 1, chartCol];
                    var valueRange = ws.Cells[chartDataStartRow, chartCol + 1, currentRow - 1, chartCol + 1];
                    var seriesPie = pieChart.Series.Add(valueRange, nameRange);
                    seriesPie.Header = "Сумма";

                    pieChart.DataLabel.ShowValue = true;
                    pieChart.DataLabel.ShowCategory = true;

                    // Отдельные листы по водителям
                    foreach (var kvp in groupedByDriver)
                    {
                        var driver = await Driver.GetDriverById(kvp.Key);
                        string driverName = driver?.FullName ?? "Неизвестно";
                        string sheetName = driverName.Length > 31 ? driverName.Substring(0, 31) : driverName;

                        var wsDriver = package.Workbook.Worksheets.Add(sheetName);
                        wsDriver.Cells["A1"].Value = $"Заказы водителя {driverName} на {time:dd.MM.yyyy}";
                        wsDriver.Cells["A1"].Style.Font.Size = 16;
                        wsDriver.Cells["A1"].Style.Font.Bold = true;

                        for (int i = 0; i < headers.Length; i++)
                        {
                            wsDriver.Cells[3, i + 1].Value = headers[i];
                            wsDriver.Cells[3, i + 1].Style.Font.Bold = true;
                            wsDriver.Cells[3, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            wsDriver.Cells[3, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }

                        int r = 4;
                        foreach (var item in kvp.Value)
                        {
                            wsDriver.Cells[r, 1].Value = item.Cargo.Name;
                            wsDriver.Cells[r, 2].Value = item.Cargo.Description;
                            wsDriver.Cells[r, 3].Value = item.Cargo.CustomerCompany?.Name ?? "Не указано";
                            wsDriver.Cells[r, 4].Value = driverName;
                            wsDriver.Cells[r, 5].Value = item.Cargo.AddressFromCargo;
                            wsDriver.Cells[r, 6].Value = item.Cargo.AddressToCargo;
                            wsDriver.Cells[r, 7].Value = item.Cargo.Price;
                            wsDriver.Cells[r, 8].Value = Cargo.GetDeliveryTypeDescription(item.Cargo.DeliveryType);
                            wsDriver.Cells[r, 9].Value = item.Part.DeliveryDate.ToString("dd.MM.yyyy");

                            var fillColor = item.Cargo.GetColorByCargoStatus();
                            wsDriver.Cells[r, 1, r, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            wsDriver.Cells[r, 1, r, 9].Style.Fill.BackgroundColor.SetColor(fillColor);

                            r++;
                        }

                        wsDriver.Column(7).Style.Numberformat.Format = "#,##0 руб.";
                        wsDriver.Cells[wsDriver.Dimension.Address].AutoFitColumns();
                    }

                    var file = new FileInfo(saveFileDialog.FileName);
                    package.SaveAs(file);
                    MessageBox.Show("Отчет по водителям успешно сохранён.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании отчета: " + ex.Message);
            }
        }


        private void поВодителямНаДатуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new DriverFilterDate(this).Show();
        }
    }
}
