using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster
{
    public partial class MenuChangeDataDriver : Form
    {
        private Boolean IsAwaitResult = false;
        private DriverInfo DriverInfo = null;
        private AddDriverContacts AddDriverContacts = null;
        private Dictionary<PhoneNumber, String> PhoneNumbersDriver = new Dictionary<PhoneNumber, String>();
        private MenuDrivers MenuDrivers = null;
        public MenuChangeDataDriver(MenuDrivers menuDrivers, DriverInfo driverInfo)
        {
            this.MenuDrivers = menuDrivers;
            this.DriverInfo = driverInfo;
            InitializeComponent();
            this.textBox1.Text = this.DriverInfo.FullName;
            this.dateTimePicker1.Value = this.DriverInfo.BirthDay;
            this.dateTimePicker2.Value = this.DriverInfo.MedSpavka;
            this.textBox2.Text = this.DriverInfo.SerialPassport;
            this.textBox3.Text = this.DriverInfo.NumberPassport;
            this.textBox4.Text = this.DriverInfo.Address;
            List<CheckBox> checkBoxesLicense = new List<CheckBox>()
            {
                this.checkBox1,
                this.checkBox2,
                this.checkBox3,
                this.checkBox4,
                this.checkBox5,
            };
            foreach (CheckBox checkBox in checkBoxesLicense)
            {
                if (this.DriverInfo.ListLicense.FindIndex(_ => _.ToString() == checkBox.Text) != -1)
                {
                    checkBox.Checked = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.AddDriverContacts != null)
                {
                    MessageBox.Show("У вас уже есть открытое меню изменения контактов водителя !");
                    return;
                }
                this.AddDriverContacts = new AddDriverContacts(menuChangeDataDriver: this, driverNumbers: this.DriverInfo.PhoneNumbers);
                this.AddDriverContacts.FormClosed += AddDriverContacts_FormClosed;
                this.AddDriverContacts.Show();
            }
            catch (Exception ex) { MessageBox.Show("button1_Click: " + ex.ToString()); }
        }
        public void AddDriverContact(Dictionary<PhoneNumber, String> phoneNumbers)
        {
            this.PhoneNumbersDriver = phoneNumbers;
        }
        private void AddDriverContacts_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.AddDriverContacts = null;
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
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `drivers` WHERE `id` = {this.DriverInfo.IdKey}");
                if (dataTable == null || dataTable.Rows.Count <= 0)
                {
                    MessageBox.Show("Водитель не был найден в базе данных, обновите меню !");
                    this.IsAwaitResult = false;
                    return;
                }
                DialogResult result = MessageBox.Show("Вы уверены что хотите изменить данные водителя ?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    await MySQL.QueryAsync($"UPDATE `drivers` SET " +
                            $"`FullName` = '{this.textBox1.Text}', " +
                            $"`DateBirthday` = '{this.dateTimePicker1.Value.ToString("G")}', " +
                            $"`MedSpravka` = '{this.dateTimePicker2.Value.ToString("G")}', " +
                            $"`ListLicenses` = '{JsonConvert.SerializeObject(licnseHave)}', " +
                            $"`SerialPassport` = '{this.textBox2.Text}', " +
                            $"`NumberPassport` = '{this.textBox3.Text}', " +
                            $"`PhoneNumbers` = '{JsonConvert.SerializeObject(this.PhoneNumbersDriver)}', " +
                            $"`Address` = '{this.textBox4.Text}' " +
                            $"WHERE `id` = {this.DriverInfo.IdKey}");
                    MySQL.AddUserLog(User.LoggedUser.Login, $"Обновил данные на водителя: {this.textBox1.Text} #{this.DriverInfo.IdKey}.");
                    MessageBox.Show("Вы успешно обновили данные на водителя !");
                    this.MenuDrivers.LoadMenu();
                }
                this.IsAwaitResult = false;
            }
            catch (Exception ex) { MessageBox.Show("buttonAddDriver_Click: " + ex.ToString()); }
        }
    }
}
