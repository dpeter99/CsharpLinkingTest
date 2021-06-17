
using System;
using System.Runtime;
using System.Runtime.InteropServices;

namespace System
{
    public class Object { public IntPtr m_pEEType; } // The layout of object is a contract with the compiler.
    public struct Void { }
    public struct Boolean { }
    public struct Char { }
    public struct SByte { }
    public struct Byte { }
    public struct Int16 { }
    public struct UInt16 { }
    public struct Int32 { }
    public struct UInt32 { }
    public struct Int64 { }
    public struct UInt64 { }
    public struct IntPtr { }
    public struct UIntPtr { }
    public struct Single { }
    public struct Double { }
    public abstract class ValueType { }
    public abstract class Enum : ValueType { }
    public struct Nullable<T> where T : struct { }

    public sealed class String
    {
        public readonly int Length;

        public String(char c, int count) { }
    }
    public abstract class Array { }
    public abstract class Delegate { }
    public abstract class MulticastDelegate : Delegate { }

    public struct RuntimeTypeHandle { }
    public struct RuntimeMethodHandle { }
    public struct RuntimeFieldHandle { }

    public class Attribute { }

    namespace Runtime.CompilerServices
    {
        public class RuntimeHelpers
        {
            public static unsafe int OffsetToStringData => sizeof(IntPtr) + sizeof(int);
        }

        public class RuntimeFeature
        {
            public const string UnmanagedSignatureCallingConvention = nameof(UnmanagedSignatureCallingConvention);
        }
    }
    
    
}

namespace System.Runtime.InteropServices
{
    public class UnmanagedType { }

    sealed class StructLayoutAttribute : Attribute
    {
        public StructLayoutAttribute(LayoutKind layoutKind)
        {
        }
    }

    internal enum LayoutKind
    {
        Sequential = 0, // 0x00000008,
        Explicit = 2, // 0x00000010,
        Auto = 3, // 0x00000000,
    }

    internal enum CharSet
    {
        None = 1,       // User didn't specify how to marshal strings.
        Ansi = 2,       // Strings should be marshalled as ANSI 1 byte chars.
        Unicode = 3,    // Strings should be marshalled as Unicode 2 byte chars.
        Auto = 4,       // Marshal Strings in the right way for the target system.
    }
}

namespace System.Runtime.InteropServices
{
    public sealed class DllImportAttribute : Attribute
    {
        public DllImportAttribute(string dllName,string EntryPoint) { }
    }
    
    public sealed class UnmanagedCallersOnlyAttribute : Attribute {
        public string EntryPoint;
        public CallingConvention CallingConvention;

        public UnmanagedCallersOnlyAttribute() { }
    }
    
    public enum CallingConvention {
        Winapi = 1,
        Cdecl = 2,
        StdCall = 3,
        ThisCall = 4,
        FastCall = 5,
    }
}



#region Things needed by ILC

namespace System
{
    namespace Runtime
    {
        internal sealed class RuntimeExportAttribute : Attribute
        {
            public RuntimeExportAttribute(string entry) { }
        }
    }
    
    
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
    
    
    class Array<T> : Array { }
    
    public class Exception
    {
    }
}


namespace Internal.Runtime.CompilerHelpers
{
    using System.Runtime;

    class StartupCodeHelpers
    {
        [RuntimeExport("RhpReversePInvoke2")]
        static void RhpReversePInvoke2() { }
        [RuntimeExport("RhpReversePInvokeReturn2")]
        static void RhpReversePInvokeReturn2() { }
        [System.Runtime.RuntimeExport("__fail_fast")]
        static void FailFast() { while (true) ; }
        [System.Runtime.RuntimeExport("RhpPInvoke")]
        static void RphPinvoke() { }
        [System.Runtime.RuntimeExport("RhpPInvokeReturn")]
        static void RphPinvokeReturn() { }
        
        
        [System.Runtime.RuntimeExport("RhpNewFast")]
        static void RhpNewFast() { }
        
        [System.Runtime.RuntimeExport("RhpThrowEx")]
        static void RhpThrowEx() { }
    }

    // ReSharper disable once InconsistentNaming
    public enum ExceptionStringID
    {
        // TypeLoadException
        ClassLoadGeneral,
        ClassLoadExplicitGeneric,
        ClassLoadBadFormat,
        ClassLoadExplicitLayout,
        ClassLoadValueClassTooLarge,
        ClassLoadRankTooLarge,

        // MissingMethodException
        MissingMethod,

        // MissingFieldException
        MissingField,

        // FileNotFoundException
        FileLoadErrorGeneric,

        // InvalidProgramException
        InvalidProgramDefault,
        InvalidProgramSpecific,
        InvalidProgramVararg,
        InvalidProgramCallVirtFinalize,
        InvalidProgramUnmanagedCallersOnly,
        InvalidProgramCallAbstractMethod,
        InvalidProgramCallVirtStatic,
        InvalidProgramNonStaticMethod,
        InvalidProgramGenericMethod,
        InvalidProgramNonBlittableTypes,
        InvalidProgramMultipleCallConv,

        // BadImageFormatException
        BadImageFormatGeneric,
        BadImageFormatSpecific,

        // MarshalDirectiveException
        MarshalDirectiveGeneric,
    }
    
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

#endregion


public static unsafe class Program
{
    static void Main() { }
    
    [DllImport("kernel","WriteString")]
    public static extern void WriteString(char* format);

    //[System.Runtime.RuntimeExport("entry")]
    [UnmanagedCallersOnly(EntryPoint = "kernel_main", CallingConvention = CallingConvention.StdCall)]
    static int EfiMain()
    {
        string hello = "Hello world!";
        fixed (char* c = hello)
        {
            
            //WriteString(c);
        }

        //while (true) ;
        return 42;
    }
}

