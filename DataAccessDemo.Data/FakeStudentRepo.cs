using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessDemo.Data.Dto;

namespace DataAccessDemo.Data
{
    public class FakeStudentRepo
    {
        //readonly keyword makes it so only the ctor can change it. 
        //in other words only when it is newed up
        private readonly List<Student> _students;

        public FakeStudentRepo()
        {
            _students = new List<Student>
            {
                new Student { Age = 14, Name = "Sarah", Sex = Enums.Sex.Female },
                new Student { Age = 18, Name = "Mike", Sex = Enums.Sex.Male },
                new Student { Age = 12, Name = "Jane", Sex = Enums.Sex.Female },
                new Student { Age = 17, Name = "Sally", Sex = Enums.Sex.Female },
                new Student { Age = 11, Name = "Scott", Sex = Enums.Sex.Male }
            };
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _students;
        }


        private void DoIt(string name)
        {
            Console.WriteLine(name);
        }


        //overload that takes a an argument of Enums.Sex
        public IEnumerable<Student> GetAllStudents(Enums.Sex theStudentsSex)
        {

            return _students.Where(x => x.Sex == theStudentsSex);
            
            // the code below is the same result as the code above.
            // switch (theStudentsSex)
            // {
            //     case Enums.Sex.Male:
            //         IEnumerable<Student> boys = _students.Where(student => student.Sex == Enums.Sex.Male);
            //         return boys;
            //     case Enums.Sex.Female:
            //         return _students.Where(x => x.Sex == Enums.Sex.Female);
            //         break;
            // }
        }
    }
}
