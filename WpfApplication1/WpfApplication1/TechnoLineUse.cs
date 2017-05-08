using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class TechnoLineUse : IComparable<TechnoLineUse>
    {
       public int NumberLine { get; }
       public int QueueSize { get; }

        public TechnoLineUse(int numberLine, int queueSize)
        {
            NumberLine = numberLine;
            QueueSize = queueSize;
        }

        public int CompareTo(TechnoLineUse other)
        {
            if (this.QueueSize > other.QueueSize)
            {
                return 1;
            }
            else if (this.QueueSize == other.QueueSize)
            { 
                 return 0;
            }
            else return -1;
        }
    }
}
