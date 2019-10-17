using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Dibujando_Figuras
{
    class Eraser
    {


        static public Point startPoint { get; set; }
        static public Point endPoint { get; set; }
        public Pen pen;
        Graphics p;
        GraphicsPath gp;
        List<GraphicsPath> gpList = new List<GraphicsPath>();
        bool drawing = false;



        public Eraser()
        {

            endPoint = new Point(0, 0);
            startPoint = new Point(0, 0);
            pen = new Pen(Color.White, 8);
            gp = new GraphicsPath();

        }
        public Eraser(Point startPoint, Point endPoint)
        {

            //gp2 = new GraphicsPath();
            // Point[] vertices = { startPoint, endPoint};
            // gp2.AddPath(gp,true);
            // gp.CloseFigure();



        }
        public void downEraser(MouseEventArgs e)
        {
            

            drawing = true;


            startPoint = e.Location;
        }
        public void MoveEraser(MouseEventArgs e, Graphics p)
        {


            if (drawing)
            {

                endPoint = e.Location;
                p.SmoothingMode = SmoothingMode.HighQuality;
                p.DrawLine(pen, startPoint, endPoint);
                gp.AddLine(startPoint, endPoint);
               

            }
            startPoint = endPoint;

        }
        public void UpEraser(MouseEventArgs e)
        {

            drawing = false;
            //endPoint = e.Location;
            gpList.Add(gp);
            //gp.AddLine(e.Location, new Point(e.Location.X, e.Location.Y));
            //gpList.Add(gp);

            gp = new GraphicsPath();


        }
        public bool inside(Point p)
        {
            if (gp.IsOutlineVisible(p, new Pen(Color.White)))
            {
                return true;
            }
            else
            {
                return gp.IsVisible(p);
            }
        }
        public void paint()
        {
            // g.DrawPath(pen, graphicsPaths);
        }

        public void Mover(int dx, int dy)
        {
            //graphicsPaths.Transform(new Matrix(1, 0, 0, 1, dx, dy));
        }
        public void toDraw(Graphics g)
        {
            // Pen pen = new Pen(Color.DarkViolet, 2);
            // e.SmoothingMode = SmoothingMode.AntiAlias;
            //  e.DrawPath(pen, gp2);
            //endPoint = e.Location;
            //g.DrawLine(pen, startPoint, endPoint);
            foreach (GraphicsPath item in gpList)
            {
                g.DrawPath(pen, item);
            }
        }
    }
}
