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
        public MainMenu(User userLogged)
        {
            User.LoggedUser = userLogged;
            InitializeComponent();
            this.labelCurrentDate.Text = $"Сегодня: {DateTime.Now.ToString("d")}";
            this.labelCurrentUser.Text = $"Пользователь: {User.LoggedUser.Name}";
        }

        private void buttonTableDrivers_Click(object sender, EventArgs e)
        {
            MenuDrivers menuDrivers = new MenuDrivers();
            menuDrivers.Show();
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
