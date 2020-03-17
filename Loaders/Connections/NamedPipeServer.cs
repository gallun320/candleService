using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Pipes;
using System.Threading.Tasks;
using System.IO;

namespace GraphCandleApp.Loaders.Connections
{
    public class NamedPipeServer : IDisposable
    {
        private NamedPipeServerStream _pipe;
        private readonly object _lockObject = new object();
        private readonly string _pipeName;
        private bool _disposeFlag = false;
        private string _msg;

        public NamedPipeServer(string pipeName, int poolCount)
        {
            _pipeName = pipeName;
            InitServer(pipeName, poolCount);
        }

        public void InitServer(string pipeName, int poolCount)
        {
            _pipe = new NamedPipeServerStream(pipeName, PipeDirection.Out, poolCount, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
            var asyn = _pipe.BeginWaitForConnection(OnConnected, null);
        }

        private void OnConnected(IAsyncResult ar)
        {
            if(!_disposeFlag)
            {
                lock(_lockObject)
                {
                    if(!_disposeFlag)
                    {
                        _pipe.EndWaitForConnection(ar);
                        Task.Factory.StartNew(SendMessage);
                    }
                }
            }
        }

        private void SendMessage()
        {
            lock(_lockObject)
            {
                using (var stream = new StreamWriter(_pipe))
                {
                   
                    stream.AutoFlush = true;
                    stream.WriteLine(_msg);

                }
            }
        }

        public void SendMessage(string message)
        {
            lock(_lockObject)
            {
                _msg = message;
                if(_pipe != null && _pipe.IsConnected)
                {
                    Task.Factory.StartNew(SendMessage);
                }
            }
        }

        public void Dispose()
        {
            _disposeFlag = true;
            _pipe.Dispose();
            _pipe = null;
        }
    }
}
