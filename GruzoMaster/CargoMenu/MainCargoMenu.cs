using GruzoMaster.Objects;
using GruzoMaster.Objects.Cargo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster.CargoMenu
{
    public partial class MainCargoMenu : Form
    {
        public List<Cargo> CargoList { get; set; } = new List<Cargo>();
        private AddCargoMenu AddCargoMenu { get; set; } = null;
        public MainCargoMenu()
        {
            InitializeComponent();
            LoadCargoMenu();
        }
        public async void LoadCargoMenu()
        {
            try
            {
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `cargo`");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Int32 idCargo = Convert.ToInt32(row["ID"]);
                        Int32 idCreatorUser = Convert.ToInt32(row["ID_user_creator"]);
                        Int32 idCompany = Convert.ToInt32(row["ID_company"]);
                        Int32 idTrasport = Convert.ToInt32(row["ID_Transport"]);
                        String name = Convert.ToString(row["Name"]);
                        String description = Convert.ToString(row["Description"]);
                        String addressFromCargo = Convert.ToString(row["addressFromCargo"]);
                        String addressToCargo = Convert.ToString(row["AddressToCargo"]);
                        Int32 idDriver = Convert.ToInt32(row["DriverID"]);
                        Int32 price = Convert.ToInt32(row["Price"]);
                        Int32 idForwarder = Convert.ToInt32(row["ForwarderID"]);
                        CargoDeliveryType cargoDeliveryType = (CargoDeliveryType)Convert.ToInt32(row["DeliveryType"]);
                        List<CargoLog> cargoLogs = JsonConvert.DeserializeObject<List<CargoLog>>(row["CargoLogs"].ToString());
                        Company company = null;
                        if (idCompany != -1)
                        {
                            company = await Companies.MainMenuCompany.GetCompanyById(idCompany);
                        }
                        Transport transport = null;
                        if (idTrasport != -1)
                        {
                            transport = await TransportMenu.TransportMenu.GetTransportById(idTrasport);
                        }
                        DriverInfo driverInfo = null;
                        if (idDriver != -1)
                        {
                            driverInfo = await MenuDrivers.GetDriverById(idDriver);
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
                        CargoList.Add(new Cargo()
                        {
                            ID = idCargo,
                            CreateUserCargo = user,
                            CustomerCompany = company,
                            Driver = driverInfo,
                            TransportCargo = transport,
                            Price = price,
                            AddressFromCargo = addressFromCargo,
                            AddressToCargo = addressToCargo,
                            DeliveryType = cargoDeliveryType,
                            CargoLogs = cargoLogs,
                            Name = name,
                            Forwarder = forwarderUser,
                        });
                    }
                    PopulateDataGridView();
                }
            }
            catch (Exception e) { MessageBox.Show("LoadCargoMenu: " + e.ToString()); }
        }
        private void PopulateDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            // Add columns
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("CreateUserCargoName", "Имя создателя");
            dataGridView1.Columns.Add("CustomerCompanyName", "Название компании");
            dataGridView1.Columns.Add("DriverName", "Имя водителя");
            dataGridView1.Columns.Add("TransportCargoGovNumber", "Гос. номер транспорта");
            dataGridView1.Columns.Add("TransportCargoModelDescriptionName", "Модель транспорта");
            dataGridView1.Columns.Add("Price", "Цена");
            dataGridView1.Columns.Add("Name", "Название");
            dataGridView1.Columns.Add("DeliveryType", "Тип доставки");
            dataGridView1.Columns.Add("AddressFromCargo", "Адрес отправления");
            dataGridView1.Columns.Add("AddressToCargo", "Адрес доставки");
            dataGridView1.Columns.Add("ForwarderName", "Экспедитор");


            // Add rows
            foreach (var cargo in CargoList)
            {
                dataGridView1.Rows.Add(
                    cargo.ID,
                    cargo.CreateUserCargo?.Name,
                    cargo.CustomerCompany?.Name,
                    cargo.Driver?.FullName,
                    cargo.TransportCargo?.GovNumber,
                    cargo.TransportCargo?.ModelDescriptionName,
                    cargo.Price,
                    cargo.Name,
                    cargo.GetDeliveryTypeDescription(),
                    cargo.AddressFromCargo,
                    cargo.AddressToCargo,
                    cargo.Forwarder?.Name
                );
            }
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

        private void AddCargoMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.AddCargoMenu = null;
        }
    }
}
