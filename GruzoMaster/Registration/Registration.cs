using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster.Registration
{
    public partial class Registration : Form
    {
        public Boolean IsWaitResult { get; set; } = false;
        public Registration()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AuthorizationMenu authorizationMenu = new AuthorizationMenu();
            authorizationMenu.Show();
            this.Hide();
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

        private void textBox3Click(object sender, EventArgs e)
        {
            if (this.textBox3.Text == "Имя")
            {
                this.textBox3.Text = "Имя";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.textBox2.PasswordChar = '*';
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            this.textBox4.PasswordChar = '*';
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
                String login = this.textBox1.Text, password = this.textBox2.Text, name = this.textBox3.Text;
                if (login == null || password == null || name == null || login == "Логин" || password == "Пароль" || name == "Имя")
                {
                    MessageBox.Show("Введите логин/пароль !");
                    return;
                }
                if (login.Length < 3 || password.Length < 3)
                {
                    MessageBox.Show("Логин/пароль должен быть не менее 3-х символов !");
                    return;
                }
                if (name.Length < 2)
                {
                    MessageBox.Show("Имя должно быть не менее 2-х символов !");
                    return;
                }
                if (password != this.textBox4.Text)
                {
                    MessageBox.Show("Пароли должны совпадать !");
                    return;
                }
                DataTable result = await MySQL.QueryRead($"SELECT * FROM `users` WHERE `Login`='{login}'");
                if (result != null && result.Rows.Count > 0)
                {
                    MessageBox.Show("Такой пользователь уже существует !");
                    return;
                }
                await MySQL.QueryAsync($"INSERT INTO `users` (`Login`,`Password`,`UserType`,`Name`) VALUES ('{login}','{OtherFunctions.GetSha256(password)}',0,'{name}')");
                MessageBox.Show("Вы успешно зарегистрировались !");
                this.Hide();
                AuthorizationMenu authorizationMenu = new AuthorizationMenu();
                authorizationMenu.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR.button1_Click: " + ex.ToString());
            }
        }
    }
}
