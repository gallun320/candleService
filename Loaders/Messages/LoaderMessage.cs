using GraphCandleApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphCandleApp.Loaders.Messages
{
    public class LoaderMessage : EventArgs
    {
        public TradeData Data { get; private set; }

        public LoaderMessage(TradeData data)
        {
            Data = data;
        }
    }
}
