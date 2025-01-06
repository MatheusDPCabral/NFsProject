using System;
using ApiNotaFiscal.Data;
using ApiNotaFiscal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiNotaFiscal.Controllers;

[ApiController]
[Route("[controller]")]
public class NotaFiscalController : Controller
{
    private readonly NotaFiscalContext _context;
    private readonly NotaFiscalService _service;

    public NotaFiscalController(NotaFiscalContext context, NotaFiscalService service)
    {
        _context = context;
        _service = service;
    }

    [HttpPost("processar-xmls")]
    public async Task<IActionResult> ProcessarXmls()
    {
        try
        {
            string caminhoPasta = @"C:\Users\DEV\Downloads\XMLs"; // Alterar o caminho para o do diretorio na maquina atual.
            await _service.ProcessarXmls(caminhoPasta); // Aguarda o processamento.
            return Ok("Arquivos XML processados e notas fiscais salvas com sucesso.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao processar XMLs: {ex.Message}");
        }
    }

    [HttpGet] // pega todas as notas fiscais do banco
    public IEnumerable<NotaFiscal> GetNotas()
    {
        return _context.NotasFiscais.ToList();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaNota(int id)
    {
        var filme = _context.NotasFiscais.FirstOrDefault(
            filme => filme.Id == id);
        if (filme == null) return NotFound();
        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete]
    public IActionResult DeletarTodasNotas()
    {
        var notas = _context.NotasFiscais.ToList();
        
        if (notas.Count == 0) return NotFound("Nenhuma nota encontrada");

        _context.NotasFiscais.RemoveRange(notas);   // Remove todos os registros encontrados
        _context.SaveChanges();                     // Salva as alterações no banco


        // Resetar a contagem dos IDs para começar do 1 novamente
        _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.NotaFiscal', RESEED, 0)");


        return NoContent();
    }
}

