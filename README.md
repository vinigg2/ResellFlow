# 🚀 ResellFlow API

A ResellFlow API é uma solução para gerenciamento de revendas e seus pedidos. Ela permite que clientes enviem pedidos para suas respectivas revendas e, posteriormente, a revenda emita um pedido consolidado para um sistema externo.

---

## 🧱 Arquitetura

- ✅ Arquitetura Hexagonal (Ports & Adapters)
- ✅ Princípios SOLID
- ✅ Clean Code
- ✅ FluentValidation para validação
- ✅ Polly para resiliência
- ✅ Testes unitários com xUnit e Moq
- ✅ Injeção de Dependência nativa do .NET

---

## 🔁 Rotas da API

### 📦 Resellers

| Método | Rota                                                      | Descrição                                               |
|--------|-----------------------------------------------------------|----------------------------------------------------------|
| POST   | `/api/reseller`                                           | Cadastra uma nova revenda                               |
| GET    | `/api/reseller`                                           | Lista todas as revendas                                 |
| GET    | `/api/reseller/{id}`                                      | Busca uma revenda pelo ID                               |
| PUT    | `/api/reseller/{id}`                                      | Atualiza dados de uma revenda                           |
| DELETE | `/api/reseller/{id}`                                      | Remove uma revenda                                      |
| POST   | `/api/reseller/{clientIdentifier}/emitir-pedido`          | Emite o pedido consolidado para o sistema externo       |

### 🧾 Orders

| Método | Rota                    | Descrição                                |
|--------|-------------------------|-------------------------------------------|
| POST   | `/api/order`            | Cria um novo pedido                       |
| GET    | `/api/order`            | Lista todos os pedidos                    |
| GET    | `/api/order/{id}`       | Busca um pedido pelo ID                   |
| DELETE | `/api/order/{id}`       | Remove um pedido                          |

---

## 🧩 Casos de Uso

- `CreateOrderUseCase`
- `ListOrderUseCase`
- `DeleteOrderUseCase`
- `CreateResellerUseCase`
- `ListResellerUseCase`
- `UpdateResellerUseCase`
- `DeleteResellerUseCase`
- `EmitResellerOrderUseCase`

---

## 🧪 Testes

Para rodar os testes unitários:

```bash
dotnet test
```

Cobrem os principais fluxos da aplicação: criação, listagem, atualização, remoção e emissão de pedidos.

---

## 🐳 Executando com Docker

```bash
docker-compose up --build
```

Acesse via Swagger: [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

## 📂 Estrutura do Projeto

```
src/
├── ResellFlow.Api             # Controllers e Program.cs
├── ResellFlow.Application     # UseCases, DTOs, Validations
├── ResellFlow.Domain          # Entidades e Interfaces
├── ResellFlow.Infrastructure  # Repositórios e HTTP Clients
├── ResellFlow.Tests           # Testes unitários com xUnit e Moq
```

---

## 🧠 Observações

- Todos os repositórios são implementados in-memory (mockados)
- O sistema externo de pedidos é simulado com retry via Polly
- A rota `/emitir-pedido` exige que a soma das quantidades dos pedidos seja >= 1000

---
