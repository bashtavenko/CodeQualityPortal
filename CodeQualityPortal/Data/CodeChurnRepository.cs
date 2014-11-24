using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CodeQualityPortal.Data
{
    public class CodeChurnRepository : ICodeChurnRepository
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["CodeQuality"].ToString();
        public IEnumerable<ViewModels.Repo> GetRepos()
        {
            using (var context = new CodeQualityContext())
            {
                return context.Repos.Select(s => new ViewModels.Repo { Name = s.Name, RepoId = s.RepoId });                    
            }            
        }
        
        public IEnumerable<ViewModels.CodeChurnByDate> GetCodeChurnTrend(int repoId, DateTime dateFrom, DateTime dateTo, string fileExtension)
        {
            using (var context = new CodeQualityContext())
            {
                return context.Churn
                    .Where(f => f.Date.Date >= dateFrom.Date && f.Date.Date <= dateTo.Date && f.File.FileExtension == fileExtension && f.File.Commit.RepoId == repoId)
                    .Select(s => new ViewModels.CodeChurnByDate
                    {
                        Date = s.Date.Date,
                        DateId = s.DateId,
                        LinesAdded = s.LinesAdded,
                        LinesModified = s.LinesModified,
                        LinesDeleted = s.LinesDeleted,
                        TotalChurn = s.LinesAdded + s.LinesModified + s.LinesDeleted
                    });
            }                        
        }

        public IEnumerable<ViewModels.FileCodeChurn> GetCodeChurnDetails(int repoId, int dateId, string fileExtension)
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
                    });
            }            
        }
    }
}