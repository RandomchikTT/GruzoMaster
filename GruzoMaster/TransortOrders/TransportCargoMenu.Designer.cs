namespace GruzoMaster.TransortOrders
{
    partial class TransportCargoMenu
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Транспорт = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.фильтрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поДнюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетПоОбщейМассеЗаПериодToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поОбщейМассеЗаПериодToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.всехЗаказовЗаПериодToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(208, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Информация о грузах транспорта";
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            this.dataGridView1.Location = new System.Drawing.Point(201, 53);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(826, 462);
            this.dataGridView1.TabIndex = 5;
            // 
            // Транспорт
            // 
            this.Транспорт.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            this.Транспорт.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Транспорт.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.Транспорт.ForeColor = System.Drawing.Color.White;
            this.Транспорт.FormattingEnabled = true;
            this.Транспорт.ItemHeight = 20;
            this.Транспорт.Location = new System.Drawing.Point(9, 41);
            this.Транспорт.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Транспорт.Name = "Транспорт";
            this.Транспорт.Size = new System.Drawing.Size(169, 460);
            this.Транспорт.TabIndex = 6;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1046, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.фильтрыToolStripMenuItem,
            this.отчетПоОбщейМассеЗаПериодToolStripMenuItem});
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // фильтрыToolStripMenuItem
            // 
            this.фильтрыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.поДнюToolStripMenuItem});
            this.фильтрыToolStripMenuItem.Name = "фильтрыToolStripMenuItem";
            this.фильтрыToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.фильтрыToolStripMenuItem.Text = "Фильтры";
            // 
            // поДнюToolStripMenuItem
            // 
            this.поДнюToolStripMenuItem.Name = "поДнюToolStripMenuItem";
            this.поДнюToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.поДнюToolStripMenuItem.Text = "По дню";
            this.поДнюToolStripMenuItem.Click += new System.EventHandler(this.поДнюToolStripMenuItem_Click);
            // 
            // отчетПоОбщейМассеЗаПериодToolStripMenuItem
            // 
            this.отчетПоОбщейМассеЗаПериодToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.поОбщейМассеЗаПериодToolStripMenuItem,
            this.всехЗаказовЗаПериодToolStripMenuItem});
            this.отчетПоОбщейМассеЗаПериодToolStripMenuItem.Name = "отчетПоОбщейМассеЗаПериодToolStripMenuItem";
            this.отчетПоОбщейМассеЗаПериодToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.отчетПоОбщейМассеЗаПериодToolStripMenuItem.Text = "Отчеты";
            // 
            // поОбщейМассеЗаПериодToolStripMenuItem
            // 
            this.поОбщейМассеЗаПериодToolStripMenuItem.Name = "поОбщейМассеЗаПериодToolStripMenuItem";
            this.поОбщейМассеЗаПериодToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.поОбщейМассеЗаПериодToolStripMenuItem.Text = "По общей массе за период";
            this.поОбщейМассеЗаПериодToolStripMenuItem.Click += new System.EventHandler(this.поОбщейМассеЗаПериодToolStripMenuItem_Click);
            // 
            // всехЗаказовЗаПериодToolStripMenuItem
            // 
            this.всехЗаказовЗаПериодToolStripMenuItem.Name = "всехЗаказовЗаПериодToolStripMenuItem";
            this.всехЗаказовЗаПериодToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.всехЗаказовЗаПериодToolStripMenuItem.Text = "Всех заказов за период";
            this.всехЗаказовЗаПериодToolStripMenuItem.Click += new System.EventHandler(this.всехЗаказовЗаПериодToolStripMenuItem_Click);
            // 
            // TransportCargoMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            this.ClientSize = new System.Drawing.Size(1046, 525);
            this.Controls.Add(this.Транспорт);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "TransportCargoMenu";
            this.Text = "TransportCargoMenu";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ListBox Транспорт;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem фильтрыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поДнюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетПоОбщейМассеЗаПериодToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поОбщейМассеЗаПериодToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem всехЗаказовЗаПериодToolStripMenuItem;
    }
}