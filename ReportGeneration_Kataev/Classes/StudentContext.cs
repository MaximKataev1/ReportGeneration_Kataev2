using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ReportGeneration_Kataev.Classes.Common;
using ReportGeneration_Kataev.Models;

namespace ReportGeneration_Kataev.Classes
{
    public class StudentContext : Student
    {
        public StudentContext(int Id, string FirstName, string LastName, int IdGroup, bool Expelled, DateTime DateExpelled) : base(Id, FirstName, LastName, IdGroup, Expelled, DateExpelled) { }
        public static List<StudentContext> AllStudents()
        {
            List<StudentContext> allStudents = new List<StudentContext>();
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader BDStudents = Connection.Query("SELECT * FROM `student` ORDER BY `LastName`", connection);
            while (BDStudents.Read())
            {
                allStudents.Add(new StudentContext(
                    BDStudents.GetInt32(0),
                    BDStudents.GetString(1),
                    BDStudents.GetString(2),
                    BDStudents.GetInt32(3),
                    BDStudents.GetBoolean(4),
                    BDStudents.IsDBNull(5) ? DateTime.Now : BDStudents.GetDateTime(5)
                    ));
            }
            Connection.CloseConnection(connection);
            return allStudents;
        }
    }
}
