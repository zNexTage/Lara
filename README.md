# LARA - Biblioteca Virtual

API para gerenciamento de uma biblioteca utilizando os conceitos de DDD 
(Domain Driven Design).

Desafio proposto pelo pessoal do [DevChallenge](https://github.com/devchallenge-io/biblioteca-backend)

![image](https://github.com/zNexTage/LaraBibliotecaVirtual/assets/42078139/60e6aad9-4af7-4400-b1b7-fbacca967c57)

# Tecnologias utilizadas

- .NET Core (C#);
- Entity Framework;
- MySQL;

# Configurações necessárias

## Definindo variáveis de ambiente
- Dentro do projeto/diretório `Lara.Application.API` crie o arquivo
`.env`;
### Variáveis de ambiente para banco de dados
- Acesse o arquivo .env e defina as seguintes variáveis de ambiente:
  ```
    LARA_DB_USER_ID=postgres
    LARA_DB_PASSWORD=postgres
    LARA_DB_HOST=localhost
    LARA_DB_PORT=5432
    LARA_DB_NAME=LaraDB
  ```
  - Você pode alterar os valores das variáveis de acordo com a sua necessidade.

### Variáveis de ambiente para email
- Acesse o arquivo .env e defina as seguintes variáveis de ambiente:
```
  LARA_EMAIL=COLOQUE_AQUI_O_EMAIL
  LARA_EMAIL_PASSWORD=COLOQUE_AQUI_A_SENHA
  LARA_EMAIL_HOST=COLOQUE_AQUI_O_HOST
  LARA_EMAIL_PORT=COLOQUE_AQUI_O_PORT
```
- como sugestão, você pode utilizar o https://ethereal.email/ para obter um email para testes.

## Criando as tabelas no banco de dados
- Rode o comando: `dotnet ef database update`;

# Endpoints da API

A seguir, será apresetando os endpoints conteplados pela API. 

Mais informações podem ser encontradas no Swagger: https://localhost:7271/swagger/index.html

## Books (Livros)

| Verbo HTTP | URL | Descrição | Parâmetro | Autenticação |
| ---------- | --- | --------- | --------- | ------------ |
| GET | /api/book | Retorna a lista dos livros cadastrados | Não recebe nenhum parâmetro | Não necessita de autenticação. |
| GET | /api/book/{id} | Retorna um livro utilizando se id | {id} -> Id do livro a ser buscado | Não necessita de autenticação |
| DELETE | /api/book/{id} | Remove um livro pelo seu id | {id} -> Id do livro a ser removido. Deve-se informar via parâmetro de caminho (path param) | Necessário estar autenticado. Deve-se acrescentar um token JWT no cabeçalho da requisição. |
| POST | /api/book | Registra um livro na base de dados. O livro cadastrado ficará como disponível para empréstimo e venda. | Deve-se informar o título, imagem, editora, autores, quantidade e preço | Necessário estar autenticado. Deve-se acrescentar um token JWT no cabeçalho da requisição. |
| PUT | /api/book/{id} | Atualiza as informações de um determinado livro. | Deve-se informar o título, imagem, editora, autores, quantidade e preço no corpo da requisição. Além disso, deve-se informar via parâmetro de caminho (path param) o id do livro. | Necessário estar autenticado. Deve-se acrescentar um token JWT no cabeçalho da requisição. |

##  Borrowed (Empréstimos)

| Verbo HTTP | URL | Descrição | Parâmetro | Autenticação |
| ---------- | --- | --------- | --------- | ------------ |
| POST | /api/Borrowed | Possibilita que obter um livro emprestado. Um email é enviado ao usuário ao obter um livro emprestado. | Deve ser informado o id do livro a ser emprestado no corpo da requisição | Necessário estar autenticado. Deve-se acrescentar um token JWT no cabeçalho da requisição. |

## User (Usuários)
| Verbo HTTP | URL | Descrição | Parâmetro | Autenticação |
| ---------- | --- | --------- | --------- | ------------ |
| POST | /api/User | Registra um usuário na base de dados | Deve ser informado o primeiro, segundo nome, email e a senha | Não necessita de autenticação |
| POST | /api/User/Login | Autentica o usuário gerando um token JWT. | Deve ser informado o email e senha no corpo da requisição | Não necessita de autenticação. |
