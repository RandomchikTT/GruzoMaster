using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster.Companies
{
    public partial class CompanyFilterDate : Form
    {
        public DateTime SelectedDate { get; private set; }
        public MainMenuCompany MainMenuCompany { get; private set; }
        public CompanyFilterDate(MainMenuCompany mainMenuCompany)
        {
            this.MainMenuCompany = mainMenuCompany;
            InitializeComponent();
            var label = new Label { Text = "Выберите дату:", AutoSize = true, Top = 20, Left = 20 };
            var datePicker = new DateTimePicker { Name = "datePicker", Format = DateTimePickerFormat.Short, Top = 50, Left = 20, Width = 200 };
            var button = new Button { Text = "Сформировать", Top = 90, Left = 20, Width = 200 };

            button.Click += (sender, e) =>
            {
                SelectedDate = datePicker.Value.Date;
                DialogResult = DialogResult.OK;
                mainMenuCompany.FilterCargosToExcelWithCharts(SelectedDate);
                Close();
            };

            Controls.Add(label);
            Controls.Add(datePicker);
            Controls.Add(button);

            this.Text = "Выбор даты";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(250, 140);
        }
    }
}
