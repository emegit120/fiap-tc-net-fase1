# FIAP Cloud Games (FCG) - Technical Reference Document

## Stack Tecnológica

### Backend
- **Framework**: .NET 8
- **Arquitetura**: Monolítica (MVP)
- **Padrão de API**: REST (Minimal API ou Controllers MVC)
- **Documentação**: Swagger/OpenAPI

### Persistência de Dados
- **ORM Principal**: Entity Framework Core
- **Banco de Dados**: SQL Server (padrão)
- **Opcional**: MongoDB para casos específicos
- **Opcional**: Dapper para consultas de alta performance

### Autenticação e Segurança
- **Método**: JWT (JSON Web Tokens)
- **Níveis de Acesso**: Usuário e Administrador
- **Validações**: 
  - Formato de e-mail
  - Senha segura (mínimo 8 caracteres)

### Testes
- **Framework**: xUnit/NUnit
- **Metodologias**: 
  - TDD (Test-Driven Development)
  - BDD (Behavior-Driven Development)

## Endpoints da API

### Usuários
```
POST /api/users - Cadastro de usuário
GET /api/users/{id} - Obter usuário
PUT /api/users/{id} - Atualizar usuário
DELETE /api/users/{id} - Remover usuário
```

### Autenticação
```
POST /api/auth/login - Login
POST /api/auth/refresh - Refresh token
```

### Jogos
```
GET /api/games - Listar jogos
GET /api/games/{id} - Obter jogo
POST /api/games - Cadastrar jogo (Admin)
PUT /api/games/{id} - Atualizar jogo (Admin)
DELETE /api/games/{id} - Remover jogo (Admin)
```

## Integrações Futuras
- Sistema de pagamentos
- Serviços de matchmaking
- Gerenciamento de servidores
- Integração com sistemas educacionais

## Requisitos de Sistema
- .NET 8 SDK
- SQL Server ou MongoDB
- Visual Studio 2022 ou VS Code
- Git para controle de versão

## Convenções de Código
- C# 12
- Clean Code
- SOLID Principles
- Domain-Driven Design (DDD) 