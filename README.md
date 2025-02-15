runSA
=====

RunSA is a fork from RunUO that began in 2006 and went on till 2010, more or less.
The main focus was to always support the newest clients, and so we did.

Supported:
- UO:Kingdom Reborn
- UO:Stygian Abyss
- UO:High Seas Expansion

=====

[![AppVeyor Build Status](https://ci.appveyor.com/api/projects/status/4tjo91e4qotjtsgq?svg=true)](https://ci.appveyor.com/project/ms/runuo) [![Travis Build Status](https://travis-ci.org/runuo/runuo.svg)](https://travis-ci.org/runuo/runuo)

[![Join the chat at https://gitter.im/runuo/runuo](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/runuo/runuo?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

RunUO Git Repository

*** 
RunUO is no longer officially supported by a core team.

If you wish to find support in a wider UO development commuity, visit [ServUO - Ultima Online Emulation](http://www.servuo.com)
***

Typical Windows Build

PS C:\runuo> C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc /optimize /unsafe /t:exe /out:RunUO.exe /win32icon:Server\runuo.ico /d:NEWTIMERS /d:NEWPARENT /recurse:Server\\*.cs


Typical Linux Build (MONO)

~/runuo$ mcs -optimize+ -unsafe -t:exe -out:RunUO.exe -win32icon:Server/runuo.ico -nowarn:219,414 -d:NEWTIMERS -d:NEWPARENT -d:MONO -reference:System.Drawing -recurse:Server/*.cs


zlib is required for certain functionality. Windows zlib builds are packaged with releases and can also be obtained separately here: https://github.com/msturgill/zlib/releases/latest

RunUO supports Intel's hardware random number generator (Secure Key, Bull Mountain, rdrand, etc). If rdrand32.dll/rdrand64.dll are present in the base directory and the hardware supports that functionality, it will be used automatically. You can find those libraries here: https://github.com/msturgill/rdrand/releases/latest

Latest Razor builds can be found at https://github.com/msturgill/razor/releases/latest

Latest UOSteam builds (previously AssistUO) can be found at http://uosteam.com

