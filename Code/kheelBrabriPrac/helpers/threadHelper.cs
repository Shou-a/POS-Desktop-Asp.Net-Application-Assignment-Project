using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace kheelBrabriPrac.helpers
{
    class threadHelper
    {
        public Thread thread;

        public void run()
        {
            thread.Priority = ThreadPriority.AboveNormal;
            thread.Start();
        }
        public void Abort()
        {
            thread.Abort();
        }
    }
}
