using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Business.Entities
{
    public class TokenVerification
    {
        public string Token { get; set; }

        public Guid UserId { get; set; }

        public DateTime ExpirationDateTime { get; set; }
    }
}
