using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VisualDraw
{
    public partial class MainScreen : Form
    {
        List<Shape> Shapes = new List<Shape>();
        List<int> SqrDistances = new List<int>();
        bool IsShapeStart = true;
        Point ShapeStart;
        string file_cur;
        Pen pMain   = new Pen(Color.Black);
        Pen pSelect = new Pen(Color.Red,2);
        Pen pTemp   = new Pen(Color.DarkGray);
        Shape TempShape;
        
        public MainScreen()
        {
            InitializeComponent();
        }
        private void MainScreen_radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            IsShapeStart = true;
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            int ind_TEMP = SelectMatching(Shapes,e.Location);
            
            if (ind_TEMP > -1)
                toolTip1.SetToolTip(MainCanvas, Convert.ToString(ShapesList.Items[ind_TEMP]));
            else
                toolTip1.SetToolTip(MainCanvas,"");
            
            if (IsShapeStart) {
                this.Text = Convert.ToString(e.X) + " - " + Convert.ToString(e.Y);
                TempShape = new Cross(e.Location);
            }
            else {
                if (radioButton_Line.Checked)   { TempShape = new   Line(ShapeStart, e.Location); }
                if (radioButton_Circle.Checked) { TempShape = new Circle(ShapeStart, e.Location); }
                if (radioButton_Rect.Checked)   { TempShape = new   Rect(ShapeStart, e.Location); }
            }
            MainCanvas.Invalidate();
        }
        private void MainCanvas_MouseLeave(object sender, EventArgs e)
        {
            TempShape = null;
            MainCanvas.Invalidate();
        }
        private void MainCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)  { MainCanvas_ProcessLButton(sender, e); }
            if (e.Button == MouseButtons.Right) { MainCanvas_ProcessRButton(sender, e); }
            MainCanvas.Invalidate();
        }

        private void MainCanvas_ProcessLButton(object sender, MouseEventArgs e)
        {
            ShapesList.SelectedIndices.Clear();
            //this.Text = Convert.ToString(e.X) + " - " + Convert.ToString(e.Y);
            if (radioButton_Cross.Checked)
                AddShape(TempShape);
            else
            {
                if (IsShapeStart)
                    ShapeStart = e.Location;
                else
                    AddShape(TempShape);
                IsShapeStart = !IsShapeStart;
            }
        }

        private void AddShape(Shape AddedShape)
        {
            if (AddedShape.IsNotDegenerate)
            {
                Shapes.Add(AddedShape);
                ShapesList.Items.Add(AddedShape.DescriptionString);
            }
        }

        private void MainCanvas_ProcessRButton(object sender, MouseEventArgs e)
        {
            IsShapeStart = true;
            ShapesList.SelectedIndices.Clear();
            ShapesList.SelectedIndices.Add(SelectMatching(Shapes, e.Location));
            //this.Text = Convert.ToString(ShapesList.SelectedIndex);
        }

        private int SelectMatching(List<Shape> Shapes, Point S)
        {
            for (int i = 0; i < Shapes.Count; i++)
                if (Shapes[i].IsNearTo(S))
                    return i;

            return -1;
        }

        private void MainCanvas_Paint(object sender, PaintEventArgs e)
        {
            foreach (Shape p in this.Shapes)
                p.DrawWith(e.Graphics,pMain);

            foreach (int i in ShapesList.SelectedIndices)
                Shapes[i].DrawWith(e.Graphics, pSelect);
            
            if (TempShape != null) 
                TempShape.DrawWith(e.Graphics,pTemp);

        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            //Taxes: Remote Desktop Connection and painting
            //http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo aProp =
                typeof(System.Windows.Forms.Control).GetProperty(
                    "DoubleBuffered",
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance);

            aProp.SetValue(MainCanvas, true, null);

            toolTip1.SetToolTip(MainCanvas, "Default Tooltip tor MainCanvas");
            toolTip1.SetToolTip(button_Delete, "Delete selection");
            toolTip1.SetToolTip(radioButton_Cross,  "Draw cross");
            toolTip1.SetToolTip(radioButton_Line,   "Draw line");
            toolTip1.SetToolTip(radioButton_Circle, "Draw circle");
            toolTip1.SetToolTip(radioButton_Rect,   "Draw rectangle");


        }
        private void SaveFile(string file_cur)
        {
            try
            {
                StreamWriter sw = new StreamWriter(file_cur);
                foreach (Shape p in this.Shapes) { p.SaveTo(sw); }
                sw.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file_cur = null;
            Shapes.Clear();
            ShapesList.Items.Clear();
            MainCanvas.Invalidate();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string line;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file_cur = openFileDialog1.FileName;
                StreamReader sr = new StreamReader(file_cur);
                Shapes.Clear();
                ShapesList.SelectedIndices.Clear();
                line = sr.ReadLine();
                while (line != null)
                {
                    switch (line)
                    {
                        case  "Cross": AddShape(new  Cross(sr)); break;
                        case   "Line": AddShape(new   Line(sr)); break;
                        case "Circle": AddShape(new Circle(sr)); break;
                        case   "Rect": AddShape(new   Rect(sr)); break;
                    }
                    line = sr.ReadLine();
                }
                sr.Close();

                MainCanvas.Invalidate();
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e) //Save
        {
            if (file_cur == null)
                saveToolStripMenuItem1_Click(sender, e);
            else
                SaveFile(file_cur);
        }
        private void saveToolStripMenuItem1_Click(object sender, EventArgs e) //SaveAs
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file_cur = saveFileDialog1.FileName;
                SaveFile(file_cur);
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            int[] SelectedIndices_TEMP = new int[ShapesList.SelectedIndices.Count];
            ShapesList.SelectedIndices.CopyTo(SelectedIndices_TEMP,0);
            Array.Sort(SelectedIndices_TEMP);
            Array.Reverse(SelectedIndices_TEMP);
            foreach (int i in SelectedIndices_TEMP)
            {
                Shapes.RemoveAt(i);
                ShapesList.Items.RemoveAt(i);
            }
            ShapesList.SelectedIndices.Clear();
            MainCanvas.Invalidate();
        }

        private void FiguresList_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainCanvas.Invalidate();
        }

        private void FiguresList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                button_Delete_Click(null, null);
        }
    }
}