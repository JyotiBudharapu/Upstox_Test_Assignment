using System;
using System.Collections.Generic;
using System.Text;

namespace Upstox_Service.Dtos
{
   public class OHLC
    {
        public float o { get; set; }
        public float h { get; set; }
        public float l { get; set; }
        public float c { get; set; }
        public int volume { get; set; }
        public string events { get;set; }
        public string symbol { get; set; }
        public int bar_num { get; set; }
    }
}
