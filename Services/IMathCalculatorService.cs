using GraphCandleApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphCandleApp.Services
{
    interface IMathCalculatorService
    {
        string GetMathDictionary(List<FutureKlineData> timeSeriesData);
    }
}
