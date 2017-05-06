using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Machine 
    {
        private Mutex mutex = new Mutex();
        public int NumberMachine { get; }
        public int NumberLine { get; }
        public int Queue { get; set; }
        public int CountDoneDetail { get; set; }
        public int MaxSizeQueue { get; set; }
        public int TimeWork { get; set; }
        public int SumTimeWork { get; set; }

        private Random randomTimeWork = new Random();

        public Machine(int timeWork, int numberMachine, int numberLine)
        {
            NumberLine = numberLine;
            NumberMachine = numberMachine;
            Queue = 0;
            CountDoneDetail = 0;
            MaxSizeQueue = 0;
            TimeWork = timeWork;
            SumTimeWork = 0;
        }

        public void addDetail(Detail detail)
        {
            if (Queue == 0 && SumTimeWork==0)
            {
                Console.WriteLine("      Станок № " + NumberMachine + "Черга "+ Queue);
                Console.WriteLine("      Станок № " + NumberMachine + "Час роботи "+SumTimeWork);
                workDetail(detail);
            }
            else
            {
                Queue++;
                Console.WriteLine("      Станок № " + NumberMachine + "Черга " + Queue);
                Console.WriteLine("      Станок № " + NumberMachine + "Час роботи " + SumTimeWork);
                Console.WriteLine("Деталь № "+detail.IdDetail+" зайшла у чергу станок № " + NumberMachine + "Лінія № " + NumberLine);
                if (MaxSizeQueue < Queue)
                {
                    MaxSizeQueue = Queue;
                }
                workDetail(detail);
                Queue--;
            }
        }

        private void workDetail(Detail detail)
        {
            mutex.WaitOne();
            int timeWorkDetale = randomTimeWork.Next(2) - 4 + TimeWork;
            SumTimeWork += timeWorkDetale;
            Console.WriteLine("Деталь № " + detail.IdDetail + " зайшла на станок № " + NumberMachine + "Лінія № " + NumberLine);
            Thread.Sleep(new TimeSpan(0, 0, timeWorkDetale));
            Console.WriteLine("Деталь № " + detail.IdDetail + " вийшла зі станка № " + NumberMachine + "Лінія № " + NumberLine);
            mutex.ReleaseMutex();
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
            Console.WriteLine("      Станок № " + NumberMachine + "  Лінія № " + NumberLine+
                            "\n          Максимальний розмір черги "+MaxSizeQueue+" Середній час роботи деталі "+getAvarageTimeWork()+" сек.");
            return "";
        }

    }
}
