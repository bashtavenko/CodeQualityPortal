using System;

namespace CodeQualityPortal.ViewModels
{
    public class ModuleItem  : MetricsItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AssemblyVersion { get; set; }        
    }
}