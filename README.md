# Introduction

TODO: Give a short introduction of your project. Let this section explain the objectives or the motivation behind this project.

# Getting Started

1. Create new project from template
   ```PowerShell
   dotnet new install C:\Repos\isolutions.Samples.WebAppReactTemplate
   dotnet new isolreact -n <YourAppName>
   ```
1. Setup terraform state container: [README](deploy/iac-core/README.md)
1. Create application resources: [README](deploy/iac/README.md)
1. Adjust `src\Server\appsettings.json` and put secrets into user secrets using Visual Studio (find secrets in key vault)
1. Run `[SOLUTION_NAME].Domain.Migrations` project

If you get an error containing the following message during execution of the migrations project, execute the following commands in PowerShell and try again.

```
error: 50 - Local Database Runtime error occurred. Cannot create an automatic instance. See the Windows Application event log for error details
```

```PowerShell
SqlLocalDB delete MSSQLLocalDB
SqlLocalDB create MSSQLLocalDB
SqlLocalDB start MSSQLLocalDB
```

# Run locally

## Client

1. Open `src\Client` in Visual Studio Code
1. Install `ZipFS` extension
1. Open a new terminal in Visual Studio Code
1. Install node in version `22.4.0` (or another stable release, even numbers)
   1. e.g use nvm -> `nvm install 22.4.0` `nvm use 22.4.0`
2. Run `corepack enable`
3. Run `yarn set version berry`
4. Run `yarn dlx @yarnpkg/sdks vscode`
5. Check file `.yarnrc.yml` that is has an entry with `nodeLinker: "pnp"`, if missing, add it.
6. Run `yarn install`
7. Open a `.tsx` file
8. Allow usage of TypeScript workspace version or alternatively do the following
    - Press `Ctrl + P` and enter `>Select TypeScript Version`
    - Choose `Use Workspace Version (.yarn/sdks/typescript/lib)`
9.  Run `yarn dev`
10. Click `Yes` on the security warning dialog

## Server

1. Open `[SOLUTION_NAME].sln
1. Set [SOLUTION_NAME].Server as startup project
1. Press `F5`

# Build and Test

TODO: Describe and show how to build your code and run the tests.

# CI/CD

How to set up the CI/CD pipelines for this project.

## Prerequisites

- Azure DevOps extension [Azure Pipelines Terraform Tasks](https://marketplace.visualstudio.com/items?itemName=JasonBJohnson.azure-pipelines-tasks-terraform) installed
- Azure DevOps environment with name `Development-IaC` exists in the corresponding Azure DevOps project
  **IMPORTANT:** go to environments security settings and assign `Project Administrators` to role `Administrator`
- Azure DevOps service connection with name `[SOLUTION_NAME]-AzureConnection-DEV` exists
  - Create new service connection of type `Azure Resource Manager` in Azure DevOps project settings
  - Select authentication method `Workload Identity federation (manual)`
  - Enter the following values:
    - Service Connection name: `The entered federated principle`
    - Subscription Id: [Subscription Id]
    - Subscription: [Subscription Name]
    - Service Principal Id: [Client ID of app registration [customer]-[environment]-devops-[shortname]]
    - Tenant Id: [Tenant Id]
  - Click `Verify and save`
  - Adjust `webAppName` and `keyVaultName` in `Kickstart.Angular.yml` to match the names of the resources created by IaC

## Create pipelines

Perform the steps below for each of the following pipelines.

- deploy\pipelines\[SOLUTION_NAME].yml
- deploy\pipelines\[SOLUTION_NAME]-iac.yml
- deploy\pipelines\[SOLUTION_NAME]-quality.yml

1. Navigate to `Pipelines` in the menu bar on the left side
1. Click `New pipeline` on the top right
1. select `Azure Repos Git`
1. Select the repository containing the pipelines
1. Select `Existing Azure Pipelines YAML file`
1. Select the pipeline file
1. Click `Continue`
1. Click `Run` to run the pipeline

### [SOLUTION_NAME]-iac.yml

- Add service principal `[customer]-[environment]-devops-[shortname]`
  - ... to AAD group `ra-[customer]-c1-rg-iac-contributor`
  - ... to Azure built-in role `Groups Administrator`
  - ... to Azure built-in role `Application Administrator`
