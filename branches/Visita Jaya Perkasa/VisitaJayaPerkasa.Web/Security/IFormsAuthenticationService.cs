using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace VisitaJayaPerkasa.Web.Security
{
    public interface IFormsAuthenticationService
    {
        string DefaultUrl { get; }

        string LoginUrl { get; }

        int Timeout { get; }

        bool RequireSSL { get; }

        HttpCookie GetAuthenticationCookie(string userName, bool createPersistentCookie);

        void SignIn(string userName, bool createPersistentCookie);

        void SignOut();
    }
}
