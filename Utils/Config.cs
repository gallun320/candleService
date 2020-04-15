using ExchangeApi.Enums;
using System.Collections.Generic;

namespace GraphCandleApp.Utils
{
    public class Config
    {
        public string TradesFileDirectory = "";
        public List<VolumeBarArbitrageData> DataList;
    }

    public class VolumeBarArbitrageData
    {
        public DataType DataType;
        public bool Enable = true;
        public string PipeName = "test";
        public int PipeCount = 10;

        public List<ExchangeData> ExchangeDataList;

        public override string ToString()
        {
            return $"{nameof(DataType)}: {DataType}, {nameof(Enable)}: {Enable}, {nameof(PipeName)}: {PipeName}, {nameof(ExchangeDataList)}: {ExchangeDataList.Count}";
        }
    }

    public class ExchangeData
    {
        public string Exchange;
        public string Instrument = "";

        /// <summary>
        /// Объем одного вольюм бара
        /// </summary>
        public decimal VolumeBarAmount;
    }
}