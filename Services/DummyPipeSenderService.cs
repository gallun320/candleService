using GraphCandleApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphCandleApp.Services
{
    public class DummyPipeSenderService : IPipeSenderService
    {
        public DummyPipeSenderService(string pipeName, VolumeBarArbitrageData configData)
        {
        }

        public void SendToClient(string mesaage)
        {
            Logger.BaseLog.Log(mesaage);
        }
    }
}
