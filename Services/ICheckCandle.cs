using GraphCandleApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphCandleApp.Services
{
    public interface ICheckCandle
    {
        bool ShouldGenerateNextKline(FutureKlineData lastKline, TradeData newTradeData);
    }
}
