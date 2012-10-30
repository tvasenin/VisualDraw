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
        bool IsFigureStart = true;
        Point FigureStart;
        string file_cur;
               
        
        public MainScreen()
        {
            InitializeComponent();
        }

        private void MainScreen_radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            IsFigureStart = true;
        }

        private void MainCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            this.Text = Convert.ToString(e.X) + " - " + Convert.ToString(e.Y);
            if (radioButton_Cross.Checked)
            {
                Figures.Add(new Cross(e.X, e.Y));
            }
            else if (radioButton_Line.Checked)
            {
                if (IsFigureStart)
                {
                    FigureStart = new Point(e.X, e.Y);
                    IsFigureStart = false;
                }
                else
                {
                    Figures.Add(new Line(FigureStart, new Point(e.X, e.Y)));
                    IsFigureStart = true;
                }
            }
            else if (radioButton_Circle.Checked)
            {
                if (IsFigureStart)
                {
                    FigureStart = new Point(e.X, e.Y);
                    IsFigureStart = false;
                }
                else
                {
                    Figures.Add(new Circle(new Point(FigureStart.X, FigureStart.Y), (float) Math.Sqrt(Math.Pow(e.X-FigureStart.X, 2) + Math.Pow(e.Y-FigureStart.Y, 2))));
                    IsFigureStart = true;
                }
            }

            MainCanvas.Invalidate();
        }

        private void MainCanvas_Paint(object sender, PaintEventArgs e)
        {
            foreach (Figure p in this.Figures)
            {
                p.DrawWith(e.Graphics);
            }
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
            MainCanvas.Invalidate();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string line;
            newToolStripMenuItem_Click(null, null);
            openFileDialog1.ShowDialog();
            file_cur = openFileDialog1.FileName;
            
            try
            {
                StreamReader sr = new StreamReader(file_cur);

                line = sr.ReadLine();
                while (line != null)
                {
                    if (line == "Cross" ) { Figures.Add(new Cross(sr));  }
                    if (line == "Line"  ) { Figures.Add(new Line(sr));   }
                    if (line == "Circle") { Figures.Add(new Circle(sr)); }

                    line = sr.ReadLine();
                }

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


    }
        
    
    public abstract class Figure
    {
        protected Pen p = new Pen(Color.Black);
        
        public abstract void DrawWith(Graphics g);
        public abstract void SaveTo(StreamWriter sw);

    }

    public class Cross : Figure
    {
        Point C;

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

        public override void DrawWith(Graphics g)
        {
            g.DrawLine(p, C.X - 2, C.Y - 2, C.X + 2, C.Y + 2);
            g.DrawLine(p, C.X - 2, C.Y + 2, C.X + 2, C.Y - 2);
        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Cross");
            sw.WriteLine(" " + C.X + " " + C.Y);
        }

    }
    
    public class Line: Figure
    {
        Point S, F;

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

        public override void DrawWith(Graphics g)
        {
            g.DrawLine(p, S.X, S.Y, F.X, F.Y);
        }
        
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Line");
            sw.WriteLine(" " + S.X + " " + S.Y + " " + F.X + " " + F.Y);
        }


    }
        
    public class Circle : Figure
    {    
        Point C;
        float R;

        public Circle(Point c, float r)
        {
            this.C = c;
            this.R = r;
        }

        public Circle(StreamReader sr)
        {
            string line;
            line = sr.ReadLine();
            line = line.Trim();
            string[] foo = line.Split(' ');
            this.C = new Point(int.Parse(foo[0]), int.Parse(foo[1]));
            this.R = float.Parse(foo[2]);
        }

        public override void DrawWith(Graphics g)
        {
            g.DrawEllipse(p, C.X - R, C.Y - R, 2 * R, 2 * R);
        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Circle");
            sw.WriteLine(" " + C.X + " " + C.Y + " " + R);
        }

    }

}