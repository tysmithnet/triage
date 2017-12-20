# Triage
Triage is an extensible memory dump analyzer. In the future I plan to proivde for event trace log parsing as well.

### Mortician
Mortician is the application that performs memory dump analysis. It uses MEF to load components at run time and so 
extending the application amounts to using [Import] and [Export] attributes and implmeneted various interfaces.

If you follow the setup correctly, your plugin will just "work". There is an excel based plugin to learn from or use
included in the Analyzers project.

##### Setup
Triage uses CLRMd under the hood -which requires the debugging assemblies typically used by windbg. These
assemblies come from the Windows Debugging Kit. You get that here: https://docs.microsoft.com/en-us/windows-hardware/drivers/debugger/
After you install it, add the following to your path:

- C:\Program Files (x86)\Windows Kits\10\Debuggers\x64
- C:\Program Files (x86)\Windows Kits\10\Debuggers\x64\winext

##### Install
1. `git clone --recursive https://github.com/tysmithnet/triage.git triage`
2. `cd triage`
3. Build with VS 2017


##### Config
Mortician can take 2 sub commands: `run` and `config`. `config` is used to create settings that can be used when you
`run`.

`mortician.exe config -k "key1" "key2" -v "val1" "val2"` -> `{"key1": "val1", "key2": "val2"}`

Consult plugin documentation on the required and optional settings.

##### Run
Mortician takes a memory dump. That's it.

`mortician.exe run -d c:\temp\mem.dmp`