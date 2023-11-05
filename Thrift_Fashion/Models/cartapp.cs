using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Thrift_Fashion.Models
{
    public class cartapp
    {
        public int P_ID { get; set; }
        public string P_Name { get; set; }
        public string P_Pic { get; set; }
        public int qty { get; set; }
        public int P_Price { get; set; }
        public int P_Totalprice { get; set; }

    }
}