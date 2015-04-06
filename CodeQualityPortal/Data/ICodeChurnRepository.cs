using System;
using System.Collections.Generic;

using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Data
{
    public interface ICodeChurnRepository : IDisposable
    {
        IList<Repo> GetRepos();
        IList<CodeChurnByDate> GetTrend(DateTime dateFrom, DateTime dateTo);
        IList<RepoCodeChurnSummary> GetRepoChurnSummaryByDate(int dateId);
        IList<CommitCodeChurn> GetCommitsByDate(int repoId, int dateId);
        IList<FileCodeChurn> GetFilesByCommit(int commitId);
        IList<FileCodeChurn> GetFilesByDate(int repoId, int dateId, int? topX);
        IList<FileCodeChurn> GetWorst(DateTime dateFrom, DateTime dateTo, int topX);
    }
}