using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster.Companies
{
    public partial class MenuAddBankDataCompany : Form
    {
        private MenuAddCompany MenuAddCompany = null;
        public MenuAddBankDataCompany(MenuAddCompany menuAddCompany, Dictionary<CompanyBankData, String> dataCompanyBank)
        {
            InitializeComponent();
            this.MenuAddCompany = menuAddCompany;
            if (dataCompanyBank.ContainsKey(CompanyBankData.INN))
            {
                this.textBox1.Text = dataCompanyBank[CompanyBankData.INN];
            }
            if (dataCompanyBank.ContainsKey(CompanyBankData.YNN))
            {
                this.textBox2.Text = dataCompanyBank[CompanyBankData.YNN];
            }
            if (dataCompanyBank.ContainsKey(CompanyBankData.NameOfBank))
            {
                this.textBox3.Text = dataCompanyBank[CompanyBankData.NameOfBank];
            }
            if (dataCompanyBank.ContainsKey(CompanyBankData.NumberBank))
            {
                this.textBox4.Text = dataCompanyBank[CompanyBankData.NumberBank];
            }
            if (dataCompanyBank.ContainsKey(CompanyBankData.AddressBank))
            {
                this.textBox5.Text = dataCompanyBank[CompanyBankData.AddressBank];
            }
        }

        private void buttonAddDriver_Click(object sender, EventArgs e)
        {
            try
            {
                String INN = this.textBox1.Text,
                    YNN = this.textBox2.Text,
                    nameBank = this.textBox3.Text,
                    numberBankAccount = this.textBox4.Text,
                    adressBank = this.textBox5.Text;
                Dictionary<CompanyBankData, String> dataCompanyBank = new Dictionary<CompanyBankData, String>
                {
                    { CompanyBankData.INN, INN },
                    { CompanyBankData.YNN, YNN },
                    { CompanyBankData.NameOfBank, nameBank },
                    { CompanyBankData.NumberBank, numberBankAccount },
                    { CompanyBankData.AddressBank, adressBank },
                };
                this.MenuAddCompany.SetBankCompanyData(dataCompanyBank);
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("ERROR.buttonAddDriver_Click: " + ex.ToString()); }
        }
    }
}
