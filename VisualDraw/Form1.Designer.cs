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
            this.MainCanvas.Location = new System.Drawing.Point(69, 12);
            this.MainCanvas.Name = "MainCanvas";
            this.MainCanvas.Size = new System.Drawing.Size(611, 442);
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
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 466);
            this.Controls.Add(this.radioButton_Circle);
            this.Controls.Add(this.MainCanvas);
            this.Controls.Add(this.radioButton_Line);
            this.Controls.Add(this.radioButton_Cross);
            this.MaximizeBox = false;
            this.Name = "MainScreen";
            this.Text = "VisualDraw";
            this.Load += new System.EventHandler(this.MainScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButton_Cross;
        private System.Windows.Forms.RadioButton radioButton_Line;
        private System.Windows.Forms.Panel MainCanvas;
        private System.Windows.Forms.RadioButton radioButton_Circle;

    }
}

