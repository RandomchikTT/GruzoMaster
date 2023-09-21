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
    public partial class MenuChangeDataDriver : Form
    {
        private DriverInfo DriverInfo = null;
        public MenuChangeDataDriver(DriverInfo driverInfo)
        {
            this.DriverInfo = driverInfo;
            InitializeComponent();
        }
    }
}
