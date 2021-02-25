using System;
using System.Collections.Generic;
using System.Text;
using Insight.Database;

namespace XUnitTestProject
{
    public class Address
    {
        public int AddressId { get; set; }
        public int AddressTypeId { get; set; }
        public AddressType AddressType { get; set; }
        public int? PartyId { get; set; }
        [ParentRecordId]
        public int? FacilityId { get; set; }
        public string Description { get; set; }
        public string VerifiedFlag { get; set; }
        public string PrimaryFlag { get; set; }
        public string UnlistedFlag { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ThruDate { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatePartyId { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdatePartyId { get; set; }
    }
}
