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
    public partial class AddDriverContacts : Form
    {
        private MenuAddDriver MenuAddDriver = null;
        public AddDriverContacts(MenuAddDriver menuAddDriver)
        {
            this.MenuAddDriver = menuAddDriver;
            InitializeComponent();
        }

        private void buttonAddDriver_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox1.Text == "" && this.textBox2.Text == "" && this.textBox3.Text == "")
                {
                    MessageBox.Show("Вы не ввели не одного номера !");
                    return;
                }
                Dictionary<PhoneNumber, String> phoneNumbers = new Dictionary<PhoneNumber, String>();
                if (this.textBox1.Text != "" && this.textBox1.Text.Length >= 7)
                {
                    if (this.textBox1.Text.Length > 12)
                    {
                        MessageBox.Show("Вы указали слишком много символов, проверьте еще раз российский номер !");
                        return;
                    }
                    phoneNumbers.Add(PhoneNumber.Russian, this.textBox1.Text);
                }
                if (this.textBox2.Text != "" && this.textBox2.Text.Length >= 7)
                {
                    if (this.textBox2.Text.Length > 13)
                    {
                        MessageBox.Show("Вы указали слишком много символов, проверьте еще раз белорусский номер !");
                        return;
                    }
                    phoneNumbers.Add(PhoneNumber.Bellarusian, this.textBox2.Text);
                }
                if (this.textBox3.Text != "" && this.textBox3.Text.Length >= 7)
                {
                    if (this.textBox3.Text.Length > 12)
                    {
                        MessageBox.Show("Вы указали слишком много символов, проверьте еще раз литовский номер !");
                        return;
                    }
                    phoneNumbers.Add(PhoneNumber.Litva, this.textBox3.Text);
                }
                this.MenuAddDriver.AddContactDriver(phoneNumbers);
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("buttonAddDriver_Click: " + ex.ToString()); }
        }
    }
}
