using GruzoMaster.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster.Companies
{
    public partial class MainMenuCompany : Form
    {
        private List<Company> CompanyActiveList = new List<Company>();
        private MenuAddCompany MenuAddCompany = null;
        private MenuEditDataCompany MenuEditDataCompany = null;
        public MainMenuCompany()
        {
            InitializeComponent();
            if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanMakeExportDataCompany))
            {
                this.экспортДанныхОКомпанииToolStripMenuItem.Enabled = false;
                this.экспортДанныхОКомпанииToolStripMenuItem.Visible = false;
            }
            if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanDeleteCompany))
            {
                this.удалитьКомпаниюToolStripMenuItem.Enabled = false;
                this.удалитьКомпаниюToolStripMenuItem.Visible = false;
            }
            if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanAppendCompany))
            {
                this.добавитьКомпаниюToolStripMenuItem.Enabled = false;
                this.добавитьКомпаниюToolStripMenuItem.Visible = false;
            }
            this.label1.Text = "";
            this.LoadMainMenuCompanyDataBase();
        }
        public async void LoadMainMenuCompanyDataBase()
        {
            try
            {
                DataTable dataTable = await MySQL.QueryRead($"SELECT `Name`,`id` FROM `companies`");
                this.CompanyActiveList = new List<Company>();
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    this.Компании.Items.Clear();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        this.CompanyActiveList.Add(new Company()
                        {
                            IdKey = Convert.ToInt32(row["id"]),
                        });
                        this.Компании.Items.Add(Convert.ToString(row["Name"]));
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("LoadMainMenuCompanyDataBase: " + ex.ToString()); }
        }

        private async void Компании_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.Компании.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите транспорт !");
                    return;
                }
                String text = await this.GetCompanyText();
                if (text == null) return;
                this.label1.Text = text;
            }
            catch (Exception ex) { MessageBox.Show("Компании_SelectedIndexChanged: " + ex.ToString()); }
        }
        private async Task<String> GetCompanyText()
        {
            try
            {
                Int32 idKey = this.CompanyActiveList[this.Компании.SelectedIndex].IdKey;
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `companies` WHERE `id`={idKey}");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow selectedCompany = dataTable.Rows[0];
                    Dictionary<PhoneNumber, String> numberCalls = JsonConvert.DeserializeObject<Dictionary<PhoneNumber, String>>(selectedCompany["Contacts"].ToString());
                    String numberPhonesText = "";
                    foreach (KeyValuePair<PhoneNumber, String> number in numberCalls)
                    {
                        numberPhonesText += number.Value;
                        if (numberCalls.Last().Value != number.Value)
                        {
                            numberPhonesText += ", ";
                        }
                    }
                    return $"Инофрмация о компании:" +
                        $"\nНазвание: {Convert.ToString(selectedCompany["Name"])}." +
                        $"\nСтрана: {Company.GetCountryRussianName((Company.CompanyCountry)Convert.ToInt32(selectedCompany["Country"]))}." +
                        $"\nГород: {Convert.ToString(selectedCompany["City"])}." +
                        $"\nВремя добавления в базу: {Convert.ToString(selectedCompany["TimeAdded"])}." +
                        $"\nКонтактные телефоны: {numberPhonesText}." +
                        $"\nПочта: {Convert.ToString(selectedCompany["Email"])}.";
                }
                else
                {
                    MessageBox.Show("Данная компания не была найдена в базе !");
                    this.LoadMainMenuCompanyDataBase();
                }
                return null;
            }
            catch (Exception e) { MessageBox.Show("GetCompanyText: " + e.ToString()); return null; }
        }

        private async void экспортДанныхОКомпанииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanMakeExportDataCompany))
                {
                    MessageBox.Show("У вас нету доступа к этому меню !");
                    return;
                }
                if (this.Компании.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите компанию !");
                    return;
                }
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.Title = "Выберите место для сохранения файла";
                DialogResult result = saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    String transportInfo = await this.GetCompanyText();
                    if (transportInfo == null) return;
                    File.WriteAllText(saveFileDialog.FileName, transportInfo);
                    MessageBox.Show("Вы успешно выгрузили данные о компании в файл.");
                }
            }
            catch (Exception ex) { MessageBox.Show("экспортДанныхОКомпанииToolStripMenuItem_Click: " + ex.ToString()); }
        }
        private async void редактироватьКомпаниюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.MenuEditDataCompany != null)
                {
                    MessageBox.Show("У вас уже есть открытое меню !");
                    return;
                }
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanEditCompany))
                {
                    MessageBox.Show("У вас нету доступа к этому меню !");
                    return;
                }
                if (this.Компании.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите компанию !");
                    return;
                }
                Int32 idKey = this.CompanyActiveList[this.Компании.SelectedIndex].IdKey;
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `companies` WHERE `id`={idKey}");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    this.MenuEditDataCompany = new MenuEditDataCompany(this, new Company()
                    {
                        IdKey = idKey,
                        Name = Convert.ToString(dataTable.Rows[0]["Name"]),
                        City = Convert.ToString(dataTable.Rows[0]["City"]),
                        Email = Convert.ToString(dataTable.Rows[0]["Email"]),
                        Country = (Company.CompanyCountry)Convert.ToInt32(dataTable.Rows[0]["Country"]),
                        PhoneNumbers = JsonConvert.DeserializeObject<Dictionary<PhoneNumber, String>>(dataTable.Rows[0]["Contacts"].ToString())
                    });
                    this.MenuEditDataCompany.FormClosed += MenuEditDataCompany_FormClosed;
                    this.MenuEditDataCompany.Show();
                }

            }
            catch (Exception ex) { MessageBox.Show("редактироватьКомпаниюToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private void MenuEditDataCompany_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.MenuEditDataCompany = null;
        }

        private async void удалитьКомпаниюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanDeleteCompany))
                {
                    MessageBox.Show("У вас нету доступа к этому меню !");
                    return;
                }
                if (this.Компании.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите компанию !");
                    return;
                }
                DialogResult result = MessageBox.Show("Вы уверены что хотите удалить компанию ?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Int32 idKey = this.CompanyActiveList[this.Компании.SelectedIndex].IdKey;
                    DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `companies` WHERE `id`={idKey}");
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        await MySQL.QueryAsync($"DELETE FROM `companies` WHERE `id`={idKey}");
                        MySQL.AddUserLog(User.LoggedUser.Login, $"Удалил компанию с базы данных {Convert.ToString(dataTable.Rows[0]["Name"])} #{idKey}.");
                        this.LoadMainMenuCompanyDataBase();
                        MessageBox.Show("Вы успешно удалили компанию с базы данных.");
                    }
                    else
                    {
                        MessageBox.Show("Данный компанию не был найден в базе данных !");
                        this.LoadMainMenuCompanyDataBase();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("удалитьКомпаниюToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private void добавитьКомпаниюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.MenuAddCompany != null)
                {
                    MessageBox.Show("У вас уже есть открытое меню !");
                    return;
                }
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanAppendCompany))
                {
                    MessageBox.Show("У вас нету доступа к этому меню !");
                    return;
                }
                this.MenuAddCompany = new MenuAddCompany(this);
                this.MenuAddCompany.FormClosed += MenuAddCompany_FormClosed;
                this.MenuAddCompany.Show();
            }
            catch (Exception ex) { MessageBox.Show("добавитьКомпаниюToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private void MenuAddCompany_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.MenuAddCompany = null;
        }
    }
}
