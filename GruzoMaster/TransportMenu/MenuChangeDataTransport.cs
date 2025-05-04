using GruzoMaster.Objects;
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
using static GruzoMaster.Objects.Transport;

namespace GruzoMaster.TransportMenu
{
    public partial class MenuChangeDataTransport : Form
    {
        private Transport Transport = null;
        private TransportMenu TransportMenu = null;
        private List<Driver> Drivers { get; set; } = new List<Driver>();
        public MenuChangeDataTransport(TransportMenu transportMenu, Transport transport)
        {
            this.TransportMenu = transportMenu;
            this.Transport = transport;
            InitializeComponent();
            LoadDrivers();
            if (this.Transport != null)
            {
                #region Выставляем марку
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
                    if (radioButtonModel.Text == this.Transport.TransportModelName.ToString())
                    {
                        radioButtonModel.Checked = true;
                        break;
                    }
                }
                #endregion
                #region Выставляем какой тип транспорта выбрал администратор 
                if (this.Transport.TransportTypeName == Transport.TransportType.TruckTractor)
                {
                    this.radioButton8.Checked = true;
                }
                else if (this.Transport.TransportTypeName == Transport.TransportType.Truck)
                {
                    this.radioButton6.Checked = true;
                }
                else if (this.Transport.TransportTypeName == Transport.TransportType.LightCommercial)
                {
                    this.radioButton5.Checked = true;
                }
                #endregion
                this.textBox1.Text = this.Transport.ModelDescriptionName;
                this.textBox2.Text = this.Transport.GovNumber;
                this.dateTimePicker1.Value = this.Transport.TimeTechInspection;
                Int32 index = this.Drivers.FindIndex(_ => _.IdKey == this.Transport.CurrentDriverId);
                this.guna2ComboBox1.SelectedIndex = index;
            }
        }

        public async void LoadDrivers()
        {
            try
            {
                this.Drivers = await Driver.GetDrivers();
                foreach (Driver driver in this.Drivers)
                {
                    this.guna2ComboBox1.Items.Add(driver.FullName);
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
                if (!int.TryParse(this.textBox3.Text, out Int32 availableWeight) || availableWeight <= 0)
                {
                    MessageBox.Show("Укажите корректное число веса !");
                    return;
                }
                if (!int.TryParse(this.textBox3.Text, out Int32 availableVolume) || availableVolume <= 0)
                {
                    MessageBox.Show("Укажите корректное число обьема !");
                    return;
                }
                DialogResult result = MessageBox.Show("Вы уверены что хотите изменить данные о транспорте ?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    await MySQL.QueryAsync($"UPDATE `transport` SET " +
                            $"`Brand` = {Convert.ToInt32(transportModel)}, " +
                            $"`Model` = '{this.textBox1.Text}', " +
                            $"`Type` = {Convert.ToInt32(transportType)}, " +
                            $"`GovNumber` = '{this.textBox2.Text}', " +
                            $"`Capacity` = '{availableWeight}', " +
                            $"`Volume` = '{availableVolume}', " +
                            $"`TechInspection` = '{this.dateTimePicker1.Value.ToString("d")}' " +
                            $"WHERE `id` = {this.Transport.IdKey}");
                    MySQL.AddUserLog(User.LoggedUser.Login, $"Изменил данные о транспорте: {transportModel.ToString()} #{this.Transport.IdKey}.");
                    MessageBox.Show("Вы успешно изменили данные о транспорте !");
                    this.TransportMenu.LoadTransportMenu();
                    this.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("buttonAddDriver_Click: " + ex.ToString()); }
        }
    }
}
