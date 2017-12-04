using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCalc
{
    public class SingleDayProcessor
    {
        private readonly DateTime _date;
        private readonly List<TransactionData> _transactionData;

        public SingleDayProcessor(DateTime date, List<TransactionData> data)
        {
            _date = date;
            _transactionData = data;
            _transactionData.RemoveAt(0);
        }

        public SingleDayResult Process()
        {
            var total = _transactionData.Sum(transactionData => transactionData.TotalDeal);
            var effectiveTotal = _transactionData.Sum(transactionData => Math.Abs(transactionData.EffectiveDeal));

            var average = effectiveTotal / _transactionData.Count;

            Debug.WriteLineIf(_transactionData.Count != 238, _transactionData.Count);

            return new SingleDayResult(
                _date,
                effectiveRatio: _transactionData.Where(item => Math.Abs(item.EffectiveDeal)> average).ToList().Sum(item=>item.EffectiveDeal) / total,
                absEffectiveRatio: _transactionData.Where(item => Math.Abs(item.EffectiveDeal) > average).ToList().Sum(item => Math.Abs(item.EffectiveDeal)) / total
                );
        }
    }

    public class SingleDayResult
    {
        public readonly double EffectiveRatio;
        public readonly double AbsEffectiveRatio;
        public readonly DateTime Date;
        public SingleDayResult(DateTime date, double effectiveRatio, double absEffectiveRatio)
        {
            Date = date;
            EffectiveRatio = effectiveRatio;
            AbsEffectiveRatio = absEffectiveRatio;
        }

        public override string ToString()
        {
            return "EffectiveRation = " + EffectiveRatio + ", AbsEffectiveRatio=" + AbsEffectiveRatio;
        }
    }
}
