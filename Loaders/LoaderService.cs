using ExchangeApi.Enums;
using ExchangeApi.Rest.Future.Data.Trades;
using GraphCandleApp.Loaders.Connections;
using GraphCandleApp.Loaders.Connections.Abstract;
using GraphCandleApp.Loaders.Messages;
using GraphCandleApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCandleApp.Loaders
{
    public class LoaderService
    {
        private readonly List<TradeData> _cache;
        private readonly List<TradeData> _webSocketCahce;
        private readonly object _lockObj = new object();
        private bool _cacheRunning = true;
        private FileLoader _fileLoader;
        private IRestConnection _restConnector;
        private IWebSocketConnection _webSocketConnector;
        private ConnectionFactory _factory;

        private readonly ExchangeData _connectionData;
        public ExchangeData Connection => _connectionData;

        public LoaderService(ExchangeData connectionData)
        {
            _cache = new List<TradeData>();
            _webSocketCahce = new List<TradeData>();
            _connectionData = connectionData;
            InitServices();
        }

        private void InitServices()
        {
            _factory = new ConnectionFactory();
            _restConnector = _factory.GetRestClient(Connection);
            _webSocketConnector = _factory.GetWebSocketClient(Connection);
            _fileLoader = new FileLoader(ConfigLoader.Config.TradesFileDirectory);
        }

        public event EventHandler<LoaderMessage> OnLoadUpdate;

        public async Task InitLoad()
        {
            await _webSocketConnector.InitSocket();
        }

        public async Task StartLoad()
        {
            Logger.BaseLog.Log("StartLoad");
            _webSocketConnector.OnDataUpdate += UpdateCache;
            var trades = _fileLoader.LoadTrades((Exchange)Enum.Parse(typeof(Exchange), Connection.Exchange) , Connection.Instrument);
            _cache.AddRange(trades);
            await _restConnector.GetDataByPeriod((Exchange)Enum.Parse(typeof(Exchange), Connection.Exchange), Connection.Instrument, trades.Last().Date, DateTime.UtcNow)
                                                  .ContinueWith((task) =>
                                                  {
                                                      var data = task.Result;
                                                      lock(_lockObj)
                                                      {
                                                          _cacheRunning = !_cacheRunning;
                                                          var filterdWebChache = _webSocketCahce.Where(tradeData => tradeData.Date >= data.Last().Date)
                                                                                                .Select(tradeData => tradeData);
                                                          _cache.AddRange(data);
                                                          _cache.AddRange(filterdWebChache);
                                                          SendAllData();
                                                      }

                                                      
                                                  });
            
        }

        private void UpdateCache(object sender, WebSocketMessage e)
        {
            if (_cacheRunning)
            {
                lock (_lockObj)
                {
                    if (_cacheRunning)
                    {
                        Logger.BaseLog.Log("Add to cache");
                        _webSocketCahce.Add(e.Data);
                        return;
                    }
                }
            }
            OnLoadUpdate.Invoke(this, new LoaderMessage(e.Data));
        }

        private void SendAllData()
        {
            foreach(var data in _cache)
            {
                OnLoadUpdate.Invoke(this, new LoaderMessage(data));
            }
        }
    }
}
