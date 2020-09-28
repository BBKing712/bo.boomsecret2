using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bo.boomsecret.Common
{
    public static class TraceHelper
    {
        public static void TraceException(string key, Exception exception)
        {
            string quote = "\"";
            if (exception != null)
            {
                string message = $"Key=[{key}] Type=[{exception.GetType().ToString()}:" + quote + exception.Message + quote;
                if (exception.InnerException != null)
                {
                    message = message + ",Inner=" + quote + exception.InnerException.Message + quote;
                }
                System.Diagnostics.Trace.TraceError(message);
            }

        }
    }
}