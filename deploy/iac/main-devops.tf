resource "azuread_application" "devops" {
  display_name = replace(local.name_template, "<service>", "devops")
}

resource "azuread_service_principal" "devops" {
  client_id = azuread_application.devops.client_id
}

resource "azuread_application_password" "devops" {
  display_name      = "apppwd"
  application_id    = azuread_application.devops.id
  end_date_relative = "17520h"
}

resource "azurerm_role_assignment" "devops" {
  scope                = azurerm_resource_group.rg.id
  role_definition_name = "Contributor"
  principal_id         = azuread_service_principal.devops.id
}

resource "azurerm_key_vault_secret" "devopspwd-secret" {
  key_vault_id = azurerm_key_vault.kv.id
  name         = "DevOpsServicePrincipal--Pwd"
  value        = azuread_application_password.devops.value
}
