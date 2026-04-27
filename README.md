# Orion Aspire Starter

A public, polished starter repository for teams building real-world distributed applications with ** Aspire 13** in .NET. This starter shows a small but realistic distributed system composed of a front-end, API and worker.

The repository is intentionally designed to be:
- easy to run locally
- easy to understand
- easy to extend into a real product

It is intentionally opinionated and practical:

- Aspire AppHost + ServiceDefaults
- API + Web + Worker
- PostgreSQL + Redis + RabbitMQ
- OpenTelemetry + health checks
- Azure Container Apps deployment with Bicep
- GitHub Actions and Azure DevOps pipelines
- Architecture tests, integration tests, smoke-test scripts

## Who this is for

This repository is designed for consultants, internal platform teams, and product engineers who want a clean baseline instead of a toy sample.

## Solution layout

- `src/Orion.Starter.AppHost` — Aspire orchestration
- `src/Orion.Starter.ServiceDefaults` — service defaults, health checks, telemetry wiring
- `src/Orion.Starter.Api` — minimal API that publishes commands/events
- `src/Orion.Starter.Worker` — background processor / consumer
- `src/Orion.Starter.Web` — front-end for demoing the flow
- `tests/*` — unit, integration, architecture tests
- `infra/bicep` — Azure Container Apps deployment
- `.github/workflows` — GitHub Actions CI/CD
- `ado` — Azure DevOps pipelines
- `scripts` — local setup, validation and deployment helpers

## Example flow

1. The web app submits an order request.
2. The API persists the request and publishes an `OrderRequested` message to RabbitMQ.
3. The worker consumes the message and simulates asynchronous fulfillment.
4. The API exposes read endpoints so the UI can poll and display results.

## Getting started

### Prerequisites
- [.NET SDK 10](https://dotnet.microsoft.com/en-us/download)
- [Aspire CLI](https://aspire.dev/get-started/install-cli/)
- [Docker Desktop](https://www.docker.com/get-started/) or [compatible container runtime](https://podman.io/)
- [Azure Developer CLI](https://learn.microsoft.com/en-us/azure/developer/azure-developer-cli/) (only for deployment)
- [An Azure subscription](https://azure.microsoft.com/en-us/pricing/purchase-options/azure-account) (only for deployment)

### Run locally

Run the full Aspire setup via the following command:

```bash
aspire run
```

Or execute the **Aspire Host** project via the IDE.

## Customization guide

Recommended order:
1. Rename the solution and namespaces
2. Replace the sample order domain with your own domain model
3. Choose an authentication strategy
   a. Add your own authentication provider.
   b. Move secrets into a real secret store before production use.
4. Decide whether RabbitMQ stays or moves to Azure Service Bus
5. Extend telemetry and SLO dashboards

## Getting help integrating this in enterprise system

The public open-source Aspire architecture blueprint focuses on project structure and the architecture best-practices. However, many enterprises we consulted for typically ask for the following components in addition to the baseline architecture:

- Keycloak federation and multi-tenant auth
- Entra enterprise SSO
- RabbitMQ and Service Bus production variants
- AKS / Helm / Kubernetes deployment assets
- agentic templates with Semantic Kernel and Microsoft Agent Framework

If you would like help integrating these Aspire architecture blueprint in your enterprise system or if you want implementation of any of the abovementioned components, feel free to [book a discovery call](https://calendly.com/f-sazanavets) or email to info@orionaiengineering.com.

## Deployment

### GitHub Actions

Included workflows:
- CI
- PR validation
- release packaging
- CodeQL security scan

### Azure DevOps

The `ado/` folder contains a CI pipeline, PR validation and a release pipeline that can deploy to Azure Container Apps.

### Deploying to Azure Container Apps

The Bicep templates create:
- resource group scoped assets
- Log Analytics
- Container Apps environment
- ACR
- Key Vault
- PostgreSQL flexible server
- managed identities