using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster.TransortOrders
{
    public partial class TransportOrderFilterByDate : Form
    {
        private TransportCargoMenu TransportCargoMenu { get; set; } = null;
        public TransportOrderFilterByDate(TransportCargoMenu transportCargoMenu)
        {
            this.TransportCargoMenu = transportCargoMenu;
            InitializeComponent();
        }

        private void buttonAddCargo_Click(object sender, EventArgs e)
        {
            this.TransportCargoMenu.FilterCargoByDate(this.guna2DateTimePicker1.Value, this.guna2DateTimePicker2.Value);
            this.Close();
        }
    }
}
