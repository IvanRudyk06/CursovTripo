using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Factory
    {
        private Random randomTime = new Random();
        public int CountOfGeneratedDetail { get; set; }
        public int CountOfDoneDetail { get; set; }

        public int TimeGenerate { get; set; }

        ObservableCollection<TechnoLine> MyTechnoLines { get; set; }

        public Factory()
        {
            MyTechnoLines = new ObservableCollection<TechnoLine>();
            MyTechnoLines.Add(new TechnoLine(1));
            MyTechnoLines.Add(new TechnoLine(2));
            MyTechnoLines.Add(new TechnoLine(3));
            TimeGenerate = 10;
            CountOfDoneDetail = 0;
            CountOfGeneratedDetail = 0;
        }

        public void startWork()
        {
            Thread thread = new Thread(new ThreadStart(run));
            thread.Start();
        }

        private void run()
        {
            while (CountOfGeneratedDetail < 10)
            {
                Detail detail = new Detail(CountOfGeneratedDetail + 1);
                int timeWorkDetail = randomTime.Next(2) - 4 + TimeGenerate;
                Thread thread = new Thread(new ParameterizedThreadStart(addDetail));
                thread.Start(detail);
                Thread.Sleep(new TimeSpan(0, 0, timeWorkDetail));
            }
            this.ToString();
        }

        public void addDetail(object detail)
        {
            Console.WriteLine("Згенеровано деталь № "+ ((Detail)detail).IdDetail);
            CountOfGeneratedDetail++;
            sellectLineForAdd((Detail)detail);
            CountOfDoneDetail++;
            Console.WriteLine("Виготовлено деталь № " + ((Detail)detail).IdDetail);
        }

        private void sellectLineForAdd(Detail detail)
        {
            List<TechnoLineUse> technoLinesUse = new List<TechnoLineUse>();
            technoLinesUse.Add(new TechnoLineUse(0, MyTechnoLines[0].Machines[0].Queue, MyTechnoLines[0].Machines[0].SumTimeWork));
            technoLinesUse.Add(new TechnoLineUse(1, MyTechnoLines[1].Machines[0].Queue, MyTechnoLines[1].Machines[0].SumTimeWork));
            technoLinesUse.Add(new TechnoLineUse(2, MyTechnoLines[2].Machines[0].Queue, MyTechnoLines[2].Machines[0].SumTimeWork));
            technoLinesUse.Sort();
            MyTechnoLines[technoLinesUse[0].NumberLine].addDeteal(detail);
        }

        public override string ToString()
        {
            Console.WriteLine("================================================");
            Console.WriteLine("    Інформація про завод :");
            Console.WriteLine("  Згенеровано деталей : "+CountOfGeneratedDetail);
            Console.WriteLine("  Виготовлено деталей : " + CountOfDoneDetail);
            foreach(TechnoLine tl in MyTechnoLines)
            {
                tl.ToString();
            }
            return "";
        }
    }
}
