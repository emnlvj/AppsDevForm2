using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EnrollmentSystem
{
    public partial class Form2 : Form
    {

        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\Server2\second semester 2023-2024\LAB802\79286_CC_APPSDEV22_1030_1230_PM_MW\79286-23217714\Desktop\Final\Jacaba.accdb";
        public Form2()
        {
            InitializeComponent();
        }

       

        private void SaveButton_Click(object sender, EventArgs e)
        {
            OleDbConnection thisConnection = new OleDbConnection(connectionString);
            string Ole = "Select * From SUBJECTSCHEDULEFILE";
            OleDbDataAdapter thisAdapter = new OleDbDataAdapter(Ole, thisConnection);
            OleDbCommandBuilder thisBuilder = new OleDbCommandBuilder(thisAdapter);
            DataSet thisDataSet = new DataSet();
            thisAdapter.Fill(thisDataSet, "SubjectScheduleFile");

            DataRow thisRow = thisDataSet.Tables["SubjectScheduleFile"].NewRow();
            thisRow["SSFSUBJCODE"] = SubjectCodeTextBox.Text;
            thisRow["SSFEDPCODE"] = Convert.ToInt32(EDPCodeTextBox.Text);
            thisRow["SSFSTARTTIME"] = StartTimeTextBox.Text;
            thisRow["SSFENDTIME"] = EndTimeTextBox.Text;
            thisRow["SSFSTATUS"] = "AC";
            thisRow["SSFDAYS"] = DaysTextBox.Text;
            thisRow["SSFROOM"] = RoomTextBox.Text;
            thisRow["SSFXM"] = AMPMTextBox.Text;
            thisRow["SSFSECTION"] = SectionTextBox.Text;
            thisRow["SSFSCHOOLYEAR"] = SchoolYearTextBox.Text;


            thisDataSet.Tables["SubjectScheduleFile"].Rows.Add(thisRow);
            thisAdapter.Update(thisDataSet, "SubjectScheduleFile");

            MessageBox.Show("Recorded");
        }

        private void SubjectCodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                OleDbConnection thisConnection = new OleDbConnection(connectionString);
                thisConnection.Open();
                OleDbCommand thisCommand = thisConnection.CreateCommand();

                string sql = "SELECT * FROM SUBJECTFILE";
                thisCommand.CommandText = sql;

                OleDbDataReader thisDataReader = thisCommand.ExecuteReader();
                //
                bool found = false;
                string subjectCode = "";
                string description = "";
                string units = "";

                while (thisDataReader.Read())
                {

                    if (thisDataReader["SFSUBJCODE"].ToString().Trim().ToUpper() == SubjectCodeTextBox.Text.ToUpper())
                    {
                        found = true;
                        
                        description = thisDataReader["SFSUBJDESC"].ToString();
                        DescriptionTextBox.Text = description;
                        
                        break;
                    }
                }
                if (found == false)
                    MessageBox.Show("Subject Code not found.");
            }
        }
    }
}
