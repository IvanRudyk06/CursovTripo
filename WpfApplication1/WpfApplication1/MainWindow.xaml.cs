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
    public partial class MainWindow : Window
    {
        public Rectangle rect;

        public Line line;

        public Factory factory { get; set; }

        private TimeWork timeWork = new TimeWork(0, new int[0], new int[0,0]);

        public static ObservableCollection<String> ListResults { get; set; }


        public MainWindow()
        {
            factory = new Factory(timeWork);
            InitializeComponent();
           // dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            drawFactory();
            ListResults = new ObservableCollection<String>();
        }


        public void drawFactory()
        {
                drawTechnoLines();
                drawFirstEndMachine();
                drawHelpsLines();
        }

        public void drawHelpsLines()
        {
            for(int i = 0; i<15; i++)
            {
                line = new Line();
                line.X1 = factory.helpedLines[i].X1;
                line.X2 = factory.helpedLines[i].X2;
                line.Y1 = factory.helpedLines[i].Y1;
                line.Y2 = factory.helpedLines[i].Y2;
                line.StrokeThickness = 2;
                line.Stroke = new SolidColorBrush(Colors.Black);
                canvas.Children.Add(line);
            }
        }

        public void drawFirstEndMachine()
        {
            for(int i = 0; i<2; i++)
            {
                drawRectangle(factory.GenerateEndMachines[i].X, factory.GenerateEndMachines[i].Y,
                               50, 50, factory.GenerateEndMachines[i].Color.Color);
            }
        }

        public void drawTechnoLines()
        {
            foreach (TechnoLine tl in factory.MyTechnoLines)
            {
                foreach (Machine m in tl.Machines)
                {
                    drawRectangle(m.X, m.Y, 50, 50, m.Color.Color);
                    int x = m.X;
                    int y = m.Y;
                    for (int j = 0; j < m.Queue; j++)
                    {
                        drawRectangle(x, y - 20, 10, 10, Colors.Red);
                        x += 20;
                    }
                }
            }
        }
   

        public void drawRectangle(int x, int y, int hight, int width, Color color)
        {
            rect = new Rectangle();
            rect.Stroke = new SolidColorBrush(Colors.Black);
            rect.Fill = new SolidColorBrush(color);
            rect.Height = hight;
            rect.Width = width;
            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
            canvas.Children.Add(rect);
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {

            double timeScheduled = 0;
            int []  timeGenerate = new int[2];
            int[,] timeWorkOnTypeMachine = new int[2,4];
            try
            {
                timeScheduled = Convert.ToDouble(tbTimeWorkFactory.Text);
                timeGenerate[0] = Convert.ToInt16(tbIntervalGenerate.Text)*1000;
                timeGenerate[1] = Convert.ToInt16(tbIntervalGenerate1Dev.Text)*1000;

                timeWorkOnTypeMachine[0, 0] = Convert.ToInt16(tbMachine1.Text)*1000;
                timeWorkOnTypeMachine[1, 0] = Convert.ToInt16(tbMachine1Dev.Text)*1000;
                timeWorkOnTypeMachine[0, 1] = Convert.ToInt16(tbMachine2.Text) * 1000;
                timeWorkOnTypeMachine[1, 1] = Convert.ToInt16(tbMachine2Dev.Text) * 1000;
                timeWorkOnTypeMachine[0, 2] = Convert.ToInt16(tbMachine3.Text) * 1000;
                timeWorkOnTypeMachine[1, 2] = Convert.ToInt16(tbMachine3Dev.Text) * 1000;
                timeWorkOnTypeMachine[0, 3] = Convert.ToInt16(tbMachine4.Text) * 1000;
                timeWorkOnTypeMachine[1, 3] = Convert.ToInt16(tbMachine4Dev.Text) * 1000;

                initsialize(timeScheduled,  timeGenerate, timeWorkOnTypeMachine);
                UpdateUI();
            }
            catch 
            {
                MessageBox.Show("Введіть коректні дані");
            }
        }

        public void initsialize(double timeScheduled, int[] timeGenerate, int[,] timeWorkOnTypeMachine)
        {
            tbResults.Clear();
            button.IsEnabled = false;
            timeWork = new TimeWork((int)(timeScheduled*1000), timeGenerate, timeWorkOnTypeMachine);
            timeWork.TimeActual = 0;
            
            factory = new Factory(timeWork);
            factory.startWork();
            ListResults.Clear();
        }

        public void refreshResults()
        {
            tbResults.Clear();
            
            for(int i = 0; i< ListResults.Count; i++)
            {
                tbResults.Text += ListResults[i]+ Environment.NewLine;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            timeWork.TimeScheduledMiliSecond = 0;
            factory = new Factory(timeWork);
            button.IsEnabled = true;
            ListResults.Clear();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            timeWork.TimeActual += timeWork.TimeScheduledMiliSecond;
        }


        public void UpdateUI()
        {
            timeWork.TimeActual = 0;
            new Thread(UpdateTick).Start();
        }

        public void UpdateTick()
        {
            try
            {
                while (timeWork.TimeActual < timeWork.TimeScheduledMiliSecond)
                {

                    this.Dispatcher.Invoke(() =>
                    {
                        showResults();
                    });
                    Thread.Sleep(1000);
                }
                this.Dispatcher.Invoke(() =>
                {
                    update();
                });
            }
            catch { }
            
        }

        public void update()
        {
            ResultsWorkFactory resultWorkWindow = new ResultsWorkFactory(factory);
            resultWorkWindow.ShowDialog();
            timeWork.TimeActual += timeWork.TimeScheduledMiliSecond;
            button.IsEnabled = true;
        }

        public void showResults()
        {
            drawRectangle(0, 0, 340, 640, Colors.White);
            drawFactory();
            refreshResults();
            timeWork.TimeActual += 20;
        }
    }
}
