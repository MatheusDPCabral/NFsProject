const { createApp } = Vue;

createApp({
  data() {
    return {
      notas: [], // Dados da API
    };
  },
  methods: {
    async fetchNotas() {
      try {                           // Trocar endpoint pelo localhost da sua maquina
        const response = await fetch('https://localhost:7096/NotaFiscal', {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        });

        if (!response.ok) {
          throw new Error(`Erro HTTP! Status: ${response.status}`);
        }

        this.notas = await response.json();
        this.populateDataTable();
      } catch (error) {
        console.error('Erro ao buscar notas:', error);
      }
    }
    ,
    populateDataTable() {
      $('#notasTable').DataTable({
        data: this.notas, // Use os dados retornados pela API
        columns: [
          { data: 'id', title: 'ID' },                         // preenche o campo "id"
          { data: 'tipo', title: 'Tipo' },                     // preenche o campo "tipo"
          { data: 'numeroNota', title: 'Número da Nota' },     // preenche o campo "numeroNota"
          { data: 'chaveNota', title: 'Chave da Nota' },       // preenche o campo "chaveNota"
          { data: 'cnpjEmitente', title: 'CNPJ Emitente' },    // preenche o campo "cnpjEmitente"
          { data: 'nomeEmitente', title: 'Nome do Emitente' }, // preenche o campo "nomeEmitente"
          { data: 'valorNota', title: 'Valor' },               // preenche o campo "valorNota"
          { data: 'dataEmissao', title: 'Data de Emissão' }    // preenche o campo "dataEmissao"
        ],
        columnDefs: [
          {
            // Formata a coluna "Valor" para exibir como moeda
            targets: 6, // Índice da coluna "Valor"
            render: function (data) {
              return `R$ ${parseFloat(data).toFixed(2)}`; // Exibe com duas casas decimais
            }
          },
          {
            // Formata a coluna "Data de Emissão" para exibir apenas a data
            targets: 7, // Índice da coluna "Data de Emissão"
            render: function (data) {
              const date = new Date(data);
              return date.toLocaleDateString(); // Exibe no formato DD/MM/YYYY
            }
          },
          {
            // Formata o CNPJ no padrão XX.XXX.XXX/XXXX-XX
            targets: 4, // Índice da coluna "CNPJ Emitente"
            render: function (data) {
              return data.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})$/, '$1.$2.$3/$4-$5');
            }
          },
        ]
      });
    }

  },
  mounted() {
    this.fetchNotas(); // Chama ao carregar a aplicação
  }
}).mount('#app');


