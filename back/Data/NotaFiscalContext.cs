using ApiNotaFiscal.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiNotaFiscal.Data;

public class NotaFiscalContext : DbContext
{
    public NotaFiscalContext(DbContextOptions<NotaFiscalContext> options) : base(options) { }

    public DbSet<NotaFiscal> NotasFiscais { get; set; }
}
