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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Polygon_Wall_Form));
            this.WallsListBox = new System.Windows.Forms.ListBox();
            this.CreateWallBtn = new System.Windows.Forms.Button();
            this.checkBoxSW = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.WallsListBox.Location = new System.Drawing.Point(12, 52);
            this.WallsListBox.Name = "WallsListBox";
            this.WallsListBox.ScrollAlwaysVisible = true;
            this.WallsListBox.Size = new System.Drawing.Size(1024, 379);
            this.WallsListBox.TabIndex = 0;
            // 
            // CreateWallBtn
            // 
            this.CreateWallBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CreateWallBtn.Location = new System.Drawing.Point(449, 454);
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
            this.checkBoxSW.Location = new System.Drawing.Point(95, 460);
            this.checkBoxSW.Name = "checkBoxSW";
            this.checkBoxSW.Size = new System.Drawing.Size(184, 29);
            this.checkBoxSW.TabIndex = 2;
            this.checkBoxSW.Text = "Structural Wall";
            this.checkBoxSW.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(324, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select a Wall from the below List";
            // 
            // Polygon_Wall_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 513);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxSW);
            this.Controls.Add(this.CreateWallBtn);
            this.Controls.Add(this.WallsListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Polygon_Wall_Form";
            this.Text = "Polygon Walls";
            this.Load += new System.EventHandler(this.Polygon_Wall_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox WallsListBox;
        private System.Windows.Forms.Button CreateWallBtn;
        private System.Windows.Forms.CheckBox checkBoxSW;
        private System.Windows.Forms.Label label1;
    }
}