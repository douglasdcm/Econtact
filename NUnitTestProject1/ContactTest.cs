using Econtact.econtactClasses;
using Moq;
using NUnit.Framework;
using System.Data;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InsertTest_Should_Pass()
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

        [Test]
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