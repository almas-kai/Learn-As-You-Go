/**
 * Configs for production environment.
 * 
 * This file is automatically used in production. Don't use it in development. See `angular.json` file for more info (`fileReplacements` section).
*/
export const environment = {
  production: true,
  apiUrl: 'http://real-api-endpoint'
} as const;