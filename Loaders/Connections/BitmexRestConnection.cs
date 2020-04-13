using ExchangeApi.Credentials;
using ExchangeApi.Enums;
using ExchangeApi.Rest.Api;
using ExchangeApi.Rest.Api.Abstract;
using ExchangeApi.Rest.Future.Data.Trades;
using GraphCandleApp.Loaders.Connections.Abstract;
using GraphCandleApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphCandleApp.Loaders.Connections
{
    public class BitmexRestConnection : IRestConnection
    {
        private RestFutureBase _restApiClient;
        public BitmexRestConnection(ExchangeData connectionData)
        {
            _connectionData = connectionData;
            Init();
        }

        private readonly ExchangeData _connectionData;
        public ExchangeData Connection => _connectionData;

        private void Init()
        {
            _restApiClient = new BitmexRestApi(new ExchangeCredentials(), false);
        }

        public async Task<IEnumerable<TradeData>> GetDataByPeriod(Exchange exchange, string instrument, DateTime startDate, DateTime endDate)
        {
            return await _restApiClient.GetTradesAsync(instrument, startDate, endDate);
        }
    }
}
