using LiveCharts.Definitions.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCalc
{
    interface ChartAdapter
    {
        void ForEach(Action<ChartItem> action);
        void ForEachSeries(Action<ISeriesView> action);
        double Upper();
        double Lower();
        String Step();
        String Xname();
        String Yname();
        Func<double, string> Formatter();
    }
}
