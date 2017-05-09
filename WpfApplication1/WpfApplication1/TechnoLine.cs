using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class TechnoLine 
    {
        private TimeWork timeWork;

        public int NumberLine { get; }
        public  Machine[] Machines { get; set; }

        private Random rand;

        public TechnoLine(int[,] kord ,int numberLine, TimeWork timeWork, Random rand)
        {
            this.rand = rand;
            this.timeWork = timeWork;
            NumberLine = numberLine;
            Machines = new Machine[4];
            Machines[0] = new Machine(kord[0,0], kord[1,0], timeWork, 1, NumberLine, rand);
            Machines[1] = new Machine(kord[0,1], kord[1,1], timeWork, 2, NumberLine, rand);
            Machines[2] = new Machine(kord[0,2], kord[1,2], timeWork, 3, NumberLine, rand);
            Machines[3] = new Machine(kord[0,3], kord[1,3], timeWork, 4, NumberLine, rand);
        }

        public void addDeteal(Detail detail)
        {
            if (timeWork.TimeActual <= timeWork.TimeScheduledMiliSecond)
            {
                MainWindow.ListResults.Add("Деталь № " + detail.IdDetail + " потрапляє у лінію № " + NumberLine);
                for (int i = 0; i < Machines.Length; i++)
                {
                    Machines[i].addDetail(detail);
                }
            }
        }

        public override string ToString()
        {
           return "    Технологічна лінія № "+NumberLine;
        }
    }
}
