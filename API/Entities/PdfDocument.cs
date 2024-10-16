using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class PdfDocument
{
    public int Id { get; set; }
    [Required]
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public DateTime UploadDate { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
public class Category
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}