using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
   public class TimeWork
    {
        public int TimeScheduledMiliSecond { get; set; }
        public int TimeActual { get; set; }

        public int[] TimeGenerate { get; set; }
        public int[,] TimeWorkOnTypeMachine { get; set; }

        public TimeWork(int timeScheduled, int[] timeGenerate, int[,] timeWorkOnTypeMachine)
        {
            TimeGenerate = timeGenerate;
            TimeWorkOnTypeMachine = timeWorkOnTypeMachine;
            TimeScheduledMiliSecond = timeScheduled;
            TimeActual = 0;
        }
    }
}
