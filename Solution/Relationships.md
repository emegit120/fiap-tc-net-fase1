
## Entidades e Relacionamentos

### User
- **Propriedades:** Id, Name, Email, Password, RoleId, CreatedAt, UpdatedAt
- **Relacionamentos:**
  - **1:N com Game:** Um usuário pode possuir vários jogos (`User.Games`).  
    - Configurado com `.HasMany(u => u.Games).WithOne().OnDelete(DeleteBehavior.Cascade);`
  - **1:1 com Role:** Cada usuário pertence a um papel (Role).
  - **Restrição:** Email único.

### Game
- **Propriedades:** Id, Name, Description, Price, CreatedAt, UpdatedAt
- **Relacionamentos:**
  - **1:N com User:** Um jogo pode pertencer a vários usuários (biblioteca do usuário).
  - **N:M com Category:** Um jogo pode ter várias categorias e uma categoria pode ter vários jogos.
    - Configurado com `.HasMany(g => g.Categories).WithMany(c => c.Games);`
  - **N:M com Promotion:** Um jogo pode estar em várias promoções e uma promoção pode abranger vários jogos.
    - Configurado com `.HasMany(p => p.Games).WithMany();` em Promotion.

### Category
- **Propriedades:** Id, Name
- **Relacionamentos:**
  - **N:M com Game:** uma categoria pode ter vários jogos e jogo pode ter várias categorias

### Promotion
- **Propriedades:** Id, Name, DiscountPercentage, StartDate, EndDate
- **Relacionamentos:**
  - **N:M com Game:** Uma promoção pode abranger vários jogos e um jogo pode estar em várias promoções
  - **Critério:** Sistema escolhe a promoção com maior desconto e aplica ao jogo.
  
### Role
- **Propriedades:** Id, Name
- **Relacionamentos:**
  - **1:N com User:** Um papel pode ser atribuído a vários usuários.

---

## Resumo dos Relacionamentos

- **User 1:N Game:** Um usuário pode ter vários jogos em sua biblioteca.
- **Game N:M Category:** Jogos podem pertencer a várias categorias e vice-versa.
- **Promotion N:M Game:** Promoções podem abranger vários jogos e um jogo pode estar em várias promoções.
- **User N:1 Role:** Cada usuário tem um papel (User/Admin), mas um papel pode ser atribuído a vários usuários.

---

> **Observação:**  
> Os relacionamentos N:M são implementados via coleções do Entity Framework Core, que cria tabelas de junção automaticamente.

**Diagrama de Entidade-Relacionamento (ERD):**
```
┌─────────────┐       ┌─────────────┐       ┌─────────────┐
│    Role     │       │    User     │       │    Game     │
├─────────────┤       ├─────────────┤       ├─────────────┤
│ PK Id       │<───┐  │ PK Id       │ ◄───┐ │ PK Id       │
│    Name     │    │  │    Name     │     │ │    Name     │
└─────────────┘    │  │    Email    │     │ │ Description │
                   │  │    Password │     │ │    Price    │
                   └──┤ FK RoleId   │     │ │ CreatedAt   │
                      │ CreatedAt   │     │ │ UpdatedAt   │
                      │ UpdatedAt   │     │ └─────────────┘
                      └─────────────┘     │        ▲  ▲
                           ▲              │        │  │
                           │              └────────┘  │
                      ┌────┴─────┐                   │
                      │ UserGame │                   │
                      ├──────────┤              ┌─────┴─────┐
                      │ FK UserId├──────────────┤ FK GameId │
                      │ FK GameId│              └──────────┘
                      └──────────┘                   ▲
                                                     │
┌─────────────┐       ┌─────────────┐                │
│  Category   │       │ Promotion   │                │
├─────────────┤       ├─────────────┤                │
│ PK Id       │       │ PK Id       │                │
│    Name     │       │    Name     │                │
└─────────────┘       │ Discount%   │                │
    ▲   ▲             │ StartDate   │                │
    │   │             │ EndDate     │                │
    │   │             └─────────────┘                │
    │   │                   ▲                        │
    │   └───────────────────┼────────────────────────┘
    │                       │
    └───────────────────────┘