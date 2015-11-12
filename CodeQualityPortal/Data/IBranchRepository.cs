using System;
using System.Collections.Generic;
using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Data
{
    public interface IBranchRepository : IDisposable
    {
        IList<ViewModels.Branch> GetBranches();
        IList<ViewModels.DataPoint> GetBranchCollectionDates(int? branchId);
        BranchDiff GetBranchDiff(int? branchAId, int dateAId, int? branchBId, int dateBId);
    }
}
