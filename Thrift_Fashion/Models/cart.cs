//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Thrift_Fashion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class cart
    {
        [Display(Name = "ID")]
        public int C_ID { get; set; }
        [Display(Name = "Product ID")]
        public string Pro_ID { get; set; }
        [Display(Name = "Product Name")]
        public string Pro_Name { get; set; }
        [Display(Name = "Product QTY")]
        public string Pro_qty { get; set; }
    }
}
