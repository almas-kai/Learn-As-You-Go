import { ComponentFixture, TestBed } from '@angular/core/testing';
import { App } from './app';
import{ vi } from 'vitest';
import { TranslateService } from '@ngx-translate/core';
import { SUPPORTED_LANGUAGES, SupportedLanguageCode } from '@public/i18n/supported-languages';

const translateServiceMock = {
  addLangs: vi.fn(),
  getBrowserLang: vi.fn(),
  use: vi.fn()
};

describe(App.name, () => {
  let fixture: ComponentFixture<App>;
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [App],
      providers: [
        {
          provide: TranslateService,
          useValue: translateServiceMock
        }
      ]
    }).compileComponents();
  });

  it('should use browser language if supported', async () => {
    const prefferedLanguage: SupportedLanguageCode = 'ru';

    translateServiceMock.getBrowserLang
      .mockReturnValue(prefferedLanguage)

    fixture = TestBed.createComponent(App);
    await fixture.whenStable();

    expect(translateServiceMock.addLangs)
      .toHaveBeenCalledWith(Object.keys(SUPPORTED_LANGUAGES));

    expect(translateServiceMock.getBrowserLang)
      .toHaveReturnedWith(prefferedLanguage);

    expect(translateServiceMock.use)
      .toHaveBeenCalledWith(prefferedLanguage);
  });

  it('should fallback to the default language if the preffered language is not supported', async () => {
    const unsupportedLanguage = 'fake-language-code';
    const fallbackLanguage: SupportedLanguageCode = 'en';

    translateServiceMock.getBrowserLang
      .mockReturnValue(unsupportedLanguage)

    fixture = TestBed.createComponent(App);
    await fixture.whenStable();

    expect(translateServiceMock.addLangs)
      .toHaveBeenCalledWith(Object.keys(SUPPORTED_LANGUAGES));

    expect(translateServiceMock.getBrowserLang)
      .toHaveReturnedWith(unsupportedLanguage);

    expect(translateServiceMock.use)
      .toHaveBeenCalledWith(fallbackLanguage);
  });
});
