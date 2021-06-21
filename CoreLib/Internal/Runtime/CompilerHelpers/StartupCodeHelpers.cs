using System.Runtime;

namespace System.Internal.Runtime.CompilerHelpers
{
    public class StartupCodeHelpers
    {
        
        [RuntimeExport("RhpReversePInvoke2")]
        static void RhpReversePInvoke2() { }
        
        [RuntimeExport("RhpReversePInvokeReturn2")]
        static void RhpReversePInvokeReturn2() { }
        
        [RuntimeExport("__fail_fast")]
        static void FailFast() { while (true) ; }
        
        [RuntimeExport("RhpPInvoke")]
        static void RphPinvoke() { }
        
        [RuntimeExport("RhpPInvokeReturn")]
        static void RphPinvokeReturn() { }
        
        
        [RuntimeExport("RhpNewFast")]
        internal static extern unsafe object RhpNewFast(EEType* pEEType); 
        
        [RuntimeExport("RhpThrowEx")]
        static void RhpThrowEx() { }
    }
}