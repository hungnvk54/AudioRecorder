using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Recorder
{
    class AudioStreamTextArgs : EventArgs
    {
        private TextResultMessage mMessage;

        public AudioStreamTextArgs(TextResultMessage message)
        {
            mMessage = message;
        }

        public TextResultMessage Message
        {
            get
            {
                return mMessage;
            }
        }
    }
}
