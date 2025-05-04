using GruzoMaster.Objects;
using GruzoMaster.Objects.Cargo;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster.TransortOrders
{
    public partial class TransportReportForPeriod : Form
    {
        private bool IsAllOrders { get; set; } = false;
        public TransportReportForPeriod(bool allOrders = false)
        {
            IsAllOrders = allOrders;
            InitializeComponent();
        }

        private async void buttonAddCargo_Click(object sender, EventArgs e)
        {
            List<Cargo> cargoList = await CargoMenu.MainCargoMenu.GetCargoList();
            await GenerateCargoReport(cargoList, this.guna2DateTimePicker1.Value, this.guna2DateTimePicker2.Value, "Отчет.xlsx");
        }
        public async Task GenerateCargoReport(List<Cargo> cargoList, DateTime startDate, DateTime endDate, string filePath)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Отчет по грузам");
                worksheet.Cells[1, 1].Value = "№";
                worksheet.Cells[1, 2].Value = "Груз";
                worksheet.Cells[1, 3].Value = "Дата доставки";
                worksheet.Cells[1, 4].Value = "Машина";
                worksheet.Cells[1, 5].Value = "Вес (кг)";
                worksheet.Cells[1, 6].Value = "Обьем (см.3)";
                worksheet.Cells[1, 7].Value = "Стоимость (Byn)";
                worksheet.Cells[1, 1, 1, 7].Style.Font.Bold = true;

                var cargoParts = IsAllOrders == false ? cargoList
                    .Where(c => c.DeliveryType == CargoDeliveryType.Сompleted)
                    .SelectMany(c => c.CargoParts)
                    .Where(cp => cp.DeliveryDate.Date >= startDate && cp.DeliveryDate.Date <= endDate)
                    .ToList()
                    :
                    cargoList.SelectMany(c => c.CargoParts)
                    .Where(cp => cp.DeliveryDate.Date >= startDate && cp.DeliveryDate.Date <= endDate)
                    .ToList();

                int row = 2;
                double totalWeight = 0, totalCost = 0, totalVolume = 0;
                Dictionary<int, (double weight, double volume)> weightByCar = new Dictionary<int, (double weight, double volume)>(); // Для графика
                Dictionary<string, (double weight, double volume)> monthlyStats = new Dictionary<string, (double weight, double volume)>(); // Для месячного графика
                List<Transport> transports = await Transport.GetTransports();

                foreach (var part in cargoParts)
                {
                    var cargo = cargoList.Find(_ => _.CargoParts.Any(x => x.ID == part.ID));
                    var price = cargo.Price / cargo.CargoParts.Count;
                    var transport = transports.Find(_ => _.IdKey == part.Transport);
                    worksheet.Cells[row, 1].Value = row - 1;
                    worksheet.Cells[row, 2].Value = cargo.Name;
                    worksheet.Cells[row, 3].Value = part.DeliveryDate.ToShortDateString();
                    worksheet.Cells[row, 4].Value = $"[{transport.GovNumber}] {transport.TransportModelName} {transport.ModelDescriptionName}";
                    worksheet.Cells[row, 5].Value = part.Weight;
                    worksheet.Cells[row, 6].Value = part.Volume;
                    worksheet.Cells[row, 7].Value = price;

                    if (IsAllOrders)
                    {
                        System.Drawing.Color statusColor = System.Drawing.Color.White;
                        switch (cargo.DeliveryType)
                        {
                            case CargoDeliveryType.Сompleted:
                                statusColor = System.Drawing.Color.LightGreen;
                                break;
                            case CargoDeliveryType.Created:
                                statusColor = System.Drawing.Color.Yellow;
                                break;
                            case CargoDeliveryType.InProcessing:
                                statusColor = System.Drawing.Color.LightBlue;
                                break;
                            case CargoDeliveryType.NotSuccessful:
                                statusColor = System.Drawing.Color.LightCoral;
                                break;
                        }
                        worksheet.Cells[row, 1, row, 7].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[row, 1, row, 7].Style.Fill.BackgroundColor.SetColor(statusColor);
                    }
                    totalWeight += part.Weight;
                    totalVolume += part.Volume;
                    totalCost += price;

                    if (weightByCar.ContainsKey(part.Transport))
                    {
                        weightByCar[part.Transport] = (weightByCar[part.Transport].weight + part.Weight, weightByCar[part.Transport].volume + part.Volume);
                    }
                    else
                        weightByCar[part.Transport] = (part.Weight, part.Volume);

                    // Месячная статистика
                    var monthKey = part.DeliveryDate.ToString("yyyy-MM");
                    if (!monthlyStats.ContainsKey(monthKey))
                    {
                        monthlyStats[monthKey] = (0, 0); // Инициализация значения для месяца
                    }
                    monthlyStats[monthKey] = (monthlyStats[monthKey].weight + part.Weight, monthlyStats[monthKey].volume + part.Volume);

                    row++;
                }

                worksheet.Cells[row, 3].Value = "ИТОГО";
                worksheet.Cells[row, 3, row, 4].Style.Font.Bold = true;
                worksheet.Cells[row, 5].Value = totalWeight;
                worksheet.Cells[row, 6].Value = totalVolume;
                worksheet.Cells[row, 7].Value = totalCost;

                if (row > 2) // Убедимся, что есть хотя бы две строки данных
                {
                    var chart = worksheet.Drawings.AddChart("chart", eChartType.Line) as ExcelLineChart;
                    chart.Title.Text = "Динамика перевозок";
                    chart.SetPosition(1, 0, 7, 0);
                    chart.SetSize(600, 300);
                    chart.Series.Add(worksheet.Cells[$"E2:E{row - 1}"], worksheet.Cells[$"C2:C{row - 1}"]);
                    chart.YAxis.Title.Text = "Вес (кг)";
                    chart.XAxis.Title.Text = "Дата доставки";
                }

                if (weightByCar.Count > 1) // График по машинам
                {
                    var carChartSheet = package.Workbook.Worksheets.Add("График по машинам");
                    carChartSheet.Cells[1, 1].Value = "Машина";
                    carChartSheet.Cells[1, 2].Value = "Общий вес (кг)";
                    carChartSheet.Cells[1, 3].Value = "Общий обьем (см.3)";
                    carChartSheet.Cells[1, 1, 1, 3].Style.Font.Bold = true;

                    int chartRow = 2;
                    foreach (var car in weightByCar)
                    {
                        Transport transport = transports.Find(_ => _.IdKey == car.Key);
                        carChartSheet.Cells[chartRow, 1].Value = $"[{transport.GovNumber}] {transport.TransportModelName} {transport.ModelDescriptionName}";
                        carChartSheet.Cells[chartRow, 2].Value = car.Value.weight;
                        carChartSheet.Cells[chartRow, 3].Value = car.Value.volume;
                        chartRow++;
                    }

                    var barChart = carChartSheet.Drawings.AddChart("cars_chart", eChartType.ColumnClustered) as ExcelBarChart;
                    barChart.Title.Text = "Распределение веса грузов по машинам";
                    barChart.SetPosition(1, 0, 4, 0);
                    barChart.SetSize(600, 300);
                    barChart.Series.Add(carChartSheet.Cells[$"B2:B{chartRow - 1}"], carChartSheet.Cells[$"A2:A{chartRow - 1}"]);
                    barChart.YAxis.Title.Text = "Вес (кг)";
                    barChart.XAxis.Title.Text = "Машины";
                }

                if (monthlyStats.Count > 0) // Месячный график
                {
                    var monthChartSheet = package.Workbook.Worksheets.Add("График по месяцам");
                    monthChartSheet.Cells[1, 1].Value = "Месяц";
                    monthChartSheet.Cells[1, 2].Value = "Вес (кг)";
                    monthChartSheet.Cells[1, 3].Value = "Объем (м³)";
                    monthChartSheet.Cells[1, 1, 1, 3].Style.Font.Bold = true;

                    int monthChartRow = 2;
                    foreach (var month in monthlyStats)
                    {
                        monthChartSheet.Cells[monthChartRow, 1].Value = month.Key;
                        monthChartSheet.Cells[monthChartRow, 2].Value = month.Value.weight;
                        monthChartSheet.Cells[monthChartRow, 3].Value = month.Value.volume;
                        monthChartRow++;
                    }

                    var monthChart = monthChartSheet.Drawings.AddChart("month_chart", eChartType.Line) as ExcelLineChart;
                    monthChart.Title.Text = "Месячная динамика перевозок";
                    monthChart.SetPosition(1, 0, 4, 0);
                    monthChart.SetSize(600, 300);
                    monthChart.Series.Add(monthChartSheet.Cells[$"B2:B{monthChartRow - 1}"], monthChartSheet.Cells[$"A2:A{monthChartRow - 1}"]);
                    monthChart.YAxis.Title.Text = "Вес (кг) и Объем (м³)";
                    monthChart.XAxis.Title.Text = "Месяц";
                }

                worksheet.Cells.AutoFitColumns();
                File.WriteAllBytes(filePath, package.GetAsByteArray());
                MessageBox.Show("Вы успешно сформировали отчет !");
                this.Close();
            }
        }


    }
}
