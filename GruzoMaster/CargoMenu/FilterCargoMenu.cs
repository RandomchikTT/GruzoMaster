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
        }
        public async void LoadForwarders()
        {
            try
            {
                List<User> forwarders = await User.GetForwarderList();
                foreach (User user in forwarders)
                {
                    this.listBox1.Items.Add($"{user.Name} #{user.ID}");
                }
            }
            catch (Exception ex) { MessageBox.Show("LoadForwarders: " + ex.ToString()); }
        }
    }
}
