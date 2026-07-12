import { Endpoints } from "./endpoints";

/**
 * Configs for local development environment.
 * 
 * This file is replaced by the `production` version of configs. See `angular.json` file for more info (`fileReplacements` section).
*/
export const environment = {
  production: false,
  api: {
    domain: 'https://localhost:7195',
    ...Endpoints
  }
} as const;