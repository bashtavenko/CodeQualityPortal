using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;

namespace CodeQualityPortal.Data
{
    public class MaintenanceRepository
    {
        private readonly CodeQualityContext _context;
        private bool _disposed;

        public MaintenanceRepository() 
            : this (new CodeQualityCreateDatabaseIfNotExists())
        {
        }
        
        public MaintenanceRepository(IDatabaseInitializer<CodeQualityContext> context)
        {
            _context = new CodeQualityContext(context);
        }

        public IList<IdName> GetIdNames(Category categories)
        {
            IQueryable<IdName> results;

            switch (categories)
            {
                case Category.Systems:
                    results = _context.Systems.Select(s => new IdName {Id = s.SystemId, Name = s.Name});
                    break;  
                case Category.Repos:
                    results = _context.Repos.Select(s => new IdName { Id = s.RepoId, Name = s.Name });
                    break;
                case Category.Teams:
                    results = _context.Teams.Select(s => new IdName { Id = s.TeamId, Name = s.Name });
                    break;
                default:
                    throw new ArgumentException();
            }

            return results.OrderBy(s => s.Name).ToList();
        }

        public IList<ViewModels.Module> GetModulesByCategory(Category category, int? value)
        {
            IQueryable<DimModule> results;

            switch (category)
            {
                case Category.Systems:
                    results = value.HasValue ? _context.Modules.Where(w => w.Systems.Any(a => a.SystemId == value)) : _context.Modules.Where(w => !w.Systems.Any());
                    break;
                case Category.Repos:
                    results = _context.Modules.Where(w => w.RepoId == value);
                    break;
                case Category.Teams:
                    results = _context.Modules.Where(w => w.TeamId == value);
                    break;
                default:
                    throw new ArgumentException();
            }

            return Mapper.Map<IList<ViewModels.Module>>(results);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}