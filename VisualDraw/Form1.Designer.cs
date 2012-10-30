namespace VisualDraw
{
    partial class MainScreen
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
            this.radioButton_Cross = new System.Windows.Forms.RadioButton();
            this.radioButton_Line = new System.Windows.Forms.RadioButton();
            this.MainCanvas = new System.Windows.Forms.Panel();
            this.radioButton_Circle = new System.Windows.Forms.RadioButton();
            this.menuStrip_Main = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButton_Cross
            // 
            this.radioButton_Cross.AutoSize = true;
            this.radioButton_Cross.Location = new System.Drawing.Point(12, 389);
            this.radioButton_Cross.Name = "radioButton_Cross";
            this.radioButton_Cross.Size = new System.Drawing.Size(51, 17);
            this.radioButton_Cross.TabIndex = 0;
            this.radioButton_Cross.TabStop = true;
            this.radioButton_Cross.Text = "Cross";
            this.radioButton_Cross.UseVisualStyleBackColor = true;
            this.radioButton_Cross.CheckedChanged += new System.EventHandler(this.MainScreen_radioButtons_CheckedChanged);
            // 
            // radioButton_Line
            // 
            this.radioButton_Line.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.radioButton_Line.AutoSize = true;
            this.radioButton_Line.Location = new System.Drawing.Point(12, 413);
            this.radioButton_Line.Name = "radioButton_Line";
            this.radioButton_Line.Size = new System.Drawing.Size(45, 17);
            this.radioButton_Line.TabIndex = 1;
            this.radioButton_Line.TabStop = true;
            this.radioButton_Line.Text = "Line";
            this.radioButton_Line.UseVisualStyleBackColor = true;
            this.radioButton_Line.CheckedChanged += new System.EventHandler(this.MainScreen_radioButtons_CheckedChanged);
            // 
            // MainCanvas
            // 
            this.MainCanvas.BackColor = System.Drawing.Color.White;
            this.MainCanvas.Location = new System.Drawing.Point(69, 27);
            this.MainCanvas.Name = "MainCanvas";
            this.MainCanvas.Size = new System.Drawing.Size(611, 427);
            this.MainCanvas.TabIndex = 2;
            this.MainCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainCanvas_MouseDown);
            this.MainCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.MainCanvas_Paint);
            // 
            // radioButton_Circle
            // 
            this.radioButton_Circle.AutoSize = true;
            this.radioButton_Circle.Location = new System.Drawing.Point(12, 437);
            this.radioButton_Circle.Name = "radioButton_Circle";
            this.radioButton_Circle.Size = new System.Drawing.Size(51, 17);
            this.radioButton_Circle.TabIndex = 3;
            this.radioButton_Circle.TabStop = true;
            this.radioButton_Circle.Text = "Circle";
            this.radioButton_Circle.UseVisualStyleBackColor = true;
            this.radioButton_Circle.CheckedChanged += new System.EventHandler(this.MainScreen_radioButtons_CheckedChanged);
            // 
            // menuStrip_Main
            // 
            this.menuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip_Main.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_Main.Name = "menuStrip_Main";
            this.menuStrip_Main.Size = new System.Drawing.Size(692, 24);
            this.menuStrip_Main.TabIndex = 4;
            this.menuStrip_Main.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveToolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem1.Text = "Save as...";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "visualdraw";
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 466);
            this.Controls.Add(this.radioButton_Circle);
            this.Controls.Add(this.MainCanvas);
            this.Controls.Add(this.radioButton_Line);
            this.Controls.Add(this.radioButton_Cross);
            this.Controls.Add(this.menuStrip_Main);
            this.MainMenuStrip = this.menuStrip_Main;
            this.MaximizeBox = false;
            this.Name = "MainScreen";
            this.Text = "VisualDraw";
            this.Load += new System.EventHandler(this.MainScreen_Load);
            this.menuStrip_Main.ResumeLayout(false);
            this.menuStrip_Main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButton_Cross;
        private System.Windows.Forms.RadioButton radioButton_Line;
        private System.Windows.Forms.Panel MainCanvas;
        private System.Windows.Forms.RadioButton radioButton_Circle;
        private System.Windows.Forms.MenuStrip menuStrip_Main;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;

    }
}

