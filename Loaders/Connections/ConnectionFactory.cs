using ExchangeApi.Enums;
using GraphCandleApp.Loaders.Connections.Abstract;
using GraphCandleApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphCandleApp.Loaders.Connections
{
    public class ConnectionFactory
    {
        public IRestConnection GetRestClient(ExchangeData connectionData)
        {
            switch(connectionData.Exchange)
            {
                case Exchange.Bitmex:
                    return new BitmexRestConnection(connectionData);
                case Exchange.FTX:
                    return new FtxRestConnection(connectionData);
                default:
                    throw new NotImplementedException();
            }
        }

        public IWebSocketConnection GetWebSocketClient(ExchangeData connectionData)
        {
            switch (connectionData.Exchange)
            {
                case Exchange.Bitmex:
                    return new BitmexWebSocketConnection(connectionData);
                case Exchange.FTX:
                    return new FtxWebSocketConnection(connectionData);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
