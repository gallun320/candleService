using System;
using System.Collections.Generic;
using System.Text;

namespace GraphCandleApp.Utils
{
    public class OrderBookTop
    {
        public OrderBookData Ask;

        public OrderBookData Bid;

        public OrderBookTop(OrderBookData ask, OrderBookData bid)
        {
            Ask = ask;
            Bid = bid;
        }

        public decimal MidPrice => (Ask.Price + Bid.Price) / 2;

        public override string ToString()
        {
            return $"Ask: {Ask.Price} - {Ask.Amount}, Bid: {Bid.Price} - {Bid.Amount}";
        }
    }

    public class OrderBookData
    {
        public decimal Price;
        public decimal Amount;
        public long Id;

        public OrderBookData(decimal price, decimal amount, long id = 0)
        {
            Update(price, amount);
            Id = id;
        }

        public void Update(decimal price, decimal amount, long id = 0)
        {
            UpdatePrice(price);
            UpdateAmount(amount);
            UpdateId(id);
        }

        public void Update(OrderBookData other)
        {
            Price = other.Price;
            Amount = other.Amount;
        }

        public void UpdateId(long id)
        {
            Id = id;
        }

        public void UpdatePrice(decimal price)
        {
            Price = price;
        }

        public void UpdateAmount(decimal amount)
        {
            Amount = amount;
        }

        public override string ToString()
        {
            return $"{nameof(Price)}: {Price}, {nameof(Amount)}: {Amount}, {nameof(Id)}: {Id}";
        }
    }

    public class FutureKlineData
    {
        public decimal Open { get; private set; }
        public decimal High { get; private set; }
        public decimal Low { get; private set; }
        public decimal Close { get; private set; }
        public decimal Volume { get; private set; }
        public decimal Time { get; protected set; }
        public DateTime Date { get; protected set; }

        public FutureKlineData()
        {
        }

        public FutureKlineData(FutureKlineData other)
        {
            Update(other);
        }

        public FutureKlineData(decimal open, decimal high, decimal low, decimal close, decimal volume, DateTime date)
        {
            Update(open, high, low, close, volume, date);
        }

        public void Update(FutureKlineData other)
        {
            Update(other.Open, other.High, other.Low, other.Close, other.Volume, other.Date);
        }

        public void Update(decimal open, decimal high, decimal low, decimal close, decimal volume, DateTime date)
        {
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
            Date = date;
        }

        public void Update(decimal price, decimal quantity)
        {
            if (Open == 0)
            {
                Open = price;
            }

            Volume += quantity;
            Close = price;
            High = Math.Max(High, price);

            if (Low == 0)
            {
                Low = price;
            }
            else
            {
                Low = Math.Min(Low, price);
            }
        }


        public override string ToString()
        {
            return $"{nameof(Close)}: {Close:##.000}, {nameof(Date)}: {Date}";
        }
    }
}
