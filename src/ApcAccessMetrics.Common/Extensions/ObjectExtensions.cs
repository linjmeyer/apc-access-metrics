using System.Collections.Generic;
using System.Linq;
using ApcAccessMetrics.Common.Metrics;

namespace ApcAccessMetrics.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static Dictionary<string, string> GetMetricTags<T>(this T obj)
        {
            var tags = new Dictionary<string, string>();
            var taggedProperty = 
                obj.GetType()
                .GetProperties()
                .Where(prop => prop.IsDefined(typeof(MetricTagAttribute), false));
            
            foreach(var property in taggedProperty)
            {
                tags.Add(property.Name, property.GetValue(obj)?.ToString());
            }

            return tags;
        }
    }
}