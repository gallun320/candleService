using GraphCandleApp.Servers;
using GraphCandleApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphCandleApp.Services
{
    public class NatsSenderService : IPipeSenderService, IDisposable
    {
        private NatsPublisher _natsPublisher;

        public NatsSenderService(string pipeName, VolumeBarArbitrageData configData)
        {
            _natsPublisher = new NatsPublisher();
            _natsPublisher.Connect(pipeName);
        }

        public void Dispose()
        {
            _natsPublisher.Dispose();
            _natsPublisher = null;
        }

        public void SendToClient(string mesaage)
        {
            _natsPublisher.Send(mesaage);
        }
    }
}
