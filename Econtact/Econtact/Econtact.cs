using Econtact.econtactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtact
{
    public partial class Econtact : Form
    {
        Contact c = new Contact();
        public Econtact()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            //get the values from the input fields
            c.FirstName = textBoxFirstName.Text;
            c.LastName = textBoxLastName.Text;
            c.ContacNo = textBoxContactNamber.Text;
            c.Address = textBoxAdress.Text;
            c.Gender = comboBoxGender.Text;

            //insert data into database
            bool success = c.Insert(c);

            if (success)
            {
                //successflly inserted
                MessageBox.Show("New contact inserted.");
                Clear();
            }
            else{
                MessageBox.Show("Failed to add a new conact. Try again.");
            }

            //load data into data grid view
            DataTable dt = c.Select();
            dataGridViewNewContactList.DataSource = dt;

        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            //get the values from the input fields
            c.FirstName = textBoxFirstName.Text;
            c.LastName = textBoxLastName.Text;
            c.ContacNo = textBoxContactNamber.Text;
            c.Address = textBoxAdress.Text;
            c.Gender = comboBoxGender.Text;

            //insert data into database
            bool success = c.Update(c);

            if (success)
            {
                //successflly inserted
                MessageBox.Show("New contact updated.");
            }
            else
            {
                MessageBox.Show("Failed to update the conact " + c.ContacNo + " . Try again.");
            }
        }

        private void Econtact_Load(object sender, EventArgs e)
        {
            //load data into data grid view
            DataTable dt = c.Select();
            dataGridViewNewContactList.DataSource = dt;
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxContactNamber.Text = "";
            textBoxAdress.Text = "";
            comboBoxGender.Text = "";
        }
    }
}
