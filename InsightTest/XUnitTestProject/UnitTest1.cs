using System;
using System.Collections.Generic;
using Xunit;
using Insight.Database;
using System.Data.SqlClient;
using XUnitTestProject.Repository;
using System.Linq;
using System.Data;

namespace XUnitTestProject
{
    public class UnitTest1
    {
        private const string  _databaseConnectionString = "Server='localhost';"
                + "Initial Catalog='CCDB_MASTER_TEST';"
                + "Integrated Security=false;"
                + "User ID='sa';"
                + "Password='yourStrong(!)Password'";

        private SqlConnectionStringBuilder _database = new SqlConnectionStringBuilder(_databaseConnectionString);

        private const string _flagYes = "Y";
        private const string _flagNo = "N";

        public UnitTest1() 
        {
        }

        [Fact]
        public void GetAddressTypes_SqlConnection()
        {
            var connection = new SqlConnection(_databaseConnectionString);
            
            // run a query right off the connection (this performs an auto-open/close)
            var addressTypes = connection.QuerySql("SELECT * FROM AddressType");
            Assert.True(addressTypes.Count == 3);
        }

        [Fact]
        public void GetAddressTypes_SqlConnection_WithRepository()
        {
            var addressTypeRepository = new AddressTypeRepository(_databaseConnectionString);

            // run a query right off the connection (this performs an auto-open/close)
            var addressTypes = addressTypeRepository.QuerySql("SELECT * FROM AddressType");
            Assert.True(addressTypes.Count == 3);
        }

        [Fact]
        public void GetAddressTypes_SqlConnectionStringBuilder()
        {
            // issue a SQL query on the AddressType table
            var addressTypes = _database.Connection().QuerySql("SELECT * FROM AddressType");
            Assert.True(addressTypes.Count == 3);
        }

        [Fact]
        public void GetAddressTypes_SqlConnectionStringBuilder_WithRepository()
        {
            var addressTypeRepository = new AddressTypeRepository(_database.Connection());
            
            // issue a SQL query on the AddressType table
            var addressTypes = addressTypeRepository.QuerySql("SELECT * FROM AddressType");
            Assert.True(addressTypes.Count == 3);
        }

        [Fact]
        public void GetAddressType_ByName_PostalAddress()
        {
            // get AddressType by Name column
            const string addressTypeName = "Postal Address";
            var addressTypes = _database.Connection().Query<AddressType>("FindAddressTypes", new { Name = addressTypeName });
            Assert.True(addressTypes.Count == 1);
            Assert.True(addressTypes[0].Name == addressTypeName);
        }

        [Fact]
        public void GetAddressType_ByName_PostalAddress_WithRepository()
        {
            var addressTypeRepository = new AddressTypeRepository(_database.Connection());

            // get AddressType by Name column
            const string addressTypeName = "Postal Address";
            var addressTypes = addressTypeRepository.FindAddressTypes(new { Name = addressTypeName });
            Assert.True(addressTypes.Count == 1);
            Assert.True(addressTypes[0].Name == addressTypeName);
        }

        [Fact]
        public void GetAddressType_All()
        {
            // Pass no parameters to Find to get all
            var addressTypes = _database.Connection().Query<AddressType>("FindAddressTypes");
            Assert.True(addressTypes.Count == 3);
        }

        [Fact]
        public void GetAddressType_All_WithRepository()
        {
            var addressTypeRepository = new AddressTypeRepository(_database.Connection());

            // Pass no parameters to Find to get all
            var addressTypes = addressTypeRepository.GetAll();
            Assert.True(addressTypes.Count == 3);
        }

        [Fact]
        public void AddressType_Select_Insert_Select_Delete()
        {
            using (var connection = _database.Open())
            {
                // get the count before the insert
                var addressTypesBefore = connection.Query<AddressType>("FindAddressTypes");
                Assert.True(addressTypesBefore.Count == 3);

                // insert new AddressType
                connection.Execute("InsertAddressType", new AddressType()
                {
                    AddressTypeId = 99,
                    AddressTypeCode = null,
                    Name = "Test",
                    Description = "A temporary test entry.",
                    FromDate = DateTime.Now,
                    ThruDate = null,
                    CreateDate = DateTime.Now,
                    CreatePartyId = 99,
                    UpdateDate = DateTime.Now,
                    UpdatePartyId = 99
                });

                // get the count after the insert, order by AddressTypeId DESC
                var addressTypesAfter = connection.Query<AddressType>("FindAddressTypes", new { OrderBy = "[AddressTypeId] DESC" });
                Assert.True(addressTypesAfter.Count == 4);

                // delete the one we just inserted
                connection.Execute("DeleteAddressType", addressTypesAfter.First());

                var addressType99 = connection.Query<AddressType>("FindAddressTypes", new { AddressTypeId = 99 });
                Assert.True(addressType99.Count == 0);
            }
        }

