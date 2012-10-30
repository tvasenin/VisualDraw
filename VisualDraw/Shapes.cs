using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.IO;

namespace VisualDraw
{
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
