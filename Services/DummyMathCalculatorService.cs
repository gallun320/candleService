using GraphCandleApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphCandleApp.Services
{
    public class DummyMathCalculatorService : IMathCalculatorService
    {
        public string GetMathDictionary(List<FutureKlineData> timeSeriesData)
        {
            return timeSeriesData.Last().Close.ToString();
        }
    }
}
