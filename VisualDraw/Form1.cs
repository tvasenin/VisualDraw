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
            int ind_TEMP = SelectMatching(Shapes,new Point(e.X,e.Y));
            if (ind_TEMP > -1) { toolTip1.SetToolTip(MainCanvas, Convert.ToString(ShapesList.Items[ind_TEMP])); }
            else { toolTip1.SetToolTip(MainCanvas,""); }
            
            if (IsShapeStart)
            {
                this.Text = Convert.ToString(e.X) + " - " + Convert.ToString(e.Y);
                TempShape = new Cross(e.X, e.Y);
            }
            else
            {
                if (radioButton_Line.Checked)   { TempShape = new Line(ShapeStart, new Point(e.X, e.Y)); }
                if (radioButton_Circle.Checked) { TempShape = new Circle(ShapeStart, new Point(e.X, e.Y)); }
                if (radioButton_Rect.Checked)   { TempShape = new Rect(ShapeStart, new Point(e.X, e.Y)); }
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
                if (IsShapeStart) { ShapeStart = new Point(e.X, e.Y); }
                else AddShape(TempShape);
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
            ShapesList.SelectedIndices.Add(SelectMatching(Shapes, new Point(e.X, e.Y)));
            //this.Text = Convert.ToString(ShapesList.SelectedIndex);
        }

        private int SelectMatching(List<Shape> Shapes, Point S)
        {
            for (int i = 0; i < Shapes.Count; i++)
            {
                if (Shapes[i].IsNearTo(S))
                    return i;
            }
            return -1;
        }

        private void MainCanvas_Paint(object sender, PaintEventArgs e)
        {
            foreach (Shape p in this.Shapes)
            {
                p.DrawWith(e.Graphics,pMain);
            }

            foreach (int i in ShapesList.SelectedIndices)
            {
                Shapes[i].DrawWith(e.Graphics, pSelect);
            }
            
            if (TempShape != null) { TempShape.DrawWith(e.Graphics,pTemp  ); }

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
            openFileDialog1.ShowDialog();
            file_cur = openFileDialog1.FileName;
            
            try
            {
                StreamReader sr = new StreamReader(file_cur);
                Shapes.Clear();
                ShapesList.SelectedIndices.Clear();
                line = sr.ReadLine();
                while (line != null)
                {
                    switch (line)
                    {
                        case "Cross": AddShape(new Cross(sr)); break;
                        case "Line": AddShape(new Line(sr)); break;
                        case "Circle": AddShape(new Circle(sr)); break;
                        case "Rect": AddShape(new Rect(sr)); break;
                    }
                    line = sr.ReadLine();
                }
                sr.Close();

                MainCanvas.Invalidate();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e) //Save
        {
            if (file_cur == null)
            {
                saveToolStripMenuItem1_Click(sender, e);
            }
            else
                SaveFile(file_cur);
        }
        private void saveToolStripMenuItem1_Click(object sender, EventArgs e) //SaveAs
        {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
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
            if (e.KeyCode == Keys.Delete) { button_Delete_Click(null,null); }
        }
    }


    public abstract class Shape
    {
        public abstract void DrawWith(Graphics g, Pen p);
        public abstract void SaveTo(StreamWriter sw);
        public abstract bool IsNearTo(Point S);
        public abstract String DescriptionString { get; }
        public abstract bool IsNotDegenerate { get; }

        protected int SqrDist(Point S, Point F)
        {
            return (int)(Math.Pow(S.X - F.X, 2) + Math.Pow(S.Y - F.Y, 2));
        }
        protected float Dist(Point S, Point F)
        {
            return (float)Math.Sqrt(SqrDist(S, F));
        }
    }
    public class Cross : Shape
    {
        public Point C;
        public Cross(Point p)
        {
            this.C = p;
        }
        public Cross(StreamReader sr)
        {
            string line;
            line = sr.ReadLine();
            line = line.Trim();
            string[] foo = line.Split(' ');
            this.C = new Point(int.Parse(foo[0]), int.Parse(foo[1]));
        }
        public Cross(int x, int y)
            : this(new Point(x, y))
        { }
        public override bool IsNotDegenerate
        {
            get { return true; }
        }
        public override void DrawWith(Graphics g, Pen p)
        {
            g.DrawLine(p, C.X - 2, C.Y - 2, C.X + 2, C.Y + 2);
            g.DrawLine(p, C.X - 2, C.Y + 2, C.X + 2, C.Y - 2);
        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Cross");
            sw.WriteLine(" " + C.X + " " + C.Y);
        }
        public override bool IsNearTo(Point S)
        {
            return (Math.Abs(C.X - S.X) <= 2) && (Math.Abs(C.Y - S.Y) <= 2);
        }
        public override String DescriptionString
        {
            get { return "Cross (" + Convert.ToString(C.X) + "," + Convert.ToString(C.Y) + ")"; }
        }
    }
    public class Line : Shape
    {
        public Point S, F;
        public int Length_sqr
        {
            get { return SqrDist(S, F); }
        }
        public Line(Point s, Point f)
        {
            this.S = s;
            this.F = f;
        }
        public Line(StreamReader sr)
        {
            string line;
            line = sr.ReadLine();
            line = line.Trim();
            string[] foo = line.Split(' ');
            this.S = new Point(int.Parse(foo[0]), int.Parse(foo[1]));
            this.F = new Point(int.Parse(foo[2]), int.Parse(foo[3]));
        }
        public override bool IsNotDegenerate
        {
            get { return Length_sqr > 0; }
        }
        public override void DrawWith(Graphics g, Pen p)
        {
            g.DrawLine(p, S.X, S.Y, F.X, F.Y);
        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Line");
            sw.WriteLine(" " + S.X + " " + S.Y + " " + F.X + " " + F.Y);
        }
        public override bool IsNearTo(Point P)
        {
            //approximate check
            return (Math.Abs(Dist(S, P) + Dist(P, F) - Dist(S, F)) <= 1);
        }
        public override String DescriptionString
        {
            get { return "Line   (" + Convert.ToString(S.X) + "," + Convert.ToString(S.Y) + ")-(" + Convert.ToString(F.X) + "," + Convert.ToString(F.Y) + ")"; }
        }
    }
    public class Circle : Shape
    {
        public Point C;
        public Point O;
        public float R
        {
            get { return Dist(C, O); }
        }

        public Circle(Point c, Point o)
        {
            this.C = c;
            this.O = o;
        }
        public Circle(StreamReader sr)
        {
            string line;
            line = sr.ReadLine();
            line = line.Trim();
            string[] foo = line.Split(' ');
            this.C = new Point(int.Parse(foo[0]), int.Parse(foo[1]));
            this.O = new Point(int.Parse(foo[2]), int.Parse(foo[3]));
        }
        public override bool IsNotDegenerate
        {
            get { return R > 0; }
        }
        public override void DrawWith(Graphics g, Pen p)
        {
            g.DrawEllipse(p, C.X - R, C.Y - R, 2 * R, 2 * R);
        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Circle");
            sw.WriteLine(" " + C.X + " " + C.Y + " " + O.X + " " + O.Y);
        }
        public override bool IsNearTo(Point P)
        {
            return (Math.Abs(R - Dist(C, P)) <= 2);
        }
        public override String DescriptionString
        {
            get { return "Circle (" + Convert.ToString(C.X) + "," + Convert.ToString(C.Y) + "); " + Convert.ToString(R); }
        }
    }

    public class Rect : Shape
    {
        public Point C1, C2;
        public int Width { get { return C2.X - C1.X; } }
        public int Height { get { return C2.Y - C1.Y; } }
        public Rect(Point c1, Point c2)
        {
            this.C1 = new Point(Math.Min(c1.X, c2.X), Math.Min(c1.Y, c2.Y));
            this.C2 = new Point(Math.Max(c1.X, c2.X), Math.Max(c1.Y, c2.Y));
        }
        public Rect(StreamReader sr)
        {
            string line;
            line = sr.ReadLine();
            line = line.Trim();
            string[] foo = line.Split(' ');
            this.C1 = new Point(int.Parse(foo[0]), int.Parse(foo[1]));
            this.C2 = new Point(int.Parse(foo[2]), int.Parse(foo[3]));
        }
        public override bool IsNotDegenerate
        {
            get { return Dist(C1, C2) > 0; }

        }
        public override void DrawWith(Graphics g, Pen p)
        {
            g.DrawRectangle(p, C1.X, C1.Y, this.Width, this.Height);
        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Rect");
            sw.WriteLine(" " + C1.X + " " + C1.Y + " " + C2.X + " " + C2.Y);
        }
        private bool IsInside(Point P)
        {
            return !((P.X < C1.X) || (P.X > C2.X) || (P.Y < C1.Y) || (P.Y > C2.Y));
        }
        public override bool IsNearTo(Point P)
        {
            Rect inbox = new Rect(new Point(C1.X + 3, C1.Y + 3), new Point(C2.X - 3, C2.Y - 3));
            Rect outbox = new Rect(new Point(C1.X - 2, C1.Y - 2), new Point(C2.X + 2, C2.Y + 2));

            if (inbox.Width <= 0) { return outbox.IsInside(P); } else { return outbox.IsInside(P) && !inbox.IsInside(P); }
        }
        public override String DescriptionString
        {
            get { return "Rect  (" + Convert.ToString(C1.X) + "," + Convert.ToString(C1.Y) + ")-(" + Convert.ToString(C2.X) + "," + Convert.ToString(C2.Y) + ")"; }
        }
    }
}