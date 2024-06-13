using System.Windows.Forms;

namespace GruzoMaster.Forwarder
{
    partial class MainForwarderMenu
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
            this.listBoxForwarder = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBoxForwarder
            // 
            this.listBoxForwarder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            this.listBoxForwarder.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.listBoxForwarder.ForeColor = System.Drawing.Color.White;
            this.listBoxForwarder.FormattingEnabled = true;
            this.listBoxForwarder.ItemHeight = 20;
            this.listBoxForwarder.Location = new System.Drawing.Point(12, 12);
            this.listBoxForwarder.Name = "listBoxForwarder";
            this.listBoxForwarder.Size = new System.Drawing.Size(227, 424);
            this.listBoxForwarder.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.label1.ForeColor = System.Drawing.Color.SeaShell;
            this.label1.Location = new System.Drawing.Point(245, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 32);
            this.label1.TabIndex = 4;
            // 
            // MainForwarderMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(37)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxForwarder);
            this.Name = "MainForwarderMenu";
            this.Text = "Меню экспедиторов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxForwarder;
        private System.Windows.Forms.Label label1;
    }
}