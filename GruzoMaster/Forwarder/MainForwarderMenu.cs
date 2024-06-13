using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster.Forwarder
{
    public partial class MainForwarderMenu : Form
    {
        public MainForwarderMenu()
        {
            InitializeComponent();
            LoadFordwarderList();
            listBoxForwarder.SelectedIndexChanged += listBoxForwarder_SelectedIndexChanged;
        }
        private List<User> Forwarders { get; set; } = new List<User>();
        public async void LoadFordwarderList()
        {
            try
            {
                this.Forwarders = await User.GetForwarderList();
                foreach (User user in this.Forwarders)
                {
                    this.listBoxForwarder.Items.Add($"{user.Name} #{user.ID}");
                }
            }
            catch (Exception ex) { MessageBox.Show("LoadFordwarderList: " + ex.ToString()); }
        }
        private void listBoxForwarder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxForwarder.SelectedIndex != -1)
            {
                User selectedUser = Forwarders[listBoxForwarder.SelectedIndex];
                label1.Text = $"ID: {selectedUser.ID}\n" +
                              $"Имя: {selectedUser.Name}\n" +
                              $"Логин: {selectedUser.Login}\n" +
                              $"Тип пользователя: {selectedUser.UserType}";
            }
        }

    }
}
