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

    public partial class admin
    {
        [Display (Name ="ID")]
        public int A_id { get; set; }
        [Display(Name = "Name")]
        public string A_Name { get; set; }
        [Display(Name = "E-Mail")]
        public string A_Email { get; set; }
        [Display(Name = "Password")]
        public string A_Password { get; set; }
        [Display(Name = "Profile Picture")]
        public string A_Pic { get; set; }
    }
}
