using ExchangeApi.Rest.Future.Data.Trades;
using GraphCandleApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphCandleApp.Loaders.Messages
{
    public class WebSocketMessage : EventArgs
    {
        public TradeData Data { get; private set; }
        public WebSocketMessage(TradeData data)
        {
            Data = data;
        }
    }
}
