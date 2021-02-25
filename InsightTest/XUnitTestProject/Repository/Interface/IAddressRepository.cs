using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestProject.Repository
{
    public interface IAddressRepository
    {
        Address SelectAddress(int addressId);
        IList<Address> SelectAddresses(List<int> addressIds);
        IList<Address> FindAddresses(Address address);
        IList<Address> FindAddresses(object criteria);
        Address InsertAddress(Address address);
        IEnumerable<Address> InsertAddresses(IList<Address> address);
        IList<Address> GetAll();
    }
}
