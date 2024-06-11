using GruzoMaster.Storage.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster
{
    public partial class AuthorizationMenu : Form
    {
        private Boolean IsWaitResult = false;
        public AuthorizationMenu()
        {
            InitializeComponent();
            this.textBox2.KeyDown += TextBox2_KeyDown;
            if (Storage.Storage.StorageInstance.AuthorizationData != null)
            {
                this.textBox1.Text = Storage.Storage.StorageInstance.AuthorizationData.Username;
                this.textBox2.Text = Storage.Storage.StorageInstance.AuthorizationData.Password;
                this.checkBox1.Checked = true;
            }
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Registration.Registration registration = new Registration.Registration();
                registration.Show();
                this.Hide();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.ToString()); }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.IsWaitResult)
                {
                    MessageBox.Show("Ожидайте ответа !");
                    return;
                }
                String login = this.textBox1.Text, password = this.textBox2.Text;
                if (login == null || password == null || login == "Логин" || password == "Пароль")
                {
                    MessageBox.Show("Введите логин/пароль !");
                    return;
                }
                if (login.Length < 3 || password.Length < 3)
                {
                    MessageBox.Show("Логин/пароль должен быть не менее 3-х символов !");
                    return;
                }
                this.IsWaitResult = true;
                login = login.ToLower();
                String passwordUnSha = password;
                password = OtherFunctions.GetSha256(password);
                DataTable result = await MySQL.QueryRead($"SELECT * FROM `users` WHERE `Login`='{login}'");
                if (result == null || result.Rows.Count <= 0)
                {
                    this.IsWaitResult = false;
                    MessageBox.Show("Пользователя с таким логином не существует !");
                    return;
                }
                if (Convert.ToString(result.Rows[0]["Password"]) != password)
                {
                    this.IsWaitResult = false;
                    MessageBox.Show("Вы не верно ввели пароль !");
                    return;
                }
                User user = new User()
                {
                    UserType = (UserType)Convert.ToInt32(result.Rows[0]["UserType"]),
                    Login = Convert.ToString(result.Rows[0]["Login"]),
                    Name = Convert.ToString(result.Rows[0]["Name"]),
                    ID = Convert.ToInt32(result.Rows[0]["id"])
                };
                AuthData authData = Storage.Storage.StorageInstance.AuthorizationData;
                if (this.checkBox1.Checked)
                {
                    if (authData == null || authData.Username != login || authData.Password != passwordUnSha)
                    {
                        Storage.Storage.StorageInstance.AuthorizationData = new AuthData()
                        {
                            Password = passwordUnSha,
                            Username = login,
                        };
                        Storage.Storage.SaveStorageInstance();
                    }
                }
                else
                {
                    if (authData != null)
                    {
                        Storage.Storage.StorageInstance.AuthorizationData = null;
                        Storage.Storage.SaveStorageInstance();
                    }
                }
                this.Hide();
                MainMenu mainMenu = new MainMenu(user);
                mainMenu.FormClosed += MainMenu_FormClosed;
                mainMenu.Show();
            }
            catch (Exception ex) 
            { 
                MessageBox.Show("Ошибка: " + ex.ToString());
                this.IsWaitResult = false;
            }
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void textBox1Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "Логин")
            {
                this.textBox1.Text = "";
            }
        }

        private void textBox2Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text == "Пароль")
            {
                this.textBox2.Text = "";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.textBox2.PasswordChar = '*';
        }
    }
}
