using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRecorderApps
{
    class AppLogger
    {
        private log4net.ILog log;
        static private AppLogger mLogerInstance = null;
        private AppLogger()
        {
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        static public AppLogger GetInstance()
        {
            if (mLogerInstance == null)
            {
                mLogerInstance = new AppLogger();
            }

            return mLogerInstance;
        }

        public log4net.ILog Logging
        {
            get { return log; }
        }
    }
}
