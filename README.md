# Orion Aspire Starter

A public, polished starter repository for teams building real-world distributed applications with **Aspire 13** in .NET. This starter shows a small but realistic distributed system composed of a front-end, API and worker.

The repository is intentionally designed to be:
- easy to run locally
- easy to understand
- easy to extend into a real product

It is intentionally opinionated and practical:

- Aspire `AppHost` + ServiceDefaults
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
- `infra/bicep` — Azure Container Apps deployment assets
- `azure.yaml` — Azure Developer CLI project configuration
- `.github/workflows` — GitHub Actions CI/CD workflows
- `ado` — Azure DevOps pipeline YAML files
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

## Getting help integrating this in enterprise systems

The public open-source Aspire architecture blueprint focuses on project structure and architecture best practices. However, many enterprises we have consulted for typically ask for the following components in addition to the baseline architecture:

- Keycloak federation and multi-tenant auth
- Entra enterprise SSO
- RabbitMQ and Service Bus production variants
- AKS / Helm / Kubernetes deployment assets
- agentic templates with Semantic Kernel and Microsoft Agent Framework

If you would like help integrating this Aspire architecture blueprint in your enterprise system or if you want implementation of any of the abovementioned components, feel free to [book a discovery call](https://calendly.com/f-sazanavets) or email info@orionaiengineering.com.

## Deployment

This repository is designed to deploy the Aspire application to **Azure Container Apps** using the **Azure Developer CLI** (`azd`) and Bicep.

The deployment entry point is the repository root, not the `AppHost` folder. This is important because this repository keeps `azure.yaml` and the `infra` folder at the root, while the `AppHost` project is under `src/Orion.Starter.AppHost`.

### How the `infra` folder is generated

The `infra` folder contains the infrastructure-as-code assets used by `azd` to provision Azure resources.

For Aspire applications, `azd` can inspect the Aspire `AppHost` and derive the Azure infrastructure model from it. Conceptually, the flow is:

1. `azd` runs the `AppHost` in manifest-publishing mode.
2. Aspire produces an application manifest that describes projects, containers, resources and bindings.
3. `azd` translates that manifest into Azure provisioning assets.
4. `azd provision` creates or updates the Azure resources.
5. `azd deploy` builds and publishes the application containers.

In simple Aspire deployments, some generated infrastructure can be handled internally by `azd`. This starter keeps the `infra/bicep` assets in the repository so the Azure deployment is reviewable, customizable and suitable as a starting point for real projects.

To generate or refresh infrastructure assets from the `AppHost`, run the following from the repository root:

```bash
azd init
azd infra gen
```

Older `azd` documentation and examples may refer to `azd infra synth`; current `azd` versions use `azd infra gen`, with `synth` retained as an older alias.

If you manually edit generated Bicep files, review the Git diff carefully before running infrastructure generation again, because regenerated files can overwrite manual changes.

### Purpose of `azure.yaml`

`azure.yaml` is the Azure Developer CLI project file. It tells `azd` how this repository maps to Azure deployment concepts.

It is normally created by running:

```bash
azd init
```

For this repository, `azure.yaml` should live in the repository root. It should point to the `AppHost` project using a relative path:

```yaml
name: orion-aspire-starter

infra:
  provider: bicep
  path: infra/bicep

services:
  app:
    language: dotnet
    project: ./src/Orion.Starter.AppHost/Orion.Starter.AppHost.csproj
    host: containerapp
```

`azd` reads this file when you run commands such as:

```bash
azd provision
azd deploy
azd up
```

The key point is that the pipeline working directory should be the repository root. Do not change the pipeline working directory to `src/Orion.Starter.AppHost` unless `azure.yaml` and `infra` are also moved there.

### Deploying to Azure Container Apps

The Bicep templates create or configure the Azure resources required by the Aspire application, including:

- resource group scoped assets
- Log Analytics
- Container Apps environment
- Azure Container Registry
- Key Vault
- PostgreSQL flexible server
- managed identities

A local deployment typically looks like this:

```bash
azd auth login
azd provision --no-prompt
azd deploy --no-prompt
```

Or, for the full provision-and-deploy flow:

```bash
azd up
```

### GitHub Actions pipelines

GitHub Actions YAML files live under:

```text
.github/workflows
```

The recommended split is:

```text
.github/workflows/ci.yml
.github/workflows/release.yml
```

#### `ci.yml`

The build workflow is the CI pipeline. It should run on pushes and pull requests to `main`.

Typical responsibilities:

- check out the repository
- install the required .NET SDK
- restore the solution
- build the solution in `Release` configuration
- run the test suite

This workflow does not need Azure deployment permissions.

#### `release.yml`

The deployment workflow is the release pipeline. It is usually triggered manually with `workflow_dispatch`, especially for production deployments.

Typical responsibilities:

- check out the repository
- install the required .NET SDK
- install `azd`
- authenticate to Azure using GitHub OIDC / federated credentials
- optionally restore, build and test again before deployment
- run `azd provision --no-prompt`
- run `azd deploy --no-prompt`

The deployment workflow should include:

```yaml
permissions:
  id-token: write
  contents: read
```

The usual GitHub repository or environment variables are:

```text
AZURE_CLIENT_ID
AZURE_TENANT_ID
AZURE_SUBSCRIPTION_ID
AZURE_ENV_NAME
AZURE_LOCATION
```

For this repository layout, the `azd` steps should run from the repository root:

```yaml
working-directory: .
```

GitHub Environments are recommended for production deployments so that approvals can be added before the release job runs.

### Azure DevOps pipelines

Azure DevOps YAML files live under:

```text
ado
```

Some teams prefer the Azure DevOps convention:

```text
.azuredevops/pipelines
```

Either folder is fine as long as the Azure DevOps pipeline is configured to use the correct YAML file.

The recommended split is:

```text
ado/templates/build-test.yml
ado/deploy-aca.yml
```

#### `ado/build-test.yml`

The Azure DevOps build pipeline is equivalent to the GitHub Actions CI workflow.

Typical responsibilities:

- run on pushes and pull requests to `main`
- check out the repository
- install the required .NET SDK using `UseDotNet@2`
- restore the solution
- build the solution in `Release` configuration
- run the test suite

This pipeline does not need an Azure service connection unless you add steps that access Azure.

#### `ado/deploy-aca.yml`

The Azure DevOps deployment pipeline is equivalent to the GitHub Actions release workflow.

Typical responsibilities:

- run manually, or only from a protected branch
- use a deployment job bound to an Azure DevOps Environment such as `dev` or `prod`
- install the required .NET SDK
- install `azd`
- authenticate to Azure using an Azure Resource Manager service connection
- run `azd provision --no-prompt`
- run `azd deploy --no-prompt`

Recommended Azure DevOps configuration:

- Create an Azure Resource Manager service connection.
- Prefer workload identity federation for new service connections where supported.
- Store subscription, location and environment values as pipeline variables or variable group entries.
- Add approvals and checks to the `prod` Azure DevOps Environment.

Common deployment variables:

```text
azureServiceConnection
AZURE_SUBSCRIPTION_ID
AZURE_LOCATION
AZURE_ENV_NAME
```

For this repository layout, the `azd` task should use the repository root as the working directory:

```yaml
workingDirectory: '$(System.DefaultWorkingDirectory)'
```

The `AppHost` path should be configured in `azure.yaml`, not by changing the Azure DevOps working directory.
