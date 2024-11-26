using System.Threading.Tasks;
using OpenAI_API;

public class OpenAIService
{
    private readonly OpenAIAPI _api;

    public OpenAIService(string apiKey)
    {
        _api = new OpenAIAPI(apiKey);
    }

    public async Task<string> GetAnswer(string query, string documentText)
    {
        var prompt = $"Using the following document, answer the query:\n\n{documentText}\n\nQuery: {query}";
        var response = await _api.Completions.CreateCompletionAsync(prompt, max_tokens: 100);
        return response.ToString();
    }
}
