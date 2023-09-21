namespace GruzoMaster
{
    partial class MenuDrivers
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
            this.Водители = new System.Windows.Forms.ListBox();
            this.labelInfoDriver = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортДанныхВодителяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьВодителяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьВодителяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьДанныеВодителяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Водители
            // 
            this.Водители.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            this.Водители.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Водители.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.Водители.ForeColor = System.Drawing.Color.White;
            this.Водители.FormattingEnabled = true;
            this.Водители.ItemHeight = 25;
            this.Водители.Location = new System.Drawing.Point(12, 62);
            this.Водители.Name = "Водители";
            this.Водители.Size = new System.Drawing.Size(208, 275);
            this.Водители.TabIndex = 0;
            this.Водители.SelectedIndexChanged += new System.EventHandler(this.Водители_SelectedIndexChanged);
            // 
            // labelInfoDriver
            // 
            this.labelInfoDriver.AutoSize = true;
            this.labelInfoDriver.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.labelInfoDriver.Location = new System.Drawing.Point(226, 62);
            this.labelInfoDriver.Name = "labelInfoDriver";
            this.labelInfoDriver.Size = new System.Drawing.Size(276, 25);
            this.labelInfoDriver.TabIndex = 1;
            this.labelInfoDriver.Text = "Информация по водителю: ";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.экспортДанныхВодителяToolStripMenuItem,
            this.добавитьВодителяToolStripMenuItem,
            this.удалитьВодителяToolStripMenuItem,
            this.изменитьДанныеВодителяToolStripMenuItem});
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(98, 24);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // экспортДанныхВодителяToolStripMenuItem
            // 
            this.экспортДанныхВодителяToolStripMenuItem.Name = "экспортДанныхВодителяToolStripMenuItem";
            this.экспортДанныхВодителяToolStripMenuItem.Size = new System.Drawing.Size(286, 26);
            this.экспортДанныхВодителяToolStripMenuItem.Text = "Экспорт данных водителя";
            this.экспортДанныхВодителяToolStripMenuItem.Click += new System.EventHandler(this.экспортДанныхВодителяToolStripMenuItem_Click);
            // 
            // добавитьВодителяToolStripMenuItem
            // 
            this.добавитьВодителяToolStripMenuItem.Name = "добавитьВодителяToolStripMenuItem";
            this.добавитьВодителяToolStripMenuItem.Size = new System.Drawing.Size(286, 26);
            this.добавитьВодителяToolStripMenuItem.Text = "Добавить водителя";
            this.добавитьВодителяToolStripMenuItem.Click += new System.EventHandler(this.добавитьВодителяToolStripMenuItem_Click);
            // 
            // удалитьВодителяToolStripMenuItem
            // 
            this.удалитьВодителяToolStripMenuItem.Name = "удалитьВодителяToolStripMenuItem";
            this.удалитьВодителяToolStripMenuItem.Size = new System.Drawing.Size(286, 26);
            this.удалитьВодителяToolStripMenuItem.Text = "Удалить водителя";
            this.удалитьВодителяToolStripMenuItem.Click += new System.EventHandler(this.удалитьВодителяToolStripMenuItem_Click);
            // 
            // изменитьДанныеВодителяToolStripMenuItem
            // 
            this.изменитьДанныеВодителяToolStripMenuItem.Name = "изменитьДанныеВодителяToolStripMenuItem";
            this.изменитьДанныеВодителяToolStripMenuItem.Size = new System.Drawing.Size(286, 26);
            this.изменитьДанныеВодителяToolStripMenuItem.Text = "Изменить данные водителя";
            this.изменитьДанныеВодителяToolStripMenuItem.Click += new System.EventHandler(this.изменитьДанныеВодителяToolStripMenuItem_Click);
            // 
            // MenuDrivers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            this.ClientSize = new System.Drawing.Size(800, 482);
            this.Controls.Add(this.labelInfoDriver);
            this.Controls.Add(this.Водители);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.White;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MenuDrivers";
            this.Text = "Меню водителей";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Водители;
        private System.Windows.Forms.Label labelInfoDriver;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem экспортДанныхВодителяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьВодителяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьВодителяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьДанныеВодителяToolStripMenuItem;
    }
}