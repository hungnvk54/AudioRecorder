using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CredentialManagement;

namespace AudioRecorderApps.Login
{
    public class PasswordRepository
    {
        private const string PasswordName = "ServerPassword";
        private const string UserName = "UserName";

        public void SaveUserName(string username)
        {
            using (var cred = new Credential())
            {
                cred.Password = username;
                cred.Target = UserName;
                cred.Type = CredentialType.Generic;
                cred.PersistanceType = PersistanceType.LocalComputer;
                cred.Save();
            }
        }

        public string GetUsername()
        {
            using (var cred = new Credential())
            {
                cred.Target = UserName;
                cred.Load();
                return cred.Password;
            }
        }
        public void SavePassword(string password)
        {
            using (var cred = new Credential())
            {
                cred.Password = password;
                cred.Target = PasswordName;
                cred.Type = CredentialType.Generic;
                cred.PersistanceType = PersistanceType.LocalComputer;
                cred.Save();
            }
        }

        public string GetPassword()
        {
            using (var cred = new Credential())
            {
                cred.Target = PasswordName;
                cred.Load();
                return cred.Password;
            }
        }
    }
}
