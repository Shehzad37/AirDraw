using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using Microsoft.Kinect;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;
using System.IO;
using Coding4Fun.Kinect.WinForm;
using System.Drawing.Drawing2D;
using paintPratice1;
using Microsoft.Kinect.Toolkit.Interaction;
using Dibujando_Figuras;
namespace Dibujando_Figuras
{
    public partial class Main : Form
    {
        private readonly GraphicsPath graphicsPaths = new GraphicsPath();
        #region Shapes Lists
        List<Triangle> triangleList;
        List<Daimond> daimondList;
        List<Circle> circleList;
        List<Rectangle1> rectangleList;
        List<Line> lineList;
        List<MyPen> penList;
        List<Eraser> eraserList;
        List<Triangle> tList;
        List<Daimond> dList;
        List<Circle> cList;
        List<Rectangle1> rList;
        List<Line> lList;
        bool rot = true;
        List<Pen> pens = new List<Pen>();
        static public bool resize = false;
        #endregion

        #region Kinect Variables
        KinectSensor sensor;
        String checkGrip;
        UserInfo[] _userInfos;
        bool handup = true;
        private byte[] colorPixels;
        InteractionStream _interactionStream;
        private const float SkeletonMaxX = 0.40f;
        private const float SkeletonMaxY = 0.30f;
        int count, x1, x2, y1, y2, dist = 0;
        Random rnd = new Random();
        // int countDown = 0;
        int countClr = 0;
        #endregion

        #region shapes variables
        public static Pen uPen;
        Brush fBrush;
        //NameForm nf = new NameForm();
        int rotX, rotY;
        Triangle triangle1, triangle2;
        Daimond daimond1, daimond2;
        Circle circle1, circle2;
        Rectangle1 rectangle1, rectangle2, rect;
        Line line1, line2;
        MyPen pen2;
        bool checkColor = false;
        //bool isShown = false;
        Eraser eraser;
        bool isCursorHide = false;
        int zoom = 1;
        #endregion

        #region point ,graphic ,tools
        Point pos;
        Graphics g;
        ToolSelec toolselec;
        bool clic;
        int dCount = 0;
        public static bool fill = false;
        int j = 0;
        public Pen pn;
        List<String> str;
        //ThreadStart ts = new ThreadStart();
        #endregion

        public string FileName { get; set; }
        String fName = "";

        #region Select tool
        public enum ToolSelec
        {
            puntero, Triangle, Daimond, circle, rectangle, line, pen, eraser, save, createNewFile, fillColor
        }
        #endregion

        public Main()
        {
            InitializeComponent();
            //this.Text = "AirDraw";
            //Console.WriteLine(AreaDraw.Size);
            //this.Size = new Size(900, 550);
        }

        #region Tool Events
        private void toolPuntero_Click(object sender, EventArgs e)
        {
            btnPen.Checked = false;
            btnEraser.Checked = false;
            btnLine.Checked = false;
            btnCircle.Checked = false;
            btnRectangle.Checked = false;
            toolTriangle.Checked = false;
            toolDaimond.Checked = false;
            toolPuntero.Checked = true;
            toolselec = ToolSelec.puntero;
            // this.Cursor = Cursors.Cross;
            // toolStrip.Hide();
            Cursor.Clip = new Rectangle(AreaDraw.Location, AreaDraw.Size);
        }
        private void toolTriangle_Click(object sender, EventArgs e)
        {
            btnEraser.Checked = false;
            btnPen.Checked = false;
            btnLine.Checked = false;
            btnCircle.Checked = false;
            btnRectangle.Checked = false;
            toolPuntero.Checked = false;
            //label1.Text = "Triangle"; 
            AreaDraw.Cursor = new Cursor(Properties.Resources.c.GetHicon());
            toolDaimond.Checked = false;
            toolStrip.Hide();
            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
        }
        private void toolDaimond_Click(object sender, EventArgs e)
        {
            btnPen.Checked = false;
            btnLine.Checked = false;
            btnCircle.Checked = false;
            btnEraser.Checked = false;
            btnRectangle.Checked = false;
            toolPuntero.Checked = false;
            toolTriangle.Checked = false;
            toolDaimond.Checked = true;
            AreaDraw.Cursor = new Cursor(Properties.Resources.c.GetHicon());
            toolselec = ToolSelec.Daimond;
            //label1.Text = "Diamond";
            // toolStrip.Hide();
            Cursor.Clip = new Rectangle(AreaDraw.Location, AreaDraw.Size);
        }
        private void btnRectangle_Click(object sender, EventArgs e)
        {
            btnPen.Checked = false;
            btnLine.Checked = false;
            btnCircle.Checked = false;
            toolDaimond.Checked = false;
            btnEraser.Checked = false;
            toolPuntero.Checked = false;
            toolTriangle.Checked = false;
            btnRectangle.Checked = true;
            AreaDraw.Cursor = new Cursor(Properties.Resources.c.GetHicon());
            //label1.Text = "Rectangle";
            toolStrip.Hide();
            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
            toolselec = ToolSelec.rectangle;
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            btnPen.Checked = false;
            btnLine.Checked = false;
            btnRectangle.Checked = false;
            toolDaimond.Checked = false;
            toolPuntero.Checked = false;
            toolTriangle.Checked = false;
            btnEraser.Checked = false;
            btnCircle.Checked = true;
            toolselec = ToolSelec.circle;
            AreaDraw.Cursor = new Cursor(Properties.Resources.c.GetHicon());
            // label1.Text = "Circle";
            // toolStrip.Hide();
            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
        }
        private void btnLine_Click(object sender, EventArgs e)
        {
            btnPen.Checked = false;
            btnCircle.Checked = false;
            btnRectangle.Checked = false;
            toolDaimond.Checked = false;
            toolPuntero.Checked = false;
            toolTriangle.Checked = false;
            btnEraser.Checked = false;
            btnLine.Checked = true;
            toolselec = ToolSelec.line;
            AreaDraw.Cursor = new Cursor(Properties.Resources.c.GetHicon());
            //label1.Text = "Line";
            //toolStrip.Hide();
            Cursor.Clip = new Rectangle(AreaDraw.Location, AreaDraw.Size);
        }
        private void btnPen_Click_1(object sender, EventArgs e)
        {
            btnLine.Checked = false;
            btnCircle.Checked = false;
            btnRectangle.Checked = false;
            toolDaimond.Checked = false;
            toolPuntero.Checked = false;
            toolTriangle.Checked = false;
            btnEraser.Checked = false;
            btnPen.Checked = true;
            toolselec = ToolSelec.pen;
            AreaDraw.Cursor = new Cursor(Properties.Resources.brush.GetHicon());
            toolStrip.Hide();
            Cursor.Show();
            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
        }
        private void btnEraser_Click(object sender, EventArgs e)
        {
            btnLine.Checked = false;
            btnCircle.Checked = false;
            btnRectangle.Checked = false;
            toolDaimond.Checked = false;
            toolPuntero.Checked = false;
            toolTriangle.Checked = false;
            btnPen.Checked = false;
            btnEraser.Checked = true;
            //MessageBox.Show("Eraser");
            //btnEraser.BackColor = Color.Yellow;
            toolselec = ToolSelec.eraser;
            // label1.Text = "Eraser";
            toolStrip.Hide();
            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
            AreaDraw.Cursor = new Cursor(Properties.Resources.ersr.GetHicon());
        }
        #endregion

