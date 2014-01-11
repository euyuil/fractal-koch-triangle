using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FractalKochTriangle
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Point T = new Point(0.5, 0);
        private static readonly Point L = new Point(0, Math.Sqrt(3) / 2);
        private static readonly Point R = new Point(1, Math.Sqrt(3) / 2);
        private static readonly Point C = new Point(0.5, Math.Sqrt(3) / 2 - Math.Tan(Math.PI / 6) / 2);

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            DrawKochTriangle(new Point(0.5, 0.5), 0.8);
        }

        private void DrawKochTriangle(Point pos, double len, double rot = 0, int max = 5)
        {
            if (max <= 0)
            {
                return;
            }

            DrawTriangle(pos, len, rot);
            DrawTriangle(pos, len, rot + 180);
            
            var a = len / 3;
            var b = C.Y * a;

            // Draw top point
            DrawKochTriangle(new Point(pos.X, pos.Y - 2 * b), a, rot + 180, max - 1);

            // Draw top left point
            DrawKochTriangle(new Point(pos.X - a, pos.Y - b), a, rot + 180, max - 1);

            // Draw top right point
            DrawKochTriangle(new Point(pos.X + a, pos.Y - b), a, rot + 180, max - 1);

            // Draw bottom left point
            DrawKochTriangle(new Point(pos.X - a, pos.Y + b), a, rot + 180, max - 1);

            // Draw bottom right point
            DrawKochTriangle(new Point(pos.X + a, pos.Y + b), a, rot + 180, max - 1);

            // Draw bottom point
            DrawKochTriangle(new Point(pos.X, pos.Y + 2 * b), a, rot + 180, max - 1);
        }

        private void DrawTriangle(Point pos, double len, double rot)
        {
            var polyline = new Polyline
            {
                Fill = Brushes.Black
            };

            polyline.Points.Add(T);
            polyline.Points.Add(L);
            polyline.Points.Add(R);
            // polyline.Points.Add(C);

            MyCanvas.Children.Add(polyline);

            Canvas.SetLeft(polyline, pos.X - C.X);
            Canvas.SetTop(polyline, pos.Y - C.Y);

            polyline.RenderTransformOrigin = new Point(0.5, 1 - Math.Tan(Math.PI / 6) / Math.Sqrt(3));

            polyline.RenderTransform = new TransformGroup
            {
                Children = new TransformCollection
                {
                    new RotateTransform(rot),
                    new ScaleTransform(len, len),
                }
            };
        }
    }
}
