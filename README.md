# Triage
Triage is a suite of tools used to perform automated memory dump analysis.

### Mortician
Mortician is the application that performs memory dump analysis. It uses MEF to load components at run time and so 
extending the application amounts to using [Import] and [Export] attributes and implmeneted various interfaces.

If you follow the setup correctly, your plugin will just "work". There is an excel based plugin to learn from or use
included in the Analyzers project.

##### Config
Mortician can take 2 sub commands: `run` and `config`. `config` is used to create settings that can be used when you
`run`.

`mortician.exe -k "key1" "key2" -v "val1" "val2"` -> `{"key1": "val1", "key2": "val2"}`

Consult plugin documentation on the required and optional settings.

##### Run
Mortician takes a memory dump. That's it.

`mortician.exe run -d c:\temp\mem.dmp`

### Todo
- [ ] test suite
- [ ] web interface
- [ ] x86 support
- [ ] improve object/type extraction