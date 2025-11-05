using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduBond
{
    internal class Student
    {
        public string RegNo { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int YearOfStudy { get; set; }
        public List<int> SubjectProficiency { get; set; }

    }
}
