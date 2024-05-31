using GruzoMaster.Objects;
using GruzoMaster.Objects.Cargo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster.CargoMenu
{
    public partial class AddCargoMenu : Form
    {
        private List<Company> CompanieList { get; set; } = new List<Company>();
        private List<DriverInfo> DriverInfos { get; set; } = new List<DriverInfo>();
        private List<Transport> Transports { get; set; } = new List<Transport>();
        public AddCargoMenu()
        {
            InitializeComponent();
            LoadComboBox();
            LoadDriverBox();
            LoadTransportBox();
        }
        public async void LoadComboBox()
        {
            try
            {
                this.CompanieList = await Companies.MainMenuCompany.GetCompanies();
                foreach (Company company in this.CompanieList)
                {
                    this.comboBox1.Items.Add(company.Name);
                }
            }
            catch (Exception ex) { MessageBox.Show("LoadComboBox: " + ex.ToString()); }
        }

        public async void LoadDriverBox()
        {
            try
            {
                this.DriverInfos = await MenuDrivers.GetDrivers();
                foreach (DriverInfo driverInfo in this.DriverInfos)
                {
                    this.comboBox2.Items.Add(driverInfo.FullName);
                }
            }
            catch (Exception ex) { MessageBox.Show("LoadDriverBox: " + ex.ToString()); }
        }
        public async void LoadTransportBox()
        {
            try
            {
                this.Transports = await TransportMenu.TransportMenu.GetTransports();
                foreach (Transport transport in this.Transports)
                {
                    this.comboBox3.Items.Add(transport.TransportModelName.ToString() + " " + transport.ModelDescriptionName + $" [{transport.GovNumber}]");
                }
            }
            catch (Exception ex) { MessageBox.Show("LoadTransportBox: " + ex.ToString()); }
        }

        private void buttonAddCargo_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Вы не выбрали компанию !");
                    return;
                }
                if (this.comboBox2.SelectedIndex == -1)
                {
                    MessageBox.Show("Вы не выбрали водителя !");
                    return;
                }
                if (this.comboBox3.SelectedIndex == -1)
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
                Transport transport = this.Transports[this.comboBox3.SelectedIndex];
                DriverInfo driverInfo = this.DriverInfos[this.comboBox2.SelectedIndex];
                Company company = this.CompanieList[this.comboBox1.SelectedIndex];
                Cargo newCargo = new Cargo()
                {
                    CargoLogs = new List<CargoLog>()
                    {
                        new CargoLog()
                        {
                            UserCreated = User.LoggedUser,
                            Description = "Создал заявку на выполнение груза"
                        }
                    },
                    TransportCargo = transport,
                    DeliveryType = CargoDeliveryType.Created,
                    Price = priceCargo,
                    Driver = driverInfo,
                    CreateUserCargo = User.LoggedUser,
                    CustomerCompany = company,
                    Name = this.nameCargo.Text,
                    AddressFromCargo = this.addressFromCargo.Text,
                    AddressToCargo = this.addressToCargo.Text,
                    Description = this.descriptionCargo.Text,
                };
                newCargo.Create();
                MessageBox.Show("Вы успешно создали заявку на груз !");
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("buttonAddCargo_Click: " + ex.ToString()); }
        }
    }
}
