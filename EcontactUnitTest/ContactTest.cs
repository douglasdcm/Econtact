using Econtact.econtactClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Configuration;
using System.Data;

namespace UnitTests
{
    [TestClass]
    public class Tests
    {
        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        [TestMethod]
        public void InsertTest_Using_Mock_Should_Pass()
        {
            Contact cc = new Contact()
            {
                FirstName = "fn",
                LastName = "ln",
                Address = "ad",
                ConatctID = 123,
                ContacNo = "234567",
                Gender = "male"
            };

            Mock<IContact> mock = new Mock<IContact>();
            mock.Setup(x => x.Insert(cc))
                .Returns(true);
        }

        [TestMethod]
        public void InsertTest_Using_LocalDB_Should_Pass()
        {
            //Arrange
            Contact cc = new Contact()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Address = "Address",
                ConatctID = 123,
                ContacNo = "234567",
                Gender = "male"
            };

            //Act
            cc.Insert(cc);
            var actual = cc.Select();

            //Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void Select_Should_Pass()
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
                ConatctID = 123,
                ContacNo = "234567",
                Gender = "male"
            };
        }
    }
}