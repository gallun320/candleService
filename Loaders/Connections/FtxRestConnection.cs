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
    public class FtxRestConnection : IRestConnection
    {
        private RestFutureBase _restApiClient;
        public FtxRestConnection(ExchangeData connectionData)
        {
            _connectionData = connectionData;
            Init();
        }

        private readonly ExchangeData _connectionData;
        public ExchangeData Connection => _connectionData;

        private void Init()
        {
            _restApiClient = new FtxRestApi(new ExchangeCredentials());
        }

        public async Task<IEnumerable<TradeData>> GetDataByPeriod(Exchange exchange, string instrument, DateTime startDate, DateTime endDate)
        {
            return await _restApiClient.GetTradesAsync(instrument, startDate, endDate);
        }
    }
}
