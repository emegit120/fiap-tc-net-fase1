# FIAP Cloud Games (FCG)

## Visão Geral

A **FIAP Cloud Games (FCG)** é uma plataforma de venda de jogos digitais e gestão de servidores para partidas online, com foco em educação tecnológica. Este projeto foi desenvolvido como parte do Tech Challenge FIAP, utilizando .NET 8, arquitetura monolítica e boas práticas de engenharia de software.

---

## Objetivos

- Oferecer uma plataforma robusta para alunos da FIAP, Alura e PM3.
- Permitir cadastro, autenticação e gerenciamento de usuários.
- Gerenciar biblioteca de jogos adquiridos.
- Permitir administração de jogos e promoções por usuários administradores.
- Garantir qualidade, segurança e escalabilidade para futuras funcionalidades.

---

## Funcionalidades Principais

- **Cadastro de Usuários:** Nome, e-mail e senha segura (mínimo 8 caracteres, números, letras e caracteres especiais).
- **Autenticação JWT:** Login seguro, geração e validação de tokens.
- **Níveis de Acesso:** Usuário (acesso à biblioteca) e Administrador (cadastro de jogos, usuários e promoções).
- **Gestão de Jogos:** Cadastro, edição, remoção e visualização de jogos.
- **Acesso a biblioteca de Jogos:** Gerenciamento dos jogos adquiridos pelo usuário.
- **Promoções:** Criação e gerenciamento de promoções (admin).
- **Logs Estruturados:** Middleware para tratamento de erros e logs com Serilog.
- **Documentação da API:** Swagger disponível para consulta e testes.

---

## Tecnologias Utilizadas

- **.NET 8**
- **Entity Framework Core** (persistência de dados)
- **SQL Server** (banco de dados relacional)
- **JWT** (autenticação)
- **Serilog** (logging)
- **Swagger** (documentação de API)
- **Clean Architecture & DDD** (organização do código)

---

## Como Executar o Projeto

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- Editor de código (Visual Studio 2022 recomendado)

### Configuração

1. **Clone o repositório:**
   git clone https://github.com/seu-usuario/fiap-cloud-games.git cd fiap-cloud-games

   
2. **Configure a string de conexão:**
   - Edite o arquivo `appsettings.json` com os dados do seu SQL Server:
 "ConnectionStrings": {
   "DefaultConnection": "Server=SEU_SERVIDOR;Database=FIAPTechChallengeDb;User Id=USUARIO;Password=SENHA;TrustServerCertificate=True"
 }
 
   
3. **Rode as migrations para criar o banco de dados:**
   dotnet ef database update

4. **Execute o projeto:**
   dotnet run

   Ou pressione F5 no Visual Studio.

5. **Acesse a documentação da API:**
   - Abra o navegador em: [https://localhost:5001/swagger](https://localhost:5001/swagger) (ou porta configurada)

---

## Endpoints Principais

Acesse o Swagger para ver todos os endpoints e testar as funcionalidades.  
Rotas para iniciar:

- `POST /api/auth/login` – Login e obtenção do JWT
- `POST /api/User` – Cadastro de usuário (perfil usuário)

> **Obs:** Cadastro de perfil Administrador somente via banco de dados.
> **Obs:** Endpoints protegidos exigem o envio do token JWT no header `Authorization: Bearer {token}`.

---

## Usuários e Perfis

- **Usuário:** Pode acessar alguns endpoints de GET.
- **Administrador:** Pode cadastrar/editar/remover jogos, gerenciar usuários e criar promoções.

---

## Padrões de Engenharia

- **Padrões de Código:** PascalCase para classes/métodos, camelCase para variáveis, interfaces com prefixo `I`.
- **Arquitetura:** Separação em camadas (Domain, Application, Infrastructure, Presentation).
- **Testes:** Testes unitários e de integração, seguindo AAA e TDD/BDD em módulos.
- **Segurança:** JWT, roles, criptografia de senha, validação de dados, HTTPS obrigatório.
- **Logging:** Serilog, logs estruturados, tratamento de exceções via middleware.

---

## Critérios de Aceitação

- Todas as funcionalidades testadas.
- Documentação atualizada.
- Código seguindo padrões definidos.
- Performance e segurança em conformidade com as melhores práticas.

---

## User Stories

### Como um novo usuário
- Quero me cadastrar na plataforma para ter acesso aos jogos educacionais, gerenciar minha biblioteca e participar de partidas online.
- Quero fazer login para acessar minha conta, ver meus jogos e gerenciar meu perfil.

### Como um usuário regular
- Quero ver a lista de jogos disponíveis, detalhes e requisitos.
- Quero gerenciar minha biblioteca de jogos.
- Quero ver promoções ativas.
- Quero gerenciar meu perfil e histórico de compras.

### Como um administrador
- Quero cadastrar novos jogos, definir preços e categorias.
- Quero gerenciar usuários e criar promoções.

### Como um desenvolvedor
- Quero ter uma API bem documentada e logs estruturados.

### Como um testador
- Quero ter testes automatizados para garantir a qualidade.

---

## Requisitos Funcionais

- Cadastro e autenticação de usuários (com validação de e-mail e senha forte).
- Gestão de jogos (CRUD para administradores).
- GET de jogos e promoções para usuários.
- Criação e gerenciamento de promoções (admin).
- Logs estruturados e tratamento de erros.
- Documentação da API via Swagger.

---

## Entregáveis da Fase 1

- Código-fonte versionado e documentado.
- API RESTful funcional.
- Testes unitários.
- Documentação DDD (Event Storming, diagramas).
- README.md completo (este arquivo).
- Relatório de entrega com links do grupo, documentação, repositório e vídeo.

---

 Nome do grupo
 - Grupo 81

○ Participantes e usernames no Discord.
 - emerson1446 
 - Emerson (Dev)

Drive
https://drive.google.com/drive/folders/1aWhEohEN1jrx0Us3lN1ivedb1SbVVxTo?usp=sharing

Link da documentação.
https://www.figma.com/board/1s1MvlkKbtpnq9UmfFQfv3/DDD---Event-Storming?node-id=0-1&p=f

GITHUB
https://github.com/emegit120/fiap-tc-net-fase1

Video


## Licença

Este projeto é apenas para fins educacionais no contexto do Tech Challenge FIAP.
