using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCalc
{
    public class CoreProcessor
    {
        public double Total;
        public double Average;

        public readonly Dictionary<DateTime, List<TransactionData>> DateSortedData = new Dictionary<DateTime, List<TransactionData>>();

        public CoreProcessor(List<TransactionData> data)
        {
            foreach (var transactionData in data)
            {
                List<TransactionData> dataListForDay;

                if (!DateSortedData.ContainsKey(transactionData.DateTime.Date))
                {
                    dataListForDay = new List<TransactionData>();
                    DateSortedData.Add(transactionData.DateTime.Date, dataListForDay);
                    continue;
                }

                dataListForDay = DateSortedData[transactionData.DateTime.Date];
                

                dataListForDay.Add(transactionData);
            }
        }

        public void Process()
        {
            foreach (var keyValuePair in DateSortedData)
            {
                var singleDayResult = new SingleDayProcessor(keyValuePair.Value).Process();
              
                Debug.WriteLine(keyValuePair.Key.Date +" -> " + singleDayResult);
            }
        }
    }
}
