using System;
using Econtact;
using Econtact.econtactClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestFFEcontact
{
    [TestClass]
    public class ContactTest
    {
        [TestMethod]
        public void TestInsert()
        {
            Contact cc = new Contact() {
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
        public void TestMethodFake()
        {
            Fake f = new Fake();
            f.Method1();
            Assert.IsTrue(true);
        }
    }
}
