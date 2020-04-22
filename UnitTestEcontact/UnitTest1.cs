using Econtact.econtactClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestEcontact
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestInsert()
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
    }
}
