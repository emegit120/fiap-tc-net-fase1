# FIAP Cloud Games (FCG) - Request for Comments

## RFC-001: Implementação de Autenticação JWT

### Status
- **Autor**: Equipe de Desenvolvimento
- **Data**: [Data Atual]
- **Status**: Proposta
- **Versão**: 1.0

### Contexto
A plataforma FCG necessita de um sistema de autenticação seguro e escalável para gerenciar o acesso dos usuários.

### Proposta
Implementar autenticação usando JWT (JSON Web Tokens) com as seguintes características:

#### Estrutura do Token
```json
{
  "sub": "user_id",
  "email": "user@email.com",
  "role": "user|admin",
  "iat": 1516239022,
  "exp": 1516242622
}
```

#### Endpoints
```
POST /api/auth/login
POST /api/auth/refresh
POST /api/auth/logout
```

### Impacto
- Necessidade de implementar middleware de autenticação
- Atualização da documentação Swagger
- Implementação de testes de integração

## RFC-002: Persistência de Dados com Entity Framework Core

### Status
- **Autor**: Equipe de Desenvolvimento
- **Data**: [Data Atual]
- **Status**: Proposta
- **Versão**: 1.0

### Contexto
Necessidade de definir a estratégia de persistência de dados para o MVP.

### Proposta
Utilizar Entity Framework Core com SQL Server como banco de dados principal:

#### Modelos Principais
```csharp
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public UserRole Role { get; set; }
}

public class Game
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}
```

### Impacto
- Criação de migrations
- Configuração do contexto do EF
- Implementação de repositórios

## RFC-003: Implementação de Testes Automatizados

### Status
- **Autor**: Equipe de Desenvolvimento
- **Data**: [Data Atual]
- **Status**: Proposta
- **Versão**: 1.0

### Contexto
Garantir a qualidade do código e facilitar a manutenção.

### Proposta
Implementar testes usando xUnit com as seguintes categorias:

#### Testes Unitários
- Validações de domínio
- Regras de negócio
- Serviços

#### Testes de Integração
- Controllers
- Repositórios
- Autenticação

### Impacto
- Configuração do ambiente de testes
- Criação de mocks e fakes
- Integração com CI/CD

## RFC-004: Documentação com Swagger

### Status
- **Autor**: Equipe de Desenvolvimento
- **Data**: [Data Atual]
- **Status**: Proposta
- **Versão**: 1.0

### Contexto
Necessidade de documentar a API para facilitar o consumo.

### Proposta
Implementar Swagger/OpenAPI com as seguintes características:

#### Configuração
```csharp
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "FCG API", 
        Version = "v1" 
    });
});
```

#### Documentação
- Descrição detalhada dos endpoints
- Exemplos de requisição/resposta
- Autenticação JWT

### Impacto
- Configuração do Swagger
- Documentação dos endpoints
- Exemplos de uso 