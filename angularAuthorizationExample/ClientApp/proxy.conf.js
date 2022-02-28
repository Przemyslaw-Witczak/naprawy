const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:44464';

const PROXY_CONFIG = [
  {
    context: [            
      "/Vehicles",
      "/Maintenances",
      "/Parts",
      "/_configuration",
      "/.well-known",
      "/Identity",
      "/connect",
      "/ApplyDatabaseMigrations",
      "/_framework"      
   ],
    target: target,
    secure: false
  }
]

module.exports = PROXY_CONFIG;
