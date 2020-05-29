using System;

namespace ApcAccessMetrics.Common.Metrics
{
    public class MetricAttribute : Attribute
    {
        public string Metric { get; set; }

    }
}