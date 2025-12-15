using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduBond
{
    internal class Message
    {
        public string RegNo { get; set; }
        public string MessageText { get; set; }
        public DateTime MessageTime { get; set; }

        public override string ToString()
        {
            return $"{MessageTime:HH:mm} - {RegNo} : {MessageText}";
        }
    }
}
