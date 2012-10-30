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
        List<Figure> Figures = new List<Figure>();
        List<int> SqrDistances = new List<int>();
        bool IsFigureStart = true;
        Point FigureStart;
        string file_cur;
        Pen pMain   = new Pen(Color.Black);
        Pen pSelect = new Pen(Color.Red,2);
        Pen pTemp   = new Pen(Color.DarkGray);
        Figure TempFigure;
    
               
        
        public MainScreen()
        {
            InitializeComponent();
        }

        private void MainScreen_radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            IsFigureStart = true;
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsFigureStart)
            {
                this.Text = Convert.ToString(e.X) + " - " + Convert.ToString(e.Y);
                TempFigure = new Cross(e.X, e.Y);
            }
            else
            {
                if (radioButton_Line.Checked)
                {
                    TempFigure = new Line(FigureStart, new Point(e.X, e.Y));
                }
                
                if (radioButton_Circle.Checked)
                {
                    TempFigure = new Circle(new Point(FigureStart.X, FigureStart.Y), new Point(e.X, e.Y));
                }
            }
            MainCanvas.Invalidate();
        }
        private void MainCanvas_MouseLeave(object sender, EventArgs e)
        {
            TempFigure = null;
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
            FiguresList.SelectedIndex = -1;
            //this.Text = Convert.ToString(e.X) + " - " + Convert.ToString(e.Y);
            if (radioButton_Cross.Checked)
            {
                Cross AddedFigure = new Cross(e.X, e.Y);
                Figures.Add(AddedFigure);
                FiguresList.Items.Add("Cross (" + Convert.ToString(e.X) + "," + Convert.ToString(e.Y) + ")");
            }
            else if (radioButton_Line.Checked)
            {
                if (IsFigureStart) { FigureStart = new Point(e.X, e.Y); }
                else 
                {
                    Line AddedFigure = new Line(FigureStart, new Point(e.X, e.Y));
                    Figures.Add(AddedFigure);
                    FiguresList.Items.Add("Line   (" + Convert.ToString(FigureStart.X) + "," + Convert.ToString(FigureStart.Y) + ")-(" + Convert.ToString(e.X) + "," + Convert.ToString(e.Y) + ")");
                }
                IsFigureStart = !IsFigureStart;
            }
            else if (radioButton_Circle.Checked)
            {
                if (IsFigureStart) { FigureStart = new Point(e.X, e.Y); }
                else
                {
                    Circle AddedFigure = new Circle(new Point(FigureStart.X, FigureStart.Y), new Point(e.X, e.Y));
                    if (AddedFigure.R > 0)
                    {
                        Figures.Add(AddedFigure);
                        FiguresList.Items.Add("Circle (" + Convert.ToString(AddedFigure.C.X) + "," + Convert.ToString(AddedFigure.C.Y) + "); " + Convert.ToString(AddedFigure.R));
                    }
                }
                IsFigureStart = !IsFigureStart;
            }
        }

        private void MainCanvas_ProcessRButton(object sender, MouseEventArgs e)
        {
            IsFigureStart = true;
            FiguresList.SelectedIndex = SelectMatching(Figures, new Point(e.X, e.Y));
        }

        private int SelectMatching(List<Figure> Figures, Point S)
        {
            FiguresList.SelectedIndex = -1;
            for (int i = 0; i < Figures.Count; i++)
            {
                if (Figures[i].IsNearTo(S)) { return i; }
            }
            return -1;
        }

        private void MainCanvas_Paint(object sender, PaintEventArgs e)
        {
            foreach (Figure p in this.Figures)
            {
                p.DrawWith(e.Graphics,pMain);
            }

            if (FiguresList.SelectedIndex >= 0) { Figures[FiguresList.SelectedIndex].DrawWith(e.Graphics, pSelect); }
            if (TempFigure != null) { TempFigure.DrawWith(e.Graphics,pTemp  ); }

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
        }
        private void SaveFile(string file_cur)
        {
            try
            {
                StreamWriter sw = new StreamWriter(file_cur);

                foreach (Figure p in this.Figures)
                {
                    p.SaveTo(sw);
                }

                sw.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file_cur = null;
            Figures.Clear();
            FiguresList.SelectedIndex = -1;
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
                Figures.Clear();
                FiguresList.SelectedIndex = -1;
                line = sr.ReadLine();
                while (line != null)
                    switch (line)
                    {
                        case "Cross": Figures.Add(new Cross(sr)); break;
                        case "Line": Figures.Add(new Line(sr)); break;
                        case "Circle": Figures.Add(new Circle(sr)); break;
                    }
                    line = sr.ReadLine();
                

                sr.Close();

                MainCanvas.Invalidate();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e) //Save
        {
            if (file_cur == null)
            {
                saveFileDialog1.ShowDialog();
                file_cur = saveFileDialog1.FileName;
            }
            SaveFile(file_cur);
        }
        private void saveToolStripMenuItem1_Click(object sender, EventArgs e) //SaveAs
        {
            saveFileDialog1.ShowDialog();
            file_cur = saveFileDialog1.FileName;
            SaveFile(file_cur);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (FiguresList.SelectedIndex >= 0)
            {
                Figures.RemoveAt(FiguresList.SelectedIndex);
                FiguresList.Items.RemoveAt(FiguresList.SelectedIndex);
                FiguresList.SelectedIndex = -1;
                MainCanvas.Invalidate();
            }
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

    
    public abstract class Figure
    {
        public abstract void DrawWith(Graphics g, Pen p);
        public abstract void SaveTo(StreamWriter sw);
        public abstract bool IsNearTo(Point S);

        protected int SqrDist(Point S, Point F)
        {
            return (int) (Math.Pow(S.X - F.X, 2) + Math.Pow(S.Y - F.Y, 2));
        }
        protected float Dist(Point S, Point F)
        {
            return (float) Math.Sqrt(SqrDist(S, F));
        }
    }
    public class Cross  : Figure
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
    }
    public class Line   : Figure
    {
        public Point S, F;

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
    }
    public class Circle : Figure
    {    
        public Point C;
        public int R;

        public Circle(Point c, Point o)
        {
            this.C = c;
            this.R = (int)Dist(c, o);
        }
        public Circle(StreamReader sr)
        {
            string line;
            line = sr.ReadLine();
            line = line.Trim();
            string[] foo = line.Split(' ');
            this.C = new Point(int.Parse(foo[0]), int.Parse(foo[1]));
            this.R = int.Parse(foo[2]);
        }

        public override void DrawWith(Graphics g, Pen p)
        {
            g.DrawEllipse(p, C.X - R, C.Y - R, 2 * R, 2 * R);
        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Circle");
            sw.WriteLine(" " + C.X + " " + C.Y + " " + R);
        }
        public override bool IsNearTo(Point P)
        {
            return (Math.Abs(R - Dist(C, P)) <= 2);
        }
    }

}