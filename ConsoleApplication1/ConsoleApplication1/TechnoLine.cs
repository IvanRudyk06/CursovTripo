using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class TechnoLine 
    {
        public int NumberLine { get; }
        public  Machine[] Machines { get; set; }

        public TechnoLine(int numberLine)
        {
            NumberLine = numberLine;
            Machines = new Machine[4];
            Machines[0] = new Machine(15, 1, NumberLine);
            Machines[1] = new Machine(12, 2, NumberLine);
            Machines[2] = new Machine(17, 3, NumberLine);
            Machines[3] = new Machine(13, 4, NumberLine);
        }

        public void addDeteal(Detail detail)
        {
            Console.WriteLine("Деталь № "+detail.IdDetail+" потрапляє у лінію № "+NumberLine);
            for(int i = 0; i< Machines.Length; i++)
            {
                Machines[i].addDetail(detail);
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
