namespace F1Web.Helpers
{
    /// <summary>
    /// Helper class accessing appsettings.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// The secret used in JWT.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// The test username.
        /// </summary>
        public string TestUserName { get; set; }

        /// <summary>
        /// The test password.
        /// </summary>
        public string TestPassword { get; set; }
    }
}
