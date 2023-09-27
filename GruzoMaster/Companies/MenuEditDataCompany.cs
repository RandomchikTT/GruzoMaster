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

namespace GruzoMaster.Companies
{
    public partial class MenuEditDataCompany : Form
    {
        private Company CurrentCompanyEdit = null;
        private Boolean IsAwaitResult = false;
        private MainMenuCompany MainMenuCompany = null;
        private MenuAddContactsCompany MenuAddContactsCompany = null;
        public MenuEditDataCompany(MainMenuCompany mainMenu, Company company)
        {
            this.MainMenuCompany = mainMenu;
            this.CurrentCompanyEdit = company;
            InitializeComponent();
            switch (this.CurrentCompanyEdit.Country)
            {
                case Company.CompanyCountry.Belarus:
                    this.radioButton1.Checked = true;
                    break;
                case Company.CompanyCountry.Russia:
                    this.radioButton3.Checked = true;
                    break;
                case Company.CompanyCountry.Litva:
                    this.radioButton7.Checked = true;
                    break;
            }
            this.textBox1.Text = this.CurrentCompanyEdit.Name;
            this.textBox2.Text = this.CurrentCompanyEdit.City;
            this.textBox3.Text = this.CurrentCompanyEdit.Email;
        }
        public void AddContactCompany(Dictionary<PhoneNumber, String> phoneNumbers)
        {
            this.CurrentCompanyEdit.PhoneNumbers = phoneNumbers;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.MenuAddContactsCompany != null)
            {
                MessageBox.Show("У вас уже есть открытое меню !");
                return;
            }
            this.MenuAddContactsCompany = new MenuAddContactsCompany(menuEditDataCompany: this, phoneNumbers: this.CurrentCompanyEdit.PhoneNumbers);
            this.MenuAddContactsCompany.FormClosed += MenuAddContactsCompany_FormClosed;
            this.MenuAddContactsCompany.Show();
        }

        private void MenuAddContactsCompany_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.MenuAddContactsCompany = null;
        }

        private async void buttonAddDriver_Click(object sender, EventArgs e)
        {
            if (this.IsAwaitResult)
            {
                MessageBox.Show("Вы уже нажали на кнопку, ожидайте ответа !");
                return;
            }
            if (this.textBox1.Text == "" || this.textBox1.Text.Length < 3)
            {
                MessageBox.Show("Введите название компании !");
                return;
            }
            if (this.textBox2.Text == "" || this.textBox2.Text.Length < 3)
            {
                MessageBox.Show("Введите город компании !");
                return;
            }
            if (this.textBox3.Text == "" || this.textBox3.Text.Length < 3)
            {
                MessageBox.Show("Введите почту компании !");
                return;
            }
            if (this.CurrentCompanyEdit.PhoneNumbers.Count <= 0)
            {
                MessageBox.Show("Вы не указали контакты компании !");
                return;
            }
            #region Выбор выбранной страны
            Company.CompanyCountry companyCountry = Company.CompanyCountry.None;
            if (this.radioButton1.Checked)
            {
                companyCountry = Company.CompanyCountry.Belarus;
            }
            else if (this.radioButton3.Checked)
            {
                companyCountry = Company.CompanyCountry.Russia;
            }
            else if (this.radioButton7.Checked)
            {
                companyCountry = Company.CompanyCountry.Litva;
            }
            if (companyCountry == Company.CompanyCountry.None)
            {
                MessageBox.Show("Вы не выбрали страну !");
                return;
            }
            #endregion
            this.IsAwaitResult = true;
            DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `companies` WHERE `id`={this.CurrentCompanyEdit.IdKey}");
            if (dataTable == null || dataTable.Rows.Count <= 0)
            {
                MessageBox.Show("Компания не была найдена в базе !");
                this.MainMenuCompany?.LoadMainMenuCompanyDataBase();
                this.IsAwaitResult = false;
                this.Close();
                return;
            }
            DialogResult result = MessageBox.Show("Вы уверены что хотите изменить данные компании ?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                await MySQL.QueryAsync($"UPDATE `companies` SET " +
                            $"`Name` = '{this.textBox1.Text}', " +
                            $"`Country` = {Convert.ToInt32(companyCountry)}, " +
                            $"`Contacts` = '{JsonConvert.SerializeObject(this.CurrentCompanyEdit.PhoneNumbers)}', " +
                            $"`City` = '{this.textBox2.Text}', " +
                            $"`Email` = '{this.textBox3.Text}' " +
                            $"WHERE `id` = {this.CurrentCompanyEdit.IdKey}");
                MySQL.AddUserLog(User.LoggedUser.Login, $"Изменил данные компании: {this.CurrentCompanyEdit.Name} #{this.CurrentCompanyEdit.IdKey}.");
                MessageBox.Show("Вы успешно изменили данные компании !");
                this.MainMenuCompany?.LoadMainMenuCompanyDataBase();
                this.Close();
            }
            this.IsAwaitResult = false;
        }
    }
}
