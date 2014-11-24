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
        public IList<ViewModels.Repo> GetRepos()
        {
            throw new NotImplementedException();
        }
        
        public IList<ViewModels.CodeChurnByDate> GetCodeChurnTrend(int repoId, DateTime dateFrom, DateTime dateTo, string fileExtension)
        {
            throw new NotImplementedException();
        }

        public IList<ViewModels.FileCodeChurn> GetCodeChurnTrend(int repoId, int dateId, string fileExtension)
        {
            throw new NotImplementedException();
        }
    }
}