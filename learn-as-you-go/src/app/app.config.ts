import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideTranslateService } from '@ngx-translate/core';
import { provideTranslateHttpLoader } from '@ngx-translate/http-loader';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideTranslateService(
      {
        fallbackLang: 'en',
        loader: provideTranslateHttpLoader(
          {
            prefix: '/assets/i18n/',
            suffix: '.json'
          }
        )
      }
    )
  ]
};
