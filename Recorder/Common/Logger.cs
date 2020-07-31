using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRecorderApps
{
    class Logger
    {
        private log4net.ILog log;
        static private Logger mLogerInstance = null;
        private Logger()
        {
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        static public Logger GetInstance()
        {
            if (mLogerInstance == null)
            {
                mLogerInstance = new Logger();
            }

            return mLogerInstance;
        }

        public log4net.ILog Logging
        {
            get { return log; }
        }
    }
}
