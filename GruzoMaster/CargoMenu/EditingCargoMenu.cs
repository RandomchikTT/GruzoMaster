using GruzoMaster.Objects;
using GruzoMaster.Objects.Cargo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster.CargoMenu
{
    public partial class EditingCargoMenu : Form
    {
        private Cargo SelectedCargo { get; set; } = null;
        private List<Company> CompanieList { get; set; } = new List<Company>();
        private List<Driver> DriverInfos { get; set; } = new List<Driver>();
        private List<Transport> Transports { get; set; } = new List<Transport>();
        public EditingCargoMenu(Cargo cargo)
        {
            this.SelectedCargo = cargo;
            InitializeComponent();
            PushDataInForm();
        }
        public void PushDataInForm()
        {
            try
            {
                LoadTransportBox();
                LoadCompanyComboBox();
                LoadDriverBox();
                this.nameCargo.Text = this.SelectedCargo.Name;
                this.descriptionCargo.Text = this.SelectedCargo.Description;
                this.priceCargo.Text = this.SelectedCargo.Price.ToString();
                this.addressFromCargo.Text = this.SelectedCargo.AddressFromCargo;
                this.addressToCargo.Text = this.SelectedCargo.AddressToCargo;
            }
            catch (Exception ex) { MessageBox.Show("PushDataInForm: " + ex.ToString()); }
        }

        public async void LoadCompanyComboBox()
        {
            try
            {
                this.CompanieList = await Company.GetCompanies();
                foreach (Company company in this.CompanieList)
                {
                    this.companyBox.Items.Add(company.Name);
                    if (this.SelectedCargo.CustomerCompany.IdKey == company.IdKey)
                    {
                        this.companyBox.SelectedIndex = this.CompanieList.IndexOf(company);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("LoadComboBox: " + ex.ToString()); }
        }

        public async void LoadDriverBox()
        {
            try
            {
                this.DriverInfos = await Driver.GetDrivers();
                foreach (Driver driverInfo in this.DriverInfos)
                {
                    this.driverBox.Items.Add(driverInfo.FullName);
                    if (this.SelectedCargo.Driver.IdKey == driverInfo.IdKey)
                    {
                        this.driverBox.SelectedIndex = this.DriverInfos.IndexOf(driverInfo);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("LoadDriverBox: " + ex.ToString()); }
        }
        public async void LoadTransportBox()
        {
            try
            {
                this.Transports = await Transport.GetTransports();
                foreach (Transport transport in this.Transports)
                {
                    this.transportBox.Items.Add(transport.TransportModelName.ToString() + " " + transport.ModelDescriptionName + $" [{transport.GovNumber}]");
                    if (this.SelectedCargo.TransportCargo.IdKey == transport.IdKey)
                    {
                        this.transportBox.SelectedIndex = this.Transports.IndexOf(transport);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("LoadTransportBox: " + ex.ToString()); }
        }

        private void buttonEditingCargo_click(object sender, EventArgs e)
        {
            try
            {
                if (this.companyBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Вы не выбрали компанию !");
                    return;
                }
                if (this.driverBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Вы не выбрали водителя !");
                    return;
                }
                if (this.transportBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Вы не выбрали транпсорт !");
                    return;
                }
                if (this.nameCargo.Text.Length <= 3)
                {
                    MessageBox.Show("Вы не указали название груза !");
                    return;
                }
                if (this.descriptionCargo.Text.Length <= 3)
                {
                    MessageBox.Show("Вы не указали описание груза !");
                    return;
                }
                if (this.addressFromCargo.Text.Length <= 3)
                {
                    MessageBox.Show("Вы не указали место отправки груза !");
                    return;
                }
                if (this.addressToCargo.Text.Length <= 3)
                {
                    MessageBox.Show("Вы не указали место прибытие груза !");
                    return;
                }
                if (!Int32.TryParse(this.priceCargo.Text, out Int32 priceCargo) || priceCargo <= 0)
                {
                    MessageBox.Show("Стоимость груза должна быть числом и больше нуля !");
                    return;
                }
                DialogResult dialogResult = MessageBox.Show("Вы уверены что хотите изменить данные о грузе !", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    Transport transport = this.Transports[this.transportBox.SelectedIndex];
                    Boolean isUpdated = false;
                    if (transport.IdKey != this.SelectedCargo.TransportCargo.IdKey)
                    {
                        stringBuilder.Append($"Изменил транспорт с " +
                            $"{this.SelectedCargo.TransportCargo.TransportTypeName.ToString()}-{this.SelectedCargo.TransportCargo.TransportModelName} " +
                            $"#{this.SelectedCargo.TransportCargo.IdKey} на " +
                            $"{transport.TransportTypeName.ToString()}-{transport.TransportModelName} #{transport.IdKey}");
                        isUpdated = true;
                    }
                    Driver driverInfo = this.DriverInfos[this.driverBox.SelectedIndex];
                    if (driverInfo.IdKey != this.SelectedCargo.Driver.IdKey)
                    {
                        stringBuilder.Append($"Изменил водителя с " +
                            $"{this.SelectedCargo.Driver.FullName} " +
                            $"#{this.SelectedCargo.Driver.IdKey} на " +
                            $"{driverInfo.FullName} #{driverInfo.IdKey}");
                        isUpdated = true;
                    }
                    Company company = this.CompanieList[this.companyBox.SelectedIndex];
                    if (company.IdKey != this.SelectedCargo.CustomerCompany.IdKey)
                    {
                        stringBuilder.Append($"Изменил компанию заказчик с " +
                            $"{this.SelectedCargo.CustomerCompany.Name} " +
                            $"#{this.SelectedCargo.CustomerCompany.IdKey} на " +
                            $"{company.Name} #{company.IdKey}");
                        isUpdated = true;
                    }
                    if (this.nameCargo.Text != this.SelectedCargo.Name)
                    {
                        stringBuilder.Append($"Изменил название груза с {this.SelectedCargo.Name} на {this.nameCargo.Text}");
                        isUpdated = true;
                    }
                    if (this.descriptionCargo.Text != this.SelectedCargo.Description)
                    {
                        stringBuilder.Append($"Изменил описание груза с {this.SelectedCargo.Description} на {this.descriptionCargo.Text}");
                        isUpdated = true;
                    }
                    if (priceCargo != this.SelectedCargo.Price)
                    {
                        stringBuilder.Append($"Изменил стоимость груза с {this.SelectedCargo.Price.ConvertToFormatMoney()}₽ на {priceCargo.ConvertToFormatMoney()}₽");
                        isUpdated = true;
                    }
                    if (this.addressFromCargo.Text != this.SelectedCargo.AddressFromCargo)
                    {
                        stringBuilder.Append($"Изменил адрес отправки груза с {this.SelectedCargo.AddressFromCargo} на {this.addressFromCargo.Text}");
                        isUpdated = true;
                    }
                    if (this.addressToCargo.Text != this.SelectedCargo.AddressToCargo)
                    {
                        stringBuilder.Append($"Изменил адрес приюытия груза с {this.SelectedCargo.AddressToCargo} на {this.addressToCargo.Text}");
                        isUpdated = true;
                    }
                    if (!isUpdated)
                    {
                        MessageBox.Show("Вы ничего не изменили !");
                        return;
                    }
                    this.SelectedCargo.TransportCargo = transport;
                    this.SelectedCargo.DeliveryType = CargoDeliveryType.Created;
                    this.SelectedCargo.Price = priceCargo;
                    this.SelectedCargo.Driver = driverInfo;
                    this.SelectedCargo.CreateUserCargo = User.LoggedUser;
                    this.SelectedCargo.CustomerCompany = company;
                    this.SelectedCargo.Name = this.nameCargo.Text;
                    this.SelectedCargo.AddressFromCargo = this.addressFromCargo.Text;
                    this.SelectedCargo.AddressToCargo = this.addressToCargo.Text;
                    this.SelectedCargo.Description = this.descriptionCargo.Text;
                    var newCargoLog = new CargoLog()
                    {
                        UserCreated = User.LoggedUser,
                        TimeCreated = DateTime.Now,
                        Description = $"Внес изменения в данные о грузе: {stringBuilder.ToString()}"
                    };
                    this.SelectedCargo.CargoLogs.Add(newCargoLog);
                    this.SelectedCargo.Update();
                    MessageBox.Show("Вы успешно обновили данные о грузе !");
                    MySQL.AddUserLog(User.LoggedUser.Login, $"Изменил данные о грузе #{this.SelectedCargo.ID}: \n{stringBuilder.ToString()} !");
                    this.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("buttonEditingCargo_click: " + ex.ToString()); }
        }
    }
}
