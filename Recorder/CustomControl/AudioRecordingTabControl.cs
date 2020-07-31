using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace CustomControl
{
    public class AudioRecordingTabControl : TabControl
    {
        public bool Selectable { get; set; } = true;
        protected override void OnSelecting(TabControlCancelEventArgs e)
        {
            if (this.Selectable) 
            {
                e.Cancel = false;
                base.OnSelecting(e);
            }
            else
            {
                e.Cancel = true;
                base.OnSelecting(e);
            }
        }

    }

}
