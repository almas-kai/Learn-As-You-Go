import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Footer } from '@layout-components/footer/footer';
import { Header } from '@layout-components/header/header';
import { TranslateService } from '@ngx-translate/core';
import { SUPPORTED_LANGUAGES } from '@public/i18n/supported-languages';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    Header,
    Footer
  ],
  templateUrl: './app.html',
  styleUrl: './app.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class App {
  private readonly translateService = inject(TranslateService);

  constructor() {
    const languageCodes = Object.keys(SUPPORTED_LANGUAGES);
    this.translateService.addLangs(languageCodes);

    const browserLanguage = this.translateService.getBrowserLang() ?? '';
    const actualLanguage = languageCodes.includes(browserLanguage) ? browserLanguage : 'en';

    this.translateService.use(actualLanguage);
  }
}
