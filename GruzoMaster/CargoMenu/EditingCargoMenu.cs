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
    }
}
