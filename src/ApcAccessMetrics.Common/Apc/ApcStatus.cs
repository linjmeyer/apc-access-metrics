using ApcAccessMetrics.Common.Metrics;
using Newtonsoft.Json;

namespace ApcAccessMetrcs.Common.Apc
{
    /// <summary>
    /// Status of an APC
    /// </summary>
    public class ApcStatus
    {   
        [Metric(Metric="apc_access_time_left_minutes")]
        public double TimeLeft { get; set; }

        [JsonProperty("BCHARGE")]
        [Metric(Metric="apc_access_battery_charge_level_percent")]
        public double BatteryChargeLevel { get; set; }

        [MetricTag]
        public string Model { get; set; }

        [MetricTag]
        [JsonProperty("UPSNAME")]
        public string Name { get; set; }
    }
}
