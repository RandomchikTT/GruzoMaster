using GruzoMaster.Objects;
using GruzoMaster.Objects.Cargo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster.TransortOrders
{
    public partial class TransportCargoMenu : Form
    {
        private List<Transport> Transports { get; set; } = new List<Transport>();
        public TransportCargoMenu()
        {
            InitializeComponent();
            LoadTransportBoxList();
            this.Транспорт.SelectedIndexChanged += Транспорт_SelectedIndexChanged;
            LoadDataGridViewVehicle();
        }

        private void Транспорт_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 index = this.Транспорт.SelectedIndex;
            if (!this.Transports.Any(_ => this.Transports.IndexOf(_) == index))
            {
                MessageBox.Show("Выберите транспорт !");
                return;
            }
            LoadDataGridViewVehicle(this.Transports[index].IdKey);
        }
        public void LoadCargoPartView(List<CargoPartViewModel> cargoParts)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Columns.Clear();
            this.dataGridView1.AutoGenerateColumns = false;

            DataGridViewComboBoxColumn statusColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "Статус груза",
                DataPropertyName = "DeliveryType",
                Name = "DeliveryType",
                DataSource = Enum.GetValues(typeof(CargoDeliveryType))
            };

            this.dataGridView1.Columns.Add(statusColumn);

            this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Транспорт",
                DataPropertyName = "VehName",
                Name = "VehName"
            });

            this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Название груза",
                DataPropertyName = "CargoName",
                Name = "CargoName"
            });

            this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "От куда",
                DataPropertyName = "CargoAddressFrom",
                Name = "CargoAddressFrom"
            });

            this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Куда",
                DataPropertyName = "CargoAddressTo",
                Name = "CargoAddressTo"
            });

            this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Описание",
                DataPropertyName = "Description",
                Name = "Description"
            });

            this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Вес части",
                DataPropertyName = "PartWeight",
                Name = "PartWeight"
            });

            this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Объем части",
                DataPropertyName = "PartVolume",
                Name = "PartVolume"
            });

            this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Сумма",
                DataPropertyName = "Cost",
                Name = "Cost"
            });

            this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Дата доставки",
                DataPropertyName = "DeliveryDate",
                Name = "DeliveryDate"
            });

            // Привязываем данные


            this.dataGridView1.DataSource = cargoParts;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if (row.DataBoundItem is CargoPartViewModel cargoPart)
                {
                    row.Cells["DeliveryType"].Value = cargoPart.DeliveryType;
                }
            }
            CargoDeliveryType previousStatus = CargoDeliveryType.NotSuccessful;

            this.dataGridView1.CellBeginEdit += (sender, e) =>
            {
                if (e.ColumnIndex == statusColumn.Index && e.RowIndex >= 0)
                {
                    var row = this.dataGridView1.Rows[e.RowIndex];
                    var cargoPart = (CargoPartViewModel)row.DataBoundItem;
                    previousStatus = cargoPart.DeliveryType; // Запоминаем старый статус
                }
            };
            this.dataGridView1.CellValueChanged += async (sender, e) =>
            {
                try
                {
                    if (e.ColumnIndex == statusColumn.Index && e.RowIndex >= 0)
                    {
                        var row = this.dataGridView1.Rows[e.RowIndex];
                        var cell = row.Cells[e.ColumnIndex];

                        if (cell.Value != null && Enum.TryParse(cell.Value.ToString(), out CargoDeliveryType newStatus))
                        {
                            var cargoPart = (CargoPartViewModel)row.DataBoundItem;
                            CargoDeliveryType oldStatus = cargoPart.DeliveryType;
                            bool isUpdated = await UpdateCargoStatus(cargoPart.ID, newStatus);
                            if (!isUpdated)
                            {
                                cell.Value = previousStatus;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка обновления статуса: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };



            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private async void LoadDataGridViewVehicle(int? idkey = null)
        {
            try
            {
                List<CargoPartViewModel> cargoPartsList = new List<CargoPartViewModel>();
                List<Cargo> cargos = await CargoMenu.MainCargoMenu.GetCargoList();
                List<Transport> transports = await Transport.GetTransports();

                foreach (var cargo in cargos)
                {
                    var filteredParts = idkey != null ? cargo.CargoParts.Where(_ => _.Transport == idkey.Value) : cargo.CargoParts;

                    foreach (var part in filteredParts)
                    {
                        Transport transport = transports.Find(t => t.IdKey == (idkey != null ? idkey.Value : part.Transport));

                        cargoPartsList.Add(new CargoPartViewModel
                        {
                            ID = part.ID,
                            CargoName = cargo.Name,
                            Cost = Convert.ToInt64(cargo.Price / cargo.CargoParts.Count),
                            VehName = $"[{transport.GovNumber}] {transport.TransportModelName} {transport.ModelDescriptionName}",
                            CargoAddressFrom = cargo.AddressFromCargo,
                            CargoAddressTo = cargo.AddressToCargo,
                            Description = cargo.Description,
                            PartWeight = part.Weight,
                            PartVolume = part.Volume,
                            DeliveryDate = part.DeliveryDate,
                            DeliveryType = part.CargoDeliveryType,
                        });
                    }
                }

                LoadCargoPartView(cargoPartsList);
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadDataGridViewVehicle: " + ex);
            }
        }
        private async Task<Boolean> UpdateCargoStatus(long cargoPartId, CargoDeliveryType newStatus)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Вы уверены что хотите изменить статус заказа ?", "Уведомление", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in this.dataGridView1.Rows)
                    {
                        if (row.DataBoundItem is CargoPartViewModel cargoPart)
                        {
                            if (cargoPart.ID == cargoPartId)
                            {
                                cargoPart.DeliveryType = newStatus;
                                List<Cargo> cargoList = await CargoMenu.MainCargoMenu.GetCargoList();
                                Cargo cargo = cargoList.Find(_ => _.CargoParts.Any(x => x.ID == cargoPartId));
                                if (cargo == null)
                                {
                                    MessageBox.Show("Заказ не найден !");
                                    return false;
                                }
                                CargoPart part = cargo.CargoParts.Find(_ => _.ID == cargoPartId);
                                part.CargoDeliveryType = newStatus;
                                if (cargo.CargoParts.Count(_ => _.CargoDeliveryType == CargoDeliveryType.Сompleted) >= cargo.CargoParts.Count)
                                {
                                    cargo.DeliveryType = CargoDeliveryType.Сompleted;
                                    cargo.Update();
                                    MessageBox.Show("Заказ полностью доставлен !");
                                    return true;
                                }
                                else
                                {
                                    if (cargo.DeliveryType != CargoDeliveryType.InProcessing && cargo.DeliveryType != CargoDeliveryType.Сompleted)
                                    {
                                        cargo.DeliveryType = CargoDeliveryType.InProcessing;
                                        cargo.Update();
                                        return true;
                                    }
                                }
                                await part.UpdateInDatabase();
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении статуса: " + ex.Message);
                return false;
            }
        }
        public async void FilterCargoByDate(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                List<CargoPartViewModel> cargoPartsList = new List<CargoPartViewModel>();
                List<Cargo> cargos = await CargoMenu.MainCargoMenu.GetCargoList();
                List<Transport> transports = await Transport.GetTransports();

                Transport selectedTransport = null;
                if (this.Транспорт.SelectedIndex != -1)
                {
                    selectedTransport = transports.Find(t => t.IdKey == this.Transports[this.Транспорт.SelectedIndex].IdKey);
                }

                foreach (var cargo in cargos)
                {
                    var filteredParts = cargo.CargoParts
                        .Where(p => (p.DeliveryDate.Date >= dateFrom.Date && p.DeliveryDate.Date <= dateTo.Date) 
                        && (selectedTransport == null || p.Transport == selectedTransport.IdKey));

                    foreach (var part in filteredParts)
                    {
                        Transport transport = selectedTransport ?? transports.Find(t => t.IdKey == part.Transport);

                        cargoPartsList.Add(new CargoPartViewModel
                        {
                            ID = part.ID,
                            CargoName = cargo.Name,
                            Cost = Convert.ToInt64(cargo.Price / cargo.CargoParts.Count),
                            VehName = $"[{transport.GovNumber}] {transport.TransportModelName} {transport.ModelDescriptionName}",
                            CargoAddressFrom = cargo.AddressFromCargo,
                            CargoAddressTo = cargo.AddressToCargo,
                            Description = cargo.Description,
                            PartWeight = part.Weight,
                            PartVolume = part.Volume,
                            DeliveryDate = part.DeliveryDate,
                            DeliveryType = part.CargoDeliveryType,
                        });
                    }
                }

                LoadCargoPartView(cargoPartsList);
            }
            catch (Exception ex)
            {
                MessageBox.Show("FilterCargoByDate: " + ex);
            }
        }

        public class CargoPartViewModel
        {
            public Int64 Cost { get; set; }
            public Int64 ID { get; set; }
            public string VehName { get; set; }
            public string CargoName { get; set; } // Название груза
            public string CargoAddressFrom { get; set; } // Тип груза
            public string CargoAddressTo { get; set; } // Общий вес груза
            public string Description { get; set; } // Общий объем груза
            public int PartWeight { get; set; } // Вес части груза
            public int PartVolume { get; set; } // Объем части груза
            public DateTime DeliveryDate { get; set; } // Дата доставки
            public CargoDeliveryType DeliveryType { get; set; } // Дата доставки
        }

        private async void LoadTransportBoxList()
        {
            try
            {
                this.Transports = await Transport.GetTransports();
                foreach (Transport transport in this.Transports)
                {
                    this.Транспорт.Items.Add($"[{transport.GovNumber}] {transport.ModelDescriptionName}");
                }
            }
            catch (Exception e) { MessageBox.Show("ERROR LoadTransportBoxList: " + e.ToString()); }
        }
        private TransportOrderFilterByDate TransportOrderFilterByDate { get; set; } = null;
        private async void поДнюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.TransportOrderFilterByDate != null)
                {
                    MessageBox.Show("У вас уже есть открытое меню фильтра по датам !");
                    return;
                }
                this.TransportOrderFilterByDate = new TransportOrderFilterByDate(this);
                this.TransportOrderFilterByDate.FormClosed += TransportOrderFilterByDate_FormClosed;
                this.TransportOrderFilterByDate.Show();
            }
            catch (Exception ex) { MessageBox.Show("ERROR поДнюToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private void TransportOrderFilterByDate_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.TransportOrderFilterByDate = null;
        }
        private TransportReportForPeriod TransportReportForPeriod { get; set; } = null;
        private void поОбщейМассеЗаПериодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.TransportOrderFilterByDate != null)
            {
                MessageBox.Show("У вас уже есть открытое меню формирования отчета !");
                return;
            }
            this.TransportReportForPeriod = new TransportReportForPeriod();
            this.TransportReportForPeriod.FormClosed += TransportReportForPeriod_FormClosed;
            this.TransportReportForPeriod.Show();
        }

        private void TransportReportForPeriod_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.TransportReportForPeriod = null;
        }

        private void всехЗаказовЗаПериодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.TransportOrderFilterByDate != null)
            {
                MessageBox.Show("У вас уже есть открытое меню формирования отчета !");
                return;
            }
            this.TransportReportForPeriod = new TransportReportForPeriod(true);
            this.TransportReportForPeriod.FormClosed += TransportReportForPeriod_FormClosed;
            this.TransportReportForPeriod.Show();
        }
    }
}
