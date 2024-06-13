using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster.CargoMenu
{
    public partial class FilterCargoMenu : Form
    {
        private MainCargoMenu MainCargoMenu { get; set; } = null;
        private List<User> Forwarders { get; set; } = new List<User>();
        public FilterCargoMenu(MainCargoMenu mainCargoMenu)
        {
            InitializeComponent();
            this.MainCargoMenu = mainCargoMenu;
            LoadForwarders();
            listBox1.MouseClick += listBox1_MouseClick;
        }
        public async void LoadForwarders()
        {
            try
            {
                this.Forwarders = await User.GetForwarderList();
                foreach (User user in this.Forwarders)
                {
                    this.listBox1.Items.Add($"{user.Name} #{user.ID}");
                }
            }
            catch (Exception ex) { MessageBox.Show("LoadForwarders: " + ex.ToString()); }
        }
        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Int32 index = listBox1.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                User selectedUser = Forwarders[index];
                this.MainCargoMenu.FilteredByForwarders(selectedUser);
                this.Close();
            }
        }

    }
}
