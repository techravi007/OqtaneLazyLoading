using Oqtane.Models;
using Oqtane.Modules;

namespace LLM.Module.LazyLoadingTest
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "LazyLoadingTest",
            Description = "LazyLoadingTest",
            Version = "1.0.0",
            ServerManagerType = "LLM.Module.LazyLoadingTest.Manager.LazyLoadingTestManager, LLM.Module.LazyLoadingTest.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "LLM.Module.LazyLoadingTest.Shared.Oqtane",
            PackageName = "LLM.Module.LazyLoadingTest" 
        };
    }
}
