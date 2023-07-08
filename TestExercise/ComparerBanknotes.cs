using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExercise
{
    public class ComparerBanknotes : IEqualityComparer<Banknote>
    {
        public bool Equals(Banknote x, Banknote y)
        {
            return x.Value == y.Value;
        }

        public int GetHashCode(Banknote obj)
        {
            return obj.Value.GetHashCode();
        }
    }
}