        #region Load Form
        private void Form1_Load(object sender, EventArgs e)
        {
            // button1.Hide();
            fName = this.FileName;
            
            //radialMenu1.CenterButtonClick += radialMenu1_CenterButtonClick;
            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
            trackBar1.ValueChanged += trackBar1_ValueChanged;
            clic = false;
            label1.Hide();
            label1.SendToBack();
            rect = new Rectangle1();
            fBrush = new SolidBrush(Color.Black);
            triangle1 = new Triangle();
            daimond1 = null;
            circle1 = null;
            rectangle1 = null;
            line1 = null;
            panel1.Cursor = new Cursor(Properties.Resources.cursor1.GetHicon());
            // pb.BringToFront();
            triangle2 = new Triangle();
            daimond2 = new Daimond();
            circle2 = new Circle();
            rectangle2 = new Rectangle1();
            line2 = new Line();
            pen2 = new MyPen();
            eraser = new Eraser();

            str = new List<String>();
            uPen = new Pen(Color.IndianRed);
            uPen.Width = 10;
           // pn = new Pen()
            AreaDraw.Cursor = new Cursor(Properties.Resources.brush.GetHicon());

            pictureBox1.Image = Properties.Resources.penbkk;
            triangleList = new List<Triangle>();
            daimondList = new List<Daimond>();
            circleList = new List<Circle>();
            rectangleList = new List<Rectangle1>();
            lineList = new List<Line>();
            penList = new List<MyPen>();
            eraserList = new List<Eraser>();

            tList = new List<Triangle>();
            dList = new List<Daimond>();
            cList = new List<Circle>();
            rList = new List<Rectangle1>();
            lList = new List<Line>();

            

            toolselec = ToolSelec.pen;
            toolPuntero.Checked = false;
            btnCircle.Checked = false;
            btnRectangle.Checked = false;
            toolTriangle.Checked = false;
            toolDaimond.Checked = false;
            btnLine.Checked = false;
            btnRotate.Checked = false;
            btnZoom.Checked = false;
            btnPen.Checked = true;
            btnEraser.Checked = false;

            g = AreaDraw.CreateGraphics();
            toolStrip.Hide();

            try
            {
                SetupSensor();
                _userInfos = new UserInfo[InteractionFrame.UserInfoArrayLength];
                _interactionStream = new InteractionStream(sensor, new DummyInteractionClient());
                _interactionStream.InteractionFrameReady += InteractionStreamOnInteractionFrameReady;
            }
            catch (Exception) { Console.WriteLine("setup error"); }






        }

       
        void trackBar1_ValueChanged(object sender, EventArgs e)            //widhtt.........
        {
            eraser.pen.Width = trackBar1.Value;
            uPen.Width = trackBar1.Value;


        }

        void radialMenu1_CenterButtonClick(object sender, DevExpress.XtraBars.Ribbon.RadialMenuCenterButtonClickEventArgs e)
        {
            //radialMenu1.HidePopup();
            Cursor.Clip = new Rectangle(AreaDraw.Location, AreaDraw.Size);
            Cursor.Show();
        }
        #endregion

        #region changeShapesColor()
        private void haberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Triangle t = new Triangle();
        }

