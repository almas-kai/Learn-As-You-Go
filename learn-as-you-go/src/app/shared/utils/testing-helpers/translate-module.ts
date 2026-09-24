import { TranslateModule, TranslateLoader, TranslationObject } from '@ngx-translate/core';
import { Observable, of } from 'rxjs';
import en from '@public/i18n/en.json';

class StaticEnLoaderForTesting implements TranslateLoader {
  public getTranslation(): Observable<TranslationObject> {
    return of(en);
  }
}

export const TranslateTestingModule = TranslateModule.forRoot(
  {
    fallbackLang: 'en',
    loader: {
      provide: TranslateLoader,
      useClass: StaticEnLoaderForTesting
    }
  }
);
