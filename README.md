# `DnStrings`

DnStrings is a tool similar to the common `strings`/`strings.exe` tool which lists strings in a file. DnStrings is
similar, but specializes in .NET binaries.

It currently supports the following methods:
- `#US` heap: Read all strings from the #US heap, which contains user-defined strings (eg. for `Console.WriteLine("Hello world")` it would contain `Hello World`). This is the default.
- `#Strings` heap: Read all strings from the `#Strings` heap, which contains symbol names and such

Keep in mind that DnStrings is a command-line tool and is intended to work together with other command-line tools (such as `grep`, `sort`, `uniq`). It will not implement features that can be trivially accomplished using an existing tool.
