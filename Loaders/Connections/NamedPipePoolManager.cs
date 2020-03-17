using System;
using System.Collections.Generic;
using System.Text;

namespace GraphCandleApp.Loaders.Connections
{
    public class NamedPipePoolManager
    {
        private readonly List<NamedPipeServer> _pipes;
        private readonly string _pipeName;
        private readonly int _poolCount;

        public NamedPipePoolManager(string pipeName, int poolCount)
        {
            _pipeName = pipeName;
            _poolCount = poolCount;
            _pipes = new List<NamedPipeServer>();
            InitPool();
        }

        public void InitPool()
        {
            for (var i = 0; i < _poolCount; ++i)
            {
                _pipes.Add(new NamedPipeServer(_pipeName, _poolCount));
            }
        }

        public void SendToAll(string msg)
        {
            foreach(var pipe in _pipes)
            {
                pipe.SendMessage(msg);
            }
        }

        public void Dispose()
        {
            for(var i = 0; i < _pipes.Count; ++i)
            {
                _pipes[i].Dispose();
                _pipes.RemoveAt(i);
            }
        }
    }
}
