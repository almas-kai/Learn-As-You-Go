import { PasswordValidatorConfigurations } from '../types';

export const MIN_PASSWORD_LENGTH = 12;
export const MIN_UNIQUE_CHARS = 6;

export const DEFAULT_PASSWORD_VALIDATOR_CONFIGURATIONS: PasswordValidatorConfigurations = {
  requireDigit: {
    isOn: true,
    isValidPassword: (password: string) => {
      return /[0-9]/.test(password);
    },
    message: 'shared.password.errors.at-least-one-digit'
  },
  requiredMinLength: {
    isOn: true,
    isValidPassword: (password: string) => {
      return password.trim().length > 0 && password.length >= MIN_PASSWORD_LENGTH;
    },
    message: 'shared.password.errors.min-length',
    params: {
      value: MIN_PASSWORD_LENGTH
    }
  },
  requireNonAlphanumeric: {
    isOn: true,
    isValidPassword: (password: string) => {
      return /[^A-Za-z0-9]/.test(password);
    },
    message: 'shared.password.errors.non-alphanumeric'
  },
  requireUppercase: {
    isOn: true,
    isValidPassword: (password: string) => {
      return /[A-Z]/.test(password);
    },
    message: 'shared.password.errors.at-least-one-uppercase'
  },
  requireLowercase: {
    isOn: true,
    isValidPassword: (password: string) => {
      return /[a-z]/.test(password);
    },
    message: 'shared.password.errors.at-least-one-lowercase'
  },
  requiredUniqueChars: {
    isOn: true,
    isValidPassword: (password: string) => {
      const uniqueChars = new Set(password.split(''));
      return uniqueChars.size >= MIN_UNIQUE_CHARS;
    },
    message: 'shared.password.errors.unique-chars',
    params: {
      value: MIN_UNIQUE_CHARS
    }
  }
};