using System;
using System.Collections.Generic;
using System.Text;

namespace GraphCandleApp.Services
{
    public interface IPipeSenderService
    {
        void SendToClient(string mesaage);
    }
}
