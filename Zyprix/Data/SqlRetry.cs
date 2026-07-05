using Microsoft.Data.SqlClient;

namespace Zyprix.Data
{
    public static class SqlRetry
    {
        private static readonly int[] TransientErrorNumbers =
        {
            -2,                         // command timeout
            0, 20, 64, 233, 997,        // connection / network level
            1203, 1204, 1205, 1222,     // lock / deadlock
            4060, 4221,
            10053, 10054, 10060, 10928, 10929,
            40143, 40197, 40501, 40540, 40613,
            42108, 42109,               // serverless instance is starting up / resuming
            49918, 49919, 49920
        };

        /// <summary>
        /// Singleton exponential-backoff retry provider (1 initial attempt + 3 retries).
        /// </summary>
        public static SqlRetryLogicBaseProvider Provider { get; } = Create();

        private static SqlRetryLogicBaseProvider Create()
        {
            var options = new SqlRetryLogicOption
            {
                NumberOfTries = 4,
                DeltaTime = TimeSpan.FromSeconds(2),
                MaxTimeInterval = TimeSpan.FromSeconds(30),
                TransientErrors = TransientErrorNumbers
            };

            return SqlConfigurableRetryFactory.CreateExponentialRetryProvider(options);
        }

        /// <summary>
        /// Attaches the shared retry policy to a connection + command pair.
        /// </summary>
        public static void Apply(SqlConnection connection, SqlCommand command)
        {
            connection.RetryLogicProvider = Provider;
            command.RetryLogicProvider = Provider;
        }
    }
}
