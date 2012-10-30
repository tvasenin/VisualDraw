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
        List<Cross> Points = new List<Cross>();
        
        public MainScreen()
        {
            InitializeComponent();
        }

        private void MainScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            this.Text = Convert.ToString(e.X) + " - " + Convert.ToString(e.Y);
            Points.Add(new Cross(e.X, e.Y));
            Invalidate();
        }

        private void MainScreen_Paint(object sender, PaintEventArgs e)
        {
            foreach (Cross p in this.Points)
            {
                p.DrawWith(e.Graphics);
            }
        }
    }
    public class Cross
    {
        int X, Y;
        Pen p = new Pen(Color.Black);
        public Cross(int X, int Y)
        {
            this.X = X; this.Y = Y;
        }

        public void DrawWith(Graphics g)
        {
            g.DrawLine(p, X - 2, Y - 2, X + 3, Y + 3);
            g.DrawLine(p, X - 2, Y + 2, X + 3, Y - 3);
        }
    }
}