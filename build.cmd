@set ILCPATH=%CS_ILCOMPILER%\tools
@if not exist %ILCPATH%\ilc.exe (
  echo The DROPPATH environment variable not set. Refer to README.md.
  exit /B
)

@del zerosharp.ilexe >nul 2>&1
@del zerosharp.obj >nul 2>&1
@del zerosharp.map >nul 2>&1
@del zerosharp.pdb >nul 2>&1

@set CSC_PATH="C:\Program Files\dotnet\sdk\6.0.100-preview.4.21255.9\Roslyn\bincore"

dotnet %CSC_PATH%/csc.dll /debug:embedded /noconfig /nostdlib /runtimemetadataversion:v4.0.30319 CsharpLinkingTest/zerosharp.cs /out:zerosharp.ilexe /langversion:latest /unsafe

:: %ILCPATH%\ilc zerosharp.ilexe -o zerosharp.obj --nativelib --systemmodule zerosharp --map zerosharp.map -O --directpinvoke:kernel
%ILCPATH%\ilc zerosharp.ilexe -o zerosharp.obj --systemmodule zerosharp --map zerosharp.map -O --directpinvoke:kernel