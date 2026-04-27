# Orion Aspire Starter

A public, polished starter repository for teams building real-world distributed applications with **.NET Aspire 13**.

It is intentionally opinionated and practical:

- Aspire AppHost + ServiceDefaults
- API + Web + Worker
- PostgreSQL + Redis + RabbitMQ
- OpenTelemetry + health checks
- Azure Container Apps deployment with Bicep
- GitHub Actions and Azure DevOps pipelines
- Architecture tests, integration tests, smoke-test scripts
- A clear upgrade path into the paid Orion template catalog

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

## Local development

See [docs/quickstart.md](docs/quickstart.md).

## Paid upgrade path

The paid catalog extends this into:

- multi-tenant Keycloak federation
- Microsoft Entra enterprise SSO patterns
- Azure Service Bus variants
- AKS / Kubernetes deployment options
- agentic system templates with Semantic Kernel and Microsoft Agent Framework
- shared reusable platform modules and release packs

See [docs/upgrade-path.md](docs/upgrade-path.md).
