using GraphCandleApp.Utils;
using System.Collections.Generic;
using System.Linq;

namespace GraphCandleApp.Services
{
    public class CandelGraphService : ICandelGraphService, ICheckCandle
    {
        private readonly Dictionary<string, List<FutureKlineData>> _candleDict;
        private readonly object _lockObject = new object();
        private readonly IPipeSenderService _pipeSender;
        private IMathCalculatorService _mathCalculatorService;

        public CandelGraphService(VolumeBarArbitrageData arbitrageData)
        {
            _candleDict = new Dictionary<string, List<FutureKlineData>>();
            _pipeSender = new PipeSenderSerivce(arbitrageData.PipeName, arbitrageData);
            _mathCalculatorService = new DummyMathCalculatorService();

        }

        public void SetTrade(TradeData data)
        {
            lock(_lockObject)
            {
                var key = $"{data.Exchange}_{data.Instrument}";
                if (_candleDict.TryGetValue(key, out var candles))
                {
                    var lastCandle = candles.Last();
                    if (ShouldGenerateNextKline(lastCandle, data))
                    {
                        var kline = new FutureKlineData(lastCandle);
                        kline.Update(data.Price, data.Quantity);
                        candles.Add(kline);
                        SendToAll(candles);
                    }
                    else
                    {
                        lastCandle.Update(data.Price, data.Quantity);
                    }
                }
                else
                {
                    var kline = new FutureKlineData();
                    kline.Update(data.Price, data.Quantity);
                    _candleDict.Add(key, new List<FutureKlineData> { kline });
                    SendToAll(_candleDict[key]);
                }
            }
        }

        public bool ShouldGenerateNextKline(FutureKlineData lastKline, TradeData newTradeData)
        {
            return true;
        }

        private void SendToAll(List<FutureKlineData> list)
        {
            var jsonData = _mathCalculatorService.GetMathDictionary(list);
            _pipeSender.SendToClient(jsonData);
        }
    }
}
