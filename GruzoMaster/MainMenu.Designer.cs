using System;

namespace GruzoMaster
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonTableDrivers = new System.Windows.Forms.Button();
            this.buttonTableOrders = new System.Windows.Forms.Button();
            this.buttonListOfCompany = new System.Windows.Forms.Button();
            this.buttonListOfAuto = new System.Windows.Forms.Button();
            this.labelCurrentDate = new System.Windows.Forms.Label();
            this.buttonListOfExpeditors = new System.Windows.Forms.Button();
            this.labelCurrentUser = new System.Windows.Forms.Label();
            this.buttonLastAction = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(98, 24);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // buttonTableDrivers
            // 
            this.buttonTableDrivers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(159)))), ((int)(((byte)(0)))));
            this.buttonTableDrivers.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonTableDrivers.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.buttonTableDrivers.ForeColor = System.Drawing.Color.White;
            this.buttonTableDrivers.Location = new System.Drawing.Point(33, 73);
            this.buttonTableDrivers.Name = "buttonTableDrivers";
            this.buttonTableDrivers.Size = new System.Drawing.Size(224, 84);
            this.buttonTableDrivers.TabIndex = 1;
            this.buttonTableDrivers.Text = "Таблица Водителей";
            this.buttonTableDrivers.UseVisualStyleBackColor = false;
            this.buttonTableDrivers.Click += new System.EventHandler(this.buttonTableDrivers_Click);
            // 
            // buttonTableOrders
            // 
            this.buttonTableOrders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(159)))), ((int)(((byte)(0)))));
            this.buttonTableOrders.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonTableOrders.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.buttonTableOrders.ForeColor = System.Drawing.Color.White;
            this.buttonTableOrders.Location = new System.Drawing.Point(280, 73);
            this.buttonTableOrders.Name = "buttonTableOrders";
            this.buttonTableOrders.Size = new System.Drawing.Size(224, 84);
            this.buttonTableOrders.TabIndex = 2;
            this.buttonTableOrders.Text = "Таблица Грузов";
            this.buttonTableOrders.UseVisualStyleBackColor = false;
            this.buttonTableOrders.Click += new System.EventHandler(this.buttonTableOrders_Click);
            // 
            // buttonListOfCompany
            // 
            this.buttonListOfCompany.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(159)))), ((int)(((byte)(0)))));
            this.buttonListOfCompany.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonListOfCompany.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.buttonListOfCompany.ForeColor = System.Drawing.Color.White;
            this.buttonListOfCompany.Location = new System.Drawing.Point(33, 182);
            this.buttonListOfCompany.Name = "buttonListOfCompany";
            this.buttonListOfCompany.Size = new System.Drawing.Size(224, 84);
            this.buttonListOfCompany.TabIndex = 3;
            this.buttonListOfCompany.Text = "Список компаний";
            this.buttonListOfCompany.UseVisualStyleBackColor = false;
            this.buttonListOfCompany.Click += new System.EventHandler(this.buttonListOfCompany_Click);
            // 
            // buttonListOfAuto
            // 
            this.buttonListOfAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(159)))), ((int)(((byte)(0)))));
            this.buttonListOfAuto.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonListOfAuto.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.buttonListOfAuto.ForeColor = System.Drawing.Color.White;
            this.buttonListOfAuto.Location = new System.Drawing.Point(280, 182);
            this.buttonListOfAuto.Name = "buttonListOfAuto";
            this.buttonListOfAuto.Size = new System.Drawing.Size(224, 84);
            this.buttonListOfAuto.TabIndex = 4;
            this.buttonListOfAuto.Text = "Список транспорта";
            this.buttonListOfAuto.UseVisualStyleBackColor = false;
            this.buttonListOfAuto.Click += new System.EventHandler(this.buttonListOfAuto_Click);
            // 
            // labelCurrentDate
            // 
            this.labelCurrentDate.AutoSize = true;
            this.labelCurrentDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelCurrentDate.ForeColor = System.Drawing.Color.White;
            this.labelCurrentDate.Location = new System.Drawing.Point(12, 393);
            this.labelCurrentDate.Name = "labelCurrentDate";
            this.labelCurrentDate.Size = new System.Drawing.Size(178, 23);
            this.labelCurrentDate.TabIndex = 5;
            this.labelCurrentDate.Text = "Сегодня: 20.09.2023";
            // 
            // buttonListOfExpeditors
            // 
            this.buttonListOfExpeditors.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(159)))), ((int)(((byte)(0)))));
            this.buttonListOfExpeditors.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonListOfExpeditors.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.buttonListOfExpeditors.ForeColor = System.Drawing.Color.White;
            this.buttonListOfExpeditors.Location = new System.Drawing.Point(33, 291);
            this.buttonListOfExpeditors.Name = "buttonListOfExpeditors";
            this.buttonListOfExpeditors.Size = new System.Drawing.Size(224, 84);
            this.buttonListOfExpeditors.TabIndex = 6;
            this.buttonListOfExpeditors.Text = "Список экспедиторов";
            this.buttonListOfExpeditors.UseVisualStyleBackColor = false;
            this.buttonListOfExpeditors.Click += new System.EventHandler(this.buttonListOfExpeditors_Click);
            // 
            // labelCurrentUser
            // 
            this.labelCurrentUser.AutoSize = true;
            this.labelCurrentUser.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelCurrentUser.ForeColor = System.Drawing.Color.White;
            this.labelCurrentUser.Location = new System.Drawing.Point(12, 418);
            this.labelCurrentUser.Name = "labelCurrentUser";
            this.labelCurrentUser.Size = new System.Drawing.Size(208, 23);
            this.labelCurrentUser.TabIndex = 7;
            this.labelCurrentUser.Text = "Пользователь: Савелий";
            // 
            // buttonLastAction
            // 
            this.buttonLastAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(159)))), ((int)(((byte)(0)))));
            this.buttonLastAction.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonLastAction.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.buttonLastAction.ForeColor = System.Drawing.Color.White;
            this.buttonLastAction.Location = new System.Drawing.Point(280, 291);
            this.buttonLastAction.Name = "buttonLastAction";
            this.buttonLastAction.Size = new System.Drawing.Size(224, 84);
            this.buttonLastAction.TabIndex = 8;
            this.buttonLastAction.Text = "Последние действия";
            this.buttonLastAction.UseVisualStyleBackColor = false;
            this.buttonLastAction.Click += new System.EventHandler(this.buttonLastAction_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonLastAction);
            this.Controls.Add(this.labelCurrentUser);
            this.Controls.Add(this.buttonListOfExpeditors);
            this.Controls.Add(this.labelCurrentDate);
            this.Controls.Add(this.buttonListOfAuto);
            this.Controls.Add(this.buttonListOfCompany);
            this.Controls.Add(this.buttonTableOrders);
            this.Controls.Add(this.buttonTableDrivers);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainMenu";
            this.Text = "Основное меню";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.Button buttonTableDrivers;
        private System.Windows.Forms.Button buttonTableOrders;
        private System.Windows.Forms.Button buttonListOfCompany;
        private System.Windows.Forms.Button buttonListOfAuto;
        private System.Windows.Forms.Label labelCurrentDate;
        private System.Windows.Forms.Button buttonListOfExpeditors;
        private System.Windows.Forms.Label labelCurrentUser;
        private System.Windows.Forms.Button buttonLastAction;
    }
}