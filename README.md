voodoo.patterns
=============
![build status](https://ci.appveyor.com/api/projects/status/9y1dqafs85pkieyd/branch/master?svg=true) ![language](https://img.shields.io/badge/language-C%23-blue.svg) ![MIT license](https://img.shields.io/badge/license-MIT-blue.svg) [![NuGet](https://img.shields.io/nuget/v/Voodoo.Patterns.svg)](https://www.nuget.org/packages/Voodoo.Patterns/)

The build is actually passing I'm having some AppVeyor issues.

A c# library containing a light cqrs implementation and a variety of extension methods.  Full <a href="http://minivercheevy.github.io/voodoo/">documentation</a>  available.

Install from nuget

>  PM> Install-Package Voodoo.Patterns


breaking changes in 3.0 
removed GridConstants
removed Request
removed ValidationExtensions.IsValid
removed EventLog support in FallbackLogger
Supports only NetStandard 2.0 (.net 4.6.1/.net Core 2.0)  