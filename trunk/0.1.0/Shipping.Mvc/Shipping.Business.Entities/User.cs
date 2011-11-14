using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Business.Entities
{
    public class User
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the salt.
        /// </summary>
        /// <value>
        /// The salt.
        /// </value>
        public string Salt { get; set; }

        /// <summary>
        /// Gets or sets the password hint.
        /// </summary>
        /// <value>
        /// The password hint.
        /// </value>
        public string PasswordHint { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the activation date.
        /// </summary>
        /// <value>
        /// The activation date.
        /// </value>
        public DateTime? ActivationDate { get; set; }

        /// <summary>
        /// Gets or sets the deactivation date.
        /// </summary>
        /// <value>
        /// The deactivation date.
        /// </value>
        public DateTime? DeactivationDate { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the middle initial.
        /// </summary>
        /// <value>
        /// The middle initial.
        /// </value>
        public string MiddleInitial { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is email verified.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is email verified; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmailVerified { get; set; }

        /// <summary>
        /// Gets or sets the password last changed.
        /// </summary>
        /// <value>
        /// The password last changed.
        /// </value>
        public DateTime? PasswordLastChanged { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is password change required.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is password change required; otherwise, <c>false</c>.
        /// </value>
        public bool IsPasswordChangeRequired { get; set; }

        /// <summary>
        /// Gets or sets the mobile phone number.
        /// </summary>
        /// <value>
        /// The mobile phone number.
        /// </value>
        public string MobilePhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the business phone number.
        /// </summary>
        /// <value>
        /// The business phone number.
        /// </value>
        public string BusinessPhoneNumber { get; set; }

        public string Nik { get; set; }

        public string address { get; set; }

        public string DateOfBirth { get; set; }

        public string MartialStatus { get; set; }

        public string Gender { get; set; }

        public override string ToString()
        {
            return this.LastName + ", " + this.FirstName;
        }
    }
}
