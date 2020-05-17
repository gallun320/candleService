using System;
using System.Collections.Generic;
using System.Text;
using ExchangeApi.Config;
using NATS.Client;

namespace GraphCandleApp.Servers
{
    public class NatsPublisher : IDisposable
    {
        private IConnection _natsConnection;
        private string _eventName;

        public void Connect(string pipeName)
        {
            var connectionFactory = new ConnectionFactory();
            _natsConnection = connectionFactory.CreateConnection();
            _eventName = pipeName;
        }

        public void Send(string data)
        {
            _natsConnection.Publish(_eventName, Encoding.UTF8.GetBytes(data));
        }

        public void Dispose()
        {
            _natsConnection?.Drain();
            _natsConnection?.Close();
            _natsConnection = null;
        }
    }
}
