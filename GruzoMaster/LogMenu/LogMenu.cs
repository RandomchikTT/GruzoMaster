using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster.LogMenu
{
    public partial class LogMenu : Form
    {
        private Int32 CurrentPage = 0;
        private Int32 MaxCountPage = 1;
        /// <summary>
        /// Сколько логов загружается на одной странице
        /// </summary>
        const Int32 CountLogsInPage = 100;
        private DataTable LogTable;
        public LogMenu()
        {
            InitializeComponent();
            this.LoadTableMenu();
        }
        public async void LoadTableMenu()
        {
            try
            {
                this.LogTable = new DataTable();
                this.dataGridView1.DataSource = this.LogTable;
                this.LogTable.Columns.Add("ID", typeof(Int32));
                this.LogTable.Columns.Add("Логин", typeof(String));
                this.LogTable.Columns.Add("Время", typeof(String));
                this.LogTable.Columns.Add("Действие", typeof(String));
                Int32 countRows = await MySQL.QueryCountRowsAsync("SELECT COUNT(*) FROM userlogs;");
                this.MaxCountPage = Convert.ToInt32(countRows / CountLogsInPage);
                DataTable dataTable = await MySQL.QueryRead("SELECT * FROM `userlogs` " +
                       $"ORDER BY `id` DESC LIMIT {CountLogsInPage} OFFSET {this.CurrentPage * CountLogsInPage}");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    for (Int32 i = 0; i < this.LogTable.Columns.Count; i++)
                    {
                        dataTable.Columns[i].ColumnName = this.LogTable.Columns[i].ColumnName;
                    }
                    this.dataGridView1.DataSource = dataTable;
                }
                this.label1.Text = $"{this.CurrentPage + 1}/{this.MaxCountPage + 1}";
            }
            catch (Exception ex) { MessageBox.Show("LoadTableMenu: " + ex.ToString()); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.CurrentPage <= 0)
                {
                    MessageBox.Show("Страницы закончились !");
                    return;
                }
                this.CurrentPage--;
                this.LoadTableMenu();
            }
            catch (Exception ex) { MessageBox.Show("button1_Click: " + ex.ToString()); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.CurrentPage + 1 >= this.MaxCountPage)
                {
                    MessageBox.Show("Страницы закончились !");
                    return;
                }
                this.CurrentPage++;
                this.LoadTableMenu();
            }
            catch (Exception ex) { MessageBox.Show("button2_Click: " + ex.ToString()); }
        }
    }
}
