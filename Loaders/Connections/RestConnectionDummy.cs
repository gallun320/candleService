using GraphCandleApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphCandleApp.Loaders.Connections
{
    public class RestConnectionDummy : IRestConnection
    {
        private readonly Random _rnd = new Random();
        private readonly List<TradeData> _trades;
        public RestConnectionDummy(ExchangeData connectionData)
        {
            _connectionData = connectionData;
            _trades = new List<TradeData>
            {
                new TradeData(Exchange.Binance, _rnd.Next(1, 10), _rnd.Next(0, 10), _rnd.Next(0, 20), SideType.Buy, DateTime.UtcNow, "XBTUSD"),
                new TradeData(Exchange.Binance, _rnd.Next(1, 10), _rnd.Next(0, 10), _rnd.Next(0, 20), SideType.Buy, DateTime.UtcNow, "XBTUSD"),
                new TradeData(Exchange.Binance, _rnd.Next(1, 10), _rnd.Next(0, 10), _rnd.Next(0, 20), SideType.Buy, DateTime.UtcNow, "XBTUSD"),
                new TradeData(Exchange.Binance, _rnd.Next(1, 10), _rnd.Next(0, 10), _rnd.Next(0, 20), SideType.Buy, DateTime.UtcNow, "XBTUSD"),
                new TradeData(Exchange.Binance, _rnd.Next(1, 10), _rnd.Next(0, 10), _rnd.Next(0, 20), SideType.Buy, DateTime.UtcNow, "XBTUSD")
            };
        }

        private readonly ExchangeData _connectionData;
        public ExchangeData Connection => _connectionData;

        public async Task<IEnumerable<TradeData>> GetDataByPeriod(Exchange exchange, string instrument, DateTime startDate, DateTime endDate)
        {
            await Task.Delay(1500);
            return await Task.FromResult(_trades);
        }
    }
}
