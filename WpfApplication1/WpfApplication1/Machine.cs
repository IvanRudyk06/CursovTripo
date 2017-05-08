using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApplication1
{
    public class Machine
    {
        public int X { get; set; }
        public int Y { get; set; }
        public SolidColorBrush Color { get; set; }

        //---------------------------------------------------------------
        private Mutex mutex = new Mutex();
        public int NumberMachine { get; }
        public int NumberLine { get; }
        public int Queue { get; set; }
        public int CountDoneDetail { get; set; }
        public int MaxSizeQueue { get; set; }
        public int SumTimeWork { get; set; }

        private Random randomTimeWork = new Random();

        private TimeWork timeWork;
        //---------------------------------------------------------------

        public Machine(int x, int y, TimeWork timeWork, int numberMachine, int numberLine)
        {
            this.timeWork = timeWork;
            X = x;
            Y = y;
            Color = Brushes.Gray;

            NumberLine = numberLine;
            NumberMachine = numberMachine;
            Queue = 0;
            CountDoneDetail = 0;
            MaxSizeQueue = 0;
            if(numberMachine != 0)
            SumTimeWork = 0;
        }

        public void addDetail(Detail detail)
        {
            if (timeWork.TimeActual <= timeWork.TimeScheduledMiliSecond)
            {
                Queue++;
                Console.WriteLine("Деталь № " + detail.IdDetail + " зайшла у чергу станок № " + NumberMachine + "Лінія № " + NumberLine);
                MainWindow.ListResults.Add("Деталь № " + detail.IdDetail + " зайшла у чергу станок № " + NumberMachine + "Лінія № " + NumberLine);
                if (MaxSizeQueue < Queue)
                {
                    MaxSizeQueue = Queue;
                }
                workDetail(detail);
            }
        }

        private void workDetail(Detail detail)
        {
            if (timeWork.TimeActual <= timeWork.TimeScheduledMiliSecond)
            {
                int div = timeWork.TimeWorkOnTypeMachine[1, NumberMachine - 1];
                mutex.WaitOne();
                Queue--;
                int timeWorkDetaleMilisecond = randomTimeWork.Next(div*2) - div + timeWork.TimeWorkOnTypeMachine[0, NumberMachine-1];
                SumTimeWork += timeWorkDetaleMilisecond;
                Color = Brushes.Green;
                Console.WriteLine("Деталь № " + detail.IdDetail + " зайшла на станок № " + NumberMachine + "Лінія № " + NumberLine);
                MainWindow.ListResults.Add("Деталь № " + detail.IdDetail + " зайшла на станок № " + NumberMachine + "Лінія № " + NumberLine);
                Thread.Sleep(new TimeSpan(0, 0, 0, 0, timeWorkDetaleMilisecond));
                Console.WriteLine("Деталь № " + detail.IdDetail + " вийшла зі станка № " + NumberMachine + "Лінія № " + NumberLine);
                MainWindow.ListResults.Add("Деталь № " + detail.IdDetail + " вийшла зі станка № " + NumberMachine + "Лінія № " + NumberLine);
                Color = Brushes.Gray;
                mutex.ReleaseMutex();
            }
                
        }

        public double getAvarageTimeWork()
        {
            if (CountDoneDetail != 0)
            {
                return SumTimeWork / CountDoneDetail;
            }
            else
            {
                return 0;
            }

        }

        public override string ToString()
        {
            Console.WriteLine("      Станок № " + NumberMachine + "  Лінія № " + NumberLine +
                            "\n          Максимальний розмір черги " + MaxSizeQueue + " Середній час роботи деталі " + getAvarageTimeWork() + " сек.");
            return "";
        }


        
}
}
