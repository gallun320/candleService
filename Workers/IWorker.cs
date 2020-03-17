using System;
using System.Collections.Generic;
using System.Text;
using GraphCandleApp.Utils;

namespace GraphCandleApp.Workers
{
    public interface IWorker
    {
        void Init(VolumeBarArbitrageData configData);
        void Start();
    }
}
