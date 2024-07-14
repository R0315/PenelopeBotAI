using Microsoft.SemanticKernel;

namespace PenelopeBOT.Utilities
{
    /// <summary>
    /// Class that builds the Kernel for the bot.
    /// </summary>
    public class KernelBuilder
    {
        private readonly ISecretsLibrary _secretsLibrary;
        public Kernel Kernel { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KernelBuilder"/> class.
        /// </summary>
        /// <param name="secretsLibrary">The ISecretsLibrary of the app.</param>
        public KernelBuilder(ISecretsLibrary secretsLibrary)
        {
            _secretsLibrary = secretsLibrary;
            Kernel = BuildKernel();
        }

        /// <summary>
        /// Builds the Kernel for the bot.
        /// </summary>
        /// <returns>The constructed Kernel.</returns>
        /// <remarks>Pragma warning required for using a different model other than OpenAI.</remarks>
        private Kernel BuildKernel()
        {
            #pragma warning disable SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            var botKernel = Kernel.CreateBuilder()
                           .AddOpenAIChatCompletion(modelId: "Meta-Llama-3-70B-Instruct", endpoint: _secretsLibrary.GetChatCompletionUri(), apiKey: _secretsLibrary.GetApiKey())
                           .Build();
            #pragma warning restore SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            return botKernel;
        }
    }
}