        private void changeShapesColor()
        {

            foreach (Triangle item in triangleList)
            {

                item.changeColor(uPen.Color);


            }
            foreach (Daimond item in daimondList)
            {
                if (item != null)
                    item.changeColor(uPen.Color);
            }
            foreach (Circle item in circleList)
            {
                if (item != null)
                    item.changeColor(uPen.Color);
            }
            foreach (Rectangle1 item in rectangleList)
            {
                if (item != null)
                    item.changeColor(uPen.Color);
            }
            foreach (Line item in lineList)
            {
                if (item != null)
                    item.changeColor(uPen.Color);
            }

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void AreaDraw_Click(object sender, EventArgs e)
        {

        }
        #endregion Arrastrar y Soltar

        #region Mouse Events
        private void AreaDraw_MouseDown_1(object sender, MouseEventArgs e)
        {

            //Console.WriteLine(Cursor.Position.X);
            //Console.WriteLine(Cursor.Position.Y);

            switch (toolselec)
            {
                case ToolSelec.puntero:
                    #region  Pointer
                    foreach (Triangle item in triangleList)
                    {
                        Point p = AreaDraw.PointToClient(Cursor.Position);
                        if (item.inside(p))
                        {

                            triangle1 = item;
                            if (btnRotate.Checked)
                            {
                                rot = false;
                                rotX = e.X;
                                rotY = e.Y;
                                Cursor.Clip = new Rectangle(Cursor.Position.X, Cursor.Position.Y, 1, 1);
                                // triangle1.rotate(e.X, e.Y);
                            }

                            changeShapesColor();

                            triangle1.changeColor(Color.Black);

                            #region
                            // int piX, piY, pfX, pfY;
                            // piX = triangle1.PI.X + 50;
                            // piY = triangle1.PI.Y + 50;
                            // pfX = triangle1.PF.X + 50;
                            // pfY = triangle1.PF.Y + 50;
                            // Point px = new Point(piX, piY);
                            // Point py = new Point(pfX, pfY);
                            // Point ptMedio = new Point(px.X - (py.X - px.X), py.Y);
                            //// triangleList.Add(new Triangle(triangle1.PI, ptMedio, triangle1.PF));
                            #endregion
                            pos = p;
                            AreaDraw.Invalidate();
                            break;
                        }

                    }
                    foreach (Daimond item in daimondList)
                    {
                        Point p = AreaDraw.PointToClient(Cursor.Position);
                        if (item.inside(p))
                        {
                            daimond1 = item;
                            if (btnRotate.Checked)
                            {
                                rot = false;
                                Cursor.Clip = new Rectangle(Cursor.Position.X, Cursor.Position.Y, 1, 1);
                                rotX = e.X;
                                rotY = e.Y;
                            }
                            //dList.Add(daimond1);
                            changeShapesColor();
                            daimond1.clr = Color.Black;

                            pos = p;
                            AreaDraw.Invalidate();
                            break;
                        }
                    }
                    foreach (Circle item in circleList)
                    {
                        Point p = AreaDraw.PointToClient(Cursor.Position);
                        if (item.inside(p))
                        {
                            circle1 = item;
                            if (btnRotate.Checked)
                            {
                                rot = false;
                                Cursor.Clip = new Rectangle(Cursor.Position.X, Cursor.Position.Y, 1, 1);
                                rotX = e.X;
                                rotY = e.Y;
                            }
                            //cList.Add(circle1);
                            changeShapesColor();
                            circle1.clr = Color.Black;

                            pos = p;
                            AreaDraw.Invalidate();
                            break;
                        }
                    }
                    foreach (Rectangle1 item in rectangleList)
                    {
                        Point p = AreaDraw.PointToClient(Cursor.Position);
                        if (item.inside(p))
                        {
                            rectangle1 = item;
                            if (btnRotate.Checked)
                            {
                                rot = false;
                                Cursor.Clip = new Rectangle(Cursor.Position.X, Cursor.Position.Y, 1, 1);
                                rotX = e.X;
                                rotY = e.Y;
                            }
                            //rList.Add(rectangle1);
                            changeShapesColor();
                            rectangle1.clr = Color.Black;
                            //ursor.Clip = new Rectangle(Cursor.Position.X, Cursor.Position.Y, 1, 1);
                            pos = p;
                            AreaDraw.Invalidate();
                            break;
                        }
                    }
                    foreach (Line item in lineList)
                    {
                        Point p = AreaDraw.PointToClient(Cursor.Position);
                        if (item.inside(p))
                        {
                            line1 = item;
                            if (btnRotate.Checked)
                            {
                                rot = false;
                                Cursor.Clip = new Rectangle(Cursor.Position.X, Cursor.Position.Y, 1, 1);
                                rotX = e.X;
                                rotY = e.Y;
                            }
                            //lList.Add(line1);
                            changeShapesColor();
                            line1.clr = Color.Black;

                            pos = p;
                            AreaDraw.Invalidate();
                            break;
                        }
                    }




                    #endregion
                    break;
                case ToolSelec.Triangle:

                    #region Triangle
                    clic = true;
                    triangle2.PI = e.Location;
                    #endregion
                    break;
                case ToolSelec.Daimond:
                    //Punto Inicial
                    #region Daimond
                    clic = true;
                    daimond2.startPoint = e.Location;
                    #endregion
                    break;
                case ToolSelec.circle:
                    #region Circle
                    clic = true;
                    circle2.startPoint = e.Location;
                    #endregion

                    break;
                case ToolSelec.rectangle:
                    #region rectangle
                    clic = true;
                    rectangle2.startPoint = e.Location;
                    #endregion

                    break;
                case ToolSelec.line:
                    #region Line
                    clic = true;
                    line2.startPoint = e.Location;

                    #endregion
                    break;
                case ToolSelec.pen:
                    #region Pen
                    clic = true;
                    pen2.downPen(e);


                    #endregion
                    break;
                case ToolSelec.eraser:
                    #region Eraser

                    clic = true;
                    eraser.downEraser(e);


                    #endregion
                    break;
                case ToolSelec.fillColor:
                    #region  Pointer
                    foreach (Triangle item in triangleList)
                    {
                        Point p = AreaDraw.PointToClient(Cursor.Position);
                        if (item.inside(p))
                        {

                            triangle1 = item;

                            if (checkColor)
                            {
                                item.changeBrush(fBrush);
                                // item.fill(g);

                            }
                            pos = p;
                            AreaDraw.Invalidate();
                            break;

                        }

                    }
                    foreach (Daimond item in daimondList)
                    {
                        Point p = AreaDraw.PointToClient(Cursor.Position);
                        if (item.inside(p))
                        {
                            daimond1 = item;

                            if (checkColor)
                            {
                                item.changeBrush(fBrush);
                            }
                            pos = p;
                            AreaDraw.Invalidate();
                            break;
                        }
                    }
                    foreach (Circle item in circleList)
                    {
                        Point p = AreaDraw.PointToClient(Cursor.Position);
                        if (item.inside(p))
                        {
                            circle1 = item;

                            if (checkColor)
                            {
                                item.changeBrush(fBrush);
                            }
                            pos = p;
                            AreaDraw.Invalidate();
                            break;
                        }
                    }
                    foreach (Rectangle1 item in rectangleList)
                    {
                        Point p = AreaDraw.PointToClient(Cursor.Position);
                        if (item.inside(p))
                        {
                            rectangle1 = item;

                            if (checkColor)
                            {
                                item.changeBrush(fBrush);
                            }
                            pos = p;
                            AreaDraw.Invalidate();
                            break;
                        }
                    }

                    break;

                    #endregion


            }


        }

        private void AreaDraw_MouseMove_1(object sender, MouseEventArgs e)
        {
            switch (toolselec)
            {
                case ToolSelec.puntero:
                    #region Pointer
                    // lbEstado.Text = "Estado: Puntero";
                    if (triangle1 != null && !btnRotate.Checked)
                    {
                        triangle1.Mover(e.Location.X - pos.X, e.Location.Y - pos.Y);

                        AreaDraw.Invalidate();
                        pos = e.Location;
                    }
                    else if (daimond1 != null && !btnRotate.Checked)
                    {
                        daimond1.Mover(e.Location.X - pos.X, e.Location.Y - pos.Y);
                        AreaDraw.Invalidate();
                        pos = e.Location;

                    }
                    else if (circle1 != null && !btnRotate.Checked)
                    {
                        circle1.Mover(e.Location.X - pos.X, e.Location.Y - pos.Y);
                        AreaDraw.Invalidate();
                        pos = e.Location;
                    }
                    else if (rectangle1 != null && !btnRotate.Checked)
                    {
                        rectangle1.Mover(e.Location.X - pos.X, e.Location.Y - pos.Y);
                        AreaDraw.Invalidate();
                        pos = e.Location;
                    }
                    else if (line1 != null && !btnRotate.Checked)
                    {
                        line1.Mover(e.Location.X - pos.X, e.Location.Y - pos.Y);
                        AreaDraw.Invalidate();
                        pos = e.Location;
                    }

                    #endregion
                    break;
                case ToolSelec.Triangle:
                    #region Circle
                    if (clic)
                    {

                        triangle2.PF = e.Location;
                        AreaDraw.Invalidate();

                    }
                    #endregion
                    break;
                case ToolSelec.Daimond:
                    #region  Daimond
                    //lbEstado.Text = "Estado: Dibujando Daimond";
                    if (clic)
                    {
                        daimond2.endPoint = e.Location;
                        AreaDraw.Invalidate();
                    }
                    #endregion
                    break;
                case ToolSelec.circle:
                    #region  Circle
                    if (clic)
                    {

                        circle2.endPoint = e.Location;
                        AreaDraw.Invalidate();
                    }
                    #endregion
                    break;
                case ToolSelec.rectangle:
                    #region  Rectangle
                    if (clic)
                    {

                        rectangle2.endPoint = e.Location;
                        AreaDraw.Invalidate();
                    }
                    #endregion
                    break;
                case ToolSelec.line:
                    #region  Line
                    if (clic)
                    {

                        line2.endPoint = e.Location;
                        AreaDraw.Invalidate();


                    }
                    #endregion
                    break;
                case ToolSelec.pen:
                    #region  Pen
                    if (clic)
                    {
                        pen2.movePen(e, g);

                        //AreaDraw.Invalidate();
                        //g = AreaDraw.CreateGraphics();
                        // g.DrawLine(Pens.Red, MyPen.startPoint,MyPen.endPoint);
                        // AreaDraw.Invalidate();
                    }
                    #endregion
                    break;
                case ToolSelec.eraser:
                    #region  Eraser
                    if (AreaDraw.ForeColor != Color.White)
                    {
                        if (clic)
                        {
                            eraser.MoveEraser(e, g);
                            //AreaDraw.Invalidate();
                            //eraser.endPoint = e.Location;
                            //eraser.eraseDrawing(e, AreaDraw, g);
                            //eraser.startPoint = eraser.endPoint;
                            //AreaDraw.Invalidate();
                        }
                    }
                    else
                        Console.WriteLine("Panel is not white");
                    #endregion

                    break;
            }

            // AreaDraw.Invalidate();
            //radialMenu1.ShowPopup(Cursor.Position);

        }
        private void AreaDraw_MouseUp_1(object sender, MouseEventArgs e)
        {
            switch (toolselec)
            {
                case ToolSelec.puntero:
                    checkColor = false;
                    break;
                case ToolSelec.Triangle:
                    #region Triangle
                    if (triangle2.PI != new Point(0, 0) & triangle2.PF != new Point(0, 0) & (triangle2.PI != triangle2.PF))
                    {
                        Point ptMedio = new Point(triangle2.PI.X - (triangle2.PF.X - triangle2.PI.X), triangle2.PF.Y);
                        triangleList.Add(new Triangle(triangle2.PI, ptMedio, triangle2.PF));
                        // midPoint = ptMedio;
                        // triangleList[0].toDraw(g);
                        //triangleList = null;
                        //triangleList = new List<Triangle>();
                        // str.Add("triangle");
                        //dCount++;
                        AreaDraw.Invalidate();
                    }
                    #endregion
                    break;
                case ToolSelec.Daimond:
                    #region Daimond
                    if (daimond2.startPoint != new Point(0, 0) && daimond2.endPoint != new Point(0, 0))
                    {
                        Point pt2 = new Point(daimond2.startPoint.X - (daimond2.endPoint.X - daimond2.startPoint.X), daimond2.endPoint.Y);
                        Point pt3 = new Point(daimond2.startPoint.X, (daimond2.endPoint.Y - daimond2.startPoint.Y) + daimond2.endPoint.Y);
                        daimondList.Add(new Daimond(daimond2.startPoint, pt2, pt3, daimond2.endPoint));
                        //daimondList[0].toDraw(g);
                        //daimondList = null;
                        //daimondList = new List<Daimond>();
                        //str.Add("daimond");
                        AreaDraw.Invalidate();
                    }
                    #endregion
                    break;
                case ToolSelec.circle:
                    #region Circle
                    if (circle2.startPoint != new Point(0, 0) && circle2.endPoint != new Point(0, 0))
                    {
                        circleList.Add(new Circle(circle2.startPoint, circle2.endPoint));
                        //circleList[0].toDraw(g);
                        //circleList=null;
                        //circleList=new List<Circle>();
                        //str.Add("circle");
                        AreaDraw.Invalidate();
                    }
                    #endregion
                    break;
                case ToolSelec.rectangle:
                    #region Rectangle
                    if (rectangle2.startPoint != new Point(0, 0) && rectangle2.endPoint != new Point(0, 0))
                    {
                        rectangleList.Add(new Rectangle1(rectangle2.startPoint, rectangle2.endPoint));
                        //rectangleList[0].toDraw(g);
                        //rectangleList = null;
                        //rectangleList = new List<Rectangle1>();
                        //str.Add("rectangle");
                        AreaDraw.Invalidate();
                    }
                    #endregion
                    break;
                case ToolSelec.line:
                    #region Line
                    if (line2.startPoint != new Point(0, 0) && line2.endPoint != new Point(0, 0))
                    {
                        lineList.Add(new Line(line2.startPoint, line2.endPoint));
                        //lineList[0].toDraw(g);
                        //lineList = null;
                        //lineList = new List<Line>();
                        //str.Add("line");
                        AreaDraw.Invalidate();
                    }
                    #endregion
                    break;
                case ToolSelec.pen:
                    #region Pen

                    if (MyPen.startPoint != new Point(0, 0) && MyPen.endPoint != new Point(0, 0))
                    {
                        penList.Add(new MyPen(MyPen.startPoint, MyPen.endPoint));
                        pen2.UpPen(e);
                        str.Add("pen");
                        //AreaDraw.Invalidate();

                    }
                    #endregion
                    break;
                case ToolSelec.eraser:
                    #region Eraser

                    eraser.UpEraser(e);
                    str.Add("eraser");
                    //AreaDraw.Invalidate();
                    eraserList.Add(new Eraser(Eraser.startPoint, Eraser.endPoint));
                    //AreaDraw.Invalidate();
                    #endregion
                    break;
            }
            if (!btnRotate.Checked)
            {
                triangle1 = null;
                daimond1 = null;
                circle1 = null;
                rectangle1 = null;
                line1 = null;
            }
            clic = false;

        }

        #endregion

        #region Paint Event

        private void AreaDraw_Paint_1(object sender, PaintEventArgs e)
        {
            if (btnZoom.Checked)
            {
                e.Graphics.ScaleTransform(zoom, zoom);
            }
            
            #region Add shapes in Temp List

            
            switch (toolselec)
            {
                case ToolSelec.Triangle:
                    Point ptMedio = new Point(triangle2.PI.X - (triangle2.PF.X - triangle2.PI.X), triangle2.PF.Y);
                    tList.Add(new Triangle(triangle2.PI, ptMedio, triangle2.PF));
                    break;
                case ToolSelec.Daimond:
                    Point pt2 = new Point(daimond2.startPoint.X - (daimond2.endPoint.X - daimond2.startPoint.X), daimond2.endPoint.Y);
                    Point pt3 = new Point(daimond2.startPoint.X, (daimond2.endPoint.Y - daimond2.startPoint.Y) + daimond2.endPoint.Y);
                    dList.Add(new Daimond(daimond2.startPoint, pt2, pt3, daimond2.endPoint));
                    break;
                case ToolSelec.circle:
                    cList.Add(new Circle(circle2.startPoint, circle2.endPoint));
                    break;
                case ToolSelec.rectangle:
                    rList.Add(new Rectangle1(rectangle2.startPoint, rectangle2.endPoint));
                    break;
                case ToolSelec.line:
                    lList.Add(new Line(line2.startPoint, line2.endPoint));
                    break;
            }

            #endregion
            #region draw shapes temporarly
            foreach (Line item in lList)
            {

                item.toDraw(e.Graphics);
            }
            foreach (Triangle item in tList)
            {

                item.toDraw(e.Graphics);

            }
            foreach (Circle item in cList)
            {
                item.toDraw(e.Graphics);
            }
            foreach (Daimond item in dList)
            {

                item.toDraw(e.Graphics);
            }

            foreach (Rectangle1 item in rList)
            {

                item.toDraw(e.Graphics);
            }

            #endregion

            #region Draw shapes Parmanantly
            //=========================Parmanant Shapes============

            foreach (Circle item in circleList)
            {
                item.toDraw(e.Graphics);
            }
            foreach (Daimond item in daimondList)
            {

                item.toDraw(e.Graphics);
            }

            foreach (Rectangle1 item in rectangleList)
            {

                item.toDraw(e.Graphics);
            }


            foreach (Line item in lineList)
            {

                item.toDraw(e.Graphics);
            }

            foreach (Triangle item in triangleList)
            {

                item.toDraw(e.Graphics);
            }


            pen2.toDraw(e.Graphics);
            eraser.toDraw(e.Graphics);

            // console.writeline("eraser");
           
            // eraser.toDraw(e.Graphics);
            #endregion

            #region Null and create temp lists
            tList = null;
            tList = new List<Triangle>();
            dList = null;
            dList = new List<Daimond>();
            lList = null;
            lList = new List<Line>();
            rList = null;
            rList = new List<Rectangle1>();
            cList = null;
            cList = new List<Circle>();
            #endregion


        }

        #endregion

        #region Kinect Code
        public void SetupSensor()
        {
            //  Console.WriteLine("im here2");

            if (KinectSensor.KinectSensors.Count > 0)
            {

                sensor = KinectSensor.KinectSensors[0];
                //KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;


            }



            var parameters = new TransformSmoothParameters
            {
                Smoothing = 0.3f,
                Correction = 0.1f,
                Prediction = 0.1f,
                JitterRadius = 1.0f,
                MaxDeviationRadius = 0.5f
            };
         //   sensor.ColorStream.Enable(ColorImageFormat.RgbResolution1280x960Fps12);
            // this.colorPixels = new byte[this.sensor.ColorStream.FramePixelDataLength];
            sensor.DepthStream.Enable();

            sensor.SkeletonStream.Enable(parameters);
            // sensor.MaxElevationAngle = 21; 
            //sensor.DepthFrameReady += sensor_DepthFrameReady;






            //sensor.ColorFrameReady += sensor_ColorFrameReady;
            sensor.AllFramesReady += sensor_AllFramesReady;
            sensor.Start();
            //float an = sensor.ElevationAngle;
            // Console.WriteLine(an);
        }







        void sensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            // Console.WriteLine("im here2");

            //using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            ////{
            //   if (colorFrame != null)
            //    {
            ////        // Copy the pixel data from the image to a temporary array
            ////        //colorFrame.CopyPixelDataTo(this.colorPixels);
            //    //  AreaDraw.BackgroundImage = colorFrame.ToBitmap();
            ////        // Write the pixel data into our bitmap

            //   }
            //}


            using (var frame = e.OpenSkeletonFrame())
            {


                if (frame != null)
                {


                    var skeletons = new Skeleton[frame.SkeletonArrayLength];
                    frame.CopySkeletonDataTo(skeletons);
                   // if (skeletons.Count() == 1)
                    //{
                        //if (skeletons != null)
                        //{
                       

                        // }



                        var tracked = skeletons.Where(s => s.TrackingState == SkeletonTrackingState.Tracked).OrderBy(skeleton => skeleton.Position.Z).FirstOrDefault();


                        if (tracked != null)
                        {
                            var wristRight = tracked.Joints[JointType.WristRight];


                            //lblStatus.Text = "Got Somebody";
                            //  var pos =wristRight.ScaleTo(1920,1080,SkeletonMaxX,SkeletonMaxY).Position;
                            var leftt = tracked.Joints[JointType.HandLeft];
                            var right = tracked.Joints[JointType.HandRight];
                            var pos = right.ScaleTo(AreaDraw.Width, AreaDraw.Height, SkeletonMaxX, SkeletonMaxY).Position;
                            var spine = tracked.Joints[JointType.Spine];
                            // ------ label18.Show();
                            // label18.Text = spine.Position.Z.ToString();
                            var top = (int)(pos.X);
                            var left = (int)(pos.Y);
                            label18.Text = spine.Position.X.ToString();
                            //
                          //label3.Text = spine.Position.Z.ToString();
                            // var x = right.Position.X * 1000;
                            //var actX = (int)x;
                            //var y = right.Position.Y * 1000;
                            //var actY = (int)y;

                            if ((spine.Position.Z > 2.0 && spine.Position.Z < 2.5) && (spine.Position.X < -0.29))
                            {
                                label1.Hide();
                                label1.SendToBack();

                                // lblStatus.Text = actX.ToString() + " " + actY.ToString();


                                MouseEvents.SetCursorPos(top, left);

                                //if (!isShown)
                                //{
                                //    Cursor.Clip = new Rectangle(AreaDraw.Location, AreaDraw.Size);
                                //}
                                //
                                // Console.WriteLine(AreaDraw.Size);

                                if (leftt.Position.Z < tracked.Joints[JointType.Spine].Position.Z - 0.3 && right.Position.Z > tracked.Joints[JointType.Spine].Position.Z - 0.18)                // detecting left hand depth to change color
                                {
                                    using (DepthImageFrame depthImagePara = e.OpenDepthImageFrame())
                                    {
                                        //  Console.WriteLine("im here1");
                                        if (depthImagePara != null)
                                        {
                                            //Console.WriteLine("im here2");


                                            //Console.WriteLine("im here3 in Z");
                                            //Joint joint = sd.Joints[JointType.HandLeft];
                                            DepthImagePoint depthPoint;
                                            depthPoint = depthImagePara.MapFromSkeletonPoint(leftt.Position);
                                            Point point = new Point((int)(AreaDraw.Width * depthPoint.X / depthImagePara.Width), (int)(AreaDraw.Height * depthPoint.Y / depthImagePara.Height));
                                            //label1.Text = string.Format(" X:{0:0.00} Y:{1:0.00} Z:{2:0.00}m", point.X, point.Y, (leftt.Position.Z * 100));
                                            float c = (leftt.Position.Z * 39);
                                            int cl = (int)c;
                                            //                                    label2.Text = c.ToString();

                                            if (leftt.Position.Y > tracked.Joints[JointType.Spine].Position.Y + 0.15)
                                            {
                                                if (cl % 8 == 0 && !btnEraser.Checked)
                                                {
                                                    //countClr = 0;
                                                    if (countClr == 0)
                                                    {
                                                        int r = rnd.Next(0, 255);
                                                        int g = rnd.Next(0, 255);
                                                        int b = rnd.Next(0, 255);
                                                        // pn.Color = Color.FromArgb(r, g, b);
                                                        uPen.Color = Color.FromArgb(r, g, b);
                                                        panel1.BackColor = Color.FromArgb(r, g, b);
                                                        //radialMenu1.MenuColor = Color.FromArgb(r, g, b);
                                                        fBrush = new SolidBrush(Color.FromArgb(r, g, b));
                                                        countClr++;
                                                    }



                                                    //if (cl % 8 == 0 && fillColor.Checked)
                                                    //{
                                                    //    //countClr = 0;
                                                    //    if (countClr == 0)
                                                    //    {
                                                    //        int r = rnd.Next(0, 255);
                                                    //        int g = rnd.Next(0, 255);
                                                    //        int b = rnd.Next(0, 255);
                                                    //        // pn.Color = Color.FromArgb(r, g, b);

                                                    //        pictureBox2.BackColor = Color.FromArgb(r, g, b);
                                                    //        panel1.BackColor = Color.FromArgb(r, g, b);
                                                    //        countClr++;
                                                    //    }
                                                    //    else if(fillColor.Checked){ countClr = 0; }
                                                    //}
                                                }
                                                else { countClr = 0; }
                                            }
                                        }

                                    }
                                }
                                //Changing Pen width by calculating distance between both hand

                                if (leftt.Position.Z < tracked.Joints[JointType.Spine].Position.Z - 0.4 && leftt.Position.Z > tracked.Joints[JointType.Spine].Position.Z - 0.5 && right.Position.Z < tracked.Joints[JointType.Spine].Position.Z - 0.4 && right.Position.Z > tracked.Joints[JointType.Spine].Position.Z - 0.5 &&
                                    (btnPen.Checked || btnEraser.Checked || btnZoom.Checked))
                                {
                                    using (DepthImageFrame depthImagePara = e.OpenDepthImageFrame())
                                    {
                                        MouseEvents.LeftClickUp();
                                        if (depthImagePara != null && !btnRotate.Checked)
                                        {



                                            DepthImagePoint depthPoint;
                                            depthPoint = depthImagePara.MapFromSkeletonPoint(right.Position);
                                            Point pointX = new Point((int)(AreaDraw.Width * depthPoint.X / depthImagePara.Width), (int)(AreaDraw.Height * depthPoint.Y / depthImagePara.Height));
                                            x1 = pointX.X;
                                            y1 = pointX.Y;
                                            depthPoint = depthImagePara.MapFromSkeletonPoint(leftt.Position);
                                            Point pointY = new Point((int)(AreaDraw.Width * depthPoint.X / depthImagePara.Width), (int)(AreaDraw.Height * depthPoint.Y / depthImagePara.Height));
                                            x2 = pointY.X;
                                            y2 = pointY.Y;

                                            //Console.WriteLine("Coordinats are: " + x1 + " " + x2 + " " + y1 + " " + y2);
                                            double xd = Math.Sqrt(Math.Pow(x1 - x2, 2));
                                            double yd = Math.Sqrt(Math.Pow(y1 - y2, 2));
                                            double dis = xd + yd;
                                            dist = (int)dis;

                                            int wdh = (int)dist / 100;
                                            label3.Text = wdh.ToString();
                                            if (wdh > 7) { wdh = 7; }
                                            else if (wdh < 1) { wdh = 1; }
                                            label3.Text = wdh.ToString();
                                            if (!btnZoom.Checked)
                                            {
                                                trackBar1.Value = 10 + wdh;
                                                uPen.Width = 10 + wdh;
                                            }
                                            else if(btnZoom.Checked)
                                            {
                                               // Console.WriteLine("in zoom");

                                                trackBar2.Value = wdh;
                                               // AreaDraw.Invalidate();
                                            }
                                            // TLabel.Text = wdh.ToString();

                                            
                                            // label3.Text = "WIDTH";
                                            // label2.Text = p.Width.ToString();
                                        }
                                        // else label3.Text = "GONE";
                                        //if (depthImagePara != null && btnRotate.Checked)
                                        //{


                                        //    label3.Text = "ROTATE";
                                        //    DepthImagePoint depthPoint;
                                        //    depthPoint = depthImagePara.MapFromSkeletonPoint(right.Position);
                                        //    Point pointX = new Point((int)(AreaDraw.Width * depthPoint.X / depthImagePara.Width), (int)(AreaDraw.Height * depthPoint.Y / depthImagePara.Height));
                                        //    x1 = pointX.X;
                                        //    y1 = pointX.Y;
                                        //    depthPoint = depthImagePara.MapFromSkeletonPoint(leftt.Position);
                                        //    Point pointY = new Point((int)(AreaDraw.Width * depthPoint.X / depthImagePara.Width), (int)(AreaDraw.Height * depthPoint.Y / depthImagePara.Height));
                                        //    x2 = pointY.X;
                                        //    y2 = pointY.Y;

                                        //    //Console.WriteLine("Coordinats are: " + x1 + " " + x2 + " " + y1 + " " + y2);
                                        //    double xd = Math.Sqrt(Math.Pow(x1 - x2, 2));
                                        //    double yd = Math.Sqrt(Math.Pow(y1 - y2, 2));
                                        //    double dis = xd + yd;
                                        //    dist = (int)dis;

                                        //    //int wdh = (int)dist / 100;

                                        //    //    if (wdh > 10) { wdh = 10; }
                                        //    //    if (wdh < 1) { wdh = 1; }
                                        //    //    trackBar1.Value = wdh;
                                        //    //    // TLabel.Text = wdh.ToString();
                                        //    //    uPen.Width = wdh;

                                        //    // label2.Text = p.Width.ToString();
                                        //}
                                    }
                                }
                                try
                                {
                                    // skeletonFrame.CopySkeletonDataTo(_skeletons);
                                    var accelerometerReading = sensor.AccelerometerGetCurrentReading();
                                    _interactionStream.ProcessSkeleton(skeletons, accelerometerReading, frame.Timestamp);
                                }
                                catch (InvalidOperationException)
                                {
                                    // SkeletonFrame functions may throw when the sensor gets
                                    // into a bad state.  Ignore the frame in that case.
                                }
                                ProcessGesture(tracked.Joints[JointType.Head], leftt, right, tracked.Joints[JointType.ShoulderLeft], tracked.Joints[JointType.ShoulderRight], tracked.Joints[JointType.Spine]);
                            }//position if
                            else if (spine.Position.Z < 2.0)
                            {
                                label1.BringToFront();
                                label1.Show();

                                label1.Text = "You Are Not In Range Move Back";

                            }
                            else if (spine.Position.Z > 2.5)
                            {
                                label1.BringToFront();
                                label1.Show();

                                label1.Text = "You Are Not In Range Move Forward";

                            }
                            else if (spine.Position.X > -0.29)
                            {
                                label1.BringToFront();
                                label1.Show();

                                label1.Text = "You Are Not In Range Move to Left";

                            }
                            //else if (spine.Position.X < -0.36)
                            //{
                            //    label1.BringToFront();
                            //    label1.Show();

                            //    label1.Text = "You Are Not In Range Move to Right";

                            //}
                        } //track if
                        else
                        {
                            label1.BringToFront();
                            label1.Show();
                            label1.Text = " No Human Found";
                        }
                   // } // count if
                    //else
                    //{
                    //    label1.Text = " Decide who wants to draw frist";
                    //}
                    ////try
                    //{
                    //    // skeletonFrame.CopySkeletonDataTo(_skeletons);
                    //    var accelerometerReading = sensor.AccelerometerGetCurrentReading();
                    //    _interactionStream.ProcessSkeleton(skeletons, accelerometerReading, frame.Timestamp);
                    //}
                    //catch (InvalidOperationException)
                    //{ }
                }
                else
                {
                    label1.BringToFront();
                    label1.Show();
                    label1.Text = " No Human Found";
                }
            }
            using (var frame = e.OpenDepthImageFrame())
            {

                if (frame == null)
                    return;


                // pb.Image = frame.ToBitmap();
                try
                {
                    _interactionStream.ProcessDepth(frame.GetRawPixelData(), frame.Timestamp);
                }
                catch (InvalidOperationException)
                {
                    // DepthFrame functions may throw when the sensor gets
                    // into a bad state.  Ignore the frame in that case.

                }


            }

            ////  Thread.Sleep()

        }

