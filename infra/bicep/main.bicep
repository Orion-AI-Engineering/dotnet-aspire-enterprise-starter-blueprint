targetScope = 'resourceGroup'

@description('Location')
param location string = resourceGroup().location

param appName string = 'orion-starter'
param environmentName string = '${appName}-env'
param containerRegistryName string = '${replace(appName, '-', '')}acr'

module logAnalytics './modules/log-analytics.bicep' = {
  name: 'logAnalytics'
  params: {
    location: location
    workspaceName: '${appName}-law'
  }
}

module aca './modules/container-app-environment.bicep' = {
  name: 'aca'
  params: {
    location: location
    environmentName: environmentName
    logAnalyticsWorkspaceId: logAnalytics.outputs.workspaceId
  }
}

module acr './modules/container-registry.bicep' = {
  name: 'acr'
  params: {
    location: location
    registryName: containerRegistryName
  }
}

output containerAppEnvironmentId string = aca.outputs.environmentId
output registryLoginServer string = acr.outputs.loginServer
