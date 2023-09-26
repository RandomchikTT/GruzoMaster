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
        private List<DriverInfo> DriverInfoList = new List<DriverInfo>();
        private MenuAddDriver MenuAddDriver = null;
        private MenuChangeDataDriver MenuChangeDataDriver = null;
        public MenuDrivers()
        {
            try
            {
                InitializeComponent();
                this.labelInfoDriver.Text = "";
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanAppendDrivers))
                {
                    this.добавитьВодителяToolStripMenuItem.Visible = false;
                    this.добавитьВодителяToolStripMenuItem.Enabled = false;
                }
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanEditDrivers))
                {
                    this.изменитьДанныеВодителяToolStripMenuItem.Visible = false;
                    this.изменитьДанныеВодителяToolStripMenuItem.Enabled = false;
                }
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanEditDrivers))
                {
                    this.изменитьДанныеВодителяToolStripMenuItem.Visible = false;
                    this.изменитьДанныеВодителяToolStripMenuItem.Enabled = false;
                }
                this.LoadMenu();
            }
            catch (Exception ex) { MessageBox.Show("MenuDrivers: " + ex.ToString()); }
        }
        public async void LoadMenu()
        {
            try
            {
                DataTable result = await MySQL.QueryRead("SELECT `FullName`,`id` FROM `drivers`");
                this.DriverInfoList = new List<DriverInfo>();
                if (result != null && result.Rows.Count > 0)
                {
                    this.Водители.Items.Clear();
                    foreach (DataRow row in result.Rows)
                    {
                        this.DriverInfoList.Add(new DriverInfo()
                        {
                            IdKey = Convert.ToInt32(row["id"]),                    
                        });
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
                Int32 idKey = this.DriverInfoList[this.Водители.SelectedIndex].IdKey;
                DataTable selectDriver = await MySQL.QueryRead($"SELECT * FROM `drivers` WHERE `id`={idKey}");
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
                            $"\nНомера телефонов: {(numberPhonesText == "" ? "Не указаны" : numberPhonesText)}." +
                            $"\nАдрес проживания: {Convert.ToString(dataRowCollection["Address"])}.";
                }
                else
                {
                    MessageBox.Show("Такой водитель не был найден в базе данных, обновите меню !");
                    this.LoadMenu();
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
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanAppendDrivers))
                {
                    MessageBox.Show("У вас нету доступа к этому пункту !");
                    return;
                }
                if (User.LoggedUser.UserType != UserType.Admin)
                {
                    MessageBox.Show("У вас нету доступа добавлять водителей !");
                    return;
                }
                if (this.MenuAddDriver != null)
                {
                    MessageBox.Show("У вас уже есть открытое меню добавление водителя !");
                    return;
                }
                this.MenuAddDriver = new MenuAddDriver(this);
                this.MenuAddDriver.FormClosed += MenuAddDriver_FormClosed;
                this.MenuAddDriver.Show();
            }
            catch (Exception ex) { MessageBox.Show("добавитьВодителяToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private void MenuAddDriver_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.MenuAddDriver = null;
        }

        private async void удалитьВодителяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanDeleteDrivers))
                {
                    MessageBox.Show("У вас нету доступа к этому пункту !");
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
                    Int32 idKey = this.DriverInfoList[this.Водители.SelectedIndex].IdKey;
                    DataTable selectDrivers = await MySQL.QueryRead($"SELECT * FROM `drivers` WHERE `id`={idKey}");
                    if (selectDrivers != null && selectDrivers.Rows.Count > 0)
                    {
                        await MySQL.QueryAsync($"DELETE FROM `drivers` WHERE `id`={idKey}");
                        this.LoadMenu();
                        String fullName = Convert.ToString(selectDrivers.Rows[0]["FullName"]);
                        Int32 idkey = Convert.ToInt32(selectDrivers.Rows[0]["id"]);
                        MySQL.AddUserLog(User.LoggedUser.Login, $"Удалил водителя {fullName} #{idkey}.");
                        MessageBox.Show("Вы успешно удалили водителя с базы данных !");
                    }
                    else
                    {
                        MessageBox.Show("Такой водитель не был найден в базе данных, обновите меню !");
                        this.LoadMenu();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Удалить_Водителя_ToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private async void изменитьДанныеВодителяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Водители.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите водителя !");
                    return;
                }
                if (this.MenuChangeDataDriver != null)
                {
                    MessageBox.Show("У вас уже есть открытое меню изменение данных водителя !");
                    return;
                }
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanEditDrivers))
                {
                    MessageBox.Show("У вас нету доступа к этому пункту !");
                    return;
                }
                Int32 idKey = this.DriverInfoList[this.Водители.SelectedIndex].IdKey;
                DataTable selectDriver = await MySQL.QueryRead($"SELECT * FROM `drivers` WHERE `id`={idKey}");
                if (selectDriver != null && selectDriver.Rows.Count > 0)
                {
                    DataRow dataRowCollection = selectDriver.Rows[this.Водители.SelectedIndex];
                    List<License> listLicense = JsonConvert.DeserializeObject<List<License>>(dataRowCollection["ListLicenses"].ToString());
                    Dictionary<PhoneNumber, String> numberCalls = JsonConvert.DeserializeObject<Dictionary<PhoneNumber, String>>(dataRowCollection["PhoneNumbers"].ToString());
                    this.MenuChangeDataDriver = new MenuChangeDataDriver(this, new DriverInfo()
                    {
                        FullName = Convert.ToString(dataRowCollection["FullName"]),
                        BirthDay = Convert.ToDateTime(dataRowCollection["DateBirthday"]),
                        MedSpavka = Convert.ToDateTime(dataRowCollection["MedSpravka"]),
                        ListLicense = listLicense,
                        PhoneNumbers = numberCalls,
                        SerialPassport = Convert.ToString(dataRowCollection["SerialPassport"]),
                        NumberPassport = Convert.ToString(dataRowCollection["NumberPassport"]),
                        Address = Convert.ToString(dataRowCollection["Address"]),
                        IdKey = Convert.ToInt32(dataRowCollection["id"]),
                    });
                    this.MenuChangeDataDriver.FormClosed += MenuChangeDataDriver_FormClosed;
                    this.MenuChangeDataDriver.Show();
                }
                else
                {
                    MessageBox.Show("Такой водитель не был найден в базе данных, обновите меню !");
                    this.LoadMenu();
                }
            }
            catch (Exception ex) { MessageBox.Show("изменитьДанныеВодителяToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private void MenuChangeDataDriver_FormClosed(object sender, EventArgs e)
        {
            this.MenuChangeDataDriver = null;
        }
    }
}
