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
        public ErrorHandlerDelegate ErrorHandler;

        public SingleDayProcessor(DateTime date, List<TransactionData> data)
        {
            _date = date;
            _transactionData = data;
            _transactionData.RemoveAt(0);
        }

        public SingleDayResult Process()
        {
            try
            {
                var total = _transactionData.Sum(transactionData => transactionData.TotalDeal);
                var effectiveTotal = _transactionData.Sum(transactionData => Math.Abs(transactionData.EffectiveDeal));

                if (_transactionData.Count != 239)
                {
                    ErrorHandler?.Invoke("计算时发生错误：" + _date.ToShortDateString() + "的数据缺失，已跳过当日数据");
                    return null;
                }

                var average = effectiveTotal / _transactionData.Count;

                return new SingleDayResult(
                    _date,
                    effectiveRatio: _transactionData.Where(item => Math.Abs(item.EffectiveDeal) > average).ToList()
                                        .Sum(item => item.EffectiveDeal) / total,
                    absEffectiveRatio: _transactionData.Where(item => Math.Abs(item.EffectiveDeal) > average).ToList()
                                           .Sum(item => Math.Abs(item.EffectiveDeal)) / total
                );
            }
            catch (Exception e)
            {
                ErrorHandler?.Invoke("计算时发生错误：" + _date.ToShortDateString() + "的数据异常，已跳过当日数据");
                return null;
            }

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
