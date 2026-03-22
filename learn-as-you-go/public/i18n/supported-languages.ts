export const SUPPORTED_LANGUAGES = {
  en: 'English',
  ru: 'Русский'
} as const;

export type SupportedLanguageCode = keyof typeof SUPPORTED_LANGUAGES;