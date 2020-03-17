using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Econtact.econtactClasses
{
    class contactClass
    {
        //getter setter properties
        //acts as Data Carrie in our application
        public int ConatctID { get; set; }
        public string FirstName{ get;  set; }
        public string LastName{ get; set; }
        public string ContacNo{ get; set; }
        public string Address{ get; set; }
        public string Gender{ get; set; }

        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        //selecting data from database
        public DataTable Select()
        {
            //step 1: database connection
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                //step 2: writing sql query
                string sql = "SELECT * FROM tbl_contact";
                //creating and using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //creating SQL DataAdpter usind cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //inserting data to databse
        public bool Insert(contactClass c)
        {
            //creating a default return type and setting its value sto false
            bool isSuccess = false;

            //step 1: connect database
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                //step 2: create a sql query to insert data
                string sql = "INSERT INTO tbl_contact (FirstName, LastName, ContactNo, Address, Gender) VALUES (@FirstName, @LastName, @ContactNo, @Address, @Gender)";
                //creating sql command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //create parameters to add data
                cmd.Parameters.AddWithValue("@FirstName",c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContacNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                //connection open here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //if the query runs successfully the value of rows will be grater thar zero else it will be zero
                if(rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        public bool Update(contactClass c)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "UPDATE tbl_contact SET FirstName = @FirstName, LastName = @LastName, ContactNo = @ContactNo, Address = @Address, Gender = @Gender WHERE ContactID=@ContactID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                //create parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContacNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                //if the query runs successfully the value of rows will be grater thar zero else it will be zero
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        //method to delete data from database
        public bool Delete(contactClass c)
        {
            //create a default valeu
            bool isSucess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ConatctID);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if(rows > 0)
                {
                    isSucess = true;
                }
                else
                {
                    isSucess = false;
                }
            }catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSucess;
        }

    }
}
