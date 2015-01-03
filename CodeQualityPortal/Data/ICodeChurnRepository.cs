using System;
using System.Collections.Generic;

using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Data
{
    public interface ICodeChurnRepository
    {
        IList<Repo> GetRepos();
        IList<CodeChurnByDate> GetTrend(int repoId, DateTime dateFrom, DateTime dateTo, string fileExtension);
        IList<CommitCodeChurn> GetCommitsByDate(int repoId, int dateId, string fileExtension);
        IList<FileCodeChurn> GetFilesByCommit(int commitId, string fileExtension);
        IList<FileCodeChurn> GetFilesByDate(int repoId, int dateId, string fileExtension, int? topX);
        IList<FileChurnSummary> GetWorst(DateTime dateFrom, DateTime dateTo, int topX);
    }
}