        private void ProcessGesture(Joint head, Joint handleft, Joint handright, Joint shoulderLeft, Joint shoulderRight, Joint spine)   //Processing different gestures
        {

            if (handleft.Position.Y > head.Position.Y)                                                                  //Selection
            {

                //  MouseEvents.LeftClick();

                clearAll();
            }
            
            //else{MouseEvents.LeftClickUp();}
            // else if (handleft.Position.Y < hip.Position.Y) { if (!handup) { MouseEvents.LeftClickUp(); handup = true; } }

            if (handleft.Position.X < shoulderLeft.Position.X - 0.4)                                                                            //Clear All
            {
                //clearAll();
            }

            else if (handright.Position.Y > head.Position.Y + 0.3)                                                                 //ToolBox
            {

              // toolStrip.Location = new Point(200,0);
                toolStrip.Show();
                //// pictureBox1.Scale(new SizeF(pictureBox1.Width + 100, pictureBox1.Height + 100));
                Cursor.Clip = new Rectangle(toolStrip.Location, toolStrip.Size);
                // Cursor.Position = new Point(900, 506);
                //Rectangle r = new Rectangle(700, 350, 350, 300);
                //Cursor.Hide();

                //Cursor.Position = new Point(900, 506);
               // radialMenu1.ShowPopup(Cursor.Position, true);
               // Rectangle r = new Rectangle(700, 220, 400, 350);

                if (btnZoom.Checked)
                { Cursor.Show(); }// isCursorHide = true; }

               
              //  Cursor.Clip = new Rectangle(r.Location, r.Size); 
            }
            else if (handleft.Position.Z < spine.Position.Z - 0.3 && fillColor.Checked && handright.Position.Z < spine.Position.Z - 0.3)                                     //Fill Color
            {
                MouseEvents.LeftClick();
                MouseEvents.LeftClickUp();
                //Console.WriteLine(fillColor.Checked);
                label2.Show();
                label2.Text = "fill";

            }
            // else { label2.Text = ""; }

            //if (handleft.Position.Z < spine.Position.Z - 0.3 && handright.Position.Z < spine.Position.Z - 0.2 && toolTriangle.Checked)                                     //Draw Shape
            //{
            //    MouseEvents.LeftClick();
            //}
            //else if(toolTriangle.Checked){ MouseEvents.LeftClickUp(); }


            //if (handright.Position.Z < spine.Position.Z - 0.3 && toolPuntero.Checked && handright.Position.Y > spine.Position.Y + 0.15)                                     //Move Shape
            //{
            //    MouseEvents.LeftClick();

            //}
            //else if(toolPuntero.Checked){ MouseEvents.LeftClickUp(); }

        }
        #endregion
        
