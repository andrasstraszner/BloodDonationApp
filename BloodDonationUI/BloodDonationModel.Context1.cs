﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BloodDonationEntities : DbContext
    {
        public BloodDonationEntities()
            : base("name=BloodDonationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ABOBloodGroup> ABOBloodGroup { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<DonationCenter> DonationCenter { get; set; }
        public virtual DbSet<DonationCenterType> DonationCenterType { get; set; }
        public virtual DbSet<DonationLog> DonationLog { get; set; }
        public virtual DbSet<Donor> Donor { get; set; }
        public virtual DbSet<DonorSex> DonorSex { get; set; }
        public virtual DbSet<OpeningHours> OpeningHours { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumber { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<RhBloodGroup> RhBloodGroup { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
