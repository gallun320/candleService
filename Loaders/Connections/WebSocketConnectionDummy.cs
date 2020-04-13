using ExchangeApi.Enums;
using ExchangeApi.Rest.Future.Data.Trades;
using GraphCandleApp.Loaders.Connections.Abstract;
using GraphCandleApp.Loaders.Messages;
using GraphCandleApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphCandleApp.Loaders.Connections
{
    public class WebSocketConnectionDummy : IWebSocketConnection
    {
        private readonly Random _rnd = new Random();
        public WebSocketConnectionDummy(ExchangeData connectionData)
        {
            _connectionData = connectionData;
        }

        private readonly ExchangeData _connectionData;
        public ExchangeData Connection => _connectionData;

        public event EventHandler<WebSocketMessage> OnDataUpdate;

        private async Task RunEventMethod()
        {
            await Task.Delay(500);
            var trade = new TradeData(Exchange.Binance, _rnd.Next(1, 10), _rnd.Next(0, 10), _rnd.Next(0, 20), SideType.Buy, DateTime.UtcNow, "XBTUSD");
            OnDataUpdate.Invoke(this, new WebSocketMessage(trade));
        }

        public async Task InitSocket()
        {
            Task.Factory.StartNew(async () =>
            {
                while(true)
                {
                    await RunEventMethod();
                }
            });
        }
    }
}
