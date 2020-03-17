using System.Collections.Generic;
using System.IO;
using GraphCandleApp.Utils;
using Newtonsoft.Json;

namespace GraphCandleApp.Loaders
{
    public static class ConfigLoader
    {

        private static Config _config;
        public static Config Config => _config;

        public static void LoadConfig()
        {
            if (!File.Exists(AppPaths.ConfigPath))
            {
                CreateDefaultConfig();
            }

            var configTxt = File.ReadAllText(AppPaths.ConfigPath);

            var json = JsonConvert.DeserializeObject<Config>(configTxt);

            _config = json;

        }

        private static void CreateDefaultConfig()
        {
            Config c = new Config();

            c.DataList = new List<VolumeBarArbitrageData>()
            {
                new VolumeBarArbitrageData()
                {
                    DataType = DataType.Arbitrage,
                    Enable = true,
                    PipeName = "test",
                    PipeCount = 2,
                    ExchangeDataList = new List<ExchangeData>
                    {
                        new ExchangeData
                            {Exchange = Exchange.Bitmex, Instrument = "XBTUSD", VolumeBarAmount = 1_000_000},
                        new ExchangeData()
                            {Exchange = Exchange.BinanceFutures, Instrument = "BTCUSDT", VolumeBarAmount = 1_000},
                    }
                }
            };

            var json = JsonConvert.SerializeObject(c, Formatting.Indented);
            File.WriteAllText(AppPaths.ConfigPath, json);
        }
    }
}