namespace GruzoMaster.Companies
{
    partial class MainMenuCompany
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
            this.Компании = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьКомпаниюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьКомпаниюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактироватьКомпаниюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Компании
            // 
            this.Компании.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            this.Компании.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Компании.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.Компании.ForeColor = System.Drawing.Color.White;
            this.Компании.FormattingEnabled = true;
            this.Компании.ItemHeight = 20;
            this.Компании.Location = new System.Drawing.Point(11, 34);
            this.Компании.Margin = new System.Windows.Forms.Padding(2);
            this.Компании.Name = "Компании";
            this.Компании.Size = new System.Drawing.Size(169, 320);
            this.Компании.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(816, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьКомпаниюToolStripMenuItem,
            this.удалитьКомпаниюToolStripMenuItem,
            this.редактироватьКомпаниюToolStripMenuItem});
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // добавитьКомпаниюToolStripMenuItem
            // 
            this.добавитьКомпаниюToolStripMenuItem.Name = "добавитьКомпаниюToolStripMenuItem";
            this.добавитьКомпаниюToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.добавитьКомпаниюToolStripMenuItem.Text = "Добавить компанию";
            // 
            // удалитьКомпаниюToolStripMenuItem
            // 
            this.удалитьКомпаниюToolStripMenuItem.Name = "удалитьКомпаниюToolStripMenuItem";
            this.удалитьКомпаниюToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.удалитьКомпаниюToolStripMenuItem.Text = "Удалить компанию";
            // 
            // редактироватьКомпаниюToolStripMenuItem
            // 
            this.редактироватьКомпаниюToolStripMenuItem.Name = "редактироватьКомпаниюToolStripMenuItem";
            this.редактироватьКомпаниюToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.редактироватьКомпаниюToolStripMenuItem.Text = "Редактировать компанию";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(185, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Информация о компании: ";
            // 
            // MainMenuCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            this.ClientSize = new System.Drawing.Size(816, 496);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Компании);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainMenuCompany";
            this.Text = "Меню компаний";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Компании;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьКомпаниюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьКомпаниюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактироватьКомпаниюToolStripMenuItem;
        private System.Windows.Forms.Label label1;
    }
}