Projeto: Sistema de Reservas para Barbearia
Disciplina: Arquitetura de Aplicações Web — 2026.1
Este projeto consiste em uma plataforma de agendamentos desenvolvida com o objetivo de gerenciar recursos (profissionais) e reservas de horários de forma assíncrona. A aplicação utiliza uma arquitetura baseada em serviços no backend e uma interface SPA (Single Page Application) no frontend.

Domínio da Aplicação
O sistema foi modelado para o nicho de barbearias, permitindo que o administrador cadastre barbeiros e os clientes realizem agendamentos vinculados a esses profissionais. A estrutura é genérica o suficiente para ser adaptada a clínicas ou sistemas de reserva de salas.

Requisitos Técnicos Atendidos
API REST: Endpoints completos (CRUD) para as entidades de Recursos e Agendamentos.

Persistência: Integração nativa com MongoDB utilizando o driver oficial.

Documentação: Mapeamento de rotas e schemas via Swagger UI.

Frontend Assíncrono: Interface em React que consome a API via Fetch sem recarregamento de página (Navegação via estado).

Estrutura do Repositório
/backend: API desenvolvida em .NET 10.

/frontend: Aplicação Web desenvolvida com React e Vite.

Tecnologias Utilizadas
Backend: .NET 10 (C#)

Banco de Dados: MongoDB (NoSQL)

Frontend: React (JavaScript)

Interface da API: Swagger/OpenAPI

Instruções de Execução
Pré-requisitos
.NET 10 SDK

Node.js (v18+)

Instância local do MongoDB (porta 27017)

Rodando o Backend
Acesse a pasta do servidor: cd backend/SistemaReservas.Api

Restaure as dependências: dotnet restore

Inicie a aplicação: dotnet watch run

A documentação Swagger estará disponível em: http://localhost:5158/swagger

Rodando o Frontend
Acesse a pasta web: cd frontend

Instale os pacotes: npm install

Inicie o servidor de desenvolvimento: npm run dev

Acesse o sistema em: http://localhost:5173

Variáveis de Ambiente e Configuração
As configurações de banco de dados estão localizadas no arquivo appsettings.json do backend. Para conexão com o banco, certifique-se de que a ConnectionString aponta para o endereço correto do seu MongoDB local ou Atlas.

JSON
"MongoDbSettings": {
  "ConnectionString": "mongodb://localhost:27017",
  "DatabaseName": "ReservaDb"
}
Aplicação de Princípios SOLID
Conforme solicitado nos requisitos bônus, o projeto aplica os seguintes princípios:

SRP (Single Responsibility): Lógica de banco isolada em classes de serviço (RecursosService e AgendamentosService), separada dos controladores de rota.

Dependency Inversion: Os serviços são registrados no contêiner de dependência do .NET e injetados nos controladores via construtor.