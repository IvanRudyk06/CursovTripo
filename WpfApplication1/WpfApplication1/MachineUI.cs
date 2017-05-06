using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApplication1
{
    public class MachineUI
    {
        public int X { get; set; }
        public int Y { get; set; }
        public SolidColorBrush Color { get; set; }
        public int QueueDetail { get; set; }

        public MachineUI(int x, int y)
        {
            X = x;
            Y = y;
            Color = Brushes.Gray;
            QueueDetail = 2;
        }

        public void addDetail()
        {

        }
    }
}
