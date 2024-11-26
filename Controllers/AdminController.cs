using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
//using Document_search_bot.Services;
using Microsoft.AspNetCore.Http;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly DocumentService _documentService;

    public AdminController(DocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadDocument([FromForm] IFormFile file)
    {
        try
        {
            var document = await _documentService.UploadDocument(file);
            return Ok(document);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDocument(int id)
    {
        await _documentService.DeleteDocument(id);
        return NoContent();
    }
}