        private void button1_Click(object sender, EventArgs e)
        {


            toolStrip.Show();
            // g.ScaleTransform(50 ,50);
            //AreaDraw.Scale(new Size(2000, 900));
            //pen2.zoom(g);
           // toolStrip.Location = Cursor.Position;
            // pictureBox1.Scale(new SizeF(pictureBox1.Width + 100, pictureBox1.Height + 100));
            Cursor.Clip = new Rectangle(toolStrip.Location, toolStrip.Size);
          //  Cursor.Position = new Point(900, 506);
            //Rectangle r = new Rectangle(700, 220, 400, 350);
            
            //graphics.DrawEllipse(System.Drawing.Pens.Black, rectangle);
           // Cursor.Hide();
            // pictureBox1.Resize += pictureBox1_Resize;
            //  g.ScaleTransform(9, 1);
            //AreaDraw.Invalidate();
            // rect.p.Color = Color.Purple;
           // radialMenu1.ShowPopup(Cursor.Position, true);
           // Cursor.Clip = new Rectangle(r.Location, r.Size);



        }

        #region Radial Clicks
        private void barLargeButtonItem1_ItemPress(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cursor.Show();
            btnLine.Checked = false;
            btnCircle.Checked = false;
            btnZoom.Checked = false;
            btnRectangle.Checked = false;
            toolDaimond.Checked = false;
            toolPuntero.Checked = false;
            toolTriangle.Checked = false;
            btnPen.Checked = true;
            btnRotate.Checked = false;
            btnEraser.Checked = false;
            btnCreateNewFile.Checked = false;
            fillColor.Checked = false;
            btnClearAll.Checked = false;
            btnSave.Checked = false;

            // isShown = false;
            //MessageBox.Show("Eraser");
            //btnEraser.BackColor = Color.Yellow;
            toolselec = ToolSelec.pen;
            // label1.Text = "Eraser";
            //toolStrip.Hide();

            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
            pictureBox1.BackgroundImage = Properties.Resources.penbkk;
            //Cursor.Clip = new Rectangle(AreaDraw.Location, AreaDraw.Size);
            AreaDraw.Cursor = new Cursor(Properties.Resources.brush.GetHicon());
            //radialMenu1.HidePopup();
            MouseEvents.LeftClickUp();
            isCursorHide = false;
        }

