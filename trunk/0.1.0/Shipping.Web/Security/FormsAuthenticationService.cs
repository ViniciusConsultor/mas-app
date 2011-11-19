using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;

namespace Shipping.Web.Security
{
    public class FormsAuthenticationService : IFormsAuthenticationService
    {

        #region IFormsAuthenticationService Members

        public string DefaultUrl
        {
            get { return FormsAuthentication.DefaultUrl; }
        }

        public string LoginUrl
        {
            get { return FormsAuthentication.LoginUrl; }
        }

        public int Timeout
        {
            get { return FormsAuthentication.Timeout.Minutes; }
        }

        public bool RequireSSL
        {
            get { return FormsAuthentication.RequireSSL; }
        }

        public HttpCookie GetAuthenticationCookie(string userName, bool createPersistentCookie)
        {
            return FormsAuthentication.GetAuthCookie(userName, createPersistentCookie);
        }

        public void SignIn(string userName, bool createPersistentCookie)
        {
            if(String.IsNullOrEmpty(userName)) 
            {
                throw new ArgumentNullException("userName");
            }
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        #endregion
    }
}
