import { TranslationKey } from '@public/i18n/types';

export interface PasswordValidatorConfigurationItem {
  readonly isOn: boolean,
  readonly message: TranslationKey,
  readonly isValidPassword: (password: string) => boolean,
  readonly params?: Record<string, unknown>
}

export interface PasswordValidatorConfigurations {
  readonly requireDigit: PasswordValidatorConfigurationItem,
  readonly requiredMinLength: PasswordValidatorConfigurationItem,
  readonly requireNonAlphanumeric: PasswordValidatorConfigurationItem,
  readonly requireUppercase: PasswordValidatorConfigurationItem,
  readonly requireLowercase: PasswordValidatorConfigurationItem,
  readonly requiredUniqueChars: PasswordValidatorConfigurationItem
}