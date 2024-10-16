using System;
using System.Security.Cryptography.X509Certificates;
using API.Data;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PdfDocumentController : ControllerBase
{
    private readonly IPdfDocumentService _pdfDocumentService;
    public PdfDocumentController(IPdfDocumentService pdfDocumentService)
    {
        _pdfDocumentService = pdfDocumentService;
    }
    
    [HttpPost("upload")]
    public async Task<IActionResult> UploadPdf([FromForm] IFormFile file, [FromForm] int categoryId)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is empty");
        if (Path.GetExtension(file.FileName).ToLower() != ".pdf")
            return BadRequest("File is not a PDF");
        var result = await _pdfDocumentService.UploadPdfAsync(file, categoryId);
        
        if (result)
            return Ok("File uploaded successfully");
        else
            return StatusCode(500, "An error occurred while uploading the file");
    }
    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetPdfsByCategory(int categoryId)
    {
        var pdfs = await _pdfDocumentService.GetPdfsByCategoryAsync(categoryId);
        return Ok(pdfs);
    }
}