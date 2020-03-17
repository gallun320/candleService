using GraphCandleApp.Loaders;
using GraphCandleApp.Workers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphCandleApp
{
    public class Startup 
    {
        private readonly List<IWorker> _workers = new List<IWorker>();
        public void Start()
        {
            ConfigLoader.LoadConfig();
            CreateWorkers();
            while (true) { };
        }

        public void CreateWorkers()
        {
            foreach(var volumeBar in ConfigLoader.Config.DataList)
            {
                var worker = new ArbitrageWorker(volumeBar);
                _workers.Add(worker);
            }

        }

    }

}
