using BasicFuncWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace BasicFuncWebApp.Helper
{
    public class DBHelper
    {
        public static String getConnectionString(String FileName = "BasicFuncWebAppDB.db")
        {
            string baseFolder = AppDomain.CurrentDomain.BaseDirectory;

            string sqlLiteConnectionString = string.Format(
              "data source=\"{0}\"",
              System.IO.Path.Combine(baseFolder, FileName));
            return sqlLiteConnectionString;
        }

        public static List<StudentModel> getStudentModels()
        {
            List<StudentModel> studentModels = new List<StudentModel>();
            string query = "SELECT * FROM [student];";
            using (SQLiteConnection connection = new SQLiteConnection(getConnectionString()))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                connection.Open();
                SQLiteDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    StudentModel stdModel = new StudentModel()
                    {
                        sid = Convert.ToInt32(sdr["sid"]),
                        email = sdr["email"].ToString(),
                        firstName = sdr["firstName"].ToString(),
                        lastName = sdr["lastName"].ToString()
                    };
                    studentModels.Add(stdModel);
                }
                sdr.Close();
                connection.Close();
            }
            return studentModels;
        }

        public static StudentModel addStudentToDB(StudentModel studentModel)
        {
            StudentModel stdModel = new StudentModel();
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(getConnectionString()))
                {

                    using (SQLiteCommand cmd = new SQLiteCommand(connection))
                    {
                        String query = $"INSERT INTO student ( firstName, lastName, email ) VALUES ( '{studentModel.firstName}', '{studentModel.lastName}', '{studentModel.email}' );  SELECT last_insert_rowid(); ";
                        cmd.Connection = connection;
                        cmd.CommandText = query;
                        cmd.CommandType = System.Data.CommandType.Text;
                        connection.Open();

                        object obj = cmd.ExecuteScalar();
                        long id = (long)obj; // Note regardless of data type, SQLite always returns autoincrement fields as long.
                        studentModel.sid = Convert.ToInt32(id);
                        return studentModel;
                    }

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return stdModel;
        }

        public static int updateStudentModel(StudentModel student)
        {
            int noOfRowEffected;
            List<StudentModel> studentModels = new List<StudentModel>();
            string query = $"UPDATE student SET firstName = '{student.firstName}', lastName = '{student.lastName}', email = '{student.email}' WHERE sid = {student.sid}; ";
            using (SQLiteConnection connection = new SQLiteConnection(getConnectionString()))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                connection.Open();
                noOfRowEffected = cmd.ExecuteNonQuery();
                connection.Close();
            }
            return noOfRowEffected;
        }

        public static StudentModel getStudentModelByID(int sid)
        {
            StudentModel studentModel = new StudentModel();
            string query = $"SELECT * FROM [student] WHERE sid = {sid};";
            using (SQLiteConnection connection = new SQLiteConnection(getConnectionString()))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                connection.Open();
                SQLiteDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    StudentModel stdModel = new StudentModel()
                    {
                        sid = Convert.ToInt32(sdr["sid"]),
                        email = sdr["email"].ToString(),
                        firstName = sdr["firstName"].ToString(),
                        lastName = sdr["lastName"].ToString()
                    };
                    studentModel = stdModel;
                }
                sdr.Close();
                connection.Close();
            }
            return studentModel;
        }
    }
}
