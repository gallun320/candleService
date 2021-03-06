﻿using ExchangeApi.Rest.Future.Data.Trades;
using ExchangeApi.WebSockets.Abstract;
using ExchangeApi.WebSockets.Concrete;
using ExchangeApi.WebSockets.Requests.FTX;
using GraphCandleApp.Loaders.Connections.Abstract;
using GraphCandleApp.Loaders.Messages;
using GraphCandleApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphCandleApp.Loaders.Connections
{
    public class FtxWebSocketConnection : IWebSocketConnection
    {
        private AbstractWebSocketApi _wsClient;
        public FtxWebSocketConnection(ExchangeData connectionData)
        {
            _connectionData = connectionData;
        }

        private readonly ExchangeData _connectionData;
        public ExchangeData Connection => _connectionData;

        public event EventHandler<WebSocketMessage> OnDataUpdate;

        public async Task InitSocket()
        {
            _wsClient = new FtxWebSocketApi(false);
            _wsClient.OnWebSocketConnect += SocketConnected;
            _wsClient.Stream.OnExhangeTrade += ExchangeTradeDataRecieved;
            await _wsClient.Connect();
        }

        private void ExchangeTradeDataRecieved(TradeData obj)
        {
            OnDataUpdate.Invoke(this, new WebSocketMessage(obj));
        }

        private void SocketConnected()
        {
            _wsClient.Send(new FtxSubscribeRequest("trades", Connection.Instrument));
        }
    }
}
