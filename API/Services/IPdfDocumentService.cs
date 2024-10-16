using System;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services;
public interface IPdfDocumentService
{
    Task<bool> UploadPdfAsync(IFormFile file, int categoryId);
    Task<IEnumerable<PdfDocument>> GetPdfsByCategoryAsync(int categoryId);
}
public class PdfDocumentService : IPdfDocumentService
{
    private readonly DataContext _context;
    private readonly string _uploadPath;
    public PdfDocumentService(DataContext context, IConfiguration configuration)
    {
        _context = context;
        _uploadPath = configuration.GetValue<string>("UploadPath");
    }
    public async Task<bool> UploadPdfAsync(IFormFile file, int categoryId)
    {
        try
        {
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(_uploadPath, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var pdfDocument = new PdfDocument
            {
                FileName = file.FileName,
                FilePath = filePath,
                UploadDate = DateTime.UtcNow,
                CategoryId = categoryId
            };
            _context.PdfDocuments.Add(pdfDocument);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public async Task<IEnumerable<PdfDocument>> GetPdfsByCategoryAsync(int categoryId)
    {
        return await _context.PdfDocuments
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }
}