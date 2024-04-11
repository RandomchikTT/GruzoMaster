using GruzoMaster.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster.TransportMenu
{
    public partial class TransportMenu : Form
    {
        private TransportAddInParkMenu TransportAddInParkMenu = null;
        private MenuChangeDataTransport MenuChangeDataTransport = null;
        private List<Transport> TransportList = new List<Transport>();
        public TransportMenu()
        {
            try
            {
                InitializeComponent();
                this.label1.Text = "";
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanDeleteTransport))
                {
                    this.удалитьТранспортToolStripMenuItem.Enabled = false;
                    this.удалитьТранспортToolStripMenuItem.Visible = false;
                }
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanAppendTransport))
                {
                    this.добавитьТранспортВАвтопаркToolStripMenuItem.Enabled = false;
                    this.добавитьТранспортВАвтопаркToolStripMenuItem.Visible = false;
                }
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanEditDataTransport))
                {
                    this.изменитьДанныеОТранспортеToolStripMenuItem.Enabled = false;
                    this.изменитьДанныеОТранспортеToolStripMenuItem.Visible = false;
                }
                this.LoadTransportMenu();
            }
            catch (Exception e) { MessageBox.Show("TransportMenu: " + e.ToString()); }
        }
        public async void LoadTransportMenu()
        {
            try
            {
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `transport`");
                this.TransportList = new List<Transport>();
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    this.Транспорт.Items.Clear();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        this.TransportList.Add(new Transport()
                        {
                            IdKey = Convert.ToInt32(row["id"]),
                        });
                        Transport.TransportModel transportModel = (Transport.TransportModel)Convert.ToInt32(row["Brand"]);
                        String model = Convert.ToString(row["Model"]);
                        this.Транспорт.Items.Add($"{transportModel.ToString()} {model}");
                    }
                }
            }
            catch (Exception e) { MessageBox.Show("LoadTransportMenu: " + e.ToString()); }
        }
        private async Task<String> GetTransportText()
        {
            try
            {
                Int32 idKey = this.TransportList[this.Транспорт.SelectedIndex].IdKey;
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `transport` WHERE `id`={idKey}");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow selectedVehicle = dataTable.Rows[0];
                    return $"Инофрмация о транспорте:" +
                        $"\nМарка: {Convert.ToString((Transport.TransportModel)Convert.ToInt32(selectedVehicle["Brand"]))}." +
                        $"\nМодель: {Convert.ToString(selectedVehicle["Model"])}." +
                        $"\nТип транспорта: {Convert.ToString(selectedVehicle["Type"])}." +
                        $"\nГосударственный номер: {Convert.ToString(selectedVehicle["GovNumber"])}." +
                        $"\nТехнический осмотр годен до: {Convert.ToDateTime(selectedVehicle["TechInspection"]).ToString("d")}.";
                }
                else
                {
                    MessageBox.Show("Данный транспорт не был найден в базе данных !");
                    this.LoadTransportMenu();
                }
                return null;
            }
            catch (Exception e) { MessageBox.Show("GetTransportText: " + e.ToString()); return null; }
        }
        private async void Транспорт_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.Транспорт.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите транспорт !");
                    return;
                }
                String text = await this.GetTransportText();
                if (text == null) return;
                this.label1.Text = text;
            }
            catch (Exception ex) { MessageBox.Show("Транспорт_SelectedIndexChanged: " + ex.ToString()); }
        }
        private async void экспортДанныхТранспортаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Транспорт.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите транспорт !");
                    return;
                }
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.Title = "Выберите место для сохранения файла";
                DialogResult result = saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    String transportInfo = await this.GetTransportText();
                    if (transportInfo == null) return;
                    File.WriteAllText(saveFileDialog.FileName, transportInfo);
                    MessageBox.Show("Вы успешно выгрузили данные по транспорту в файл.");
                }
            }
            catch (Exception ex) { MessageBox.Show("экспортДанныхТранспортаToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private async void удалитьТранспортToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanDeleteTransport))
                {
                    MessageBox.Show("У вас нету доступа к этому пункуту !");
                    return;
                }
                if (this.Транспорт.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите транспорт !");
                    return;
                }
                DialogResult result = MessageBox.Show("Вы уверены что хотите удалить транспорт ?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Int32 idKey = this.TransportList[this.Транспорт.SelectedIndex].IdKey;
                    DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `transport` WHERE `id`={idKey}");
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        await MySQL.QueryAsync($"DELETE FROM `transport` WHERE `id`={idKey}");
                        MySQL.AddUserLog(User.LoggedUser.Login, $"Удалил транспорт с базы данных {((Transport.TransportType)Convert.ToInt32(dataTable.Rows[0]["Brand"])).ToString()} #{idKey}.");
                        this.LoadTransportMenu();
                        MessageBox.Show("Вы успешно удалили транспорт с базы данных.");
                    }
                    else
                    {
                        MessageBox.Show("Данный транспорт не был найден в базе данных !");
                        this.LoadTransportMenu();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("удалитьТранспортToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private void добавитьТранспортВАвтопаркToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanAppendTransport))
                {
                    MessageBox.Show("У вас нету доступа к этому пункуту !");
                    return;
                }
                if (this.TransportAddInParkMenu != null)
                {
                    MessageBox.Show("У вас уже есть открытое меню !");
                    return;
                }
                this.TransportAddInParkMenu = new TransportAddInParkMenu(this);
                this.TransportAddInParkMenu.FormClosed += TransportAddInParkMenu_FormClosed;
                this.TransportAddInParkMenu.Show();
            }
            catch (Exception ex) { MessageBox.Show("добавитьТранспортВАвтопаркToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private void TransportAddInParkMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.TransportAddInParkMenu = null;
        }

        private async void изменитьДанныеОТранспортеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanEditDataTransport))
                {
                    MessageBox.Show("У вас нету доступа к этому пункуту !");
                    return;
                }
                if (this.Транспорт.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите транспорт !");
                    return;
                }
                if (this.MenuChangeDataTransport != null)
                {
                    MessageBox.Show("У вас уже есть открытое меню !");
                    return;
                }
                Int32 idKey = this.TransportList[this.Транспорт.SelectedIndex].IdKey;
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `transport` WHERE `id`={idKey}");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    this.MenuChangeDataTransport = new MenuChangeDataTransport(this, new Transport()
                    {
                        IdKey = Convert.ToInt32(dataTable.Rows[0]["id"]),
                        TransportModelName = (Transport.TransportModel)Convert.ToInt32(dataTable.Rows[0]["Brand"]),
                        ModelDescriptionName = Convert.ToString(dataTable.Rows[0]["Model"]),
                        TransportTypeName = (Transport.TransportType)Convert.ToInt32(dataTable.Rows[0]["Type"]),
                        GovNumber = Convert.ToString(dataTable.Rows[0]["GovNumber"]),
                        TimeTechInspection = Convert.ToDateTime(dataTable.Rows[0]["TechInspection"])
                    });
                    this.MenuChangeDataTransport.FormClosed += MenuChangeDataTransport_FormClosed;
                    this.MenuChangeDataTransport.Show();
                }
                else
                {
                    MessageBox.Show("Данный транспорт не был найден в базе данных !");
                    this.LoadTransportMenu();
                }
            }
            catch (Exception ex) { MessageBox.Show("изменитьДанныеОТранспортеToolStripMenuItem_Click: " + ex.ToString()); }
        }

        private void MenuChangeDataTransport_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.MenuChangeDataTransport = null;
        }
    }
}
