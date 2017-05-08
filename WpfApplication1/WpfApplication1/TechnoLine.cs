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

        public TechnoLine(int[,] kord ,int numberLine, TimeWork timeWork)
        {
            this.timeWork = timeWork;
            NumberLine = numberLine;
            Machines = new Machine[4];
            Machines[0] = new Machine(kord[0,0], kord[1,0], timeWork, 1, NumberLine);
            Machines[1] = new Machine(kord[0,1], kord[1,1], timeWork, 2, NumberLine);
            Machines[2] = new Machine(kord[0,2], kord[1,2], timeWork, 3, NumberLine);
            Machines[3] = new Machine(kord[0,3], kord[1,3], timeWork, 4, NumberLine);
        }

        public void addDeteal(Detail detail)
        {
            if (timeWork.TimeActual <= timeWork.TimeScheduledMiliSecond)
            {
                Console.WriteLine("Деталь № " + detail.IdDetail + " потрапляє у лінію № " + NumberLine);
                MainWindow.ListResults.Add("Деталь № " + detail.IdDetail + " потрапляє у лінію № " + NumberLine);
                for (int i = 0; i < Machines.Length; i++)
                {
                    Machines[i].addDetail(detail);
                }
            }
        }

        public override string ToString()
        {
            foreach(Machine machine in Machines)
            {
                machine.ToString();
            }
            return "";
        }
    }
}
