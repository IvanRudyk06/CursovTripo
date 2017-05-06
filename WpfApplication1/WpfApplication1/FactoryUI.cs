using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication1
{
   public class FactoryUI
    {
        public ObservableCollection<MachineUI> Line1 { get; set; }
        public ObservableCollection<MachineUI> Line2 { get; set; }
        public ObservableCollection<MachineUI> Line3 { get; set; }

        public MachineUI []GenrateEndMachines { get; set; }

        public MyLine[] helpedLines { get; set; } 

        public FactoryUI()
        {
            helpedLines = new MyLine[15];
            GenrateEndMachines = new MachineUI[2];
            Line1 = new ObservableCollection<MachineUI>();
            Line2 = new ObservableCollection<MachineUI>();
            Line3 = new ObservableCollection<MachineUI>();
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
            int x = 150, y1 = 50, y2 = 150, y3 = 250;
            for (int i = 0; i < 4; i++)
            {
                Line1.Add(new MachineUI(x, y1));
                Line2.Add(new MachineUI(x, y2));
                Line3.Add(new MachineUI(x, y3));
                x += 100;
            }
            GenrateEndMachines[0] = new MachineUI(50, 150);
            GenrateEndMachines[1] = new MachineUI(550, 150);
        }
    }
}
