using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGeneration_Kataev.Models
{
    public class Student
    {
        public int Id { get; set; }
        public  string FirstName { get; set; }
        public string LastName { get; set; }
        public int IdGroup { get; set; }
        public bool Expelled { get; set; }
        public DateTime DateExpelled { get; set; }
        public Student(int Id, string FirstName, string LastName, int IdGroup, bool Expelled, DateTime DateExpelled)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.IdGroup = IdGroup;
            this.Expelled = Expelled;
            this.DateExpelled = DateExpelled;
        }
    }
}
