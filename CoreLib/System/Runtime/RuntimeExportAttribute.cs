namespace System.Runtime
{
    public sealed class RuntimeExportAttribute : Attribute
    {
        public RuntimeExportAttribute(string entry) { }
    }
    
    public sealed class RuntimeImportAttribute : Attribute
    {
        public RuntimeImportAttribute(string entry) { }
    }
}