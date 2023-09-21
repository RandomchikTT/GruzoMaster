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
        private MenuChangeDataDriver MenuChangeDriver = null;
        public AddDriverContacts(MenuAddDriver menuAddDriver = null, MenuChangeDataDriver menuChangeDataDriver = null, Dictionary<PhoneNumber, String> driverNumbers = null)
        {
            this.MenuAddDriver = menuAddDriver;
            this.MenuChangeDriver = menuChangeDataDriver;
            InitializeComponent();
            if (this.MenuChangeDriver != null && driverNumbers != null)
            {
                this.textBox2.Text = driverNumbers.ContainsKey(PhoneNumber.Bellarusian) ? driverNumbers[PhoneNumber.Bellarusian] : "";
                this.textBox1.Text = driverNumbers.ContainsKey(PhoneNumber.Russian) ? driverNumbers[PhoneNumber.Russian] : "";
                this.textBox3.Text = driverNumbers.ContainsKey(PhoneNumber.Litva) ? driverNumbers[PhoneNumber.Litva] : "";
                this.buttonAddDriver.Text = "Изменить";
            }
        }

        private void buttonAddDriver_Click(object sender, EventArgs e)
        {
            try
            {
                #region Если администратор добавляет водителя 
                if (this.MenuAddDriver != null)
                {
                    Dictionary<PhoneNumber, String> phoneNumbers = this.GetPhoneNumberDictionary();
                    if (phoneNumbers != null)
                    {
                        this.MenuAddDriver.AddContactDriver(phoneNumbers);
                        this.Close();
                    }
                }
                #endregion
                #region Если администратор меняет контакты водителя
                if (this.MenuChangeDriver != null)
                {
                    Dictionary<PhoneNumber, String> phoneNumbers = this.GetPhoneNumberDictionary();
                    if (phoneNumbers != null)
                    {
                        this.MenuChangeDriver.AddDriverContact(phoneNumbers);
                        this.Close();
                    }
                }
                #endregion
            }
            catch (Exception ex) { MessageBox.Show("buttonAddDriver_Click: " + ex.ToString()); }
        }
        private Dictionary<PhoneNumber, String> GetPhoneNumberDictionary()
        {
            if (this.textBox1.Text == "" && this.textBox2.Text == "" && this.textBox3.Text == "")
            {
                MessageBox.Show("Вы не ввели не одного номера !");
                return null;
            }
            Dictionary<PhoneNumber, String> phoneNumbers = new Dictionary<PhoneNumber, String>();
            if (this.textBox1.Text.Length >= 7)
            {
                if (this.textBox1.Text.Length > 12)
                {
                    MessageBox.Show("Вы указали слишком много символов, проверьте еще раз российский номер !");
                    return null;
                }
                phoneNumbers.Add(PhoneNumber.Russian, this.textBox1.Text);
            }
            if (this.textBox2.Text.Length >= 7)
            {
                if (this.textBox2.Text.Length > 13)
                {
                    MessageBox.Show("Вы указали слишком много символов, проверьте еще раз белорусский номер !");
                    return null;
                }
                phoneNumbers.Add(PhoneNumber.Bellarusian, this.textBox2.Text);
            }
            if (this.textBox3.Text.Length >= 7)
            {
                if (this.textBox3.Text.Length > 12)
                {
                    MessageBox.Show("Вы указали слишком много символов, проверьте еще раз литовский номер !");
                    return null;
                }
                phoneNumbers.Add(PhoneNumber.Litva, this.textBox3.Text);
            }
            return phoneNumbers;
        }
    }
}
