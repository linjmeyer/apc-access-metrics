using System;

namespace ApcAccessMetrcs.Common.Apc
{
    /// <summary>
    /// Collects the status of an APC UPC
    /// </summary>
    public interface IApcStatusCollector
    {
        /// <summary>
        /// Collects the status of an APC UPC
        /// </summary>
        IApcStatus GetStatus();
    }
}
