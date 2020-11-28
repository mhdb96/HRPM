using KDABackendLibrary;
using KDABackendLibrary.Models;
using KDAKeyboardVisualizer.Analysis;
using KDASharedLibrary.DataAccess;
using KDASharedLibrary.Enums;
using KDASharedLibrary.Helpers;
using KDASharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KDAKeyboardVisualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KeystrokeData[] data;
        int minCount = 0;
        int maxCount = 0;
        Dictionary<int, Button> buttons = new Dictionary<int, Button>();
        List<Color> colors = new List<Color>();
        List<Color> colorList = new List<Color>();
        Analyzer a;
        public MainWindow()
        {
            InitializeComponent();
            GlobalConfig.InitializeConnections();
            UsersCombobox.ItemsSource = GlobalConfig.Connection.User_SelectAll();
            endDate.SelectedDate = DateTime.Now;
            startDate.SelectedDate = DateTime.Now.AddDays(-7);
        }
        void CreateColors()
        {
            colors.Add(Color.FromRgb(250, 250, 250));
            colors.Add(Color.FromRgb(255, 119, 35));
            colors.Add(Color.FromRgb(255, 0, 0));
            colors.Add(Color.FromRgb(102, 0, 16));
            //colors.Add(Color.FromRgb(112, 26, 110));
            //colors.Add(Color.FromRgb(229, 69, 91));
            //colors.Add(Color.FromRgb(255, 119, 35));
        }


        private void ActivateButtons(Byte color, Range range)
        {
            double slope = 1.0 * (299 - 0) / (range.Max - range.Min);
            foreach (var el in range.Elements)
            {
                var b = buttons[el.Item1];
                double output = 0 + slope * (el.Item2 - range.Min);
                b.ToolTip = el.Item2;
                //File.AppendAllText(@"C:\Users\mhdb9\Desktop\holdtimes.txt", $"{data[id].HoldTimes.Count}, {((KeysList)Convert.ToInt32(b.Uid)).GetDescription()}\n");
                if (color == 1)
                {
                    b.Background = new SolidColorBrush(colorList[(int)output]);
                    //b.Background = new SolidColorBrush(Color.FromRgb(150, (byte)(150 - (int)output), (byte)(150 - (int)output)));
                }
                else
                {
                    slope = 1.0 * (449 - 300) / (range.Max - range.Min);
                    output = 300 + slope * (el.Item2 - range.Min);
                    b.Background = new SolidColorBrush(colorList[(int)output]);
                }
                b.IsEnabled = true;
            }
        }

        void CreateColorPalette()
        {
            for (int j = 0; j < colors.Count-1; j++)
            {
                int count = 150;
                var start = colors[j];
                var end = colors[j + 1];
                int rMax = end.R;
                int rMin = start.R;
                int gMax = end.G;
                int gMin = start.G;
                int bMax = end.B;
                int bMin = start.B;
                for (int i = 0; i < count; i++)
                {
                    var rAverage = rMin + ((rMax - rMin) * i / count);
                    var gAverage = gMin + ((gMax - gMin) * i / count);
                    var bAverage = bMin + ((bMax - bMin) * i / count);
                    colorList.Add(Color.FromRgb((byte)rAverage, (byte)gAverage, (byte)bAverage));
                }
            }
        }


        //private void GetData()
        //{
        //    data = BinaryConnector.StaticLoad<KeystrokeData[]>(@"C:\Users\mhdb9\Desktop\data.kdf");
        //    bool first = true;
        //    for (int i = 0; i < data.Length; i++)
        //    {
        //        if(data[i] != null)
        //        {
        //            if (first)
        //            {
        //                first = false;
        //                minCount = data[i].HoldTimes.Count;
        //                maxCount = data[i].HoldTimes.Count;
        //            }
        //            else
        //            {
        //                if (data[i].HoldTimes.Count > maxCount)
        //                {
        //                    maxCount = data[i].HoldTimes.Count;
        //                }
        //                if (data[i].HoldTimes.Count < minCount)
        //                {
        //                    minCount = data[i].HoldTimes.Count;
        //                }
        //            }
        //        }
        //    }
        //}

        private void Init()
        {
            //double slope = 1.0 * (255 - 0) / (maxCount - minCount);
            foreach (var child in rows.Children)
            {
                if (child.GetType() == typeof(Grid))
                {
                    foreach (var btn in ((Grid)child).Children)
                    {
                        if (btn.GetType() == typeof(Button))
                        {
                            Button b = (Button)btn;
                            int id = (int)(KeysList)Convert.ToInt32(b.Uid);
                            b.Content = ((KeysList)Convert.ToInt32(b.Uid)).GetDescription();
                            buttons.Add(id, b);
                            b.IsEnabled = false;
                            b.Background = new SolidColorBrush(Colors.White);
                            //if (data[id] != null)
                            //{
                            //    double output = 0 + slope * (data[id].HoldTimes.Count - minCount);
                            //    b.ToolTip = data[id].HoldTimes.Count;
                            //    File.AppendAllText(@"C:\Users\mhdb9\Desktop\holdtimes.txt", $"{data[id].HoldTimes.Count}, {((KeysList)Convert.ToInt32(b.Uid)).GetDescription()}\n");
                            //    b.Background = new SolidColorBrush(Color.FromRgb(255, (byte)(255-(int)output), (byte)(255 - (int)output)));
                            //    b.IsEnabled = true;
                            //}
                        }
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //canvas.Children.Clear();
            var btn = (Button)sender;
            var h = ((UIElement)sender).RenderSize.Height / 2;
            var w = ((UIElement)sender).RenderSize.Width / 2;
            Point from = btn.TransformToAncestor(rows)
                              .Transform(new Point(0, 0));
            var d = data[Convert.ToInt32(btn.Uid)];
            if (d != null)
            {
                for (int i = 0; i < d.SeekTimes.Length; i++)
                {
                    if (d.SeekTimes[i] != null)
                    {
                        var b = buttons[i];
                        var h1 = ((UIElement)sender).RenderSize.Height / 2;
                        var w1 = ((UIElement)sender).RenderSize.Width / 2;
                        Point to = b.TransformToAncestor(rows)
                                  .Transform(new Point(0, 0));
                        Line l = new Line();
                        Panel.SetZIndex(l, 3);
                        l.X1 = from.X + w;
                        l.Y1 = from.Y - h;
                        l.X2 = to.X + w1;
                        l.Y2 = to.Y - h1;
                        l.StrokeThickness = 5;
                        l.Stroke = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                        l.ToolTip = ((KeysList)i).GetDescription();
                        //canvas.Children.Add(l);
                    }
                }
            }
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {


        }

        private void GetDataBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
            a = new Analyzer(((UserModel)UsersCombobox.SelectedItem).Id, (DateTime)startDate.SelectedDate, (DateTime)endDate.SelectedDate);
            CreateColors();


            // Create a vertical linear gradient with four stops.
            LinearGradientBrush myVerticalGradient =
                new LinearGradientBrush();
            myVerticalGradient.StartPoint = new Point(0.5, 0);
            myVerticalGradient.EndPoint = new Point(0.5, 1);
            myVerticalGradient.GradientStops.Add(
                new GradientStop(colors[0], 1.0));
            myVerticalGradient.GradientStops.Add(
                new GradientStop(colors[1], 0.75));
            myVerticalGradient.GradientStops.Add(
                new GradientStop(colors[2], 0.50));
            myVerticalGradient.GradientStops.Add(
                new GradientStop(colors[3], 0.0));

            // Use the brush to paint the rectangle.
            scale.Fill = myVerticalGradient;
            top.Text = "- "+ a.UpRange.Max.ToString();
            down.Text = "- " + a.InRange.Min.ToString();

            Init();
            CreateColorPalette();
            ActivateButtons(1, a.InRange);
            ActivateButtons(2, a.UpRange);
        }

        private void ClearAll()
        {
            minCount = 0;
            maxCount = 0;
            buttons = new Dictionary<int, Button>();
            colors = new List<Color>();
            colorList = new List<Color>();
        }
    }
}