        private void barLargeButtonItem3_ItemPress(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnLine.Checked = false;
            btnCircle.Checked = false;
            btnZoom.Checked = false;
            btnRectangle.Checked = false;
            toolDaimond.Checked = false;
            toolPuntero.Checked = false;
            toolTriangle.Checked = false;
            btnPen.Checked = false;
            btnCreateNewFile.Checked = false;
            fillColor.Checked = false;
            btnClearAll.Checked = false;
            btnSave.Checked = false;
            btnRotate.Checked = false;
            btnEraser.Checked = true;
            //  isShown = false;
            //MessageBox.Show("Eraser");
            //btnEraser.BackColor = Color.Yellow;
            toolselec = ToolSelec.eraser;
            // label1.Text = "Eraser";
            //toolStrip.Hide();
            Cursor.Show();
            // Console.WriteLine("Working till here");
            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
            pictureBox1.BackgroundImage = Properties.Resources.eraserbkk;
            //Cursor.Clip = new Rectangle(AreaDraw.Location, AreaDraw.Size);
            AreaDraw.Cursor = new Cursor(Properties.Resources.ersr.GetHicon());
           // radialMenu1.HidePopup();
            MouseEvents.LeftClickUp();
            isCursorHide = false;
            // barLargeButtonItem1.ItemAppearance.Changed += ItemAppearance_Changed;
        }


        private void barLargeButtonItem5_ItemPress(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //clear All

            btnLine.Checked = false;
            btnZoom.Checked = false;
            btnCircle.Checked = false;
            btnRectangle.Checked = false;
            toolDaimond.Checked = false;
            toolPuntero.Checked = false;
            toolTriangle.Checked = false;
            btnPen.Checked = false;
            btnCreateNewFile.Checked = false;
            fillColor.Checked = false;
            btnClearAll.Checked = true;
            btnSave.Checked = false;
            btnRotate.Checked = false;
            btnEraser.Checked = false;
            //  isShown = false;
            //MessageBox.Show("Eraser");
            //btnEraser.BackColor = Color.Yellow;
            //  toolselec = ToolSelec.clea;
            // label1.Text = "Eraser";
            //toolStrip.Hide();
            Cursor.Show();
            // Console.WriteLine("Working till here");
            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
            pictureBox1.BackgroundImage = Properties.Resources.eraserbkk;
            //Cursor.Clip = new Rectangle(AreaDraw.Location, AreaDraw.Size);
            AreaDraw.Cursor = new Cursor(Properties.Resources.brush.GetHicon());
            //radialMenu1.HidePopup();
            MouseEvents.LeftClickUp();
            isCursorHide = false;

            clearAll();
        }
                                                          
