using GraphCandleApp.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace GraphCandleApp.Loaders
{
    public class FileLoader
    {
        private DateTime _epochTime = new DateTime(1970, 1, 1, 0, 0, 0);
        private readonly string _dirPath;

        public FileLoader(string path)
        {
            _dirPath = path;
        }

        public List<TradeData> LoadTrades(Exchange e, string instrument)
        {
            var fileName = Path.Combine(_dirPath, $"{e}_{instrument}.txt");
            if (!File.Exists(fileName))
            {
                Logger.BaseLog.Log($"Can't find Trades file: {fileName}");
                throw new FileNotFoundException();
            }

            var l = new List<TradeData>();
            var lines = File.ReadAllLines(fileName);

            foreach (var line in lines)
            {
                var trade = ParseTrade(line, e, instrument);
                if(string.IsNullOrEmpty(trade.Instrument)) continue;
                l.Add(trade);
            }

            return l;
        }

        private TradeData ParseTrade(string line, Exchange e, string instrument)
        {
            if (string.IsNullOrEmpty(line)) return default;

            var split = line.Split(' ');

            var timeStamp = long.Parse(split[0]);
            var dateTime = _epochTime.AddMilliseconds(timeStamp);
            SideType side = split[1].ToLower() == "buy" ? SideType.Buy : SideType.Sell;
            var price = decimal.Parse(split[2]);
            var quantity = decimal.Parse(split[3]);

            return new TradeData(e, 0, quantity, price, side, dateTime, instrument);
        }
    }
}