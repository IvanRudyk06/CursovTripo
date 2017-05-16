using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class TechnoLineUse : IComparable<TechnoLineUse>
    {
       public int NumberLine { get; }
       public int QueueSize { get; }
       public int TimeUse { get; }

        public TechnoLineUse(int numberLine, int queueSize, int timeUse)
        {
            NumberLine = numberLine;
            QueueSize = queueSize;
            TimeUse = timeUse;
        }

        public int CompareTo(TechnoLineUse other)
        {
            if (this.QueueSize > other.QueueSize)
            {
                return 1;
            }
            else if (this.QueueSize == other.QueueSize)
            {
                if (this.TimeUse == 0 && other.TimeUse != 0)
                {
                    return -1;
                }
                else if (this.TimeUse != 0 && other.TimeUse == 0)
                {
                    return 1;
                }
                else return 0;
            }
            else return -1;
        }
    }
}
