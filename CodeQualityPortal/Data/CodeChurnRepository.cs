using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CodeQualityPortal.Data
{
    public class CodeChurnRepository : ICodeChurnRepository
    {        
        public IList<ViewModels.Repo> GetRepos()
        {
            using (var context = new CodeQualityContext())
            {
                return context.Repos.Select(s => new ViewModels.Repo { Name = s.Name, RepoId = s.RepoId }).ToList();                    
            }            
        }
        
        public IList<ViewModels.CodeChurnByDate> GetCodeChurnTrend(int repoId, DateTime dateFrom, DateTime dateTo, string fileExtension)
        {
            using (var context = new CodeQualityContext())
            {
                var query = context
                    .Churn
                    .Where(f => f.Date.Date >= dateFrom.Date && f.Date.Date <= dateTo.Date && f.File.Commit.RepoId == repoId);

                if (!string.IsNullOrEmpty(fileExtension))
                {
                    query = query.Where ( w => w.File.FileExtension == fileExtension);
                }

                var itemsQuery = query
                    .GroupBy(s => new { s.DateId, s.Date.DateTime })                    
                    .Select(s => new ViewModels.CodeChurnByDate
                    {
                        Date = s.Key.DateTime,
                        DateId = s.Key.DateId,
                        LinesAdded = s.Sum(a => a.LinesAdded),
                        LinesModified = s.Sum(m => m.LinesModified),
                        LinesDeleted = s.Sum(d => d.LinesDeleted),
                        TotalChurn = s.Sum(t => t.LinesAdded + t.LinesModified + t.LinesDeleted)
                    });

                var items = itemsQuery.ToList();

                var dates = new List<DateTime>();
                for (var date = dateFrom; date <= dateTo; date = date.AddDays(1))
                {
                    dates.Add(date);                
                }

                return dates.Join(items.DefaultIfEmpty(),
                    d => d.Date,
                    i => i.Date,
                    (d, i) => new ViewModels.CodeChurnByDate { Date = d.Date, DateId = i.DateId, LinesAdded = i.LinesAdded, LinesModified = i.LinesModified, LinesDeleted = i.LinesDeleted, TotalChurn = i.TotalChurn })                    
                    .ToList();
            }                        
        }

        public IList<ViewModels.FileCodeChurn> GetCodeChurnDetails(int repoId, int dateId, string fileExtension)
        {
            using (var context = new CodeQualityContext())
            {
                return context.Churn
                    .Where(f => f.DateId == dateId && f.File.Commit.RepoId == repoId && f.File.FileExtension == fileExtension)
                    .Select(s => new ViewModels.FileCodeChurn
                    {
                        FileName = s.File.FileName,
                        Url = s.File.Url,
                        LinesAdded = s.LinesAdded,
                        LinesModified = s.LinesModified,
                        LinesDeleted = s.LinesDeleted,
                        TotalChurn = s.LinesAdded + s.LinesModified + s.LinesDeleted
                    }).ToList();
            }            
        }
    }
}