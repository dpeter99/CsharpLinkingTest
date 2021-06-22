using System;
using System.Internal.Runtime.CompilerHelpers;

namespace Internal.Runtime.CompilerHelpers
{
    public static class ThrowHelpers
    {
        public static void ThrowInvalidProgramException(ExceptionStringID id)
        {
            throw new Exception();
        }
        
        public static void ThrowInvalidProgramExceptionWithArgument(ExceptionStringID id, string methodName)
        {
            throw new Exception();
        }
    }
}