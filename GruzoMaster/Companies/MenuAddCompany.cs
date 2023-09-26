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
    public partial class MenuAddCompany : Form
    {
        private Boolean IsAwaitResult = false;
        private MenuAddContactsCompany MenuAddContactsCompany = null;
        private Dictionary<PhoneNumber, String> PhoneNumbers = new Dictionary<PhoneNumber, String>();
        private MainMenuCompany MainMenuCompany = null;
        public MenuAddCompany(MainMenuCompany mainMenuCompany = null)
        {
            this.MainMenuCompany = mainMenuCompany;
            InitializeComponent();
        }
        public void AddContactCompany(Dictionary<PhoneNumber, String> phoneNumbers)
        {
            this.PhoneNumbers = phoneNumbers;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.MenuAddContactsCompany != null)
            {
                MessageBox.Show("У вас уже есть открытое меню добавление контактов !");
                return;
            }
            this.MenuAddContactsCompany = new MenuAddContactsCompany(this);
            this.MenuAddContactsCompany.FormClosed += MenuAddContactsCompany_FormClosed;
            this.MenuAddContactsCompany.Show();
        }

        private void MenuAddContactsCompany_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.MenuAddContactsCompany = null;
        }

        private async void buttonAddDriver_Click(object sender, EventArgs e)
        {
            try
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
                if (this.PhoneNumbers.Count <= 0)
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
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `companies` WHERE `Name`='{this.textBox1.Text}'");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    MessageBox.Show("Компания с похожими данными уже находится в базе данных !");
                    this.IsAwaitResult = false;
                    return;
                }
                DialogResult result = MessageBox.Show("Вы уверены что хотите добавить новую компанию ?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Int64 id = await MySQL.QueryLastInsertAsync($"INSERT INTO `companies` (`Name`,`Country`,`Contacts`,`City`,`TimeAdded`) " +
                    $"VALUES ('{this.textBox1.Text}',{(Int32)companyCountry},'{JsonConvert.SerializeObject(this.PhoneNumbers)}','{this.textBox2.Text}','{DateTime.Now}')");
                    MySQL.AddUserLog(User.LoggedUser.Login, $"Добавил компанию в базу данных: {this.textBox1.Text} #{id}.");
                    MessageBox.Show("Вы успешно добавили компанию в базу данных !");
                    this.MainMenuCompany?.LoadMainMenuCompanyDataBase();
                }
                this.IsAwaitResult = false;
            }
            catch (Exception ex) { MessageBox.Show("buttonAddDriver_Click: " + ex.ToString()); this.IsAwaitResult = false; }
        }
    }
}
