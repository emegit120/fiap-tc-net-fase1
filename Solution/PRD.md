# FIAP Cloud Games (FCG) - Product Requirements Document

## Visão Geral
A FIAP Cloud Games (FCG) é uma plataforma de venda de jogos digitais e gestão de servidores para partidas online, focada na educação de tecnologia. O projeto será desenvolvido em quatro fases, com lançamento gradual para melhorar continuamente o serviço.

## Objetivo
Criar uma plataforma robusta que suporte todos os alunos e alunas da FIAP, Alura e PM3, oferecendo uma experiência completa de games educacionais.

## Público-Alvo
- Alunos da FIAP
- Alunos da Alura
- Alunos do PM3

## Requisitos de Alto Nível

### Fase 1 (MVP)
1. **Cadastro de Usuários**
   - Sistema de identificação com nome, e-mail e senha
   - Validação de formato de e-mail
   - Senha segura (mínimo 8 caracteres, números, letras e caracteres especiais)

2. **Autenticação e Autorização**
   - Autenticação via token JWT
   - Dois níveis de acesso:
     - Usuário: acesso à plataforma e biblioteca de jogos
     - Administrador: cadastro de jogos, administração de usuários e criação de promoções

3. **Biblioteca de Jogos**
   - Gerenciamento de jogos adquiridos
   - Base para futuras funcionalidades (matchmaking e gerenciamento de servidores)

### Próximas Fases (Planejadas)
- Matchmaking
- Gerenciamento de servidores
- Funcionalidades adicionais de gamificação
- Integrações com sistemas educacionais

## Métricas de Sucesso
- Número de usuários ativos
- Quantidade de jogos adquiridos
- Tempo médio de uso da plataforma
- Satisfação dos usuários

## Restrições
- Desenvolvimento em .NET 8
- Arquitetura monolítica para MVP
- Persistência de dados com Entity Framework Core
- Documentação completa com Swagger 