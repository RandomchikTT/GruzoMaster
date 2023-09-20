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
        public MainMenu(User userLogged)
        {
            User.LoggedUser = userLogged;
            InitializeComponent();
            this.labelCurrentDate.Text = $"Сегодня: {DateTime.Now.ToString("d")}";
            this.labelCurrentUser.Text = $"Пользователь: {User.LoggedUser.Name}";
        }

        private void buttonTableDrivers_Click(object sender, EventArgs e)
        {
            if (this.MenuDrivers != null)
            {
                MessageBox.Show("У вас уже есть открытое меню водителей !");
                return;
            }
            this.MenuDrivers = new MenuDrivers();
            this.MenuDrivers.FormClosed += MenuDrivers_FormClosed;
            this.MenuDrivers.Show();
        }

        private void MenuDrivers_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.MenuDrivers = null;
        }

        private void buttonTableOrders_Click(object sender, EventArgs e)
        {

        }

        private void buttonListOfCompany_Click(object sender, EventArgs e)
        {

        }

        private void buttonListOfAuto_Click(object sender, EventArgs e)
        {

        }

        private void buttonListOfExpeditors_Click(object sender, EventArgs e)
        {

        }

        private void buttonLastAction_Click(object sender, EventArgs e)
        {

        }
    }
}
