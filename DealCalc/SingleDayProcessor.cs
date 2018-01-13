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

                var effectiveAmount = _transactionData.Where(item => Math.Abs(item.EffectiveDeal) > average).ToList()
                                        .Sum(item => item.EffectiveDeal);

                return new SingleDayResult(
                    _date,
                    effectiveRatio:  effectiveAmount / total,
                    absEffectiveRatio: _transactionData.Where(item => Math.Abs(item.EffectiveDeal) > average).ToList()
                                           .Sum(item => Math.Abs(item.EffectiveDeal)) / total,
                    effectiveAmount: effectiveAmount,
                    totalAmount: total
                );
            }
            catch (Exception e)
            {
                ErrorHandler?.Invoke("计算时发生错误：" + _date.ToShortDateString() + "的数据异常，已跳过当日数据");
                return null;
            }

        }
    }
}
