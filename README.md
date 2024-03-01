# LARA - Biblioteca Virtual

API para gerenciamento de uma biblioteca utilizando os conceitos de DDD 
(Domain Driven Design).

Desafio proposto pelo pessoal do [DevChallenge](https://github.com/devchallenge-io/biblioteca-backend)

# Configurações necessárias

## Definindo variáveis de ambiente
- Dentro do projeto/diretório `Lara.Application.API` crie o arquivo
`.env`;
- Acesse o arquivo .env e defina as seguintes variáveis de ambiente:
  ```
    LARA_DB_USER_ID=postgres
    LARA_DB_PASSWORD=postgres
    LARA_DB_HOST=localhost
    LARA_DB_PORT=5432
    LARA_DB_NAME=LaraDB
  ```
  - Você pode alterar os valores das variáveis de acordo com a sua necessidade.

## Criando as tabelas no banco de dados
- Rode o comando: `dotnet ef database update`;