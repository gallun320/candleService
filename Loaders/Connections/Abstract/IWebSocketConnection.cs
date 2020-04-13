using GraphCandleApp.Loaders.Messages;
using GraphCandleApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphCandleApp.Loaders.Connections.Abstract
{
    public interface IWebSocketConnection
    {
        ExchangeData Connection { get; }
        event EventHandler<WebSocketMessage> OnDataUpdate;
        Task InitSocket();
    }
}
