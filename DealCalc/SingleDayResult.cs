using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCalc
{
    public class SingleDayResult
    {
        public readonly double EffectiveRatio;
        public readonly double AbsEffectiveRatio;
        public readonly DateTime Date;
        public readonly double EffectiveAmount;
        public readonly double TotalAmount;
        public SingleDayResult(DateTime date, double effectiveRatio, double absEffectiveRatio, double effectiveAmount, double totalAmount)
        {
            Date = date;
            EffectiveRatio = effectiveRatio;
            AbsEffectiveRatio = absEffectiveRatio;
            EffectiveAmount = effectiveAmount;
            TotalAmount = totalAmount;
        }

        public override string ToString()
        {
            return "Date = " + Date.ToShortDateString() + ", EffectiveAmount=" + EffectiveAmount +", Total=" + TotalAmount;
        }
    }
}
