//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Manulife.TopFiveWebsites.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserProfile
    {
        public string userId { get; set; }
        public string password { get; set; }
        public bool isActive { get; set; }
        public string createdBy { get; set; }
        public System.DateTime createdOn { get; set; }
        public string modifiedBy { get; set; }
        public Nullable<System.DateTime> modifiedOn { get; set; }
        public string roles { get; set; }
    }
}
