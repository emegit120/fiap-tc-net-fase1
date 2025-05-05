# FIAP Cloud Games (FCG) - Functional Requirements Document

## 1. Gestão de Usuários

### 1.1 Cadastro de Usuário
- **Funcionalidade**: Permitir que novos usuários se cadastrem na plataforma
- **Requisitos**:
  - Nome completo (obrigatório)
  - E-mail válido (obrigatório, formato válido)
  - Senha segura (obrigatório, mínimo 8 caracteres)
  - Confirmação de senha
- **Validações**:
  - E-mail único no sistema
  - Formato de e-mail válido
  - Senha com números, letras e caracteres especiais
- **Regras de Negócio**:
  - Usuário inicia com perfil básico
  - E-mail deve ser confirmado

### 1.2 Autenticação
- **Funcionalidade**: Login e gerenciamento de sessão
- **Requisitos**:
  - Login com e-mail e senha
  - Geração de token JWT
  - Refresh token
  - Logout
- **Regras de Negócio**:
  - Token expira em 1 hora
  - Refresh token válido por 7 dias

## 2. Gestão de Jogos

### 2.1 Cadastro de Jogos (Admin)
- **Funcionalidade**: Permitir que administradores cadastrem novos jogos
- **Requisitos**:
  - Nome do jogo
  - Descrição
  - Categoria
  - Preço
  - Requisitos do sistema
- **Validações**:
  - Nome único no sistema
  - Preço positivo
- **Regras de Negócio**:
  - Apenas administradores podem cadastrar
  - Jogos podem ser editados/removidos

### 2.2 Biblioteca de Jogos
- **Funcionalidade**: Gerenciar jogos adquiridos pelos usuários
- **Requisitos**:
  - Listar jogos disponíveis
  - Visualizar detalhes do jogo
  - Adicionar à biblioteca
  - Remover da biblioteca
- **Regras de Negócio**:
  - Usuários podem ver todos os jogos
  - Apenas jogos adquiridos podem ser jogados

## 3. Gestão de Promoções (Admin)

### 3.1 Criação de Promoções
- **Funcionalidade**: Permitir que administradores criem promoções
- **Requisitos**:
  - Nome da promoção
  - Desconto
  - Período de validade
  - Jogos incluídos
- **Validações**:
  - Desconto entre 0 e 100%
  - Data de início anterior à data de fim
- **Regras de Negócio**:
  - Promoções podem ser editadas/removidas
  - Apenas administradores podem gerenciar

## 4. Interface do Usuário

### 4.1 Dashboard
- **Funcionalidade**: Exibir informações relevantes ao usuário
- **Requisitos**:
  - Lista de jogos adquiridos
  - Promoções ativas
  - Status da conta
- **Regras de Negócio**:
  - Diferentes visualizações para usuário e admin

### 4.2 Perfil do Usuário
- **Funcionalidade**: Gerenciar informações do usuário
- **Requisitos**:
  - Editar informações pessoais
  - Alterar senha
  - Visualizar histórico de compras
- **Regras de Negócio**:
  - Senha atual necessária para alterações
  - E-mail não pode ser alterado 