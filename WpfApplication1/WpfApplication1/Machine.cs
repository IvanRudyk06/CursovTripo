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
        public int MaxSizeQueue { get; set; }
        public int SumTimeWork { get; set; }

        public int TimeQueue;

        public int SumDetail { get; set; }

        private Random randomTimeWork; 

        private TimeWork timeWork;

        public bool MayWork { get; set; }
        //---------------------------------------------------------------

        public Machine(int x, int y, TimeWork timeWork, int numberMachine, int numberLine, Random rand)
        {
            MayWork = true;
            TimeQueue = 0;
            randomTimeWork = rand;
            this.timeWork = timeWork;
            X = x;
            Y = y;
            Color = Brushes.Gray;

            NumberLine = numberLine;
            NumberMachine = numberMachine;
            Queue = 0;
            MaxSizeQueue = 0;
            if(numberMachine != 0)
            SumTimeWork = 0;
            SumDetail = 0;
        }

       

        public void addDetail(Detail detail)
        {
            if (MayWork)
            {
                Queue++;
                MainWindow.ListResults.Add("Деталь №"+detail.IdDetail+" зайшла у чергу станок № " + NumberMachine + " Лінія № " + NumberLine);
                if (MaxSizeQueue < Queue)
                {
                    MaxSizeQueue = Queue;
                }
                workDetail(detail);
            }
            
        }

        private void workDetail(Detail detail)
        {
            if (MayWork)
            {
                int div = timeWork.TimeWorkOnTypeMachine[1, NumberMachine - 1];
                mutex.WaitOne();
                Queue--;
                int timeWorkDetaleMilisecond = randomTimeWork.Next(div*2) - div + timeWork.TimeWorkOnTypeMachine[0, NumberMachine-1];
                SumTimeWork += timeWorkDetaleMilisecond;
                Console.WriteLine(SumTimeWork);
                SumDetail += 1;
                Color = Brushes.Green;
                MainWindow.ListResults.Add("Деталь №" + detail.IdDetail + " зайшла на станок № " + NumberMachine + " Лінія № " + NumberLine);
                Thread.Sleep(new TimeSpan(0, 0, 0, 0, timeWorkDetaleMilisecond));
                MainWindow.ListResults.Add("Деталь №" + detail.IdDetail + " вийшла зі станка № " + NumberMachine + " Лінія № " + NumberLine);
                Color = Brushes.Gray;
                mutex.ReleaseMutex();
            }
        }

        public double getAvarageTimeWork()
        {
            if (SumDetail != 0)
            {
                return ((int)SumTimeWork / (SumDetail*10))/100.0;
            }
            else
            {
                return 0;
            }

        }

        public double getUtilizationMachine()
        {
            return SumTimeWork / (timeWork.TimeScheduledMiliSecond * 50.0);
        }

        public override string ToString()
        {
            return("        Станок № " + NumberMachine + " Максимальний розмір черги " + MaxSizeQueue
                              + "   Середній час обробки деталі " + getAvarageTimeWork() + " сек.   " + " Коефіцієнт використання : "
                               + getUtilizationMachine()+" Середній час перебування деталей у черзі : " + TimeQueue/100.0 + "хв.");
        }


        
}
}
