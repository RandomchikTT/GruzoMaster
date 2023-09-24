namespace GruzoMaster.TransportMenu
{
    partial class TransportMenu
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
            this.Транспорт = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортДанныхТранспортаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьТранспортВАвтопаркToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьТранспортToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьДанныеОТранспортеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Транспорт
            // 
            this.Транспорт.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            this.Транспорт.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Транспорт.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.Транспорт.ForeColor = System.Drawing.Color.White;
            this.Транспорт.FormattingEnabled = true;
            this.Транспорт.ItemHeight = 20;
            this.Транспорт.Location = new System.Drawing.Point(9, 50);
            this.Транспорт.Margin = new System.Windows.Forms.Padding(2);
            this.Транспорт.Name = "Транспорт";
            this.Транспорт.Size = new System.Drawing.Size(169, 320);
            this.Транспорт.TabIndex = 0;
            this.Транспорт.SelectedIndexChanged += new System.EventHandler(this.Транспорт_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(184, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Информация о транспорте: ";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.экспортДанныхТранспортаToolStripMenuItem,
            this.добавитьТранспортВАвтопаркToolStripMenuItem,
            this.удалитьТранспортToolStripMenuItem,
            this.изменитьДанныеОТранспортеToolStripMenuItem});
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // экспортДанныхТранспортаToolStripMenuItem
            // 
            this.экспортДанныхТранспортаToolStripMenuItem.Name = "экспортДанныхТранспортаToolStripMenuItem";
            this.экспортДанныхТранспортаToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.экспортДанныхТранспортаToolStripMenuItem.Text = "Экспорт данных транспорта";
            this.экспортДанныхТранспортаToolStripMenuItem.Click += new System.EventHandler(this.экспортДанныхТранспортаToolStripMenuItem_Click);
            // 
            // добавитьТранспортВАвтопаркToolStripMenuItem
            // 
            this.добавитьТранспортВАвтопаркToolStripMenuItem.Name = "добавитьТранспортВАвтопаркToolStripMenuItem";
            this.добавитьТранспортВАвтопаркToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.добавитьТранспортВАвтопаркToolStripMenuItem.Text = "Добавить транспорт";
            this.добавитьТранспортВАвтопаркToolStripMenuItem.Click += new System.EventHandler(this.добавитьТранспортВАвтопаркToolStripMenuItem_Click);
            // 
            // удалитьТранспортToolStripMenuItem
            // 
            this.удалитьТранспортToolStripMenuItem.Name = "удалитьТранспортToolStripMenuItem";
            this.удалитьТранспортToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.удалитьТранспортToolStripMenuItem.Text = "Удалить транспорт";
            this.удалитьТранспортToolStripMenuItem.Click += new System.EventHandler(this.удалитьТранспортToolStripMenuItem_Click);
            // 
            // изменитьДанныеОТранспортеToolStripMenuItem
            // 
            this.изменитьДанныеОТранспортеToolStripMenuItem.Name = "изменитьДанныеОТранспортеToolStripMenuItem";
            this.изменитьДанныеОТранспортеToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.изменитьДанныеОТранспортеToolStripMenuItem.Text = "Изменить данные о транспорте";
            this.изменитьДанныеОТранспортеToolStripMenuItem.Click += new System.EventHandler(this.изменитьДанныеОТранспортеToolStripMenuItem_Click);
            // 
            // TransportMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Транспорт);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TransportMenu";
            this.Text = "Автопарк";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Транспорт;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem экспортДанныхТранспортаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьТранспортВАвтопаркToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьТранспортToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьДанныеОТранспортеToolStripMenuItem;
    }
}