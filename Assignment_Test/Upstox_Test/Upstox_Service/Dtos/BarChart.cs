using System;
using System.Collections.Generic;
using System.Text;

namespace Upstox_Service.Dtos
{
  public  class BarChart
    {
        public string sym { get; set; }
        public string T { get; set; }
        public float P { get; set; }
        public float Q { get; set; }
        public int bar_number { get; set; }

    }
}
