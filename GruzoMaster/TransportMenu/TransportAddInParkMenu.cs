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

namespace GruzoMaster.TransportMenu
{
    public partial class TransportAddInParkMenu : Form
    {
        private TransportMenu TransportMenu = null;
        private List<Driver> Drivers { get; set; } = new List<Driver>();  
        public TransportAddInParkMenu(TransportMenu transportMenu)
        {
            this.TransportMenu = transportMenu;
            InitializeComponent();
            LoadDrivers();
        }
        public async void LoadDrivers()
        {
            try
            {
                this.Drivers = await Driver.GetDrivers();
                foreach (Driver driver in this.Drivers)
                {
                    this.driverBox1.Items.Add(driver.FullName);
                }
            }
            catch (Exception e) { MessageBox.Show("ERROR LoadDrivers: " + e.ToString()); }
        }
        private async void buttonAddDriver_Click(object sender, EventArgs e)
        {
            try
            {
                Transport.TransportModel transportModel = Transport.TransportModel.None;
                Transport.TransportType transportType = Transport.TransportType.None;
                #region Выставляем какую марку выбрал администратор 
                RadioButton[] radioButtonModels = new RadioButton[]
                {
                    this.radioButton1,
                    this.radioButton2,
                    this.radioButton3,
                    this.radioButton4,
                    this.radioButton7,
                };
                foreach (RadioButton radioButtonModel in radioButtonModels)
                {
                    if (radioButtonModel.Checked)
                    {
                        Enum.TryParse(radioButtonModel.Text, out Transport.TransportModel model);
                        transportModel = model;
                        break;
                    }
                }
                if (transportModel == Transport.TransportModel.None)
                {
                    MessageBox.Show("Вы не выбрали марку транспорта !");
                    return;
                }
                #endregion
                #region Выставляем какой тип транспорта выбрал администратор 
                if (this.radioButton8.Checked)
                {
                    transportType = Transport.TransportType.TruckTractor;
                }
                else if (this.radioButton6.Checked)
                {
                    transportType = Transport.TransportType.Truck;
                }
                else if (this.radioButton5.Checked)
                {
                    transportType = Transport.TransportType.LightCommercial;
                }
                if (transportType == Transport.TransportType.None)
                {
                    MessageBox.Show("Вы не выбрали тип транспорта !");
                    return;
                }
                #endregion
                if (String.IsNullOrEmpty(this.textBox1.Text) || this.textBox1.Text.Length < 3)
                {
                    MessageBox.Show("Вы не указали модель транспорта !");
                    return;
                }
                if (String.IsNullOrEmpty(this.textBox2.Text) || this.textBox2.Text.Length < 3)
                {
                    MessageBox.Show("Вы не указали гос. номер транспорта !");
                    return;
                }
                if (this.dateTimePicker1.Value.ToString("d") == "01.01.1900")
                {
                    MessageBox.Show("Вы не выбрали время окончания тех. осмотра !");
                    return;
                }
                Int32 index = this.driverBox1.SelectedIndex;
                if (!this.Drivers.Any(_ => this.Drivers.IndexOf(_) == index))
                {
                    MessageBox.Show("Вы не выбрали водителя !");
                    return;
                }
                if (!Int32.TryParse(this.availableVolume.Text.ToString(), out Int32 volume) || volume <= 0)
                {
                    MessageBox.Show("Допустимый обьем должен быть числом и больше 0 !");
                    return;
                }
                if (!Int32.TryParse(this.availableWeight.Text.ToString(), out Int32 weight) || weight <= 0)
                {
                    MessageBox.Show("Допустимый вес должен быть числом и больше 0 !");
                    return;
                }
                Driver driver = this.Drivers[index];
                List<Transport> transports = await Transport.GetTransports(); 
                if (transports.Any(_ => _.CurrentDriverId == driver.IdKey))
                {
                    MessageBox.Show("Данный водитель уже привязан к другому транспорту !");
                    return;
                }
                DialogResult result = MessageBox.Show("Вы уверены что хотите добавить новый транспорт ?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Int64 id = await MySQL.QueryLastInsertAsync($"INSERT INTO `transport` (`Brand`,`Model`,`Type`,`GovNumber`,`TechInspection`,`Capacity`,`Volume`,`CurrentDriverId`) " +
                    $"VALUES ({Convert.ToInt32(transportModel)},'{this.textBox1.Text}','{Convert.ToInt32(transportType)}'," +
                    $"'{this.textBox2.Text}','{this.dateTimePicker1.Value.ToString("d")}', {weight}, {volume}, {driver.IdKey})");
                    MySQL.AddUserLog(User.LoggedUser.Login, $"Добавил транспорт в базу данных: {transportModel.ToString()} #{id}.");
                    MessageBox.Show("Вы успешно добавили транспорт в базу данных !");
                    this.TransportMenu.LoadTransportMenu();
                    this.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("buttonAddDriver_Click: " + ex.ToString()); }
        }
    }
}
