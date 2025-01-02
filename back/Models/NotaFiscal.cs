using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiNotaFiscal.Models
{
    public enum TipoNota
    {
        NFe,
        CTe,
        NFCe,
        CFe
    }

    [Table("NotaFiscal")]
    public class NotaFiscal
    {
        [Key]
        public int Id { get; set; } // Chave primária para o EF

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public TipoNota Tipo { get; set; }

        [Required]
        public int NumeroNota { get; set; }

        [Required]
        [MaxLength(44)]
        public string ChaveNota { get; set; } // Propriedade pública com get/set

        [MaxLength(18)]
        public string CnpjEmitente { get; set; } // Propriedade pública com get/set

        [MaxLength(100)]
        public string NomeEmitente { get; set; } // Propriedade pública com get/set

        public double ValorNota { get; set; } // Propriedade pública com get/set

        public DateTime DataEmissao { get; set; } // Propriedade pública com get/set

        // Construtor padrão (necessário para o EF)
        public NotaFiscal() { }

        // Construtor parametrizado para uso no código
        public NotaFiscal(TipoNota tipo, int numeroNota, string chave, DateTime data, string cnpj, string nome, double valor)
        {
            Tipo = tipo;
            NumeroNota = numeroNota;
            ChaveNota = chave;
            DataEmissao = data;
            CnpjEmitente = cnpj;
            NomeEmitente = nome;
            ValorNota = valor;
        }
    }
}
