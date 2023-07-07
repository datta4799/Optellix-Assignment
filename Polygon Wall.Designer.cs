namespace Optellix_Assignment
{
    partial class Polygon_Wall_Form
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
            this.WallsListBox = new System.Windows.Forms.ListBox();
            this.CreateWallBtn = new System.Windows.Forms.Button();
            this.checkBoxSW = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // WallsListBox
            // 
            this.WallsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WallsListBox.FormattingEnabled = true;
            this.WallsListBox.HorizontalScrollbar = true;
            this.WallsListBox.ItemHeight = 25;
            this.WallsListBox.Location = new System.Drawing.Point(12, 12);
            this.WallsListBox.Name = "WallsListBox";
            this.WallsListBox.ScrollAlwaysVisible = true;
            this.WallsListBox.Size = new System.Drawing.Size(916, 379);
            this.WallsListBox.TabIndex = 0;
            // 
            // CreateWallBtn
            // 
            this.CreateWallBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CreateWallBtn.Location = new System.Drawing.Point(397, 425);
            this.CreateWallBtn.Name = "CreateWallBtn";
            this.CreateWallBtn.Size = new System.Drawing.Size(151, 47);
            this.CreateWallBtn.TabIndex = 1;
            this.CreateWallBtn.Text = "Create Wall";
            this.CreateWallBtn.UseVisualStyleBackColor = true;
            this.CreateWallBtn.Click += new System.EventHandler(this.CreateWallBtn_Click);
            // 
            // checkBoxSW
            // 
            this.checkBoxSW.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.checkBoxSW.AutoSize = true;
            this.checkBoxSW.Location = new System.Drawing.Point(41, 435);
            this.checkBoxSW.Name = "checkBoxSW";
            this.checkBoxSW.Size = new System.Drawing.Size(184, 29);
            this.checkBoxSW.TabIndex = 2;
            this.checkBoxSW.Text = "Structural Wall";
            this.checkBoxSW.UseVisualStyleBackColor = true;
            // 
            // Polygon_Wall_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 496);
            this.Controls.Add(this.checkBoxSW);
            this.Controls.Add(this.CreateWallBtn);
            this.Controls.Add(this.WallsListBox);
            this.Name = "Polygon_Wall_Form";
            this.Text = "Polygon_Wall_Form";
            this.Load += new System.EventHandler(this.Polygon_Wall_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox WallsListBox;
        private System.Windows.Forms.Button CreateWallBtn;
        private System.Windows.Forms.CheckBox checkBoxSW;
    }
}