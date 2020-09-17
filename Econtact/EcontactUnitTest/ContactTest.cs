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
    enum ContactTable
    {
        ContactID = 1,
        FirstName = 2,
        LastName = 3,
        ContactNo = 4,
        Address = 5,
        Gender = 6
    }

    [TestClass]
    public class Tests
    {
        private string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        private Contact contact = new Contact()
        {
            FirstName = "FirstName",
            LastName = "LastName",
            Address = "Address",
            ContactID = 1,
            ContacNo = "234567",
            Gender = "male"
        };

        private Contact contact2 = new Contact()
        {
            FirstName = "FirstName2",
            LastName = "LastName2",
            Address = "Address2",
            ContactID = 2,
            ContacNo = "2345672",
            Gender = "female"
        };

        private Contact contact3 = new Contact()
        {
            FirstName = "FirstName3",
            LastName = "LastName3",
            Address = "Address3",
            ContactID = 3,
            ContacNo = "2345673",
            Gender = "female"
        };        

        private DataTable DropTableIfExists()
        {
            //step 1: database connection
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                //step 2: writing sql query
                string sql = "DROP TABLE IF EXISTS tbl_contact";
                //creating and using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //creating SQL DataAdpter usind cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        private DataTable CreateTableIfDoesNotExist()
        {
            //step 1: database connection
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                //step 2: writing sql query
                StringBuilder query = new StringBuilder()
                                    .Append("CREATE TABLE [dbo].[tbl_contact] (")
                                    .Append("[ContactID] INT           IDENTITY (1, 1) NOT NULL,")
                                    .Append("[FirstName] VARCHAR (50)  NOT NULL,")
                                    .Append("[LastName]  VARCHAR (50)  NOT NULL,")
                                    .Append("[ContactNo] VARCHAR (20)  NULL,")
                                    .Append("[Address]   VARCHAR (255) NULL,")
                                    .Append("[Gender]    VARCHAR (10)  NULL,")
                                    .Append("CONSTRAINT [PK_tbl_contact] PRIMARY KEY CLUSTERED ([ContactID] ASC)")
                                    .Append(");");

                string sql = query.ToString();
                //creating and using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //creating SQL DataAdpter usind cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        [TestInitialize]
        public void Setup()
        {
            DropTableIfExists();
            CreateTableIfDoesNotExist();
        }
               
        [TestMethod]
        public void InsertTest_Using_LocalDB_Should_Pass()
        {
            //Arrange

            //Act
            var actual = contact.Insert(contact);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void InsertTest_Using_LocalDB_Adding_Null_Contact_Should_Fail()
        {
            //Arrange

            //Act
            var actual = contact.Insert(null);
        }

        [TestMethod]
        public void UpdateTest_Using_LocalDB_Should_Pass()
        {
            //Arrange
            contact.Insert(contact);

            contact = contact2;

            contact.ContactID = 1;

            //Act
            var actual = contact.Update(contact);
                       
            //Assert
            Assert.IsTrue(actual);
        }


        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void UpdateTest_Using_LocalDB_Contact_Null_Should_Throw_Exception()
        {
            //Arrange

            //Act
            var actual = contact.Update(null);
        }

        [TestMethod]
        public void UpdateTest_Using_LocalDB_Passing_Invalid_Id_Should_Return_False()
        {
            //Arrange
            contact.ContactID = 123456;

            contact.Insert(contact);

            //Act
            var actual = contact.Update(contact);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void SelectTest_Using_LocalDB_Should_Pass()
        {
            //Arrange
            contact.Insert(contact);
            contact.Insert(contact2);
            contact.Insert(contact3);

            var expected = 3;

            //Act
            var contacts = contact.Select();

            var actual = contacts.Rows.Count;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SelectTest_Using_LocalDB_With_Empty_Table_Should_Return_Empty()
        {
            //Arrange
            var expected = 0;

            //Act
            var contacts = contact.Select();

            var actual = contacts.Rows.Count;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Ignore("Not possible to chage the connection string")]
        [TestMethod]
        [ExpectedException(typeof(System.Data.SqlClient.SqlException))]
        public void SelectTest_Using_LocalDB_With_Fake_Connection_Return_Exception()
        {
            //Arrange
            string myconnstring = ConfigurationManager.ConnectionStrings["connstringFake"].ConnectionString;

            //Act
            contact.Select();
        }

        [TestMethod]
        public void DeleteTest_Using_LocalDB_Should_Pass()
        {
            //Arrange
            //Arrange
            contact.Insert(contact);
            contact.Insert(contact2);
            contact.Insert(contact3);

            var expected = 2;

            //Act
            contact.ContactID = 1;

            var actual = contact.Delete(contact);

            var actualQtde = contact.Select().Rows.Count;

            //Assert
            Assert.IsTrue(actual);
            Assert.AreEqual(expected, actualQtde);
        }

        [TestMethod]
        public void DeleteTest_Using_LocalDB_Using_invalid_Id_Should_Return_False()
        {
            //Arrange
            contact.ContactID = 123456;

            contact.Insert(contact);
            contact.Insert(contact2);
            contact.Insert(contact3);


            var expected = 3;

            //Act

            var actual = contact.Delete(contact);

            var actualQtde = contact.Select().Rows.Count;

            //Assert
            Assert.IsFalse(actual);
            Assert.AreEqual(expected, actualQtde);
        }


        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        private void DeleteTest_Using_LocalDB_Using_invalid_Contact_Should_Return_Exception()
        {
            //Arrange
            contact.Insert(contact);
            contact.Insert(contact2);
            contact.Insert(contact3);

            //Act

            contact.Delete(null);
        }

        [TestMethod]
        public void FormTest_ClickButton()
        {
            var econtact = new Econtact.Econtact();
            //var contactDesigner = new Econtact.Designer();
            //econtact.ButtonAdd_Click(new object(), new EventArgs());


        } 

    }
}