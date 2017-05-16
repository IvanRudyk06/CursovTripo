using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public delegate void TecnoLineStateHandler();
    public class TechnoLine 
    {
        public event TecnoLineStateHandler eventRefresh;

        private TimeWork timeWork;

        public int NumberLine { get; }
        public  Machine[] Machines { get; set; }

        private Random rand;

        public bool MayWork { get; set; }

        public TechnoLine(int[,] kord ,int numberLine, TimeWork timeWork, Random rand)
        {
            MayWork = true;
            this.rand = rand;
            this.timeWork = timeWork;
            NumberLine = numberLine;
            Machines = new Machine[4];
            Machines[0] = new Machine(kord[0,0], kord[1,0], timeWork, 1, NumberLine, rand);
            Machines[1] = new Machine(kord[0,1], kord[1,1], timeWork, 2, NumberLine, rand);
            Machines[2] = new Machine(kord[0,2], kord[1,2], timeWork, 3, NumberLine, rand);
            Machines[3] = new Machine(kord[0,3], kord[1,3], timeWork, 4, NumberLine, rand);

            for(int i = 0; i<4; i++)
            {
                Machines[i].eventRefresh += eventExecute;
            }
        }

        public void eventExecute()
        {
            if (eventRefresh != null)
                eventRefresh();
        }

        public void addDeteal(Detail detail)
        {
            if (MayWork)
            {
                MainWindow.ListResults.Add("Мікросхема №" + detail.IdDetail + " потрапляє у лінію № " + NumberLine);
                if (eventRefresh != null)
                    eventRefresh();
                Machines[0].addDetail(detail);
                Thread.Sleep(1000+rand.Next(2000));
                Machines[1].addDetail(detail);
                Thread.Sleep(rand.Next(2000));
                Machines[2].addDetail(detail);
                Thread.Sleep(1000+rand.Next(3000));
                Machines[3].addDetail(detail);
            }
        }

        public override string ToString()
        {
           return "    Технологічна лінія № "+NumberLine;
        }
    }
}
