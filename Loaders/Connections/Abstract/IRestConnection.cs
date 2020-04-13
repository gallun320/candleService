using ExchangeApi.Enums;
using ExchangeApi.Rest.Future.Data.Trades;
using GraphCandleApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphCandleApp.Loaders.Connections.Abstract
{
    public interface IRestConnection
    {
        ExchangeData Connection { get; }
        Task<IEnumerable<TradeData>> GetDataByPeriod(Exchange exchange, string instrument, DateTime startDate, DateTime endDate);
    }
}
