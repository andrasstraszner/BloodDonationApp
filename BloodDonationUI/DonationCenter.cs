//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BloodDonationUI
{
    using System;
    using System.Collections.Generic;
    
    public partial class DonationCenter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonationCenter()
        {
            this.DonationLog = new HashSet<DonationLog>();
            this.PhoneNumber = new HashSet<PhoneNumber>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int DonationCenterTypeId { get; set; }
        public Nullable<int> RegionId { get; set; }
        public Nullable<int> AddressId { get; set; }
        public Nullable<int> OpeningHoursId { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual DonationCenterType DonationCenterType { get; set; }
        public virtual OpeningHours OpeningHours { get; set; }
        public virtual Region Region { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonationLog> DonationLog { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhoneNumber> PhoneNumber { get; set; }
    }
}