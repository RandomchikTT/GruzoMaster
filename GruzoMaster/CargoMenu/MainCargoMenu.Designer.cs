using System.Windows.Forms;

namespace GruzoMaster.CargoMenu
{
    partial class MainCargoMenu
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.фильтрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьЗаказToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выставитьСчетФактуруToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьАктВыполненныхРаботToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.фильтрToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поЭкспедиторамToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.очиститьФильтрToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.документыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заявкуНаПеревозкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.путевойЛистToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.договорНаОказаниеУслугToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(554, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Информация о грузах ";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1443, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.фильтрыToolStripMenuItem,
            this.добавитьЗаказToolStripMenuItem,
            this.выставитьСчетФактуруToolStripMenuItem,
            this.создатьАктВыполненныхРаботToolStripMenuItem,
            this.фильтрToolStripMenuItem,
            this.документыToolStripMenuItem});
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // фильтрыToolStripMenuItem
            // 
            this.фильтрыToolStripMenuItem.Name = "фильтрыToolStripMenuItem";
            this.фильтрыToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.фильтрыToolStripMenuItem.Text = "Фильтры";
            // 
            // добавитьЗаказToolStripMenuItem
            // 
            this.добавитьЗаказToolStripMenuItem.Name = "добавитьЗаказToolStripMenuItem";
            this.добавитьЗаказToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.добавитьЗаказToolStripMenuItem.Text = "Добавить заказ";
            this.добавитьЗаказToolStripMenuItem.Click += new System.EventHandler(this.добавитьЗаказToolStripMenuItem_Click);
            // 
            // выставитьСчетФактуруToolStripMenuItem
            // 
            this.выставитьСчетФактуруToolStripMenuItem.Name = "выставитьСчетФактуруToolStripMenuItem";
            this.выставитьСчетФактуруToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.выставитьСчетФактуруToolStripMenuItem.Text = "Выставить счет фактуру";
            this.выставитьСчетФактуруToolStripMenuItem.Click += new System.EventHandler(this.выставитьСчетФактуруToolStripMenuItem_Click);
            // 
            // создатьАктВыполненныхРаботToolStripMenuItem
            // 
            this.создатьАктВыполненныхРаботToolStripMenuItem.Name = "создатьАктВыполненныхРаботToolStripMenuItem";
            this.создатьАктВыполненныхРаботToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.создатьАктВыполненныхРаботToolStripMenuItem.Text = "Создать акт выполненных работ";
            this.создатьАктВыполненныхРаботToolStripMenuItem.Click += new System.EventHandler(this.создатьАктВыполненныхРаботToolStripMenuItem_Click_1);
            // 
            // фильтрToolStripMenuItem
            // 
            this.фильтрToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.поЭкспедиторамToolStripMenuItem,
            this.очиститьФильтрToolStripMenuItem});
            this.фильтрToolStripMenuItem.Name = "фильтрToolStripMenuItem";
            this.фильтрToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.фильтрToolStripMenuItem.Text = "Фильтр";
            // 
            // поЭкспедиторамToolStripMenuItem
            // 
            this.поЭкспедиторамToolStripMenuItem.Name = "поЭкспедиторамToolStripMenuItem";
            this.поЭкспедиторамToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.поЭкспедиторамToolStripMenuItem.Text = "По экспедиторам";
            this.поЭкспедиторамToolStripMenuItem.Click += new System.EventHandler(this.поЭкспедиторамToolStripMenuItem_Click);
            // 
            // очиститьФильтрToolStripMenuItem
            // 
            this.очиститьФильтрToolStripMenuItem.Name = "очиститьФильтрToolStripMenuItem";
            this.очиститьФильтрToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.очиститьФильтрToolStripMenuItem.Text = "Очистить фильтр";
            this.очиститьФильтрToolStripMenuItem.Click += new System.EventHandler(this.очиститьФильтрToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            this.dataGridView1.Location = new System.Drawing.Point(9, 58);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1425, 501);
            this.dataGridView1.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(543, 574);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(44, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(484, 574);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 19);
            this.button1.TabIndex = 6;
            this.button1.Text = "<";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(601, 573);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(47, 19);
            this.button2.TabIndex = 7;
            this.button2.Text = ">";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // документыToolStripMenuItem
            // 
            this.документыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.заявкуНаПеревозкуToolStripMenuItem,
            this.путевойЛистToolStripMenuItem,
            this.договорНаОказаниеУслугToolStripMenuItem});
            this.документыToolStripMenuItem.Name = "документыToolStripMenuItem";
            this.документыToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.документыToolStripMenuItem.Text = "Документы";
            // 
            // заявкуНаПеревозкуToolStripMenuItem
            // 
            this.заявкуНаПеревозкуToolStripMenuItem.Name = "заявкуНаПеревозкуToolStripMenuItem";
            this.заявкуНаПеревозкуToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.заявкуНаПеревозкуToolStripMenuItem.Text = "Заявку на перевозку";
            this.заявкуНаПеревозкуToolStripMenuItem.Click += new System.EventHandler(this.заявкуНаПеревозкуToolStripMenuItem_Click);
            // 
            // путевойЛистToolStripMenuItem
            // 
            this.путевойЛистToolStripMenuItem.Name = "путевойЛистToolStripMenuItem";
            this.путевойЛистToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.путевойЛистToolStripMenuItem.Text = "Путевой лист";
            this.путевойЛистToolStripMenuItem.Click += new System.EventHandler(this.путевойЛистToolStripMenuItem_Click);
            // 
            // договорНаОказаниеУслугToolStripMenuItem
            // 
            this.договорНаОказаниеУслугToolStripMenuItem.Name = "договорНаОказаниеУслугToolStripMenuItem";
            this.договорНаОказаниеУслугToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.договорНаОказаниеУслугToolStripMenuItem.Text = "Договор на оказание услуг";
            this.договорНаОказаниеУслугToolStripMenuItem.Click += new System.EventHandler(this.договорНаОказаниеУслугToolStripMenuItem_Click);
            // 
            // MainCargoMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            this.ClientSize = new System.Drawing.Size(1443, 609);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainCargoMenu";
            this.Text = "Меню грузов";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem фильтрыToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem добавитьЗаказToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private ToolStripMenuItem выставитьСчетФактуруToolStripMenuItem;
        private ToolStripMenuItem создатьАктВыполненныхРаботToolStripMenuItem;
        private ToolStripMenuItem фильтрToolStripMenuItem;
        private ToolStripMenuItem поЭкспедиторамToolStripMenuItem;
        private ToolStripMenuItem очиститьФильтрToolStripMenuItem;
        private ToolStripMenuItem документыToolStripMenuItem;
        private ToolStripMenuItem заявкуНаПеревозкуToolStripMenuItem;
        private ToolStripMenuItem путевойЛистToolStripMenuItem;
        private ToolStripMenuItem договорНаОказаниеУслугToolStripMenuItem;
    }
}