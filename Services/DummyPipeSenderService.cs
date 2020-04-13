using GraphCandleApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphCandleApp.Services
{
    public class DummyPipeSenderService : IPipeSenderService
    {
        public void SendToClient(string mesaage)
        {
            Logger.BaseLog.Log(mesaage);
        }
    }
}
