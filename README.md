# PenelopeAI
Penelope is a Discord bot designed to call Azure AI studio models and built with Semantic Kernel.

## Current features
At this time, Penelope can only perform text-in/text-out tasks; generating responses to users questions or comments.

## Hopeful future features
- Image parsing
- Image generation
- Internet searches
- Document Q&A
- Document summarization

# Note:
This bot as it is now requires you set up the following in Azure:
1. An app registration.
2. Azure AI Studio with Meta-Llama-3-70B-Instruct deployed
3. An Azure Key Vault to security store your bot token
4. Setting up the ENV variables in the SecretsLibrary class accordingly.

This is a basic implementation in its currenty form though, and you could easily change the target model with Semantic Kernel to whatever model or generative ai service you wish.
