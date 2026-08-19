import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { SUPPORTED_LANGUAGES } from '@public/i18n/supported-languages';
import { SideBar } from './local-components/side-bar/side-bar';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    SideBar
  ],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  private readonly _translateService = inject(TranslateService);

  constructor() {
    const languageCodes = Object.keys(SUPPORTED_LANGUAGES);
    this._translateService.addLangs(languageCodes);

    const browserLanguage = this._translateService.getBrowserLang() ?? '';
    const actualLanguage = languageCodes.includes(browserLanguage) ? browserLanguage : 'en';

    this._translateService.use(actualLanguage);
  }
}
