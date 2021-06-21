using System.Runtime;

namespace System
{
    public static partial class AppContext
    {
        [RuntimeExport("OnFirstChanceException")]
        internal static void OnFirstChanceException(object e)
        {
            
        }

        [RuntimeExport("OnUnhandledException")]
        internal static void OnUnhandledException(object e)
        {
            
        }
        
        public static void SetData(string key, string data){}
    }
}