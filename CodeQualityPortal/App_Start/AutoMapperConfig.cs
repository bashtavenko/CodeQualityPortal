using System.Linq;

using AutoMapper;

using CodeQualityPortal.Data;
using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal
{
    public class AutoMapperConfig
    {
        public static void CreateMaps()
        {
            Mapper.CreateMap<IGrouping<DateTuple, FactMetrics>, TrendItem>()
                .ForMember(m => m.DateId, opt => opt.MapFrom(src => src.Key.DateId))
                .ForMember(m => m.Date, opt => opt.MapFrom(src => src.Key.DateTime))
                .ForMember(m => m.MaintainabilityIndex, opt => opt.MapFrom(src => (int?)src.Average(a => a.MaintainabilityIndex)))
                .ForMember(m => m.CyclomaticComplexity, opt => opt.MapFrom(src => (int?)src.Sum(a => a.CyclomaticComplexity)))
                .ForMember(m => m.ClassCoupling, opt => opt.MapFrom(src => (int?)src.Sum(a => a.ClassCoupling)))
                .ForMember(m => m.DepthOfInheritance, opt => opt.MapFrom(src => (int?)src.Max(a => a.DepthOfInheritance)))
                .ForMember(m => m.LinesOfCode, opt => opt.MapFrom(src => (int?)src.Sum(a => a.LinesOfCode)));

            Mapper.CreateMap<FactMetrics, ModuleItem>()
                .ForMember(m => m.Id, opt => opt.MapFrom(src => src.Module.ModuleId))
                .ForMember(m => m.Name, opt => opt.MapFrom(src => src.Module.Name))
                .ForMember(m => m.AssemblyVersion, opt => opt.MapFrom(src => src.Module.AssemblyVersion));

            Mapper.CreateMap<DimModule, Module>()
                .ForMember(m => m.Id, opt => opt.MapFrom(src => src.ModuleId));

            Mapper.CreateMap<FactMetrics, ModuleItem>()
                .ForMember(m => m.Id, opt => opt.MapFrom(src => src.Module.ModuleId))
                .ForMember(m => m.Name, opt => opt.MapFrom(src => src.Module.Name))
                .ForMember(m => m.AssemblyVersion, opt => opt.MapFrom(src => src.Module.AssemblyVersion));

            Mapper.CreateMap<FactMetrics, NamespaceItem>()
                .ForMember(m => m.Id, opt => opt.MapFrom(src => src.Namespace.NamespaceId))
                .ForMember(m => m.Name, opt => opt.MapFrom(src => src.Namespace.Name));

            Mapper.CreateMap<DimType, ViewModels.Type>()
                .ForMember(m => m.Id, opt => opt.MapFrom(src => src.TypeId));
            
            Mapper.CreateMap<DimNamespace, Namespace>()
                .ForMember(m => m.Id, opt => opt.MapFrom(src => src.NamespaceId));

            Mapper.CreateMap<FactMetrics, TypeItem>()
                .ForMember(m => m.Id, opt => opt.MapFrom(src => src.Type.TypeId))
                .ForMember(m => m.Name, opt => opt.MapFrom(src => src.Type.Name));

            Mapper.CreateMap<DimMember, Member>()
                .ForMember(m => m.Id, opt => opt.MapFrom(src => src.MemberId));

            Mapper.CreateMap<FactMetrics, MemberItem>()
                .ForMember(m => m.Id, opt => opt.MapFrom(src => src.Member.MemberId))
                .ForMember(m => m.Name, opt => opt.MapFrom(src => src.Member.Name));

            Mapper.CreateMap<FactMetrics, MemberSummary>()
                .ForMember(m => m.Tag, opt => opt.MapFrom(src => src.Member.Type.Namespace.Module.Target.Tag))
                .ForMember(m => m.Module, opt => opt.MapFrom(src => src.Member.Type.Namespace.Module.Name))
                .ForMember(m => m.Namespace, opt => opt.MapFrom(src => src.Member.Type.Namespace.Name))
                .ForMember(m => m.Type, opt => opt.MapFrom(src => src.Member.Type.Name))
                .ForMember(m => m.Id, opt => opt.MapFrom(src => src.Member.MemberId))
                .ForMember(m => m.Name, opt => opt.MapFrom(src => src.Member.Name));

            Mapper.CreateMap<FactCodeChurn, FileChurnSummary>()
                .ForMember(m => m.Date, opt => opt.MapFrom(src => src.Date.Date))
                .ForMember(m => m.FileName, opt => opt.MapFrom(src => src.File.FileName))
                .ForMember(m => m.Url, opt => opt.MapFrom(src => src.File.Url));
        }       
    }
}