        [Fact]
        public void AddressType_Select_Insert_Select_Delete_WithRepository()
        {
            using (var connection = _database.Open())
            {
                var addressTypeRepository = new AddressTypeRepository(connection);

                // get the count before the insert
                var addressTypesBefore = addressTypeRepository.GetAll();
                Assert.True(addressTypesBefore.Count == 3);

                // insert new AddressType
                addressTypeRepository.InsertAddressType(new AddressType()
                {
                    AddressTypeId = 99,
                    AddressTypeCode = null,
                    Name = "Test",
                    Description = "A temporary test entry.",
                    FromDate = DateTime.Now,
                    ThruDate = null,
                    CreateDate = DateTime.Now,
                    CreatePartyId = 99,
                    UpdateDate = DateTime.Now,
                    UpdatePartyId = 99
                });

                // get the count after the insert, order by AddressTypeId DESC
                var addressTypesAfter = addressTypeRepository.FindAddressTypes(new { OrderBy = "[AddressTypeId] DESC" });
                Assert.True(addressTypesAfter.Count == 4);

                // delete the one we just inserted
                addressTypeRepository.DeleteAddressType(addressTypesAfter.First());

                // make sure its gone
                var addressType99 = addressTypeRepository.SelectAddressType(99);
                Assert.True(addressType99 == null);
            }
        }

        [Fact]
        public void AddressType_Insert_Address_Insert_Rollback()
        {
            try
            {
                using (var connection = _database.OpenWithTransaction())
                {
                    // insert new AddressType
                    connection.Execute("InsertAddressType", new AddressType()
                    {
                        AddressTypeId = 99,
                        AddressTypeCode = null,
                        Name = "Test",
                        Description = "A temporary test entry.",
                        FromDate = DateTime.Now,
                        ThruDate = null,
                        CreateDate = DateTime.Now,
                        CreatePartyId = 99,
                        UpdateDate = DateTime.Now,
                        UpdatePartyId = 99
                    });

                    // insert new AddressType
                    connection.Execute("InsertAddressType", new Address()
                    {
                        AddressId = 99,
                        AddressTypeId = 99,
                        Description = "A temporary test entry.",
                        FacilityId = null,
                        PartyId = null,
                        PrimaryFlag = _flagYes,
                        UnlistedFlag = _flagNo,
                        VerifiedFlag = _flagNo,
                        FromDate = DateTime.Now,
                        ThruDate = null,
                        CreateDate = DateTime.Now,
                        CreatePartyId = 99,
                        UpdateDate = DateTime.Now,
                        UpdatePartyId = 99
                    });

                    // force an error so we don't commit
                    throw new Exception("Just for testing purposes.");

                    connection.Commit();
                }
            }
            catch
            {
                // check that we didn't commit the AddressType
                var addressType99 = _database.Connection().Query<AddressType>("FindAddressTypes", new { AddressTypeId = 99 });
                Assert.True(addressType99.Count == 0);

                // check that we didn't commit the Address
                var address99 = _database.Connection().Query<Address>("FindAddresses", new { AddressId = 99 });
                Assert.True(address99.Count == 0);
            }
        }

