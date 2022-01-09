using System;

namespace F1Web.Security
{
    /// <summary>
    /// Interface for adding JWT token policy.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generate a JWT token for the given user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The generated token.</returns>
        string GenerateToken(Guid userId);
    }
}
