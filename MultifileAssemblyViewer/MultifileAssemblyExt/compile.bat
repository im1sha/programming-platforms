@echo off

csc /t:module /r:System.Runtime.dll /r:AttributeViewer.dll /r:ExportDll.dll /r:MultiDll.dll  /out:ClassPackage.module ClassPackage.cs
csc /addmodule:ClassPackage.module EntryPoint.cs