        private void barLargeButtonItem6_ItemPress(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            pictureBox1.BackgroundImage = Properties.Resources.lnbkk;
            btnPen.Checked = false;
            btnZoom.Checked = false;
            btnCircle.Checked = false;
            btnRectangle.Checked = false;
            toolDaimond.Checked = false;
            toolPuntero.Checked = false;
            toolTriangle.Checked = false;
            btnEraser.Checked = false;            btnLine.Checked = true;
            btnCreateNewFile.Checked = false;
            fillColor.Checked = false;
            btnClearAll.Checked = false;
            btnSave.Checked = false;
            btnRotate.Checked = false;
            toolselec = ToolSelec.line;
            AreaDraw.Cursor = new Cursor(Properties.Resources.c.GetHicon());
            //label1.Text = "Line";
            //toolStrip.Hide();
            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
            Cursor.Show();
            MouseEvents.LeftClickUp();
            isCursorHide = false;
            //radialMenu1.HidePopup();

            //Cursor.Clip = new Rectangle(AreaDraw.Location, AreaDraw.Size);

        }

        private void barLargeButtonItem7_ItemPress(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            pictureBox1.BackgroundImage = Properties.Resources.tribkk;
//            radialMenu1.HidePopup();
            btnEraser.Checked = false;
            btnPen.Checked = false;
            btnLine.Checked = false;
            btnCircle.Checked = false;
            btnRectangle.Checked = false;
            toolPuntero.Checked = false;
            toolTriangle.Checked = true;
            btnCreateNewFile.Checked = false;
            fillColor.Checked = false;
            btnClearAll.Checked = false;
            btnSave.Checked = false;
            btnRotate.Checked = false;
            toolselec = ToolSelec.Triangle;
            //label1.Text = "Triangle"; 
            AreaDraw.Cursor = new Cursor(Properties.Resources.c.GetHicon());
            toolDaimond.Checked = false;
            btnZoom.Checked = false;
            //toolStrip.Hide();
            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
            Cursor.Show();
            MouseEvents.LeftClickUp();
            isCursorHide = false;





        }

        private void barLargeButtonItem8_ItemPress(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            pictureBox1.BackgroundImage = Properties.Resources.sqrbkk;
            btnPen.Checked = false;
            btnLine.Checked = false;
            btnCircle.Checked = false;
            toolDaimond.Checked = false;
            btnEraser.Checked = false;
            toolPuntero.Checked = false;
            btnZoom.Checked = false;
            toolTriangle.Checked = false;
            btnRectangle.Checked = true;
            btnCreateNewFile.Checked = false;
            fillColor.Checked = false;
            btnClearAll.Checked = false;
            btnSave.Checked = false;
            btnRotate.Checked = false;
            AreaDraw.Cursor = new Cursor(Properties.Resources.c.GetHicon());
            //label1.Text = "Rectangle";
            //toolStrip.Hide();
            Cursor.Show();
            //  MessageBox.Show(pn.Width.ToString());
            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
            toolselec = ToolSelec.rectangle;
  //          radialMenu1.HidePopup();
            MouseEvents.LeftClickUp();
            isCursorHide = false;
        }

        private void barLargeButtonItem9_ItemPress(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            pictureBox1.BackgroundImage = Properties.Resources.dimondbkk;
            btnPen.Checked = false;
            btnLine.Checked = false;
            btnCircle.Checked = false;
            btnEraser.Checked = false;
            btnRectangle.Checked = false;
            toolPuntero.Checked = false;
            toolTriangle.Checked = false;
            toolDaimond.Checked = true;
            btnZoom.Checked = false;
            btnCreateNewFile.Checked = false;
            fillColor.Checked = false;
            btnRotate.Checked = false;
            btnClearAll.Checked = false;
            btnSave.Checked = false;
            AreaDraw.Cursor = new Cursor(Properties.Resources.c.GetHicon());
            toolselec = ToolSelec.Daimond;
            //label1.Text = "Diamond";
            // toolStrip.Hide();
            Cursor.Show();
            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
    //        radialMenu1.HidePopup();

        }

        private void barLargeButtonItem10_ItemPress(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            pictureBox1.BackgroundImage = Properties.Resources.circlebkk; //circle
            btnPen.Checked = false;
            btnZoom.Checked = false;
            btnLine.Checked = false;
            btnRectangle.Checked = false;
            toolDaimond.Checked = false;
            toolPuntero.Checked = false;
            toolTriangle.Checked = false;
            btnEraser.Checked = false;
            btnCircle.Checked = true;
            btnRotate.Checked = false;
            btnCreateNewFile.Checked = false;
            fillColor.Checked = false;
            btnClearAll.Checked = false;
            btnSave.Checked = false;
            toolselec = ToolSelec.circle;
            AreaDraw.Cursor = new Cursor(Properties.Resources.c.GetHicon());
            // label1.Text = "Circle";
            // toolStrip.Hide();
            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
            Cursor.Show();
      //      radialMenu1.HidePopup();
            MouseEvents.LeftClickUp();
            isCursorHide = false;

        }

        private void barLargeButtonItem11_ItemPress(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//rotate
            pictureBox1.BackgroundImage = Properties.Resources.rotateLbkk;
            btnPen.Checked = false;
            btnLine.Checked = false;
            btnRectangle.Checked = false;
            toolDaimond.Checked = false;
            btnZoom.Checked = false;
            toolPuntero.Checked = true;
            toolTriangle.Checked = false;
            btnEraser.Checked = false;
            btnCircle.Checked = false;
            btnRotate.Checked = true;
            btnCreateNewFile.Checked = false;
            fillColor.Checked = false;
            btnClearAll.Checked = false;
            btnSave.Checked = false;
            toolselec = ToolSelec.puntero;
            AreaDraw.Cursor = new Cursor(Properties.Resources.c.GetHicon());
            // label1.Text = "Circle";
            // toolStrip.Hide();
            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
            Cursor.Show();
        //    radialMenu1.HidePopup();
            MouseEvents.LeftClickUp();
            isCursorHide = false;

        }

        private void barLargeButtonItem12_ItemPress(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //zoom
            pictureBox1.BackgroundImage = Properties.Resources.zoombkk;
            btnPen.Checked = false;
            btnLine.Checked = false;
            btnRectangle.Checked = false;
            toolDaimond.Checked = false;
            btnZoom.Checked = true;
            toolPuntero.Checked = false;
            toolTriangle.Checked = false;
            btnEraser.Checked = false;
            btnCircle.Checked = false;
            btnRotate.Checked = false;
            btnCreateNewFile.Checked = false;
            fillColor.Checked = false;
            btnClearAll.Checked = false;
            btnSave.Checked = false;
            toolselec = ToolSelec.puntero;
            AreaDraw.Cursor = new Cursor(Properties.Resources.zoombkk.GetHicon());
            // label1.Text = "Circle";
            // toolStrip.Hide();
            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
            Cursor.Show();
          //  radialMenu1.HidePopup();
            MouseEvents.LeftClickUp();
            isCursorHide = false;
        }

