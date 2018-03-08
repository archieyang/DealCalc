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
        private readonly ChartySeriesFactory chartySeriesFactory = new LineSeriesFactory();
        public ConsecutiveCompositeAdapter(List<SingleDayResult> data, int consecutiveNum) : base(data, consecutiveNum)
        {
            
        }

        public override void ForEachSeries(Action<ISeriesView> data, Action<string> labels)
        {
            int[] conseuctiveNums = { 5, 10, 20, 60};

            foreach ( int i in conseuctiveNums) 
            {
                Debug.WriteLine(i + "");
                ConsecutiveAverageAdapter adapter = new ConsecutiveAverageAdapter(_data, i,chartySeriesFactory, values=>values.Add(double.NaN));

                adapter.ForEachSeries(data, l => { } );
            }

            foreach(SingleDayResult dd in _data)
            {
                labels?.Invoke(dd.Date.ToShortDateString());
            }
        }

    }
}
