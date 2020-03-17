using GraphCandleApp.Loaders.Connections;
using GraphCandleApp.Loaders.Messages;
using GraphCandleApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCandleApp.Loaders
{
    public class LoaderManager
    {
        private readonly List<LoaderService> _services;
        private readonly List<ExchangeData> _connections;

        public LoaderManager(IEnumerable<ExchangeData> connectionDatas)
        {
            _connections = connectionDatas.ToList();
            _services = new List<LoaderService>();
        }

        public List<ExchangeData> Connections => _connections;
        public List<LoaderService> Loaders => _services;

        public void InitAll()
        {
            foreach(var connection in Connections)
            {
                _services.Add(new LoaderService(connection));
            }
        }

        public void StartAll()
        {
            Logger.BaseLog.Log("Start all");
            var taskList = new List<Task>();
            foreach (var service in _services)
            {
                taskList.Add(new Task(async () => {
                    await service.InitLoad();
                    await service.StartLoad();
                }));
            }

            Task.Factory.StartNew(async () => {
                foreach(var task in taskList)
                {
                    task.Start();
                }
                await Task.WhenAll(taskList.ToArray());
           });  
        }
    }
}
