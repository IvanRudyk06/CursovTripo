using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public FactoryUI factoryUi { get; set; }
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public MainWindow()
        {
            factoryUi = new FactoryUI();
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Start();
        }


        public void drawFactory()
        {
                drawTechnoLine(factoryUi.Line1);
                drawTechnoLine(factoryUi.Line2);
                drawTechnoLine(factoryUi.Line3);
                drawFirstEndMachine();
                drawHelpsLines();
                Thread.Sleep(100);
        }

        public void drawHelpsLines()
        {
            for(int i = 0; i<15; i++)
            {
                Line line = new Line();
                line.X1 = factoryUi.helpedLines[i].X1;
                line.X2 = factoryUi.helpedLines[i].X2;
                line.Y1 = factoryUi.helpedLines[i].Y1;
                line.Y2 = factoryUi.helpedLines[i].Y2;
                line.StrokeThickness = 2;
                line.Stroke = new SolidColorBrush(Colors.Black);
                canvas.Children.Add(line);
            }
        }

        public void drawFirstEndMachine()
        {
            for(int i = 0; i<2; i++)
            {
                drawRectangle(factoryUi.GenrateEndMachines[i].X, factoryUi.GenrateEndMachines[i].Y,
                               50, factoryUi.GenrateEndMachines[i].Color.Color);
            }
        }

        public void drawTechnoLine(ObservableCollection<MachineUI> Line)
        {
            for (int i = 0; i < 4; i++)
            {
                drawRectangle(Line[i].X, Line[i].Y, 50, factoryUi.Line1[i].Color.Color);
                int x = Line[i].X;
                int y = Line[i].Y;
                for (int j = 0; j< Line[i].QueueDetail; j++)
                {
                    drawRectangle(x, y-20, 10, Colors.Red);
                    x += 20;
                }
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Thread.Sleep(100);
            drawFactory();
        }

        public void drawRectangle(int x, int y, int lengh, Color color)
        {
            Rectangle rect = new Rectangle();
            rect.Stroke = new SolidColorBrush(Colors.Black);
            rect.Fill = new SolidColorBrush(color);
            rect.Height = lengh;
            rect.Width = lengh;
            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
            canvas.Children.Add(rect);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
