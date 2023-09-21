using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster
{
    public partial class MenuAddDriver : Form
    {
        private Boolean IsAwaitResult = false;
        private MenuDrivers MenuDrivers = null;
        private AddDriverContacts AddDriverContacts = null;
        private Dictionary<PhoneNumber, String> PhoneNumbersDriver = new Dictionary<PhoneNumber, String>();
        public MenuAddDriver(MenuDrivers menuDrivers)
        {
            this.MenuDrivers = menuDrivers;
            InitializeComponent();
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
                    MessageBox.Show("Введите ФИО !");
                    return;
                }
                if (this.textBox1.Text.Split(' ').Count() < 3)
                {
                    MessageBox.Show("Надо указать ФИО через пробелы !");
                    return;
                }
                if (DateTime.Now.Subtract(this.dateTimePicker1.Value).Days < 365 * 18)
                {
                    MessageBox.Show("Водителю не может быть младше 18 лет !");
                    return;
                }
                if (this.textBox2.Text.Length != 9)
                {
                    MessageBox.Show("Номер пасспорта должен иметь 9 символов !");
                    return;
                }
                if (this.textBox3.Text.Length != 14)
                {
                    MessageBox.Show("Идентификационный номер пасспорта должен быть 14 символов !");
                    return;
                }
                if (this.dateTimePicker1.Value.ToString("d") == "01.01.1900")
                {
                    MessageBox.Show("Вы не указали дату рождения !");
                    return;
                }
                if (this.dateTimePicker2.Value.ToString("d") == "01.01.1900")
                {
                    MessageBox.Show("Вы не указали дату окончания медицинской справки !");
                    return;
                }
                if (PhoneNumbersDriver.Count <= 0)
                {
                    MessageBox.Show("Вы не указали контакты водителя !");
                    return;
                }
                if (this.textBox4.Text.Length < 5)
                {
                    MessageBox.Show("Вы не указали адрес водителя !");
                    return;
                }
                this.IsAwaitResult = true;
                List<CheckBox> listCheckBoxes = new List<CheckBox>() 
                { 
                    this.checkBox1,
                    this.checkBox2,
                    this.checkBox3,
                    this.checkBox4,
                    this.checkBox5,
                };
                List<License> licnseHave = new List<License>();
                foreach (CheckBox checkBox in listCheckBoxes)
                {
                    if (checkBox.Checked)
                    {
                        if (!Enum.TryParse(checkBox.Text, out License lic)) continue;
                        licnseHave.Add(lic);
                    }
                }
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `drivers` WHERE `FullName`='{this.textBox1.Text}' OR `SerialPassport`='{this.textBox2.Text}' OR `NumberPassport`='{this.textBox3.Text}'");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    MessageBox.Show("Водитель с похожими данными уже находится в базе данных !");
                    this.IsAwaitResult = false;
                    return;
                }
                DialogResult result = MessageBox.Show("Вы уверены что хотите добавить нового водителя ?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    await MySQL.QueryAsync($"INSERT INTO `drivers` (`FullName`,`DateBirthday`,`MedSpravka`,`ListLicenses`,`SerialPassport`,`NumberPassport`,`PhoneNumbers`,`Address`) " +
                    $"VALUES ('{this.textBox1.Text}','{this.dateTimePicker1.Value.ToString("G")}','{this.dateTimePicker2.Value.ToString("G")}'," +
                    $"'{JsonConvert.SerializeObject(licnseHave)}','{this.textBox2.Text}','{this.textBox3.Text}','{JsonConvert.SerializeObject(this.PhoneNumbersDriver)}','{this.textBox4.Text}')");
                    MySQL.AddUserLog(User.LoggedUser.Login, $"Добавил водителя в базу данных: {this.textBox1.Text}.");
                    MessageBox.Show("Вы успешно добавили водителя в базу данных !");
                    this.MenuDrivers.LoadMenu();
                }
                this.IsAwaitResult = false;
            }
            catch (Exception ex) { MessageBox.Show("buttonAddDriver_Click: " + ex.ToString()); this.IsAwaitResult = false; }
        }
        public void AddContactDriver(Dictionary<PhoneNumber, String> phoneNumbers)
        {
            this.PhoneNumbersDriver = phoneNumbers;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.AddDriverContacts != null)
            {
                MessageBox.Show("У вас уже есть открытое меню ввода контактов !");
                return;
            }
            this.AddDriverContacts = new AddDriverContacts(this);
            this.AddDriverContacts.FormClosed += AddDriverContacts_FormClosed;
            this.AddDriverContacts.Show();
        }

        private void AddDriverContacts_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.AddDriverContacts = null;
        }
    }
}
