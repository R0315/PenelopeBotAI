using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace PenelopeBOT.Utilities
{
    /// <summary>
    /// Interface for accessing secrets and credentials.
    /// </summary>
    public interface ISecretsLibrary
    {
        /// <summary>
        /// Retrieves the bot token.
        /// </summary>
        /// <returns>The bot token.</returns>
        string GetBotToken();

        /// <summary>
        /// Retrieves the chat completion URI.
        /// </summary>
        /// <returns>The chat completion URI.</returns>
        Uri GetChatCompletionUri();

        /// <summary>
        /// Retrieves the API key.
        /// </summary>
        /// <returns>The API key.</returns>
        string GetApiKey();

        /// <summary>
        /// Retrieves the secret credential.
        /// </summary>
        /// <returns>The secret credential.</returns>
        ClientSecretCredential GetSecretCredential();
    }

    /// <summary>
    /// Class for accessing secrets and credentials.
    /// </summary>
    public class SecretsLibrary : ISecretsLibrary
    {
        private readonly SecretClient _secretClient;
        private readonly ClientSecretCredential _secretCredential;
        private readonly Uri _completionUri;
        private readonly string _apiKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretsLibrary"/> class.
        /// </summary>
        /// <param name="configuration">The configuration object.</param>
        public SecretsLibrary(IConfiguration configuration)
        {
            var keyVaultUrl = Environment.GetEnvironmentVariable("AZURE_KEYVAULT_URI")!;
            var clientId = Environment.GetEnvironmentVariable("AZURE_CLIENT_ID");
            var clientSecret = Environment.GetEnvironmentVariable("AZURE_CLIENT_SECRET");
            var tenantId = Environment.GetEnvironmentVariable("AZURE_TENANT_ID");
            _completionUri = new Uri(Environment.GetEnvironmentVariable("AZURE_CHAT_COMPLETION_URI")!);
            _apiKey = Environment.GetEnvironmentVariable("AZURE_CHAT_COMPLETION_APIKEY")!;

            _secretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            _secretClient = new SecretClient(new Uri(keyVaultUrl), _secretCredential);
        }

        /// <summary>
        /// Retrieves the bot token.
        /// </summary>
        /// <returns>The bot token.</returns>
        public string GetBotToken()
        {
            KeyVaultSecret secret = _secretClient.GetSecret(Environment.GetEnvironmentVariable("AZURE_KEYVAULT_TOKEN_SECRET"));
            return secret.Value;
        }

        /// <summary>
        /// Return the secret credential.
        /// </summary>
        /// <returns>The ClientSecretCredential</returns>
        public ClientSecretCredential GetSecretCredential() => _secretCredential;

        /// <summary>
        /// Retrieves the chat completion URI.
        /// </summary>
        /// <returns>The chat completion URI.</returns>
        public Uri GetChatCompletionUri() => _completionUri;

        /// <summary>
        /// Retrieves the API key.
        /// </summary>
        /// <returns>The API key.</returns>
        public string GetApiKey() => _apiKey;
    }
}
