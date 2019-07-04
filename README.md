# plsql-developer-plugin-net
A demo PL/SQL Developer plug-in written in C# and a tutorial on writing plug-ins in C#. It is a C# class library (dll file), exporting API required by the Allround Automations PL/SQL Developer IDE.

The goal of the project is to provide documentation, tips & hints, and demonstrate how plug-ins for PL/SQL Developer IDE can be created using C#. Visit [project's wiki](https://github.com/aniskop/plsql-developer-plugin-net/wiki) for more info.

Tools used
=====
* Microsoft Visual Studio 2019 Community edition
* Microsoft Visual Studio Code
* [UnmanagedExports by Robert Giesecke](https://www.nuget.org/packages/UnmanagedExports) (NuGet package)

PLSQLDevWrapper
====
* Used for working with x64 builds 

Building
====
* Open the solution in Visual Studio.
* Make sure platform is not AnyCPU, because AnyCPU assemblies cannot export functions.
* For Visual Studio 2019
  * install Microsoft Build Tools 2015

* Build the solution.

Installing and running the demo plug-in
==============
* Just copy the DemoPluginNet.dll to <PL/SQL Developer home>\plugins or to plug-ins directory set in PL/SQL Developer preferences.
* Restart the PL/SQL Developer.

If everything went correct, you see "Demo plug-in in C#" entry in the Plug-in Manager (Tools > Configure Plug-Ins...) and a new menu  item "Demo plug-in in C#" under Tools menu.
