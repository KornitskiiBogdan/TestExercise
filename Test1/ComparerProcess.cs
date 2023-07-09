using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    internal class ComparerProcess : IEqualityComparer<HistoryProcessing>
    {
        public bool Equals(HistoryProcessing? x, HistoryProcessing? y)
        {
            if (x == null || y == null)
            {
                return x == null && y == null;
            }
            return x.FileInput.Equals(y.FileInput) && x.FileOutput.Equals(y.FileOutput);
        }

        public int GetHashCode(HistoryProcessing obj)
        {
            return 0;
        }
    }
}
