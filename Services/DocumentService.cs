using System.IO;
using System.Threading.Tasks;
using Document_search_bot.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Document_search_bot.Data;

public class DocumentService
{
    private readonly DocumentContext _context;

    public DocumentService(DocumentContext context)
    {
        _context = context;
    }

    public async Task<Document> UploadDocument(IFormFile file)
    {
        if (file.Length > 2 * 1024 * 1024)
            throw new Exception("File size exceeds the 2MB limit.");

        string uploadsFolder = Path.Combine("Uploads");
        Directory.CreateDirectory(uploadsFolder);
        string filePath = Path.Combine(uploadsFolder, file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var document = new Document
        {
            FileName = file.FileName,
            FilePath = filePath,
            FileType = Path.GetExtension(file.FileName),
            UploadedAt = DateTime.Now
        };

        _context.Documents.Add(document);
        await _context.SaveChangesAsync();

        return document;
    }

    public async Task DeleteDocument(int id)
    {
        var document = _context.Documents.FirstOrDefault(d => d.Id == id);
        if (document != null)
        {
            File.Delete(document.FilePath);
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
        }
    }
}
