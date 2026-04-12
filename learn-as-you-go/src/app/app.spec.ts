import { ComponentFixture, TestBed } from '@angular/core/testing';
import { App } from './app';
import { vi, describe, it, expect, beforeEach } from 'vitest';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { SUPPORTED_LANGUAGES, SupportedLanguageCode } from '@public/i18n/supported-languages';

describe(App.name, () => {
  let fixture: ComponentFixture<App>;
  let translate: TranslateService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        App,
        TranslateModule.forRoot()
      ]
    }).compileComponents();

    translate = TestBed.inject(TranslateService);
    vi.spyOn(translate, 'addLangs');
    vi.spyOn(translate, 'use');
  });

  it('should use browser language if supported', async () => {
    const prefferedLanguage: SupportedLanguageCode = 'ru';

    vi.spyOn(translate, 'getBrowserLang')
      .mockReturnValue(prefferedLanguage);

    fixture = TestBed.createComponent(App);
    await fixture.whenStable();

    expect(translate.addLangs)
      .toHaveBeenCalledWith(Object.keys(SUPPORTED_LANGUAGES));

    expect(translate.use)
      .toHaveBeenCalledWith(prefferedLanguage);
  });

  it('should fallback to the default language if the preffered language is not supported', async () => {
    const unsupportedLanguage = 'fake-language-code';
    const fallbackLanguage: SupportedLanguageCode = 'en';

    vi.spyOn(translate, 'getBrowserLang')
      .mockReturnValue(unsupportedLanguage);

    fixture = TestBed.createComponent(App);
    await fixture.whenStable();

    expect(translate.addLangs)
      .toHaveBeenCalledWith(Object.keys(SUPPORTED_LANGUAGES));

    expect(translate.use)
      .toHaveBeenCalledWith(fallbackLanguage);
  });
});
