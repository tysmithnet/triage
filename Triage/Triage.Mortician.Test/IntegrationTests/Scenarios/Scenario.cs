using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Microsoft.CSharp;
using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician.Test.IntegrationTests.Scenarios
{
    public abstract partial class Scenario
    {
        public virtual string CompileCSharp()
        {
            
            var provider = new CSharpCodeProvider();
            var compilerParameters = new CompilerParameters
            {
                GenerateExecutable = !IsLibrary,
                GenerateInMemory = false,
                CompilerOptions =
                    "/unsafe " + (IntPtr.Size == 4 ? "/platform:x86" : "/platform:x64"), // todo: can we do better?
                IncludeDebugInformation = true,
                
                OutputAssembly = DestinationFile
            };
            compilerParameters.ReferencedAssemblies.Add("system.dll"); // todo: allow scenarios to define 

            var compilerResults = provider.CompileAssemblyFromSource(compilerParameters, SourceFiles);

            if (compilerResults.Errors.Count > 0 && Debugger.IsAttached)
                Debugger.Break();

            return compilerResults.PathToAssembly;
        }
        public abstract bool IsLibrary { get; }
        public abstract string DestinationFile { get; }
        public abstract string[] SourceFiles { get; }

        public virtual DataTarget GetDataTarget()
        {
            return null;
        }

        private bool IsCompileNecessary() => true;
    }
}