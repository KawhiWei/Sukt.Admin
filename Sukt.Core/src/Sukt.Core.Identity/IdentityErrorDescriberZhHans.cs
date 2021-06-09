using Microsoft.AspNetCore.Identity;
using Sukt.Module.Core.Extensions;

namespace Sukt.Core.Identity
{
    ///具体请查看Microsoft.AspNetCore.Identity源码
    /// <summary>
    /// Service to enable localization for application facing identity errors.
    /// </summary>
    /// <remarks>
    /// These errors are returned to controllers and are generally used as display messages to end users.
    /// </remarks>
    public class IdentityErrorDescriberZhHans : IdentityErrorDescriber
    {
        /// <summary>
        /// Returns the default <see cref="IdentityError"/>.
        /// </summary>
        /// <returns>The default <see cref="IdentityError"/>.</returns>
        public override IdentityError DefaultError()
        {
            IdentityError error = base.DefaultError();
            error.Description = Resource.DefaultError;
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating a concurrency failure.
        /// </summary>
        /// <returns>An <see cref="IdentityError"/> indicating a concurrency failure.</returns>
        public override IdentityError ConcurrencyFailure()
        {
            IdentityError error = base.ConcurrencyFailure();
            error.Description = Resource.ConcurrencyFailure;
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating a password mismatch.
        /// </summary>
        /// <returns>An <see cref="IdentityError"/> indicating a password mismatch.</returns>
        public override IdentityError PasswordMismatch()
        {
            IdentityError error = base.PasswordMismatch();
            error.Description = Resource.PasswordMismatch;
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating an invalid token.
        /// </summary>
        /// <returns>An <see cref="IdentityError"/> indicating an invalid token.</returns>
        public override IdentityError InvalidToken()
        {
            IdentityError error = base.InvalidToken();
            error.Description = Resource.InvalidToken;
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating a recovery code was not redeemed.
        /// </summary>
        /// <returns>An <see cref="IdentityError"/> indicating a recovery code was not redeemed.</returns>
        public override IdentityError RecoveryCodeRedemptionFailed()
        {
            IdentityError error = base.RecoveryCodeRedemptionFailed();
            error.Description = Resource.RecoveryCodeRedemptionFailed;
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating an external login is already associated with an account.
        /// </summary>
        /// <returns>An <see cref="IdentityError"/> indicating an external login is already associated with an account.</returns>
        public override IdentityError LoginAlreadyAssociated()
        {
            IdentityError error = base.LoginAlreadyAssociated();
            error.Description = Resource.LoginAlreadyAssociated;
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating the specified user <paramref name="userName"/> is invalid.
        /// </summary>
        /// <param name="userName">The user name that is invalid.</param>
        /// <returns>An <see cref="IdentityError"/> indicating the specified user <paramref name="userName"/> is invalid.</returns>
        public override IdentityError InvalidUserName(string userName)
        {
            IdentityError error = base.InvalidUserName(userName);
            error.Description = Resource.InvalidUserName.FormatWith(userName);
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating the specified <paramref name="email"/> is invalid.
        /// </summary>
        /// <param name="email">The email that is invalid.</param>
        /// <returns>An <see cref="IdentityError"/> indicating the specified <paramref name="email"/> is invalid.</returns>
        public override IdentityError InvalidEmail(string email)
        {
            IdentityError error = base.InvalidEmail(email);
            error.Description = Resource.InvalidEmail.FormatWith(email);
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating the specified <paramref name="userName"/> already exists.
        /// </summary>
        /// <param name="userName">The user name that already exists.</param>
        /// <returns>An <see cref="IdentityError"/> indicating the specified <paramref name="userName"/> already exists.</returns>
        public override IdentityError DuplicateUserName(string userName)
        {
            IdentityError error = base.DuplicateUserName(userName);
            error.Description = Resource.DuplicateUserName.FormatWith(userName);
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating the specified <paramref name="email"/> is already associated with an account.
        /// </summary>
        /// <param name="email">The email that is already associated with an account.</param>
        /// <returns>An <see cref="IdentityError"/> indicating the specified <paramref name="email"/> is already associated with an account.</returns>
        public override IdentityError DuplicateEmail(string email)
        {
            IdentityError error = base.DuplicateEmail(email);
            error.Description = Resource.DuplicateEmail.FormatWith(email);
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating the specified <paramref name="role"/> name is invalid.
        /// </summary>
        /// <param name="role">The invalid role.</param>
        /// <returns>An <see cref="IdentityError"/> indicating the specific role <paramref name="role"/> name is invalid.</returns>
        public override IdentityError InvalidRoleName(string role)
        {
            IdentityError error = base.InvalidRoleName(role);
            error.Description = Resource.InvalidRoleName.FormatWith(role);
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating the specified <paramref name="role"/> name already exists.
        /// </summary>
        /// <param name="role">The duplicate role.</param>
        /// <returns>An <see cref="IdentityError"/> indicating the specific role <paramref name="role"/> name already exists.</returns>
        public override IdentityError DuplicateRoleName(string role)
        {
            IdentityError error = base.DuplicateRoleName(role);
            error.Description = Resource.DuplicateRoleName.FormatWith(role);
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating a user already has a password.
        /// </summary>
        /// <returns>An <see cref="IdentityError"/> indicating a user already has a password.</returns>
        public override IdentityError UserAlreadyHasPassword()
        {
            IdentityError error = base.UserAlreadyHasPassword();
            error.Description = Resource.UserAlreadyHasPassword;
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating user lockout is not enabled.
        /// </summary>
        /// <returns>An <see cref="IdentityError"/> indicating user lockout is not enabled.</returns>
        public override IdentityError UserLockoutNotEnabled()
        {
            IdentityError error = base.UserLockoutNotEnabled();
            error.Description = Resource.UserLockoutNotEnabled;
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating a user is already in the specified <paramref name="role"/>.
        /// </summary>
        /// <param name="role">The duplicate role.</param>
        /// <returns>An <see cref="IdentityError"/> indicating a user is already in the specified <paramref name="role"/>.</returns>
        public override IdentityError UserAlreadyInRole(string role)
        {
            IdentityError error = base.UserAlreadyInRole(role);
            error.Description = Resource.UserAlreadyInRole.FormatWith(role);
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating a user is not in the specified <paramref name="role"/>.
        /// </summary>
        /// <param name="role">The duplicate role.</param>
        /// <returns>An <see cref="IdentityError"/> indicating a user is not in the specified <paramref name="role"/>.</returns>
        public override IdentityError UserNotInRole(string role)
        {
            IdentityError error = base.UserNotInRole(role);
            error.Description = Resource.UserNotInRole.FormatWith(role);
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating a password of the specified <paramref name="length"/> does not meet the minimum length requirements.
        /// </summary>
        /// <param name="length">The length that is not long enough.</param>
        /// <returns>An <see cref="IdentityError"/> indicating a password of the specified <paramref name="length"/> does not meet the minimum length requirements.</returns>
        public override IdentityError PasswordTooShort(int length)
        {
            IdentityError error = base.PasswordTooShort(length);
            error.Description = Resource.PasswordTooShort.FormatWith(length);
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating a password does not meet the minimum number <paramref name="uniqueChars"/> of unique chars.
        /// </summary>
        /// <param name="uniqueChars">The number of different chars that must be used.</param>
        /// <returns>An <see cref="IdentityError"/> indicating a password does not meet the minimum number <paramref name="uniqueChars"/> of unique chars.</returns>
        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        {
            IdentityError error = base.PasswordRequiresUniqueChars(uniqueChars);
            error.Description = Resource.PasswordRequiresUniqueChars.FormatWith(uniqueChars);
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating a password entered does not contain a non-alphanumeric character, which is required by the password policy.
        /// </summary>
        /// <returns>An <see cref="IdentityError"/> indicating a password entered does not contain a non-alphanumeric character.</returns>
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            IdentityError error = base.PasswordRequiresNonAlphanumeric();
            error.Description = Resource.PasswordRequiresNonAlphanumeric;
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating a password entered does not contain a numeric character, which is required by the password policy.
        /// </summary>
        /// <returns>An <see cref="IdentityError"/> indicating a password entered does not contain a numeric character.</returns>
        public override IdentityError PasswordRequiresDigit()
        {
            IdentityError error = base.PasswordRequiresDigit();
            error.Description = Resource.PasswordRequiresDigit;
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating a password entered does not contain a lower case letter, which is required by the password policy.
        /// </summary>
        /// <returns>An <see cref="IdentityError"/> indicating a password entered does not contain a lower case letter.</returns>
        public override IdentityError PasswordRequiresLower()
        {
            IdentityError error = base.PasswordRequiresLower();
            error.Description = Resource.PasswordRequiresLower;
            return error;
        }

        /// <summary>
        /// Returns an <see cref="IdentityError"/> indicating a password entered does not contain an upper case letter, which is required by the password policy.
        /// </summary>
        /// <returns>An <see cref="IdentityError"/> indicating a password entered does not contain an upper case letter.</returns>
        public override IdentityError PasswordRequiresUpper()
        {
            IdentityError error = base.PasswordRequiresUpper();
            error.Description = Resource.PasswordRequiresUpper;
            return error;
        }
    }
}