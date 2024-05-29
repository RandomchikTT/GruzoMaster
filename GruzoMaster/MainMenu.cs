using GruzoMaster.CargoMenu;
using GruzoMaster.Companies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster
{
    public partial class MainMenu : Form
    {
        private MenuDrivers MenuDrivers = null;
        private MainMenuCompany MenuCompany = null;
        private LogMenu.LogMenu LogMenu = null;
        private TransportMenu.TransportMenu TransportMenu = null;
        private MainCargoMenu MainCargoMenu = null;
        public MainMenu(User userLogged)
        {
            try
            {
                User.LoggedUser = userLogged;
                InitializeComponent();
                this.labelCurrentDate.Text = $"Сегодня: {DateTime.Now.ToString("d")}";
                this.labelCurrentUser.Text = $"Пользователь: {User.LoggedUser.Name}";
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanCheckDrivers))
                {
                    this.buttonTableDrivers.Visible = false;
                    this.buttonTableDrivers.Enabled = false;
                }
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanCheckLogs))
                {
                    this.buttonLastAction.Visible = false;
                    this.buttonLastAction.Enabled = false;
                }
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanCheckTransport))
                {
                    this.buttonListOfAuto.Visible = false;
                    this.buttonListOfAuto.Enabled = false;
                }
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanCheckCompanyMenu))
                {
                    this.buttonListOfCompany.Visible = false;
                    this.buttonListOfCompany.Enabled = false;
                }
            }
            catch (Exception ex) { MessageBox.Show("MainMenu: " + ex.ToString()); }
        }

        private void buttonTableDrivers_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanCheckDrivers))
                {
                    MessageBox.Show("У вас нету доступа к этому меню !");
                    return;
                }
                if (this.MenuDrivers != null)
                {
                    MessageBox.Show("У вас уже есть открытое меню водителей !");
                    return;
                }
                this.MenuDrivers = new MenuDrivers();
                this.MenuDrivers.FormClosed += MenuDrivers_FormClosed;
                this.MenuDrivers.Show();
            }
            catch (Exception ex) { MessageBox.Show("buttonTableDrivers_Click: " + ex.ToString()); }
        }

        private void MenuDrivers_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.MenuDrivers = null;
        }

        private void buttonTableOrders_Click(object sender, EventArgs e)
        {
            if (this.MainCargoMenu != null)
            {
                MessageBox.Show("У вас уже есть открытое меню грузов !");
                return;
            }
            if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CheckCargoMenu))
            {
                MessageBox.Show("У вас нету доступа к этому меню !");
                return;
            }
            this.MainCargoMenu = new MainCargoMenu();
            this.MainCargoMenu.FormClosed += MainCargoMenu_FormClosed;
            this.MainCargoMenu.Show();
        }

        private void MainCargoMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.MainCargoMenu = null;
        }

        private void buttonListOfCompany_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.MenuCompany != null)
                {
                    MessageBox.Show("У вас уже есть открытое меню компаний !");
                    return;
                }
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanCheckCompanyMenu))
                {
                    MessageBox.Show("У вас нету доступа к этому меню !");
                    return;
                }
                this.MenuCompany = new MainMenuCompany();
                this.MenuCompany.FormClosed += MenuCompany_FormClosed;
                this.MenuCompany.Show();
            }
            catch (Exception ex) { MessageBox.Show("buttonListOfCompany_Click: " + ex.ToString()); }
        }

        private void MenuCompany_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.MenuCompany = null;
        }

        private void buttonListOfAuto_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.TransportMenu != null)
                {
                    MessageBox.Show("У вас уже есть открытое меню автопарка !");
                    return;
                }
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanCheckTransport))
                {
                    MessageBox.Show("У вас нету доступа к этому меню !");
                    return;
                }
                this.TransportMenu = new TransportMenu.TransportMenu();
                this.TransportMenu.FormClosed += TransportMenu_FormClosed;
                this.TransportMenu.Show();
            }
            catch (Exception ex) { MessageBox.Show("buttonListOfAuto_Click: " + ex.ToString()); }
        }

        private void TransportMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.TransportMenu = null;
        }

        private void buttonListOfExpeditors_Click(object sender, EventArgs e)
        {

        }

        private void buttonLastAction_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.LogMenu != null)
                {
                    MessageBox.Show("У вас уже есть открытое меню просмотра логов !");
                    return;
                }
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanCheckLogs))
                {
                    MessageBox.Show("У вас нету доступа к этому меню !");
                    return;
                }
                this.LogMenu = new LogMenu.LogMenu();
                this.LogMenu.FormClosed += LogMenu_FormClosed;
                this.LogMenu.Show();
            }
            catch (Exception ex) { MessageBox.Show("buttonLastAction_Click: " + ex.ToString()); }
        }

        private void LogMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.LogMenu = null;
        }
    }
}
