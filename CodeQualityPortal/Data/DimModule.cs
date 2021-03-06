﻿using System.Collections.Generic;

namespace CodeQualityPortal.Data
{
    public class DimModule
    {
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string AssemblyVersion { get; set; }
        public string FileVersion { get; set; }
        public virtual List<DimSystem> Systems { get; set; }
        public virtual List<DimNamespace> Namespaces { get; set; }
        public virtual List<FactMetrics> Metrics { get; set; }
        public int? RepoId { get; set; }
        public virtual DimRepo Repo { get; set; }
        public int? TeamId { get; set; }
        public virtual DimTeam Team { get; set; }

        public DimModule ()
        {
            Namespaces = new List<DimNamespace>();
            Metrics = new List<FactMetrics>();
        }
    }
}
