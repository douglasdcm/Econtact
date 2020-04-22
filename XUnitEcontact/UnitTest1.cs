using System;
using Xunit;
using Econtact.econtactClasses;
using Moq;

namespace XUnitEcontact
{
    public class UnitTest1
    {

        [Fact]
        public void TestInsert()
        {
            Contact cc = new Contact();
            cc.FirstName = "fn";
            cc.LastName = "ln";
            cc.Address = "ad";
            cc.ConatctID = 123;
            cc.ContacNo = "234567";
            cc.Gender = "male";

            bool result = cc.Insert(cc);
            Assert.True(result);

        }

        [Fact]
        public void TestUsingMock()
        {

            Contact cc = new Contact
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

        [Fact]
        public void NullContact()
        {

            Contact cc = null;

            Mock<IContact> mock = new Mock<IContact>();
            mock.Setup(x => x.Insert(cc))
                .Returns(true);

        }
    }
}
