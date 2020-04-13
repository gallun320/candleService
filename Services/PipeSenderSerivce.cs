using GraphCandleApp.Loaders.Connections;
using GraphCandleApp.Servers;
using GraphCandleApp.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Text;

namespace GraphCandleApp.Services
{
    public class PipeSenderSerivce : IPipeSenderService, IDisposable
    {
        private NamedPipePoolManager _pipeManager;
        public PipeSenderSerivce(string pipeName, VolumeBarArbitrageData configData)
        {
            _pipeManager = new NamedPipePoolManager(pipeName, configData.PipeCount);
        }

        public void Dispose()
        {
            _pipeManager.Dispose();
            _pipeManager = null;
        }

        public void SendToClient(string mesaage)
        {
            if(_pipeManager != null)
            {
                _pipeManager.SendToAll(mesaage);
            }
        }
    }
}
