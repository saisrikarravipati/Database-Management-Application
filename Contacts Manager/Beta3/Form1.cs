/******************************************************************************
* Conatact Managing Program.
* 
* This program keeps a track of all the contacts you encounter in the life
* 
* It has 3 major functionalities
* 
* 1) Save a Contact
* 2) Update a Contact
* 3) Delete a contact
* 
* The following fields will be present in the User Interface of the program
* 
* 1) First Name
* 2) Middle Name
* 3) Last Name
* 4) Sex
* 5) Phone Number
* 6) email Address
* 7) Date of birth
* 8) SSN of the contact
* 9) Address - Line1
* 10) Address - Line2
* 11) Address - City
* 12) Address - State
* 13) Address - Country
* 14) Address - Zipcode
* 
* The program provides a datagrid view to see the existing records and upon selecting a record
* the data will be populated in the text fields of the Porgram, This enables to easily update or delete
* a selected record
* 
* DB procedures are called to execute save, update and delete operations in SQL Database
* 
* Various constraints on several fields are mentioned below as comments
* 
* Written by SAI SRIKAR RAVIPATI (SXR180014) at The University of Texas at Dallas
* starting October 22, 2018.
******************************************************************************/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Beta3
{


// Inititating the form

    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=JARVIS\\SQLEXPRESS;Initial Catalog=people;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        
        
        //Initializes the component and dislays the data

        public Form1()
        {
            InitializeComponent();
            displayData();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Save Button funtionality with message on succesful saving of data
        // we are calling a SQL DB Procedure to execute the save opertaion

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd= new SqlCommand("EXECUTE [people].[dbo].[insertcontact] '"+fname.Text+"','"+mname.Text+"','"+lname.Text+"','"+sbox.Text+"','"+SSNbox.Text+"','"+dobbox.Text+"','"+phonebox.Text+"','"+emailbox.Text+"','"+line1box.Text+"','"+line2box.Text+"','"+citybox.Text+"','"+statebox.Text+"','"+countrybox.Text+"','"+zipbox.Text+"'",con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Contact Information succesfully SAVED");
            con.Close();
            displayData();
            Clear();
        }

        // display data loads up the data from the sql and fills it ino the datagrid

        public void displayData()
        {
            con.Open();
            adpt = new SqlDataAdapter("EXECUTE [people].[dbo].[displaydetails]", con);
            dt = new DataTable();
            adpt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        // This will clear up the textfields and this enables us to insert new data easily

        public void Clear()
        {

            fname.Text = "";
            mname.Text = "";
            lname.Text = "";
            sbox.Text = "";
            SSNbox.Text = "";
            dobbox.Text = "";
            phonebox.Text = "";
            emailbox.Text = "";
            line1box.Text = "";
            line2box.Text = "";
            citybox.Text = "";
            statebox.Text = "";
            countrybox.Text = "";
            zipbox.Text = "";

        }

        // This will laod the contents from the datagrid upon click and loads it up into the text fields of the GUI

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                fname.Text = row.Cells[0].Value.ToString();
                mname.Text = row.Cells[1].Value.ToString();
                lname.Text = row.Cells[2].Value.ToString();
                sbox.Text = row.Cells[3].Value.ToString();
                SSNbox.Text = row.Cells[4].Value.ToString();
                dobbox.Text = row.Cells[5].Value.ToString();
                phonebox.Text = row.Cells[6].Value.ToString();
                emailbox.Text = row.Cells[7].Value.ToString();
                line1box.Text = row.Cells[8].Value.ToString();
                line2box.Text = row.Cells[9].Value.ToString();
                citybox.Text = row.Cells[10].Value.ToString();
                statebox.Text = row.Cells[11].Value.ToString();
                countrybox.Text = row.Cells[12].Value.ToString();
                zipbox.Text = row.Cells[13].Value.ToString();

            }



        }

        // Update button loads the updated information from the text fields and updates the database
        // we are calling a SQL DB Procedure to execute the update opertaion

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("EXECUTE [people].[dbo].[updatecontact] '" + fname.Text + "','" + mname.Text + "','" + lname.Text + "','" + sbox.Text + "','" + SSNbox.Text + "','" + dobbox.Text + "','" + phonebox.Text + "','" + emailbox.Text + "','" + line1box.Text + "','" + line2box.Text + "','" + citybox.Text + "','" + statebox.Text + "','" + countrybox.Text + "','" + zipbox.Text + "'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Contact Information succesfully UPDATED");
            con.Close();
            displayData();
            Clear();
        }

        // Delete button delete the selected record from the database
        // we are calling a SQL DB Procedure to execute the delete opertaion


        private void button4_Click(object sender, EventArgs e)
        {

            con.Open();
            cmd = new SqlCommand("EXECUTE [people].[dbo].[deletecontact] '"+ SSNbox.Text +"'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Contact Information succesfully DELETED");
            con.Close();
            displayData();
            Clear();

        }


        // Constraint that first name textbox should accept only letters and whitespace

        private void fname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
                  
        }

        // Constraint that middle name textbox should accept only letters and whitespace

        private void mname_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        // Constraint that last name textbox should accept only letters and whitespace

        private void lname_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Constraint that SSN textbox should accept only numbers

        private void SSNbox_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Constraint that Phone Number textbox should accept only numbers

        private void phonebox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            
        }

        // Constraint that Address line1 textbox should accept only letters, numbers or whitespace

        private void line1box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Constraint that Address line2 textbox should accept only letters, numbers or whitespace

        private void line2box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        // Constraint that City textbox should accept only letters or whitepsaces

        private void citybox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Constraint that State textbox should accept only letters or whitepsaces

        private void statebox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Constraint that Country textbox should accept only letters or whitepsaces

        private void countrybox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        // Constraint that Zipcode textbox should accept only letters 

        private void zipbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Constraint that Phone Number textbox should contain 10 digits

        private void phonebox_Leave(object sender, EventArgs e)
        {
            if ((phonebox.Text.Length < 10) && (phonebox.Text.Length > 0) )
            {
                MessageBox.Show("Phone number must be 10 digits!");
                phonebox.Focus();
            }
            else if((phonebox.Text.Length > 10))
            {
                MessageBox.Show("Phone number must be 10 digits!");
                phonebox.Focus();
            }
        }


        // Constraint that SSN textbox should contain 9 digits

        private void SSNbox_Leave(object sender, EventArgs e)
        {
            if ((SSNbox.Text.Length < 9) && (SSNbox.Text.Length > 0))
            {
                MessageBox.Show("SSN must be 9 digits!");
                SSNbox.Focus();
            }
            else if((SSNbox.Text.Length > 9))
            {
                MessageBox.Show("SSN must be 9 digits!");
                SSNbox.Focus();
            }
        }


        // Constraint to validate the email address to contain @ and .

        private void emailbox_Leave(object sender, EventArgs e)
        {
            Regex mRegxExpression;
            if (emailbox.Text.Trim() != string.Empty)
            {
                mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

                if (!mRegxExpression.IsMatch(emailbox.Text.Trim()))
                {
                    MessageBox.Show("E-mail address format is not correct.");
                    emailbox.Focus();
                }
            }
        }

        // Constraint to validate the date format


        private void dobbox_Leave(object sender, EventArgs e)
        {
            Regex mRegxExpression;
            if (dobbox.Text.Trim() != string.Empty)
            {
                mRegxExpression = new Regex(@"^(((0)[1-9])|((1)[0-2]))(\/)([0-2][0-9]|(3)[0-1])(\/)\d{4}$");

                if (!mRegxExpression.IsMatch(dobbox.Text.Trim()))
                {
                    MessageBox.Show("Date format is not correct.");
                    dobbox.Focus();
                }
            }
        }
    }
    
}
