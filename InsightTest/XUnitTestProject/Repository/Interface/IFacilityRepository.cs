using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestProject.Repository
{
    public interface IFacilityRepository
    {
        Facility SelectFacility(int facilityId);
        void InsertFacility(Facility facility);
        void UpdateFacility(Address facility);
    }
}
