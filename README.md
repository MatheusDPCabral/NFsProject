# Projeto de leitor de Notas Fiscais

## Descrição
Sistema desenvolvido com:

- **Back-end - Visual Studio**: C#/.NET, ASP.NET
- **Front-end - VS Code**: Vue.js, Tailwind CSS
- **Banco de Dados**: SQL Server

Este sistema permite salvar dados de notas fiscais em um banco de dados a partir de arquivos XML e exibi-los no formato de uma tabela em uma interface web.

## Configuração do Projeto

### Pré-requisitos

**Banco de Dados**: Ter um banco de dados SQL Server configurado.

### Configuração Inicial

#### API (Back-end)
1. Navegue até a pasta da API.
2. Abra o arquivo `appsettings.json` e configure a string de conexão com os dados do seu banco de dados.
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "[Sua String de Conexão Aqui]"
   }
   ```
3. No arquivo `NotaFiscalController.cs`, ajuste o caminho dentro do método `HttpPost` para apontar para o diretório da pasta XML no seu desktop.
4. Ainda no `NotaFiscalController.cs`, dentro do metodo de deletar todas as notas, troque o `dbo.NotaFisca` pelo nome de sua tabela do banco de dados do projeto

## Executando o Projeto

### 1. Inicializar a API
- No Visual Studio avegue até a pasta do back-end e aperte o botão de "Run" ou aperte Ctrl+F5 (após fazer as alterações ditas acima).

- A API será iniciada e abrirá o **Swagger** (ou use outra ferramenta de requisições, como Postman) e, após isso, as notas fiscais estarão disponiveis na tabela para visualização.

### 2. Configurar e Enviar Dados para o Banco de Dados
1. Dentro da sua ferramenta de requisição, faça uma requisição **POST** na API para enviar os dados dos arquivos XML para o banco de dados.
2. **Atenção**: Ao executar o POST, todos os arquivos XML do diretório configurado serão carregados no banco. Caso a requisição seja repetida, os dados serão duplicados. Se necessário, utilize um **DELETE** request para limpar os dados antes de executar o POST novamente.

### 3. Inicializar o Front-end
- No VS Code, baixe a extensão `Live Server`, depois navegue até a pasta do front-end e depois aperte em "Go Live".
 
- O front-end será iniciado e irá abrir no seu navegador principal automaticamente.

## Observações Importantes

- Certifique-se de que a API esteja em execução antes de inicializar o front-end.
- Caso ocorra duplicação de dados, utilize um DELETE request na API para corrigir.
