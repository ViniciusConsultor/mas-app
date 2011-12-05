using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Web.Mvc
{
    /// <summary>
    /// Represents the context of a controller.  Used to hold context information needed in MediMobile controllers.
    /// </summary>
    public class Context
    {

        /// <summary>
        /// Gets or sets the User that is currently logged in.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; }
    }
}
