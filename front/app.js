const { createApp } = Vue;

createApp({
  data() {
    return {
      notas: [], // Dados da API
    };
  },
  methods: {
    // pegar todas as notas do banco de dados
    async fetchNotes() {
      try {
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
    },

    // deletar notas individualmente
    async deleteNote(id) {
      try {
        const response = await fetch(`https://localhost:7096/NotaFiscal/${id}`, {
          method: 'DELETE',
          headers: {
            'Content-Type': 'application/json',
          },
        });

        if (!response.ok) {
          throw new Error(`Erro HTTP! Status: ${response.status}`);
        }

        alert('Nota excluída com sucesso!');
        // Atualiza os dados após a exclusão
        this.fetchNotes();
      } catch (error) {
        console.error(`Erro ao excluir a nota com ID ${id}:`, error);
        alert('Erro ao excluir a nota. Tente novamente.');
      }
    },

    // deletar todas notas
    async deleteAllNotes() {
      try {
        const response = await fetch(`https://localhost:7096/NotaFiscal/`, {
          method: 'DELETE',
          headers: {
            'Content-Type': 'application/json',
          },
        });

        if (!response.ok) {
          throw new Error(`Erro HTTP! Status: ${response.status}`);
        }
        // Atualiza os dados após a exclusão
        this.fetchNotes();
      } catch (error) {
        console.error(`Erro ao excluir as notas:`, error);
        alert('Erro ao excluir as notas. Tente novamente.');
      }
    },

    //extrair notas e salvar no banco de dados novamente
    async extractAllNotes(){
      try {
        const response = await fetch(`https://localhost:7096/NotaFiscal/processar-xmls`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
        });

        if (!response.ok) {
          throw new Error(`Erro HTTP! Status: ${response.status}`);
        }
        // Atualiza os dados após a exclusão
        this.fetchNotes();
      }
      catch (error) {
        console.error(`Erro ao extrair as notas:`, error);
        alert('Erro ao extrair as nota. Tente novamente.');
      }
    },

    // alimentar
    populateDataTable() {
      $('#notasTable').DataTable({
        data: this.notas, // Use os dados retornados pela API
        columns: [
          { data: 'id', title: 'ID' },
          { data: 'tipo', title: 'Tipo' },
          { data: 'numeroNota', title: 'Número da Nota' },
          { data: 'chaveNota', title: 'Chave da Nota' },
          { data: 'cnpjEmitente', title: 'CNPJ Emitente' },
          { data: 'nomeEmitente', title: 'Nome do Emitente' },
          { data: 'valorNota', title: 'Valor' },
          { data: 'dataEmissao', title: 'Data de Emissão' },
          {
            data: null, // Essa coluna não precisa de um dado específico
            title: 'Deletar',
            render: (data, type, row) => {
              return `
                <button class="btn-excluir w-2/3 font-sans text-stone-500" data-id="${row.id}">
                  <i class="fas fa-trash-alt"></i>
                </button>
              `;
            },
          },
        ],
        columnDefs: [
          {
            targets: 6,
            render: function (data) {
              return `R$ ${parseFloat(data).toFixed(2)}`;
            },
          },
          {
            targets: 7,
            render: function (data) {
              const date = new Date(data);
              return date.toLocaleDateString();
            },
          },
          {
            targets: 4,
            render: function (data) {
              return data.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})$/, '$1.$2.$3/$4-$5');
            },
          },
        ],
        destroy: true, // Adicionado para recriar a tabela ao recarregar os dados
      });
    },
  },
  mounted() {
    this.fetchNotes(); // Chama ao carregar a aplicação

    // Adiciona o Event Listener para excluir
    $(document).on('click', '.btn-excluir', (event) => {
      const id = $(event.currentTarget).data('id'); // Pega o ID do botão clicado
      if (confirm('Tem certeza que deseja excluir esta nota?')) {
        this.deleteNote(id); // Chama o método para deletar a nota
      }
    });
  },
}).mount('#app');
