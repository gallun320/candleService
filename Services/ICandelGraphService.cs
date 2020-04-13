using ExchangeApi.Rest.Future.Data.Trades;
using GraphCandleApp.Utils;

namespace GraphCandleApp.Services
{
    public interface ICandelGraphService
    {
        void SetTrade(TradeData data);
    }
}
