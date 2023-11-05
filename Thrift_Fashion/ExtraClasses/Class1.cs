using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Thrift_Fashion.Models;
namespace Thrift_Fashion.ExtraClasses
{
    public class listTables
    {
        public IEnumerable<Billing> Billings { get; set; }

        public IEnumerable<product> Products { get; set; }

        public IEnumerable<user> Users { get; set; }

        public IEnumerable<admin> Admins { get; set; }
    }
}