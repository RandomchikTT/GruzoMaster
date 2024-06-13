using GruzoMaster.CargoMenu;
using GruzoMaster.Companies;
using GruzoMaster.Forwarder;
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
        private Boolean sideBar_Expand { get; set; } = true;
        private MainMenuCompany MenuCompany { get; set; } = null;
        private MainCargoMenu MainCargoMenu { get; set; } = null;
        private LogMenu.LogMenu LogMenu { get; set; } = null;
        private TransportMenu.TransportMenu TransportMenu { get; set; } = null;
        private MainForwarderMenu MainForwarderMenu { get; set; } = null;
        private MenuDrivers MenuDrivers { get; set; } = null;
        public MainMenu(User user)
        {
            User.LoggedUser = user;
            InitializeComponent();
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

        private void gunaPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Close_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Timer_Sidebar_Menu_Tick(object sender, EventArgs e)
        {
            if (sideBar_Expand)
            {
                SideBar.Width -= 10;
                if (SideBar.Width == SideBar.MinimumSize.Width)
                {
                    sideBar_Expand = false;
                    Timer_Sidebar_Menu.Stop();
                }
            }
            else
                {
                    SideBar.Width += 10;
                    if (SideBar.Width == SideBar.MaximumSize.Width)
                    {
                        sideBar_Expand = true;
                        Timer_Sidebar_Menu.Stop();
                    }
                }
        }   
        
        

        private void Menu_Button_Click(object sender, EventArgs e)
        {
            Timer_Sidebar_Menu.Start();
        }

        private void Home_Button_Click(object sender, EventArgs e)
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
            catch (Exception ex) { MessageBox.Show("Home_Button_Click: " + ex.ToString()); }
        }

        private void MenuCompany_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.MenuCompany = null;
        }

        private void Orders_Button_Click(object sender, EventArgs e)
        {
            try
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
                this.MainCargoMenu.FormClosed += MainCargoMenu_FormClosed; ;
                this.MainCargoMenu.Show();
            }
            catch (Exception ex) { MessageBox.Show("Orders_Button_Click: " + ex.ToString()); }
        }

        private void MainCargoMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.MainCargoMenu = null;
        }

        private void Customers_Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UserSettings.GetAccessUser(UserSettings.UserSetting.CanOpenForwarderMenu))
                {
                    MessageBox.Show("У вас нету доступа к этому меню !");
                    return;
                }
                if (this.MainForwarderMenu != null)
                {
                    MessageBox.Show("У вас уже есть открытое меню это !");
                    return;
                }
                this.MainForwarderMenu = new MainForwarderMenu();
                this.MainForwarderMenu.FormClosed += MainForwarderMenu_FormClosed;
                this.MainForwarderMenu.Show();
            }
            catch (Exception ex) { MessageBox.Show("Customers_Button_Click: " + ex.ToString()); }
        }

        private void MainForwarderMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.MainForwarderMenu = null;
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
                this.LogMenu.FormClosed += LogMenu_FormClosed; ;
                this.LogMenu.Show();
            }
            catch (Exception ex) { MessageBox.Show("buttonLastAction_Click: " + ex.ToString()); }
        }

        private void LogMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.LogMenu = null;
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
                this.TransportMenu.FormClosed += TransportMenu_FormClosed; ;
                this.TransportMenu.Show();
            }
            catch (Exception ex) { MessageBox.Show("buttonListOfAuto_Click: " + ex.ToString()); }
        }

        private void TransportMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.TransportMenu = null;
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
                this.MenuDrivers.FormClosed += MenuDrivers_FormClosed; ;
                this.MenuDrivers.Show();
            }
            catch (Exception ex) { MessageBox.Show("buttonTableDrivers_Click: " + ex.ToString()); }
        }

        private void MenuDrivers_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.MenuDrivers = null;
        }
    }
}
