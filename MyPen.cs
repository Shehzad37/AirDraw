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
    class MyPen
    {


       static public Point startPoint { get; set; }
      static  public Point endPoint { get; set; }
        public Pen pen;
        Graphics p;
        Main m = new Main();
        GraphicsPath gp;
        List<GraphicsPath> gpList = new List<GraphicsPath>();
        bool drawing = false;
        Color clr = new Color();
        float width = 1;
       
        
        public MyPen()
        {

            endPoint = new Point(0, 0);
            startPoint = new Point(0, 0);
            //pen = new Pen(Color.Red, 10);
            gp = new GraphicsPath();
            
           
            
            

        }
        public MyPen(Point startPoint,Point endPoint)
        {
           
            //gp2 = new GraphicsPath();
            // Point[] vertices = { startPoint, endPoint};
           // gp2.AddPath(gp,true);
           // gp.CloseFigure();
            
            
            
        }
        public void downPen( MouseEventArgs e)
        {
            
            drawing = true;


            startPoint = e.Location;
        }
        public void movePen(MouseEventArgs e,Graphics p)
        {


            if (drawing)
            {

            endPoint = e.Location;
            p.SmoothingMode = SmoothingMode.HighQuality;
           // clr = Main.uPen.Color;
            //width = Main.uPen.Width;
            //pen = new Pen(clr, width);
            p.DrawLine(Main.uPen, startPoint, endPoint);
            gp.AddLine(startPoint, endPoint);
            
            }
            startPoint = endPoint;
           
        }
        public void UpPen(MouseEventArgs e)
        {

            drawing = false;
            //endPoint = e.Location;
            gpList.Add(gp);
            //gp.AddLine(e.Location, new Point(e.Location.X, e.Location.Y));
            //gpList.Add(gp);
            
            gp = new GraphicsPath();
           // pen = new Pen(clr, width);


        }
        public bool inside(Point p)
        {
            if (gp.IsOutlineVisible(p, new Pen(Color.Black)))
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
            
            foreach (GraphicsPath item in gpList) { 
            g.DrawPath(Main.uPen, item);
           // Console.WriteLine(gpList.Count);
            }
        }

        public void zoom(Graphics p)
        {
            p.ScaleTransform(50, 50);

        }

        public void clearPath(){
            gpList.Clear();

        }

    }
    
}
