﻿namespace CodeQualityPortal.ViewModels
{
    public class MemberSummary : MetricsItem
    {
        public string Tag { get; set; }
        public string Module { get; set; }
        public string Namespace { get; set; }
        public string Type { get; set; }
    }
}