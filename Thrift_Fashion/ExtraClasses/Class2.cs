using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Thrift_Fashion.Models;

namespace Thrift_Fashion.ExtraClasses
{
    public class Class2
    {
        public Models.product Product { get; set; }
        public Models.Review review { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<product> WOMENPRODUCT { get; set; }
        public IEnumerable<product> MENPRODUCT { get; set; }
    }
}