        [Fact]
        public void AddressType_Insert_Address_Insert_Commit()
        {
            try
            {
                using (var connection = _database.OpenWithTransaction())
                {
                    // insert new AddressType
                    connection.Execute("InsertAddressType", new AddressType()
                    {
                        AddressTypeId = 99,
                        AddressTypeCode = null,
                        Name = "Test",
                        Description = "A temporary test entry.",
                        FromDate = DateTime.Now,
                        ThruDate = null,
                        CreateDate = DateTime.Now,
                        CreatePartyId = 99,
                        UpdateDate = DateTime.Now,
                        UpdatePartyId = 99
                    });

                    // insert new AddressType
                    connection.Execute("InsertAddress", new Address()
                    {
                        AddressTypeId = 99,
                        Description = "A temporary test entry.",
                        FacilityId = null,
                        PartyId = null,
                        PrimaryFlag = _flagYes,
                        UnlistedFlag = _flagNo,
                        VerifiedFlag = _flagNo,
                        FromDate = DateTime.Now,
                        ThruDate = null,
                        CreateDate = DateTime.Now,
                        CreatePartyId = 99,
                        UpdateDate = DateTime.Now,
                        UpdatePartyId = 99
                    });

                    connection.Commit();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                // check that we committed the AddressType
                var addressType99 = _database.Connection().Query<AddressType>("FindAddressTypes", new { AddressTypeId = 99 });
                Assert.True(addressType99.Count == 1);

                // check that we committed the Address, find it by the AddressTypeId
                var address99 = _database.Connection().Query<Address>("FindAddresses", new { AddressTypeId = 99 });
                Assert.True(address99.Count == 1);

                // delete what we just inserted
                _database.Connection().Execute("DeleteAddress", new { AddressId = address99.First().AddressId });
                _database.Connection().Execute("DeleteAddressType", new { AddressTypeId = 99 });
            }
        }

        [Fact]
        public void AddressType_GetAll_With_Reader()
        {
            using (SqlConnection connection = _database.Open())
            using (IDataReader reader = connection.GetReader("FindAddressTypes"))
            {
                var someTotal = 0;
                var addressTypes = reader.ToList<AddressType>();
                foreach (var addressType in addressTypes)
                {
                    someTotal += addressType.AddressTypeId;
                }

                Assert.True(someTotal > 0);
                Assert.True(addressTypes.Count() > 0);
            }
        }

        [Fact]
        public void AddressType_GetAll_With_Reader_AsEnumerable()
        {
            using (SqlConnection connection = _database.Open())
            using (IDataReader reader = connection.GetReader("FindAddressTypes"))
            {
                var someTotal = 0;
                var addressTypes = reader.AsEnumerable<AddressType>();
                foreach (var addressType in addressTypes)
                {
                    someTotal += addressType.AddressTypeId;
                }

                Assert.True(someTotal > 0);
            }
        }

        [Fact]
        public void Address_Insert_GetIdentityInserted_Delete()
        {
            using (var connection = _database.Open())
            {
                var addressToInsert = new Address()
                {
                    AddressTypeId = 1,
                    Description = "A temporary test entry.",
                    FacilityId = null,
                    PartyId = null,
                    PrimaryFlag = _flagYes,
                    UnlistedFlag = _flagNo,
                    VerifiedFlag = _flagNo,
                    FromDate = DateTime.Now,
                    ThruDate = null,
                    CreateDate = DateTime.Now,
                    CreatePartyId = 99,
                    UpdateDate = DateTime.Now,
                    UpdatePartyId = 99
                };

                // insert that new Address with 'Insert' rather than 'Execute', this will
                // populate the entity with the returned identity value of the insert.
                connection.Insert("InsertAddress", addressToInsert);
                Assert.True(addressToInsert.AddressId > 0);

                // delete the one we just inserted
                connection.Execute("DeleteAddress", new { AddressId = addressToInsert.AddressId });

                // check that we in fact deleted the address
                var inserttedAddress = _database.Connection().Query<Address>("FindAddresses", new { AddressId = addressToInsert.AddressId });
                Assert.True(inserttedAddress.Count == 0);
            }
        }

        [Fact]
        public void Addresses_InsertList_GetIdentitiesInserted_Delete()
        {
            using (var connection = _database.Open())
            {
                var addressesToInsert = new List<Address>()
                {
                    new Address()
                    {
                        AddressTypeId = 1,
                        Description = "A temporary test entry #1.",
                        FacilityId = null,
                        PartyId = null,
                        PrimaryFlag = _flagYes,
                        UnlistedFlag = _flagNo,
                        VerifiedFlag = _flagNo,
                        FromDate = DateTime.Now,
                        ThruDate = null,
                        CreateDate = DateTime.Now,
                        CreatePartyId = 99,
                        UpdateDate = DateTime.Now,
                        UpdatePartyId = 99
                    },
                    new Address()
                    {
                        AddressTypeId = 2,
                        Description = "A temporary test entry #2.",
                        FacilityId = null,
                        PartyId = null,
                        PrimaryFlag = _flagYes,
                        UnlistedFlag = _flagNo,
                        VerifiedFlag = _flagNo,
                        FromDate = DateTime.Now,
                        ThruDate = null,
                        CreateDate = DateTime.Now,
                        CreatePartyId = 99,
                        UpdateDate = DateTime.Now,
                        UpdatePartyId = 99
                    },
                    new Address()
                    {
                        AddressTypeId = 3,
                        Description = "A temporary test entry #3.",
                        FacilityId = null,
                        PartyId = null,
                        PrimaryFlag = _flagYes,
                        UnlistedFlag = _flagNo,
                        VerifiedFlag = _flagNo,
                        FromDate = DateTime.Now,
                        ThruDate = null,
                        CreateDate = DateTime.Now,
                        CreatePartyId = 99,
                        UpdateDate = DateTime.Now,
                        UpdatePartyId = 99
                    }
                };

                connection.InsertList("InsertAddresses", addressesToInsert);
                addressesToInsert.ForEach(a => Assert.True(a.AddressId > 0));

                // delete all the addresses we insertted
                addressesToInsert.ForEach(a => connection.Execute("DeleteAddress", new { AddressId = a.AddressId }));

                // check that we in fact deleted the address
                addressesToInsert.ForEach(a =>
                {
                    var inserttedAddress = _database.Connection().Query<Address>("FindAddresses", new { AddressId = a.AddressId });
                    Assert.True(inserttedAddress.Count == 0);
                });
            }
        }

        [Fact]
        public void AddressType_SelectList_Insert_QueryOntoList_Delete()
        {
            using (var connection = _database.Open())
            {
                // get the count before the insert
                var masterAddressTypes = connection.Query<AddressType>("FindAddressTypes");
                Assert.True(masterAddressTypes.Count == 3);

                // declare a new AddressType
                var newAddressType = new AddressType()
                {
                    AddressTypeId = 99,
                    AddressTypeCode = null,
                    Name = "Test",
                    Description = "A temporary test entry.",
                    FromDate = DateTime.Now,
                    ThruDate = null,
                    CreateDate = DateTime.Now,
                    CreatePartyId = 99,
                    UpdateDate = DateTime.Now,
                    UpdatePartyId = 99
                };

                // insert new AddressType
                connection.Insert("InsertAddressType", newAddressType);

                // Create a list if AddressTypes with the IDs required set
                var newMasterAddressTypes = new List<AddressType>() {
                    new AddressType() { AddressTypeId = 1 },
                    new AddressType() { AddressTypeId = 2 },
                    new AddressType() { AddressTypeId = 3 },
                    new AddressType() { AddressTypeId = 99 }
                };

                // populate all AddressType columns in the list with QueryOntoList
                connection.QueryOntoList("FindAddressTypes", newMasterAddressTypes);
                Assert.True(newMasterAddressTypes.Count == 4);

                // delete the one we just inserted
                connection.Execute("DeleteAddressType", new { AddressTypeId = newAddressType.AddressTypeId });

                // verify its deleted
                var addressType99 = connection.Query<AddressType>("FindAddressTypes", new { AddressTypeId = newAddressType.AddressTypeId });
                Assert.True(addressType99.Count == 0);
            }
        }

        [Fact]
        public void Addresses_InsertList_TestOneToOneQueries_Delete_WithRepository()
        {
            using (var connection = _database.Open())
            {
                var addressRepository = new AddressRepository(connection);

                var addressesToInsert = new List<Address>()
                {
                    new Address()
                    {
                        AddressTypeId = 1,
                        Description = "A temporary test entry #1.",
                        FacilityId = null,
                        PartyId = null,
                        PrimaryFlag = _flagYes,
                        UnlistedFlag = _flagNo,
                        VerifiedFlag = _flagNo,
                        FromDate = DateTime.Now,
                        ThruDate = null,
                        CreateDate = DateTime.Now,
                        CreatePartyId = 99,
                        UpdateDate = DateTime.Now,
                        UpdatePartyId = 99
                    },
                    new Address()
                    {
                        AddressTypeId = 2,
                        Description = "A temporary test entry #2.",
                        FacilityId = null,
                        PartyId = null,
                        PrimaryFlag = _flagYes,
                        UnlistedFlag = _flagNo,
                        VerifiedFlag = _flagNo,
                        FromDate = DateTime.Now,
                        ThruDate = null,
                        CreateDate = DateTime.Now,
                        CreatePartyId = 99,
                        UpdateDate = DateTime.Now,
                        UpdatePartyId = 99
                    },
                    new Address()
                    {
                        AddressTypeId = 3,
                        Description = "A temporary test entry #3.",
                        FacilityId = null,
                        PartyId = null,
                        PrimaryFlag = _flagYes,
                        UnlistedFlag = _flagNo,
                        VerifiedFlag = _flagNo,
                        FromDate = DateTime.Now,
                        ThruDate = null,
                        CreateDate = DateTime.Now,
                        CreatePartyId = 99,
                        UpdateDate = DateTime.Now,
                        UpdatePartyId = 99
                    }
                };

                addressRepository.InsertAddresses(addressesToInsert);
                addressesToInsert.ForEach(a => Assert.True(a.AddressId > 0));

                // Address and its Address Type as two different result sets
                var result = connection.QueryResults<Address, AddressType>("GetAddressAndAddressType", new { AddressId = addressesToInsert.First().AddressId });
                var address = result.Set1.First();
                var addressType = result.Set2.First();

                var result2 = connection.Query("GetAddressAndAddressType", new { AddressId = addressesToInsert.First().AddressId },
                    Query.Returns(Some<Address>.Records).Then(Some<AddressType>.Records));
                var address2 = result2.Set1.First();
                var addressType2 = result2.Set2.First();

                // Address and its Address Type in one result set
                var addresses = connection.Query<Address, AddressType>("GetAddressAndItsAddressType", new { AddressId = addressesToInsert.First().AddressId });
                var address3 = addresses.First();
                var addressType3 = address3.AddressType;

                var addresses2 = connection.Query("GetAddressAndItsAddressType", new { AddressId = addressesToInsert.First().AddressId },
                    Query.Returns(OneToOne<Address, AddressType>.Records));

                // delete all the addresses we insertted
                addressesToInsert.ForEach(a => addressRepository.DeleteAddress(a.AddressId));

                // check that we in fact deleted the address
                addressesToInsert.ForEach(a =>
                {
                    var inserttedAddress = addressRepository.FindAddresses(new { AddressId = a.AddressId });
                    Assert.True(inserttedAddress.Count == 0);
                });
            }
        }

        [Fact]
        public void Addresses_InsertList_TestOneToManyQueries_Delete_WithRepository()
        {
            using (var connection = _database.Open())
            {
                var addressRepository = new AddressRepository(connection);

                var addressesToInsert = new List<Address>()
                {
                    new Address()
                    {
                        AddressTypeId = 1,
                        Description = "A temporary test entry #1.",
                        FacilityId = 2,
                        PartyId = null,
                        PrimaryFlag = _flagYes,
                        UnlistedFlag = _flagNo,
                        VerifiedFlag = _flagNo,
                        FromDate = DateTime.Now,
                        ThruDate = null,
                        CreateDate = DateTime.Now,
                        CreatePartyId = 99,
                        UpdateDate = DateTime.Now,
                        UpdatePartyId = 99
                    },
                    new Address()
                    {
                        AddressTypeId = 2,
                        Description = "A temporary test entry #2.",
                        FacilityId = 2,
                        PartyId = null,
                        PrimaryFlag = _flagYes,
                        UnlistedFlag = _flagNo,
                        VerifiedFlag = _flagNo,
                        FromDate = DateTime.Now,
                        ThruDate = null,
                        CreateDate = DateTime.Now,
                        CreatePartyId = 99,
                        UpdateDate = DateTime.Now,
                        UpdatePartyId = 99
                    },
                    new Address()
                    {
                        AddressTypeId = 3,
                        Description = "A temporary test entry #3.",
                        FacilityId = 2,
                        PartyId = null,
                        PrimaryFlag = _flagYes,
                        UnlistedFlag = _flagNo,
                        VerifiedFlag = _flagNo,
                        FromDate = DateTime.Now,
                        ThruDate = null,
                        CreateDate = DateTime.Now,
                        CreatePartyId = 99,
                        UpdateDate = DateTime.Now,
                        UpdatePartyId = 99
                    }
                };

                addressRepository.InsertAddresses(addressesToInsert);
                addressesToInsert.ForEach(a => Assert.True(a.AddressId > 0));

                // Read in the records involved in the OneToMany relationship, use the attributes in the
                // model classes to define the relationship (ie: [RecordID], [ParentID], [ChildRecords])
                var results = connection.Query("GetFacilityAndItsAddresses", new { FacilityId = 2 },
                    Query.Returns(Some<Facility>.Records).ThenChildren(Some<Address>.Records));

                // delete all the addresses we insertted
                addressesToInsert.ForEach(a => addressRepository.DeleteAddress(a.AddressId));

                // check that we in fact deleted the address
                addressesToInsert.ForEach(a =>
                {
                    var inserttedAddress = addressRepository.FindAddresses(new { AddressId = a.AddressId });
                    Assert.True(inserttedAddress.Count == 0);
                });
            }
        }
    }
}
