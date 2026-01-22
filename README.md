📝 SimpleBlog API
API RESTful desenvolvida para gerenciamento de postagens e comentários de um blog simples. O projeto foca em boas práticas de arquitetura, separação de responsabilidades e performance.

🚀 Tecnologias Utilizadas
.NET 9 (Versão mais recente)

C# 13

ASP.NET Core Web API

Entity Framework Core (InMemory Database para demonstração)

FluentValidation (Validação de entrada)

Mapster (Mapeamento de objetos DTO <-> Entity)

Swashbuckle (Swagger) (Documentação)

🛠️ Pré-requisitos
Para rodar este projeto, você precisará ter instalado em sua máquina:

SDK do .NET 9.0 ou superior.

Uma IDE para compilar o código C#:

Visual Studio 2022 (Recomendado, versão 17.12+)

Ou Visual Studio Code com a extensão C# Dev Kit.

⚙️ Configuração e Execução

1. Clonar o Repositório
   Bash
   git clone https://github.com/joaopauloosantos/api-blog.git
   cd api-blog
2. Configurar Autenticação do Swagger
   A documentação (Swagger UI) é protegida por senha para simular um ambiente de produção seguro. As credenciais padrão estão configuradas no arquivo appsettings.json:

Usuário: admin

Senha: admin

Nota: Você pode alterar essas credenciais diretamente no appsettings.json se desejar.

3. Executar o Projeto
   Via terminal na pasta raiz da API:

Bash
dotnet restore
dotnet run --project SimpleBlog.API
Ou abra a solução (.sln) no Visual Studio e pressione F5.

4. Acessar a Documentação
   Após iniciar, acesse no navegador (a porta pode variar, verifique o terminal):

https://localhost:7154/swagger (ou a porta indicada no seu console)

Ao acessar, será solicitado o login. Utilize as credenciais informadas acima (admin / admin).

🏗️ Padrões de Arquitetura
O projeto foi estruturado seguindo princípios de Clean Code e SOLID:

Repository Pattern: Abstração da camada de acesso a dados.

Notification Pattern: Substituição de Exceptions por notificações de domínio (Domain Notifications) para regras de negócio, melhorando a performance e controle de fluxo.

DTOs (Data Transfer Objects): Separação estrita entre os objetos de domínio e os contratos da API.

Fluent Validation: Validação automática das requisições via filtro global (FluentValidationFilter).

Optimized Queries: Uso de projeções e contagem via banco de dados para listagens performáticas.

📈 Melhorias Futuras (Roadmap)
Se houvesse mais tempo para a evolução deste projeto, os próximos passos seriam:

Tratamento Global de Erros: Implementação de um Middleware robusto para interceptar exceções não tratadas (Exception Middleware) e padronizar o retorno 500.

Testes Automatizados: Criação de testes unitários (xUnit) para a camada de Service e testes de integração para as Controllers.

Banco de Dados Real: Substituição do provedor InMemory por um banco relacional robusto como SQL Server ou PostgreSQL rodando em Docker.

Frontend: Desenvolvimento de uma interface gráfica (Mini Front) em React, Angular ou Blazor para consumir a API.

Cloud & CI/CD: Configuração de pipelines de deploy automático e hospedagem do serviço em nuvem (Azure App Service ou AWS Lambda/EC2).
