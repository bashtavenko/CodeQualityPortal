using System.Collections.Generic;
using CodeQualityPortal.Data;

namespace CodeQualityPortal.ViewModels
{
    public class Trend
    {
        public int LastValue { get; set; }
        public List<DataPoint> DataPoints { get; set; }
        public TrendIndicator TrendIndicator { get; set; }

        // Angular ng-switch and ng-if directives don't work as expected inside of wj-flex-grid-column. 
        public string TrendIndicatorCssClass
        {
            get
            {
                switch (TrendIndicator)
                {
                    case TrendIndicator.Ascends:
                        return "fa fa-arrow-up";
                    case TrendIndicator.Descends:
                        return "fa fa-arrow-down";
                    default:
                        return "fa fa-arrows-h";
                }
            }
        }
    }
}