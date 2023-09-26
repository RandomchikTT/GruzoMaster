using GruzoMaster.Objects;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster.Companies
{
    public partial class MainMenuCompany : Form
    {
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
                DataTable dataTable = await MySQL.QueryRead($"SELECT `name` FROM `transport`");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    this.Компании.Items.Clear();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        this.Компании.Items.Add(Convert.ToString(row["Brand"]));
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
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `transport`");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    if (this.Компании.SelectedIndex < 0 || this.Компании.SelectedIndex > dataTable.Rows.Count)
                    {
                        MessageBox.Show("Данная компания не была найдена в базе данных !");
                        this.LoadMainMenuCompanyDataBase();
                        return null;
                    }
                    DataRow selectedCompany = dataTable.Rows[this.Компании.SelectedIndex];
                    return $"Инофрмация о компании:" +
                        $"\nНазвание: {Convert.ToString(selectedCompany["Name"])}." +
                        $"\nСтрана: {Company.GetCountryRussianName((Company.CompanyCountry)Convert.ToInt32(selectedCompany["Country"]))}." +
                        $"\nГород: {Convert.ToString(selectedCompany["City"])}." +
                        $"\nВремя добавления в базу данных: {Convert.ToString(selectedCompany["TimeAdded"])}.";
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

        private void редактироватьКомпаниюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
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
            }
            catch (Exception ex) { MessageBox.Show("редактироватьКомпаниюToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private void удалитьКомпаниюToolStripMenuItem_Click(object sender, EventArgs e)
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
            }
            catch (Exception ex) { MessageBox.Show("удалитьКомпаниюToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private void добавитьКомпаниюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanAppendCompany))
                {
                    MessageBox.Show("У вас нету доступа к этому меню !");
                    return;
                }
            }
            catch (Exception ex) { MessageBox.Show("добавитьКомпаниюToolStripMenuItem_Click: " + ex.ToString()); }
        }
    }
}
