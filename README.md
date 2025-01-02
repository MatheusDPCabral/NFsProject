# Projeto: Sistema de Notas Fiscais

## Descrição
Sistema desenvolvido com as seguintes tecnologias:

- **Back-end**: C#/.NET
- **Front-end**: Vue.js, Tailwind CSS
- **Banco de Dados**: SQL Server

Este sistema permite salvar dados de notas fiscais em um banco de dados a partir de arquivos XML e exibi-los em uma interface web responsiva.

## Configuração do Projeto

### Pré-requisitos

1. **Banco de Dados**: Certifique-se de ter um banco de dados SQL Server configurado.
2. **Dependências**: Certifique-se de ter as dependências necessárias instaladas:
   - .NET SDK
   - Node.js (para o front-end)
   - Gerenciador de pacotes npm ou yarn

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

#### Front-end
1. Navegue até a pasta do front-end.
2. Instale as dependências necessárias:
   ```bash
   npm install
   ```

## Executando o Projeto

### 1. Inicializar a API
- Navegue até a pasta do back-end e execute o comando:
  ```bash
  dotnet run
  ```
- A API será iniciada e abrirá o **Swagger** (ou use outra ferramenta de requisições, como Postman).

### 2. Configurar e Enviar Dados para o Banco de Dados
1. Faça uma requisição **POST** na API para enviar os dados dos arquivos XML para o banco de dados.
2. **Atenção**: Ao executar o POST, todos os arquivos XML do diretório configurado serão carregados no banco. Caso a requisição seja repetida, os dados serão duplicados. Se necessário, utilize um **DELETE** request para limpar os dados antes de executar o POST novamente.

### 3. Inicializar o Front-end
- Navegue até a pasta do front-end e execute o comando:
  ```bash
  npm run dev
  ```
- O front-end será iniciado e estará disponível no navegador no endereço indicado.

## Observações Importantes

- Certifique-se de que a API esteja em execução antes de inicializar o front-end.
- Caso ocorra duplicação de dados, utilize um DELETE request na API para corrigir.

## Contato
Para dúvidas ou sugestões, entre em contato através do meu [portfólio](#).
