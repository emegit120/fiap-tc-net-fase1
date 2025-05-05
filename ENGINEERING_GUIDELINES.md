# FIAP Cloud Games (FCG) - Engineering Guidelines

## Padrões de Código

### Nomenclatura
- **Classes**: PascalCase
- **Métodos**: PascalCase
- **Variáveis**: camelCase
- **Constantes**: UPPER_CASE
- **Interfaces**: I + PascalCase
- **Enums**: PascalCase

### Organização
- Um arquivo por classe
- Namespaces seguindo a estrutura de pastas
- Usar partial classes quando necessário
- Agrupar membros relacionados

### Comentários
- Documentar APIs públicas
- Usar XML comments para documentação
- Evitar comentários óbvios
- Manter comentários atualizados

## Padrões de Arquitetura

### Clean Architecture
- Separar em camadas (Domain, Application, Infrastructure, Presentation)
- Dependências apontando para dentro
- Inversão de dependência
- Interfaces no domínio

### SOLID
- Single Responsibility Principle
- Open/Closed Principle
- Liskov Substitution Principle
- Interface Segregation Principle
- Dependency Inversion Principle

## Padrões de Teste

### Testes Unitários
- Nome do teste: [Método]_[Cenário]_[ResultadoEsperado]
- Usar AAA (Arrange, Act, Assert)
- Um assert por teste
- Mockar dependências

### Testes de Integração
- Testar fluxos completos
- Usar banco de dados de teste
- Limpar dados após testes
- Testar cenários de erro

## Segurança

### Autenticação
- Usar JWT com expiração
- Implementar refresh tokens
- Validar tokens em todas as requisições
- Usar HTTPS

### Autorização
- Implementar roles
- Validar permissões
- Usar atributos de autorização
- Logar tentativas de acesso

### Dados
- Criptografar senhas
- Sanitizar inputs
- Validar dados
- Usar parâmetros em queries

## Performance

### Código
- Usar async/await
- Implementar cache
- Otimizar queries
- Usar paginação

### API
- Implementar rate limiting
- Usar compression
- Implementar cache
- Monitorar performance

## Logging

### Estrutura
- Usar ILogger
- Logar exceções
- Logar operações críticas
- Usar níveis apropriados

### Formato
- JSON estruturado
- Incluir correlation ID
- Incluir timestamp
- Incluir contexto

## Versionamento

### Git
- Usar feature branches
- Commits atômicos
- Mensagens descritivas
- Pull requests

### API
- Versionar endpoints
- Documentar mudanças
- Manter compatibilidade
- Deprecar versões antigas

## CI/CD

### Build
- Compilar projeto
- Rodar testes
- Gerar documentação
- Publicar pacotes

### Deploy
- Ambiente de desenvolvimento
- Ambiente de homologação
- Ambiente de produção
- Rollback automático

## Monitoramento

### Métricas
- Tempo de resposta
- Taxa de erro
- Uso de recursos
- Disponibilidade

### Alertas
- Definir thresholds
- Configurar notificações
- Documentar procedimentos
- Testar alertas 