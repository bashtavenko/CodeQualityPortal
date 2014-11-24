using System;
using System.Collections.Generic;

using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Data
{
    public interface ICodeChurnRepository
    {
        IEnumerable<Repo> GetRepos();
        IEnumerable<CodeChurnByDate> GetCodeChurnTrend(int repoId, DateTime dateFrom, DateTime dateTo, string fileExtension);
        IEnumerable<FileCodeChurn> GetCodeChurnDetails(int repoId, int dateId, string fileExtension);
    }
}