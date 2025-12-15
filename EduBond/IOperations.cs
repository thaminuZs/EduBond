using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduBond
{
    internal interface IOperations
    {
        bool AddStudent(Student student);
        bool IsRegistered(string regNo);
        bool IsValidPassword(string regNo, string passw);
        List<int> FetchLevels(string regNo);
        bool SendMessage(string regNo, string message);
        List<Message> FetchMessages();
        bool ScheduleKuppi(Kuppi kuppi);
    }
}
