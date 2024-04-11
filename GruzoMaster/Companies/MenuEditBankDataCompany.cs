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
    public partial class MenuEditBankDataCompany : Form
    {
        public MenuEditDataCompany MenuEditDataCompany { get; set; } = null;
        public MenuEditBankDataCompany(MenuEditDataCompany menuEditDataCompany, Dictionary<CompanyBankData, String> bankData)
        {
            this.MenuEditDataCompany = menuEditDataCompany;
            InitializeComponent();
            if (bankData.TryGetValue(CompanyBankData.INN, out String inn))
            {
                this.textBox1.Text = inn;
            }
            if (bankData.TryGetValue(CompanyBankData.LTD, out String ynn))
            {
                this.textBox2.Text = ynn;
            }
            if (bankData.TryGetValue(CompanyBankData.NameOfBank, out String nameOfBank))
            {
                this.textBox3.Text = nameOfBank;
            }
            if (bankData.TryGetValue(CompanyBankData.NumberBank, out String numberBank))
            {
                this.textBox4.Text = numberBank;
            }
            if (bankData.TryGetValue(CompanyBankData.AddressBank, out String addressBank))
            {
                this.textBox5.Text = addressBank;
            }
        }

        private void buttonAddDriver_Click(object sender, EventArgs e)
        {
            String INN = this.textBox1.Text,
                    LTD = this.textBox2.Text,
                    nameBank = this.textBox3.Text,
                    numberBankAccount = this.textBox4.Text,
                    adressBank = this.textBox5.Text;
            Dictionary<CompanyBankData, String> dataCompanyBank = new Dictionary<CompanyBankData, String>
                {
                    { CompanyBankData.INN, INN },
                    { CompanyBankData.LTD, LTD },
                    { CompanyBankData.NameOfBank, nameBank },
                    { CompanyBankData.NumberBank, numberBankAccount },
                    { CompanyBankData.AddressBank, adressBank },
                };
            this.MenuEditDataCompany.AddBankDataCompany(dataCompanyBank);
            this.Close();
        }
    }
}
