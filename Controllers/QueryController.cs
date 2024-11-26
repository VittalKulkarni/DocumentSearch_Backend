using Microsoft.AspNetCore.Mvc;
//using Document_search_bot.Services;
using System.Threading.Tasks;
using Document_search_bot.Models;
using Document_search_bot.Models;

[ApiController]
[Route("api/query")]
public class QueryController : ControllerBase
{
    private readonly DocumentService _documentService;
    private readonly OpenAIService _openAIService;

    public QueryController(DocumentService documentService, OpenAIService openAIService)
    {
        _documentService = documentService;
        _openAIService = openAIService;
    }

    [HttpPost]
    public async Task<IActionResult> AskQuery([FromBody] QueryRequest request)
    {
        var document = _documentService.GetDocumentForQuery(); // get the document text or summary
        var answer = await _openAIService.GetAnswer(request.Query, document.Text);
        return Ok(new QueryResponse { Response = answer });
    }
}
