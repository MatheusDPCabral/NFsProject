using ApiNotaFiscal.Data;
using ApiNotaFiscal.Models;
using System.Xml.Linq;

public class NotaFiscalService
{
    private readonly NotaFiscalContext _context;

    public NotaFiscalService(NotaFiscalContext context)
    {
        _context = context;
    }

    public async Task ProcessarXmls(string caminhoPasta)
    {
        if (!Directory.Exists(caminhoPasta))
        {
            throw new DirectoryNotFoundException($"Caminho da pasta {caminhoPasta} não encontrado.");
        }

        var arquivosXml = Directory.GetFiles(caminhoPasta, "*.xml");
        var listaNotas = new List<NotaFiscal>();

        foreach (var arquivo in arquivosXml)
        {
            try
            {
                using (var fs = new FileStream(arquivo, FileMode.Open, FileAccess.Read))
                {
                    XDocument xml = XDocument.Load(fs);
                    var allElements = xml.Descendants().ToList();

                    var mod = allElements.FirstOrDefault(e => e.Name.LocalName == "mod")?.Value;
                    var cnpjEmitente = allElements.FirstOrDefault(e => e.Name.LocalName == "CNPJ")?.Value;
                    var nomeEmitente = allElements.FirstOrDefault(e => e.Name.LocalName == "xNome")?.Value;
                    var valorNota = allElements.FirstOrDefault(e => e.Name.LocalName == "vNF" || e.Name.LocalName == "vTPrest" || e.Name.LocalName == "vCFe")?.Value;

                    string numeroNota = null, chaveNota = null, dataEmissao = null;

                    if (mod == "59") // CFe
                    {
                        numeroNota = allElements.FirstOrDefault(e => e.Name.LocalName == "nCFe")?.Value;
                        chaveNota = allElements.FirstOrDefault(e => e.Name.LocalName == "infCFe")?.Attribute("Id")?.Value?.Substring(3);
                        dataEmissao = allElements.FirstOrDefault(e => e.Name.LocalName == "dEmi")?.Value;

                        if (!string.IsNullOrEmpty(dataEmissao) && dataEmissao.Length == 8)
                        {
                            dataEmissao = $"{dataEmissao.Substring(0, 4)}-{dataEmissao.Substring(4, 2)}-{dataEmissao.Substring(6, 2)}";
                        }
                    }
                    else if (mod == "57") // CTe
                    {
                        numeroNota = allElements.FirstOrDefault(e => e.Name.LocalName == "nCT")?.Value;
                        chaveNota = allElements.FirstOrDefault(e => e.Name.LocalName == "infCte")?.Attribute("Id")?.Value?.Substring(3);
                        dataEmissao = allElements.FirstOrDefault(e => e.Name.LocalName == "dhEmi")?.Value;
                    }
                    else if (mod == "55" || mod == "65") // NFe ou NFCe
                    {
                        numeroNota = allElements.FirstOrDefault(e => e.Name.LocalName == "nNF")?.Value;
                        chaveNota = allElements.FirstOrDefault(e => e.Name.LocalName == "infNFe")?.Attribute("Id")?.Value?.Substring(3);
                        dataEmissao = allElements.FirstOrDefault(e => e.Name.LocalName == "dhEmi" || e.Name.LocalName == "dEmi")?.Value;
                    }

                    TipoNota tipoNota = mod switch
                    {
                        "55" => TipoNota.NFe,
                        "57" => TipoNota.CTe,
                        "65" => TipoNota.NFCe,
                        "59" => TipoNota.CFe
                    };

                    if (!string.IsNullOrEmpty(numeroNota) && !string.IsNullOrEmpty(dataEmissao))
                    {
                        listaNotas.Add(new NotaFiscal(
                            tipoNota,
                            int.TryParse(numeroNota, out int n) ? n : 0,
                            chaveNota,
                            DateTime.TryParse(dataEmissao, out DateTime data) ? data : DateTime.MinValue,
                            cnpjEmitente,
                            nomeEmitente,
                            double.TryParse(valorNota, out double v) ? v : 0
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar o arquivo {arquivo}: {ex.Message}");
            }
        }

        // Salvar as notas fiscais no banco
        /*if (listaNotas.Any())
        {
            _context.NotasFiscais.AddRange(listaNotas);
            await _context.SaveChangesAsync();
        }*/
    }
}
