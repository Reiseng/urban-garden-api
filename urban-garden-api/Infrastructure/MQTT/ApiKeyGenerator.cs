using System.Security.Cryptography;

namespace UrbanGarden.Api.Infrastructure
{
    public static class ApiKeyGenerator
    {
        public static string Generate()
        {
            return Convert.ToHexString(
                RandomNumberGenerator.GetBytes(32));
        }
    }
}
