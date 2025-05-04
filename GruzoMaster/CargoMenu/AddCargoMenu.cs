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
    public partial class AddCargoMenu : Form
    {
        private List<Company> CompanieList { get; set; } = new List<Company>();
        private List<Driver> DriverInfos { get; set; } = new List<Driver>();
        private List<Transport> Transports { get; set; } = new List<Transport>();
        public AddCargoMenu()
        {
            InitializeComponent();
            LoadComboBox();
        }
        public async void LoadComboBox()
        {
            try
            {
                this.CompanieList = await Company.GetCompanies();
                foreach (Company company in this.CompanieList)
                {
                    this.companyBox.Items.Add(company.Name);
                }
            }
            catch (Exception ex) { MessageBox.Show("LoadComboBox: " + ex.ToString()); }
        }
        private async void buttonAddCargo_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.companyBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Вы не выбрали компанию !");
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
                DateTime deadline = this.dateTimePicker1.Value;
                if (deadline <= DateTime.Now)
                {
                    MessageBox.Show("Нельзя установить такую дату доставки !");
                    return;
                }
                if (!Int32.TryParse(this.textBoxVolume.Text, out Int32 volumeCargo) || volumeCargo <= 0)
                {
                    MessageBox.Show("Обьем груза должна быть числом и больше нуля !");
                    return;
                }
                if (!Int32.TryParse(this.textBoxWeight.Text, out Int32 weightCargo) || weightCargo <= 0)
                {
                    MessageBox.Show("Вес груза должна быть числом и больше нуля !");
                    return;
                }
                Company company = this.CompanieList[this.companyBox.SelectedIndex];
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
                    DeadlineTime = deadline,
                    DeliveryType = CargoDeliveryType.Created,
                    Price = priceCargo,
                    CreateUserCargo = User.LoggedUser,
                    CustomerCompany = company,
                    Name = this.nameCargo.Text,
                    AddressFromCargo = this.addressFromCargo.Text,
                    AddressToCargo = this.addressToCargo.Text,
                    Description = this.descriptionCargo.Text,
                };
                Boolean isCreated = await newCargo.Create(weightCargo, volumeCargo);
                if (!isCreated) return;
                MessageBox.Show("Вы успешно создали заявку на груз !");
                this.Close();
                MySQL.AddUserLog(User.LoggedUser.Login, "Создал заявку на выполнение груза !");
            }
            catch (Exception ex) { MessageBox.Show("buttonAddCargo_Click: " + ex.ToString()); }
        }
    }
}
