
using System;
using System.Runtime;
using System.Runtime.InteropServices;

namespace System
{
    public struct UInt64 { }
    
    public struct RuntimeTypeHandle { }
    public struct RuntimeMethodHandle { }
    public struct RuntimeFieldHandle { }
    
}


public static unsafe class Program
{
    static void Main() { }
    
    [DllImport("kernel","WriteString")]
    public static extern void WriteString(char* format);

    //[System.Runtime.RuntimeExport("entry")]
    [UnmanagedCallersOnly(EntryPoint = "sharp_entryPoint")]
    static int EfiMain()
    {
        string hello = "Hello world! from csharp";
        fixed (char* c = hello)
        {
            
            WriteString(c);
        }

        //while (true) ;
        return 42;
    }
}

