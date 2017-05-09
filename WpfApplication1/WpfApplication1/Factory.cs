using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication1
{
   public class Factory
    {

        public Machine []GenerateEndMachines { get; set; }

        public MyLine[] helpedLines { get; set; }

        public TimeWork TimeWork { get; set; }

        //--------------------------------------------------
        private Random randomTime = new Random();
        public int CountOfGeneratedDetail { get; set; }
        public int CountOfDoneDetail { get; set; }

        public int TimeGenerate { get; set; }

        public ObservableCollection<TechnoLine> MyTechnoLines { get; set; }

        //--------------------------------------------------

        public Factory(TimeWork timeWork)
        {
            this.TimeWork = timeWork;
            helpedLines = new MyLine[15];
            GenerateEndMachines = new Machine[2];
            MyTechnoLines = new ObservableCollection<TechnoLine>();

            TimeGenerate = 3;
            CountOfDoneDetail = 0;
            CountOfGeneratedDetail = 0;
            initsializeLine();
            initsializeFactory();
        }



        public void initsializeLine()
        {
            int x = 200, y1 = 75, y2 = 175, y3 = 275;
            for(int i= 0; i<3; i++)
            {
                helpedLines[i] = new MyLine(x, y1, x + 50, y1);
                helpedLines[i+3] = new MyLine(x, y2, x + 50, y2);
                helpedLines[i+6] = new MyLine(x, y3, x + 50, y3);
                x += 100;
            }
            helpedLines[9] = new MyLine(75, 150, 150, 75);
            helpedLines[10] = new MyLine(100, 175, 150, 175);
            helpedLines[11] = new MyLine(75, 200, 150, 275);
            helpedLines[12] = new MyLine(500, 75, 575, 150);
            helpedLines[13] = new MyLine(500, 175, 550, 175);
            helpedLines[14] = new MyLine(500, 275, 575, 200);
        }

        private void initsializeFactory()
        {
            int y = 50;
            for (int i = 0; i < 3; i++)
            {
                int x = 150;
                int [,] arr = new int[2,4];
                for (int j = 0; j < 4; j++)
                {
                    arr[0, j] = x;
                    arr[1, j] = y;
                    x += 100;                 
                }
                y += 100;
                MyTechnoLines.Add(new TechnoLine(arr, i+1, TimeWork, randomTime));
            }

            GenerateEndMachines[0] = new Machine(50, 150, TimeWork, 0, 0, randomTime);
            GenerateEndMachines[1] = new Machine(550, 150, TimeWork, 0, 0, randomTime);
        }


        public void startWork()
        {
            Thread thread = new Thread(new ThreadStart(run));
            thread.Start();
        }

        private void run()
        {
            while (TimeWork.TimeActual <= TimeWork.TimeScheduledMiliSecond)
            {
                Detail detail = new Detail(CountOfGeneratedDetail + 1);
                int div = TimeWork.TimeGenerate[1];
                int timeWorkDetail = randomTime.Next(div*2) - div + TimeWork.TimeGenerate[0];
                Thread thread = new Thread(new ParameterizedThreadStart(addDetail));
                thread.Start(detail);
                Thread.Sleep(new TimeSpan(0, 0, 0, 0, timeWorkDetail));
            }
            this.ToString();
        }

        public void addDetail(object detail)
        {
            if (TimeWork.TimeActual <= TimeWork.TimeScheduledMiliSecond)
            {
                MainWindow.ListResults.Add("   Згенеровано деталь № " + ((Detail)detail).IdDetail);
                generateEndDetaleBlink(GenerateEndMachines[0]);
                CountOfGeneratedDetail++;
                sellectLineForAdd((Detail)detail);
                MainWindow.ListResults.Add("   Виготовлено деталь № " + ((Detail)detail).IdDetail);
                CountOfDoneDetail++;
                generateEndDetaleBlink(GenerateEndMachines[1]);
            }
        }

        private void generateEndDetaleBlink(Machine machine)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(blinkingMachine));
            thread.Start(machine);
        }

        private void blinkingMachine(object machine)
        {
            ((Machine)machine).Color = Brushes.Red;
            Thread.Sleep(new TimeSpan(0, 0, 0, 1));
            ((Machine)machine).Color = Brushes.Gray;
        }

        private void sellectLineForAdd(Detail detail)
        {
            int index = 0;
            int sum = Int32.MaxValue;
            for(int i = 0; i<MyTechnoLines.Count; i++)
            {
                int sumTemp = getSumTimeWorkLine(MyTechnoLines[i]);
                if (sumTemp < sum)
                {
                    index = i;
                    sum = sumTemp;
                }
            }
            MyTechnoLines[index].addDeteal(detail);
        }

        private int getSumTimeWorkLine(TechnoLine technoLine)
        {
            int sum = 0;
            for(int i = 0; i< technoLine.Machines.Length; i++)
            {
                sum += technoLine.Machines[i].SumTimeWork;
            }
            return sum;
        }

        public override string ToString()
        {
            Console.WriteLine("================================================");
            Console.WriteLine("    Інформація про завод :");
            Console.WriteLine("  Згенеровано деталей : " + CountOfGeneratedDetail);
            Console.WriteLine("  Виготовлено деталей : " + CountOfDoneDetail);
            foreach (TechnoLine tl in MyTechnoLines)
            {
                tl.ToString();
            }
            return "";
        }
    }
}

