﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InterswitchNameEnquiry.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class InterswitchNameEnquiryEntities : DbContext
    {
        public InterswitchNameEnquiryEntities()
            : base("name=InterswitchNameEnquiryEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public virtual DbSet<RequestResponseLog> RequestResponseLogs { get; set; }
        public virtual DbSet<AccessTokenLog> AccessTokenLogs { get; set; }
        public virtual DbSet<DebitTransactionLog> DebitTransactionLogs { get; set; }
    }
}
