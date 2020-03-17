using GraphCandleApp.Loaders;
using GraphCandleApp.Loaders.Messages;
using GraphCandleApp.Services;
using GraphCandleApp.Utils;

namespace GraphCandleApp.Workers
{
    public class ArbitrageWorker : IWorker
    {
        private readonly ICandelGraphService _candelGraphService;
        private LoaderManager _manager;

        public ArbitrageWorker(VolumeBarArbitrageData configData)
        {
            _candelGraphService = new CandelGraphService(configData);
            Init(configData);
        }

        public void Init(VolumeBarArbitrageData configData)
        {
            _manager = new LoaderManager(configData.ExchangeDataList);
            _manager.InitAll();
            foreach(var loader in _manager.Loaders)
            {
                loader.OnLoadUpdate += UpdateCandle;
            }
            Start();
        }

        private void UpdateCandle(object sender, LoaderMessage e)
        {
            _candelGraphService.SetTrade(e.Data);
        }

        public void Start()
        {
            _manager.StartAll();
        }
    }
}
