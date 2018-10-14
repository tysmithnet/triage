# Mortician
Mortician is an extensible Windows memory dump analyzer. I wrote it to assist in my daily site reliability tasks. In my case, memory dumps regularly exceed 30GB, and so processing must be as fast as possible. I should also mention that I run this in an m4.4xlarge	instance for dumps of this size. For nominally sized dumps, your desktop should be fine.

### Features
- [ ] Memory dump and crash dump analysis
- [x] x86 and x64 support
- [x] Parallel plugin execution