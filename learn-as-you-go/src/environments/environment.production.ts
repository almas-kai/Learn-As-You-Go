import { Endpoints } from "./endpoints";

/**
 * Configs for production environment.
 * 
 * This file is automatically used in production. Don't use it in development. See `angular.json` file for more info (`fileReplacements` section).
*/
export const environment = {
  production: true,
  api: {
    domain: 'paste-real-domain-here',
    ...Endpoints
  }
} as const;