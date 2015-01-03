using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Data
{
    public class CodeChurnRepository : ICodeChurnRepository
    {
        public IList<Repo> GetRepos()
        {
            using (var context = new CodeQualityContext())
            {
                return context.Repos.Select(s => new Repo { Name = s.Name, RepoId = s.RepoId }).ToList();                    
            }            
        }
        
        public IList<CodeChurnByDate> GetTrend(int repoId, DateTime dateFrom, DateTime dateTo, string fileExtension)
        {
            using (var context = new CodeQualityContext())
            {
                IQueryable<FactCodeChurn> query;
                
                if (!string.IsNullOrEmpty(fileExtension))
                {
                    // FactCodeChurn join File join Commit
                    query = context
                        .Churn
                        .Where(f => f.Date.Date >= dateFrom.Date && f.Date.Date <= dateTo.Date && f.File.Commit.RepoId == repoId && f.File.FileExtension == fileExtension);   
                }
                else
                {                    
                    // FactCodeChurn join Commit
                    query = context
                        .Churn
                        .Where(f => f.Date.Date >= dateFrom.Date && f.Date.Date <= dateTo.Date && f.Commit.RepoId == repoId && f.FileId == null);
                }

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
        }

        public IList<CommitCodeChurn> GetCommitsByDate(int repoId, int dateId, string fileExtension)
        {
            using (var context = new CodeQualityContext())
            {              
                if (!string.IsNullOrEmpty(fileExtension))
                {
                    var commits = context.Churn.Where(f => f.Date.DateId == dateId && f.File.FileExtension == fileExtension)
                    .GroupBy(f => f.CommitId)
                    .Select(s => new CommitCodeChurn {
                        CommitId = s.Key,
                        LinesAdded = s.Sum(a => a.LinesAdded),                        
                        LinesDeleted = s.Sum(a => a.LinesDeleted),
                        TotalChurn = s.Sum(a => a.TotalChurn) })
                    .ToList();

                    commits = commits.Join(context.Commits.ToList(), s => s.CommitId, d => d.CommitId,
                        (s, d) => new CommitCodeChurn
                        {
                            CommitId = d.CommitId,
                            Sha = d.Sha,
                            Message = d.Message,
                            Url = d.Url,
                            Committer = d.Committer,
                            CommitterAvatarUrl = d.CommitterAvatarUrl,                            
                            LinesAdded = s.LinesAdded,                            
                            LinesDeleted = s.LinesDeleted,                            
                            TotalChurn = s.TotalChurn
                        }).ToList();

                    return commits;
                }
                else
                {
                    return context.Churn.Where(f => f.Date.DateId == dateId && f.FileId == null)
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
            }
        }

        public IList<FileCodeChurn> GetFilesByCommit(int commitId, string fileExtension)
        {
            using (var context = new CodeQualityContext())
            {
                var query = context.Churn.Where(f => f.CommitId == commitId && f.FileId != null);

                if (!string.IsNullOrEmpty(fileExtension))
                {
                    query = query.Where(f => f.File.FileExtension == fileExtension);
                }

                return query
                    .Select(s => new FileCodeChurn
                    {
                        FileName = s.File.FileName,
                        Url = s.File.Url,
                        LinesAdded = s.LinesAdded,                        
                        LinesDeleted = s.LinesDeleted,
                        TotalChurn = s.TotalChurn
                    }).ToList();
            }
        }

        public IList<FileCodeChurn> GetFilesByDate(int repoId, int dateId, string fileExtension, int? topX)
        {
            using (var context = new CodeQualityContext())
            {
                var query = context.Churn
                    .Where(f => f.DateId == dateId && f.File.Commit.RepoId == repoId);

                if (!string.IsNullOrEmpty(fileExtension))
                {
                    query = query.Where (w => w.File.FileExtension == fileExtension);
                }

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
        }

        public IList<FileChurnSummary> GetWorst(DateTime dateFrom, DateTime dateTo, int topX)
        {
            using (var context = new CodeQualityContext())
            {
                var files = context.Churn
                    .Where(w => w.Date.Date >= dateFrom && w.Date.Date <= dateTo && w.File != null)
                    .OrderByDescending(o => o.TotalChurn)
                    .Take(topX);

                var items = Mapper.Map<IList<FileChurnSummary>>(files);
                return items;                
            }
        }
    }
}