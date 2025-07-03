namespace UGMessenger.Infrastructure.Settings
{
    public class Connection
    {
        public static string GetOptionConfiguration(string defaultConnection)
        {
            if (!string.IsNullOrEmpty(defaultConnection))
                return defaultConnection;

            string envString = Environment.GetEnvironmentVariable(APIGatewaySet.Configuration);

            return envString;
        }
    }
}
