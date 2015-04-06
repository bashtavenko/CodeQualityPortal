using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Data
{
    public class CodeChurnRepository : ICodeChurnRepository
    {
        private readonly CodeQualityContext _context;
        private bool _disposed;

        public CodeChurnRepository()
            : this(new CodeQualityCreateDatabaseIfNotExists())
        {
        }

        public CodeChurnRepository(IDatabaseInitializer<CodeQualityContext> context)
        {
            _context = new CodeQualityContext(context);
        }

        public IList<Repo> GetRepos()
        {
            return _context.Repos.Select(s => new Repo { Name = s.Name, RepoId = s.RepoId }).ToList();                    
        }
        
        public IList<CodeChurnByDate> GetTrend(DateTime dateFrom, DateTime dateTo)
        {
            IQueryable<FactCodeChurn> query = _context
                    .Churn
                    .Where(f => f.Date.Date >= dateFrom.Date && f.Date.Date <= dateTo.Date && f.FileId == null);
            
            var itemsQuery = query
                .GroupBy(s => new { s.DateId, s.Date.DateTime })                    
                .Select(s => new CodeChurnByDate
                {
                    Date = s.Key.DateTime,
                    DateId = s.Key.DateId,
                    LinesAdded = s.Sum(a => a.LinesAdded),                        
                    LinesDeleted = s.Sum(d => d.LinesDeleted),
                    TotalChurn = s.Sum(t => t.TotalChurn)
                });

            var items = itemsQuery.ToList();

            if (items.Any())
            {
                var dates = new List<DateTime>();
                for (var date = dateFrom; date <= dateTo; date = date.AddDays(1))
                {
                    dates.Add(date);
                }

                // Essentially "dates left outer join items"
                return dates.GroupJoin(items.DefaultIfEmpty(),
                    d => d.Date,
                    i => i.Date.Date,
                    (d, i) => new CodeChurnByDate
                    {
                        Date = d.Date,
                        DateId = i.Any() ? i.First().DateId : null, // there's always 1 or 0 items in the group
                        LinesAdded = i.Any() ? i.First().LinesAdded : null,                            
                        LinesDeleted = i.Any() ? i.First().LinesDeleted : null,
                        TotalChurn = i.Any() ? i.First().TotalChurn : null
                    }).ToList();
            }
            else
            {
                return new List<CodeChurnByDate>();
            }
        }

        public IList<CommitCodeChurn> GetCommitsByDate(int repoId, int dateId)
        {
            return _context.Churn.Where(f => f.Commit.RepoId == repoId && f.Date.DateId == dateId && f.FileId == null)
                .Select(s => new CommitCodeChurn
                {
                    CommitId = s.Commit.CommitId,
                    Sha = s.Commit.Sha,
                    Message = s.Commit.Message,
                    Url = s.Commit.Url,
                    Committer = s.Commit.Committer,
                    CommitterAvatarUrl = s.Commit.CommitterAvatarUrl,                            
                    LinesAdded = s.LinesAdded,                            
                    LinesDeleted = s.LinesDeleted,
                    TotalChurn = s.TotalChurn
                }).ToList();
        }

        public IList<FileCodeChurn> GetFilesByCommit(int commitId)
        {
            return _context.Churn
                .Where(f => f.CommitId == commitId && f.FileId != null)
                .Select(s => new FileCodeChurn
                {
                    FileName = s.File.FileName,
                    Url = s.File.Url,
                    LinesAdded = s.LinesAdded,                        
                    LinesDeleted = s.LinesDeleted,
                    TotalChurn = s.TotalChurn
                }).ToList();
        }

        public IList<FileCodeChurn> GetFilesByDate(int repoId, int dateId, int? topX)
        {
            var query = _context.Churn
                .Where(f => f.DateId == dateId && f.File.Commits.Any(a => a.RepoId == repoId));
            
            if (topX.HasValue)
            {
                query = query.OrderByDescending(s => s.TotalChurn).Take(topX.Value);
            }

            return query.Select(s => new FileCodeChurn
            {
                FileName = s.File.FileName,
                Url = s.File.Url,
                LinesAdded = s.LinesAdded,                    
                LinesDeleted = s.LinesDeleted,
                TotalChurn = s.TotalChurn
            }).ToList();
        }

        public IList<FileCodeChurn> GetWorst(DateTime dateFrom, DateTime dateTo, int topX)
        {
            var files = _context.Churn
                .Where(w => w.Date.Date >= dateFrom && w.Date.Date <= dateTo && w.FileId != null && !string.IsNullOrEmpty(w.File.FileName))
                .GroupBy(g => g.File.FileName)
                .Select(s => new FileCodeChurn
                    {
                        FileName = s.Key,
                        LinesAdded = s.Max(k => k.LinesAdded),
                        LinesDeleted = s.Max(k => k.LinesDeleted),
                        TotalChurn = s.Max(k => k.TotalChurn)
                    })
                .OrderByDescending(o => o.TotalChurn)
                .Take(topX)
                .ToList();
                
            return files;                
        }

        public IList<RepoCodeChurnSummary> GetRepoChurnSummaryByDate(int dateId)
        {
            return _context.Churn
                .Where(w => w.DateId == dateId && w.FileId == null)
                .GroupBy(g => new {g.Commit.Repo.RepoId, g.Commit.Repo.Name})
                .Select(s => new RepoCodeChurnSummary
                {
                    RepoId = s.Key.RepoId,
                    RepoName = s.Key.Name,
                    LinesAdded = s.Sum(a => a.LinesAdded),
                    LinesDeleted = s.Sum(a => a.LinesDeleted),
                    CommitCount = s.Count()
                }).ToList();
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