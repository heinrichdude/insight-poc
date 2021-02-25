using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestProject.Repository
{
    public interface IAddressTypeRepository
    {
        AddressType SelectAddressType(int addressTypeId);
        IList<AddressType> SelectAddressTypes(List<int> addressTypeIds);
        IList<AddressType> FindAddressTypes(AddressType addressType);
        IList<AddressType> FindAddressTypes(object criteria);
        AddressType InsertAddressType(AddressType addressType);
        IEnumerable<AddressType> InsertAddressTypes(IList<AddressType> addressTypes);
        IList<AddressType> GetAll();
    }
}
