using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recorder.DataMessage
{
    class LoginSuccessfullyMessage
    {
        public string Authorization
        {
            get;set;
        }

        public string userName
        {
            get;set;
        }

        public string roleType
        {
            get; set;
        }

        public string firstName
        {
            get; set;
        }

        public string lastName
        {
            get; set;
        }

        public string id
        {
            get; set;
        }

        public string idTypeSession
        {
            get; set;
        }
    }
}
