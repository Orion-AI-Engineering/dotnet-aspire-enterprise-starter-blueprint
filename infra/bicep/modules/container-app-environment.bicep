param location string
param environmentName string
param logAnalyticsWorkspaceId string

resource env 'Microsoft.App/managedEnvironments@2024-03-01' = {
  name: environmentName
  location: location
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logAnalyticsWorkspaceId
        sharedKey: 'replace-at-deploy-time'
      }
    }
  }
}

output environmentId string = env.id
