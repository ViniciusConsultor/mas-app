using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Business.Entities
{
    /// <summary>
    /// Represents the stats of a User Audit Entry.
    /// </summary>
    public enum UserAuditEntryStatus
    {
        /// <summary>
        /// Indicates that the login was successful.
        /// </summary>
        Success,

        /// <summary>
        /// Indicates the the login failed.
        /// </summary>
        Failure
    }
}
