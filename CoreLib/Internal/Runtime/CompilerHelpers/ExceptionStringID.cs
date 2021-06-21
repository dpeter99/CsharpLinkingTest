namespace System.Internal.Runtime.CompilerHelpers
{
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
}