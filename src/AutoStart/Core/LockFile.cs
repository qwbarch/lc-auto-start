using System;
using System.Threading;

namespace AutoStart.Core
{
    static class LockFile
    {
        /// <summary>
        /// Wait until the mutex is avaialble and then run the callback.<br />
        /// The mutex is released after the callback ends, or if an exception is found.
        /// </summary>
        public static void WithLock(Action action)
        {
            using var handle = new EventWaitHandle(true, EventResetMode.AutoReset, Plugin.PluginId);
            try
            {
                handle.WaitOne();
                action();
            }
            finally
            {
                handle.Set();
            }
        }
    }
}