        private void barLargeButtonItem13_ItemPress(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnPen.Checked = false;
            btnEraser.Checked = false;
            btnLine.Checked = false;
            btnCircle.Checked = false;
            btnRectangle.Checked = false;
            toolTriangle.Checked = false;
            toolDaimond.Checked = false;
            //tnChangeColor.Checked = false;
            fillColor.Checked = false;
            btnCreateNewFile.Checked = false;
            btnClearAll.Checked = false;
            btnSave.Checked = false;
            btnZoom.Checked = false;
            btnRotate.Checked = false;
            toolPuntero.Checked = true;
            Cursor.Show();
            toolselec = ToolSelec.puntero;
            AreaDraw.Cursor = new Cursor(Properties.Resources.cursor1.GetHicon());
            pictureBox1.BackgroundImage = Properties.Resources.selectbkk; //circle
            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
            //radialMenu1.HidePopup();
            MouseEvents.LeftClickUp();
            isCursorHide = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void barLargeButtonItem16_ItemPress(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //radialMenu1.HidePopup();
            Cursor.Clip = new Rectangle(AreaDraw.Location, AreaDraw.Size);
            Cursor.Show();
            //MouseEvents.LeftClickUp();
            isCursorHide = false;
            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //resize = true;
            // AreaDraw.Invalidate();
            //  fBrush = new SolidBrush(Color.Yellow);
            //  uPen.Color = Color.Brown;
            //Cursor.Clip =  new Rectangle(Cursor.Position.X, Cursor.Position.Y, 1, 1);
            //triangle1.rotate(rotX,rotY);
            //zoom++; 
            uPen.Color = Color.Aqua;
            //AreaDraw.Invalidate();
        }

        private void barLargeButtonItem15_ItemPress(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //save
            //radialMenu1.HidePopup();
            btnPen.Checked = false;
            btnEraser.Checked = false;
            btnLine.Checked = false;
            btnCircle.Checked = false;
            btnRectangle.Checked = false;
            toolTriangle.Checked = false;
            toolDaimond.Checked = false;
            // btnChangeColor.Checked = false;
            fillColor.Checked = false;
            btnClearAll.Checked = false;
            btnRotate.Checked = false;
            toolPuntero.Checked = false;
            btnClearAll.Checked = false;
            btnCreateNewFile.Checked = false;
            btnSave.Checked = true;
            toolselec = ToolSelec.save;
            isCursorHide = false;
            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
            Cursor.Show();

            //MouseEvents.LeftClickUp();
            saveFile();
        }

        private void barLargeButtonItem14_ItemPress(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //new

            toolselec = ToolSelec.createNewFile;

            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
            Cursor.Show();
            isCursorHide = false;
            //radialMenu1.HidePopup();
            MouseEvents.LeftClickUp();
            count++;
            createNewFile();
            //  btnPen.Checked = true;
        }
        #region Save
        public void saveFile()
        {


            #region save as
            Stream mystream;
            string location;
            SaveFileDialog savefiledialog1 = new SaveFileDialog();

            //savefiledialog1.Filter = "Image files (*.PNG)|*.PNG|all files (*.*)|*.*";
            //savefiledialog1.FilterIndex = 2;
            //savefiledialog1.RestoreDirectory = true;


            //if (savefiledialog1.ShowDialog() == DialogResult.OK)
            //{

            //    if ((mystream = savefiledialog1.OpenFile()) != null)
            //    {
            //        int width = AreaDraw.Size.Width;
            //        int height = AreaDraw.Size.Height;
            //        Bitmap bm = new Bitmap(width, height);

            //        location = savefiledialog1.FileName;
            //        // Console.WriteLine("hello " + location);
            //        AreaDraw.DrawToBitmap(bm, new Rectangle(0, 0, width, height));
            //        bm.Save(@savefiledialog1.FileName + ".png", ImageFormat.Png);
            //    }
            //}
            #endregion
            int width = AreaDraw.Size.Width;
            int height = AreaDraw.Size.Height;

            Bitmap bm = new Bitmap(width, height);
            AreaDraw.DrawToBitmap(bm, new Rectangle(0, 0, width, height));

            bm.Save(@"F:\" + fName + ".png", ImageFormat.Png);

        }

        #endregion

        #region Create New File
        public void createNewFile()
        {
            //AreaDraw = new PictureBox();
            clearAll();
        }
        #endregion


        #region ClearAll
        public void clearAll()
        {
            //var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
            //                          "Confirm Delete!!",
            //                          MessageBoxButtons.YesNo);
            //if (confirmResult == DialogResult.Yes)
            //{
                g = AreaDraw.CreateGraphics();
                g.Clear(AreaDraw.BackColor);
                triangleList.Clear();

                daimondList.Clear();

                rectangleList.Clear();

                lineList.Clear();

                circleList.Clear();
                pen2.clearPath();
               // btnPen.Checked = true;
                //toolselec = ToolSelec.pen;
            //}
            //else
            //{

            //}


        }
        #endregion

        private void barLargeButtonItem17_ItemPress(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //fill shape
            btnPen.Checked = false;
            btnEraser.Checked = false;
            btnLine.Checked = false;
            btnCircle.Checked = false;
            btnRectangle.Checked = false;
            toolTriangle.Checked = false;
            toolDaimond.Checked = false;
            // btnChangeColor.Checked = false;
            toolPuntero.Checked = false;
            btnClearAll.Checked = false;
            btnSave.Checked = false;
            btnCreateNewFile.Checked = false;
            btnRotate.Checked = false;
            fillColor.Checked = true;
            // isChangeBrush = true;
            checkColor = true;
            fill = true;
            isCursorHide = false;
            toolselec = ToolSelec.fillColor;
            AreaDraw.Cursor = new Cursor(Properties.Resources.paint_bucket.GetHicon());
            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
            Cursor.Show();
            //radialMenu1.HidePopup();
            MouseEvents.LeftClickUp();
        }
        #endregion

        //  private Dictionary<int, InteractionHandEventType> _lastLeftHandEvents = new Dictionary<int, InteractionHandEventType>();
        private Dictionary<int, InteractionHandEventType> _lastRightHandEvents = new Dictionary<int, InteractionHandEventType>();

        private void InteractionStreamOnInteractionFrameReady(object sender, InteractionFrameReadyEventArgs args)
        {
            using (var iaf = args.OpenInteractionFrame()) //dispose as soon as possible
            {
                if (iaf == null)
                    return;
                // label3.Text = "iad";
                iaf.CopyInteractionDataTo(_userInfos);
                // iaf.Dispose();
            }

            //StringBuilder dump = new StringBuilder();

            // var hasUser = false;
            foreach (var userInfo in _userInfos)
            {
                var userID = userInfo.SkeletonTrackingId;
                // if (userID == 0)
                //     continue;

                // hasUser = true;

                var hands = userInfo.HandPointers;


                foreach (var hand in hands)
                {
                    if (hand.HandType == InteractionHandType.Right)
                    {

                        var lastHandEvents = _lastRightHandEvents;


                        if (hand.HandEventType != InteractionHandEventType.None)
                            lastHandEvents[userID] = hand.HandEventType;

                        var lastHandEvent = lastHandEvents.ContainsKey(userID)
                                                ? lastHandEvents[userID]
                                                : InteractionHandEventType.None;

                        //dump.AppendLine();
                        //  dump.AppendLine("    HandType: " + hand.HandType);
                        // dump.AppendLine("    HandEventType: " + hand.HandEventType);
                        if (lastHandEvent.ToString() == "Grip" && handup)
                        {
                            //  if(btnRotate.Checked)
                            checkGrip = "Grip";
                            //label3.Text = "Grip";
                            MouseEvents.LeftClick();
                            handup = false;
                        }
                        else if (lastHandEvent.ToString() == "GripRelease")
                        {
                            if (!handup)
                            {
                                checkGrip = "none";
                              //  label3.Text = "Grip Release";
                                MouseEvents.LeftClickUp();
                                handup = true;

                            }

                        }


                    }
                }
                // label18.Text = "user detected.";



                // if (!hasUser)
                //label18.Text = "No user detected.";
            }


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void AreaDraw_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            btnLine.Checked = false;
            btnCircle.Checked = false;
            btnRectangle.Checked = false;
            toolDaimond.Checked = false;
            toolPuntero.Checked = false;
            toolTriangle.Checked = false;
            btnEraser.Checked = false;
            btnPen.Checked = false;
            //toolselec = ToolSelec.pen;
            AreaDraw.Cursor = new Cursor(Properties.Resources.brush.GetHicon());
            toolStrip.Hide();
            Cursor.Show();
            Cursor.Clip = new Rectangle(panel2.Location, panel2.Size);
            btnZoom.Checked = true;
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                sensor.Stop();
                // nf.Close();
            }
            catch { }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            //zoom = trackBar2.Value/2;
            //AreaDraw.Invalidate();
        }

        private void trackBar2_CursorChanged(object sender, EventArgs e)
        {
           
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            zoom = trackBar2.Value;
            AreaDraw.Invalidate();
            Console.WriteLine("in omm@#####!@3");
        }

           }
}
//latest