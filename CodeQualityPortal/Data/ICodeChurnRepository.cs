using System;
using System.Collections.Generic;

using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Data
{
    public interface ICodeChurnRepository
    {
        IList<Repo> GetRepos();
        IList<CodeChurnByDate> GetCodeChurnTrend(int repoId, DateTime dateFrom, DateTime dateTo, string fileExtension);
        IList<FileCodeChurn> GetCodeChurnTrend(int repoId, int dateId, string fileExtension);
    }
}