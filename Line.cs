using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using Dibujando_Figuras;
namespace Dibujando_Figuras
{
    class Line
    {
        GraphicsPath gp;

        public Point startPoint { get; set; }


        public Point endPoint { get; set; }

        public Color clr = new Color();
        float width = 1;
        public Line() { }

        public Line(Point ptI, Point ptF)
        {
            gp = new GraphicsPath();
            Point[] vertices = { ptI, ptF };
            gp.AddLine(ptI, ptF);
            gp.CloseFigure();
            clr = Main.uPen.Color;
            width = Main.uPen.Width;
        }
        public void DrawLine(Graphics g, Point startPoint, Point endPoint)
        {
            // Pen pen = new Pen(clr,2);
            // g.SmoothingMode = SmoothingMode.AntiAlias;
            // if(gp!=null)
            // g.DrawPath(pen, gp);

            g.Dispose();


        }
        public bool inside(Point p)
        {
            if (gp.IsOutlineVisible(p, new Pen(clr)))
            {
                return true;
            }
            else
            {
                return gp.IsVisible(p);
            }
        }
        public void Mover(int dx, int dy)
        {
            gp.Transform(new Matrix(1, 0, 0, 1, dx, dy));
        }
        public void toDraw(Graphics e)
        {
            Pen pen = new Pen(clr,width);
            e.SmoothingMode = SmoothingMode.AntiAlias;
            e.DrawPath(pen, gp);
        }
        public void changeColor(Color color)
        {
            clr = new Color();
            clr = color;
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
            gp.Transform(m);
            m.Reset();

        }
    }
}
