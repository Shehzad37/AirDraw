using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace Dibujando_Figuras
{
    class Circle
    {
        GraphicsPath graphicPath;
        Brush cBrush = new SolidBrush(Color.White);
        public Point startPoint { get; set; }
        public Point endPoint { get; set; }
        public Color clr = new Color();
        float width = 1;
        Pen p;

        public Circle()
        {

        }
        public Circle(Point sPoint, Point ePoint)
        {
            graphicPath = new GraphicsPath();
            graphicPath.AddEllipse(new Rectangle(sPoint, new Size(ePoint.X - sPoint.X, ePoint.X - sPoint.X)));

            graphicPath.CloseFigure();

            clr = Main.uPen.Color;
            width = Main.uPen.Width;
        }

        public void toDraw(Graphics graphics)
        {
            p = new Pen(clr,width);
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            if (Main.fill)
            {
                graphics.FillPath(cBrush, graphicPath);
            }
            graphics.DrawPath(p, graphicPath);
        }
        public bool inside(Point point)
        {
            if (graphicPath.IsOutlineVisible(point, new Pen(clr)))
            {
                return true;
            }
            else
            {
                return graphicPath.IsVisible(point);
            }
        }

        public void Mover(int dx, int dy)
        {
            graphicPath.Transform(new Matrix(1, 0, 0, 1, dx, dy));
        }
        public void changeColor(Color color)
        {
            clr = new Color();
            clr = color;
        }
        public void changeBrush(Brush b)
        {

            cBrush = b;
        }
        public void rotate(int dx, int dy)
        {

            Matrix m = new Matrix();
            Point p = new Point();
            p.X = dx;
            p.Y = dy;
            Console.WriteLine("dx" + dx + "");
            Console.WriteLine("dy" + dy + "");
            // m.Scale(p.X,p.Y);
            //  m.Translate(200, 200);
            m.RotateAt(10, p);
            // m.Translate(0, 0);
            // m.Reset();
            graphicPath.Transform(m);
            m.Reset();

        }

    }

}
