terraform {
  required_version = "~> 1.10.4"
  required_providers {
    azapi = {
      source  = "azure/azapi"
      version = "~> 2.2.0"
    }
    azuread = {
      source  = "hashicorp/azuread"
      version = "~> 2.53.1"
    }
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.117.0"
    }
    external = {
      source  = "hashicorp/external"
      version = "~> 2.3.4"
    }
    null = {
      source  = "hashicorp/null"
      version = "~> 3.2.3"
    }
    random = {
      source  = "hashicorp/random"
      version = "~> 3.6.3"
    }
  }
}
