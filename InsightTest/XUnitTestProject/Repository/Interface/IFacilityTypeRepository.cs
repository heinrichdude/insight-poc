using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestProject.Repository
{
    public interface IFacilityTypeRepository
    {
        FacilityType SelectFacilityType(int facilityTypeId);
        void InsertAddress(FacilityType facilityType);
        void UpdateAddress(FacilityType facilityType);
    }
}
