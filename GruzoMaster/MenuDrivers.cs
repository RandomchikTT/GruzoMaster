using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster
{
    public partial class MenuDrivers : Form
    {
        public MenuDrivers()
        {
            InitializeComponent();
            this.labelInfoDriver.Text = "";
            this.LoadMenu();
        }
        public async void LoadMenu()
        {
            try
            {
                DataTable result = await MySQL.QueryRead("SELECT `FullName` FROM `drivers`");
                if (result != null && result.Rows.Count > 0)
                {
                    this.Водители.Items.Clear();
                    foreach (DataRow row in result.Rows)
                    {
                        String[] fullName = Convert.ToString(row["FullName"]).Split(' ');
                        this.Водители.Items.Add($"{fullName[0]} {fullName[1][0]}. {fullName[2][0]}.");
                    }
                }
            }
            catch (Exception e) { MessageBox.Show("LoadMenu: " + e.ToString()); }
        }
        private async void Водители_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String driverInfo = await this.GetDriverInfo();
                if (driverInfo == null) return;
                this.labelInfoDriver.Text = driverInfo;
            }
            catch (Exception ex) { MessageBox.Show("Водители_SelectedIndexChanged: " + ex.ToString()); }
        }
        private async Task<String> GetDriverInfo()
        {
            try
            {
                if (this.Водители.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите водителя !");
                    return null;
                }
                DataTable selectDriver = await MySQL.QueryRead($"SELECT * FROM `drivers`");
                if (selectDriver != null && selectDriver.Rows.Count > 0)
                {
                    DataRow dataRowCollection = selectDriver.Rows[this.Водители.SelectedIndex];
                    List<License> listLicense = JsonConvert.DeserializeObject<List<License>>(dataRowCollection["ListLicenses"].ToString());
                    String licText = "";
                    foreach (License license in listLicense)
                    {
                        licText += license.ToString();
                        if (listLicense.Last() != license)
                        {
                            licText += ", ";
                        }
                    }
                    Dictionary<PhoneNumber, String> numberCalls = JsonConvert.DeserializeObject<Dictionary<PhoneNumber, String>>(dataRowCollection["PhoneNumbers"].ToString());
                    String numberPhonesText = "";
                    foreach (KeyValuePair<PhoneNumber, String> number in numberCalls)
                    {
                        numberPhonesText += number.Value;
                        if (numberCalls.Last().Value != number.Value)
                        {
                            numberPhonesText += ", ";
                        }
                    }
                    return $"Информация о водителе: " +
                            $"\nФИО: {Convert.ToString(dataRowCollection["FullName"])}" +
                            $"\nМед. Справка до: {Convert.ToDateTime(dataRowCollection["MedSpravka"]).ToString("d")}" +
                            $"\nДата рождения: {Convert.ToDateTime(dataRowCollection["DateBirthday"]).ToString("d")}" +
                            $"\nОткрытые Категории: {(licText == "" ? "Не указаны" : licText)}." +
                            $"\nНомера телефонов: {(numberPhonesText == "" ? "Не указаны" : numberPhonesText)}.";
                }
                return null;
            }
            catch (Exception ex) { MessageBox.Show("GetDriverInfo: " + ex.ToString()); return null; }
        }
        private async void экспортДанныхВодителяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Водители.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите водителя !");
                    return;
                }
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.Title = "Выберите место для сохранения файла";
                DialogResult result = saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    String driverInfo = await this.GetDriverInfo();
                    if (driverInfo == null) return;
                    File.WriteAllText(saveFileDialog.FileName, driverInfo);
                    MessageBox.Show("Вы успешно выгрузили данные по водителю в файл.");
                }
            }
            catch (Exception ex) { MessageBox.Show("экспортДанныхВодителяToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private void добавитьВодителяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (User.LoggedUser.UserType != UserType.Admin)
                {
                    MessageBox.Show("У вас нету доступа добавлять водителей !");
                    return;
                }
                MenuAddDriver menuAddDrive = new MenuAddDriver(this);
                menuAddDrive.Show();
            }
            catch (Exception ex) { MessageBox.Show("добавитьВодителяToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private async void удалитьВодителяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (User.LoggedUser.UserType != UserType.Admin)
                {
                    MessageBox.Show("У вас нету доступа удалять водителей !");
                    return;
                }
                if (this.Водители.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите водителя !");
                    return;
                }
                DialogResult result = MessageBox.Show("Вы уверены что хотите удалить водителя ?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DataTable selectDrivers = await MySQL.QueryRead($"SELECT * FROM `drivers`");
                    if (selectDrivers != null && selectDrivers.Rows.Count > 0)
                    {
                        await MySQL.QueryAsync($"DELETE FROM `drivers` WHERE `idkey`={Convert.ToInt32(selectDrivers.Rows[this.Водители.SelectedIndex]["idkey"])}");
                        this.LoadMenu();
                        MessageBox.Show("Вы удалили водителя с базы данных !");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("удалитьВодителяToolStripMenuItem_Click: " + ex.ToString()); }
        }
    }
}
