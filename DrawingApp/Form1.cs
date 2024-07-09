using System;
using System.Drawing;
using System.Windows.Forms;

namespace DrawingApp
{
    public partial class Form1 : Form
    {
        private bool isDrawing = false;
        private Point lastPoint;
        private Bitmap drawingBitmap;
        private Pen drawingPen = new Pen(Color.Black, 2); // Set the pen with a width of 2

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Enable double buffering for the form

            // Initialize the drawing bitmap and set it as the background image of the panel
            drawingBitmap = new Bitmap(drawingPanel.Width, drawingPanel.Height);
            drawingPanel.BackgroundImage = drawingBitmap;
            drawingPanel.BackgroundImageLayout = ImageLayout.None;

            // Add event handlers for mouse events
            drawingPanel.MouseDown += new MouseEventHandler(drawingPanel_MouseDown);
            drawingPanel.MouseMove += new MouseEventHandler(drawingPanel_MouseMove);
            drawingPanel.MouseUp += new MouseEventHandler(drawingPanel_MouseUp);
            drawingPanel.Paint += new PaintEventHandler(drawingPanel_Paint);
        }

        private void drawingPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                lastPoint = e.Location;
            }
        }

        private void drawingPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                using (Graphics g = Graphics.FromImage(drawingBitmap))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.DrawLine(drawingPen, lastPoint, e.Location);
                }
                drawingPanel.Invalidate();
                lastPoint = e.Location;
            }
        }

        private void drawingPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = false;
            }
        }

        private void drawingPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(drawingBitmap, Point.Empty);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            drawingPen.Dispose();
            drawingBitmap.Dispose();
        }
    }
}
