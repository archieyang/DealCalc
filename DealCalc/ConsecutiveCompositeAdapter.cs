using LiveCharts.Definitions.Series;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCalc
{
    internal class ConsecutiveCompositeAdapter : ConsecutiveAverageAdapter
    {
        public ConsecutiveCompositeAdapter(List<SingleDayResult> data, int consecutiveNum) : base(data, consecutiveNum)
        {
            
        }
        public override void ForEach(Action<ChartItem> action)
        {
            for (int i = 0; i < _data.Count; ++i)
            {
                ChartItem chartItem = new ChartItem(_data[i].Date.ToShortDateString(), 0);
                action?.Invoke(chartItem);
            }
        }

        public override void ForEachSeries(Action<ISeriesView> action)
        {
            int[] conseuctiveNums = { 5, 10, 20, 60};

            foreach ( int i in conseuctiveNums) 
            {
                Debug.WriteLine(i + "");
                ConsecutiveAverageAdapter adapter = new ConsecutiveAverageAdapter(_data, i, new LineSeriesFactory());

                adapter.ForEachSeries(action);
            }
        }

    }
}
