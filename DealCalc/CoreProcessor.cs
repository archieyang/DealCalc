using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCalc
{
    public delegate void ErrorHandlerDelegate(string error);

    public class CoreProcessor
    {
        private readonly DateTime VALID_DATA_START = new DateTime(2017, 8, 15, 0, 0, 0);

        public double Total;
        public double Average;
        public ErrorHandlerDelegate ErrorHandler;

        public readonly List<TransactionData> Data;

        public CoreProcessor(List<TransactionData> data)
        {
            Data = data;

        }

        public List<SingleDayResult> Process()
        {
            try
            {
                var dateSortedData = new Dictionary<DateTime, List<TransactionData>>();
                foreach (var transactionData in Data)
                {
                    if (DateTime.Compare(transactionData.DateTime.Date, VALID_DATA_START) < 0)
                    {
                        continue;
                    }

                    List<TransactionData> dataListForDay;

                    if (!dateSortedData.ContainsKey(transactionData.DateTime.Date))
                    {
                        dataListForDay = new List<TransactionData>();
                        dateSortedData.Add(transactionData.DateTime.Date, dataListForDay);
                    }

                    dataListForDay = dateSortedData[transactionData.DateTime.Date];


                    dataListForDay.Add(transactionData);
                }
                var resList = new List<SingleDayResult>();
                foreach (var keyValuePair in dateSortedData)
                {
                    var singleDayResult = new SingleDayProcessor(keyValuePair.Key, keyValuePair.Value)
                    {
                        ErrorHandler = ErrorHandler

                    }.Process();
                    if (singleDayResult != null)
                    {
                        resList.Add(singleDayResult);
                    }
                }

                return resList;
            }
            catch (Exception e)
            {
                ErrorHandler ?.Invoke("计算时发生错误：" + e.Message);
                return new List<SingleDayResult>();
            }

        }
    }
}
