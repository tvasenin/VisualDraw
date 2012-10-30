using System;
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


        
        
    }

    public abstract class Figure
    {
        protected Pen p = new Pen(Color.Black);
        
        public abstract void DrawWith(Graphics g);
    }
    public class Line: Figure
    {
        Point S, F;

        public Line(Point s, Point f)
        {
            this.S = s;
            this.F = f;
        }
        public override void DrawWith(Graphics g)
        {
            g.DrawLine(p, S.X, S.Y, F.X, F.Y);
            //new Cross(S);
        }

    }
    public class Cross : Figure
    {
        Point C;

        public Cross(Point p)
        {
            this.C = p;
        }

        public Cross(int x, int y)
            :this(new Point(x, y))
        { }
        
        public override void DrawWith(Graphics g)
        {
            g.DrawLine(p, C.X - 2, C.Y - 2, C.X + 2, C.Y + 2);
            g.DrawLine(p, C.X - 2, C.Y + 2, C.X + 2, C.Y - 2);
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
        public override void DrawWith(Graphics g)
        {
            g.DrawEllipse(p, C.X - R, C.Y - R, 2 * R, 2 * R);
        }

    }

}