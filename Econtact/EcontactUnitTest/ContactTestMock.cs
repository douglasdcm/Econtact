using Econtact.econtactClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EcontactUnitTest
{
    
    //[TestClass]
    public class TestsMock
    {

        [TestMethod]
        public void InsertTest_Using_Mock_Should_Pass()
        {
            Contact cc = new Contact()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Address = "Address",
                ContactID = 123,
                ContacNo = "234567",
                Gender = "male"
            };

            Mock<IContact> mock = new Mock<IContact>();
            mock.Setup(x => x.Insert(cc))
                .Returns(true);
        }

        
        [TestMethod]
        public void Select_Using_Mock_Should_Pass()
        {

            //Arrange
            Contact cc = GetListOfContacts();

            Mock<IContact> mock = new Mock<IContact>();
            mock.Setup(x => x.Select())
                .Returns(new DataTable());

            //Act
            var actual = mock.Object;

            //Assert
            Assert.IsNotNull(actual);
        }

        private static Contact GetListOfContacts()
        {
            return new Contact()
            {
                FirstName = "fn",
                LastName = "ln",
                Address = "ad",
                ContactID = 123,
                ContacNo = "234567",
                Gender = "male"
            };
        }
    }
}