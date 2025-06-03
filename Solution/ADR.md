# FIAP Cloud Games (FCG) - Architectural Decision Records

## ADR-001: Arquitetura Monolítica para MVP

### Status
- **Status**: Aceito
- **Contexto**: Necessidade de desenvolver rapidamente o MVP da plataforma FCG.

### Decisão
Adotar uma arquitetura monolítica para o MVP, utilizando .NET 8 com uma estrutura em camadas.

### Consequências
#### Positivas
- Desenvolvimento mais rápido
- Menor complexidade inicial
- Mais fácil de manter no início
- Facilita a implementação de mudanças

#### Negativas
- Pode se tornar complexo com o crescimento
- Menor escalabilidade horizontal
- Acoplamento entre componentes

## ADR-002: Domain-Driven Design (DDD)

### Status
- **Status**: Aceito
- **Contexto**: Necessidade de modelar o domínio do negócio de forma clara e organizada.

### Decisão
Implementar DDD com as seguintes camadas:
- Domain (Entidades, Value Objects, Agregados)
- Application (Serviços, DTOs)
- Infrastructure (Repositórios, Contexto)
- Presentation (Controllers, ViewModels)

### Consequências
#### Positivas
- Melhor organização do código
- Domínio mais claro e expressivo
- Facilita a manutenção
- Melhor separação de responsabilidades

#### Negativas
- Curva de aprendizado inicial
- Mais código boilerplate
- Pode ser complexo para casos simples

## ADR-003: Entity Framework Core como ORM Principal

### Status
- **Data**: [Data Atual]
- **Status**: Aceito
- **Contexto**: Necessidade de persistência de dados robusta e flexível.

### Decisão
Utilizar Entity Framework Core como ORM principal, com SQL Server como banco de dados.

### Consequências
#### Positivas
- Integração nativa com .NET
- Migrations para controle de versão do banco
- LINQ para consultas
- Suporte a múltiplos bancos de dados

#### Negativas
- Performance pode ser um problema em consultas complexas
- Curva de aprendizado para otimizações
- Pode gerar consultas ineficientes

## ADR-004: Autenticação JWT

### Status
- **Status**: Aceito
- **Contexto**: Necessidade de um sistema de autenticação seguro e escalável.

### Decisão
Implementar autenticação usando JWT com refresh tokens.

### Consequências
#### Positivas
- Stateless
- Escalável
- Fácil de implementar
- Suporte a múltiplos clientes

#### Negativas
- Tokens não podem ser invalidados
- Necessidade de gerenciar refresh tokens
- Segurança depende da implementação

## ADR-005: Testes Automatizados

### Status
- **Status**: Aceito
- **Contexto**: Necessidade de garantir qualidade e facilitar manutenção.

### Decisão
Implementar testes unitários e de integração usando xUnit.

### Consequências
#### Positivas
- Maior confiabilidade
- Facilita refatorações
- Documentação viva
- Reduz bugs

#### Negativas
- Aumenta tempo de desenvolvimento
- Necessidade de manter testes
- Pode criar falsa sensação de segurança 