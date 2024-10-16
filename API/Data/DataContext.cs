using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions options) : DbContext(options){
    public DbSet<PdfDocument> PdfDocuments { get; set; }
    public DbSet<Category> Categories { get; set; }
}