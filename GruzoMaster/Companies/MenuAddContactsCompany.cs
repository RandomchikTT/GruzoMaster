using GruzoMaster.Objects;
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
    public partial class MenuAddContactsCompany : Form
    {
        private MenuAddCompany MenuAddCompany = null;
        private MenuEditDataCompany MenuEditDataCompany = null;
        public MenuAddContactsCompany(MenuAddCompany menuAddCompany = null, MenuEditDataCompany menuEditDataCompany = null, Dictionary<PhoneNumber, String> phoneNumbers = null)
        {
            this.MenuEditDataCompany = menuEditDataCompany;
            this.MenuAddCompany = menuAddCompany;
            InitializeComponent();
            if (this.MenuEditDataCompany != null && phoneNumbers != null)
            {
                this.textBox2.Text = phoneNumbers.ContainsKey(PhoneNumber.Bellarusian) ? phoneNumbers[PhoneNumber.Bellarusian] : "";
                this.textBox1.Text = phoneNumbers.ContainsKey(PhoneNumber.Russian) ? phoneNumbers[PhoneNumber.Russian] : "";
                this.textBox3.Text = phoneNumbers.ContainsKey(PhoneNumber.Litva) ? phoneNumbers[PhoneNumber.Litva] : "";
                this.buttonAddDriver.Text = "Изменить";
            }
        }

        private void buttonAddDriver_Click(object sender, EventArgs e)
        {
            try
            {
                #region Если администратор добавляет компанию 
                if (this.MenuAddCompany != null)
                {
                    Dictionary<PhoneNumber, String> phoneNumbers = this.GetPhoneNumberDictionary();
                    if (phoneNumbers != null)
                    {
                        this.MenuAddCompany.AddContactCompany(phoneNumbers);
                        this.Close();
                    }
                }
                #endregion
                #region Если администратор изменяет данные о компании
                if (this.MenuEditDataCompany != null)
                {
                    Dictionary<PhoneNumber, String> phoneNumbers = this.GetPhoneNumberDictionary();
                    if (phoneNumbers != null)
                    {
                        this.MenuEditDataCompany.AddContactCompany(phoneNumbers);
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
