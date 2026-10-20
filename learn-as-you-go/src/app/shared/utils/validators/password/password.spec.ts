import { Injector, Signal, signal } from '@angular/core';
import { password } from './password';
import { FieldTree, form } from '@angular/forms/signals';
import { TestBed } from '@angular/core/testing';
import { DEFAULT_PASSWORD_VALIDATOR_CONFIGURATIONS } from './default-configuration/default-configuration';
import { PasswordValidatorConfigurationItem, PasswordValidatorConfigurations } from './types';

interface PasswordFormModelPair {
  passwordForm: FieldTree<string>,
  model: Signal<string>
}

type PasswordErrorKind = keyof PasswordValidatorConfigurations;

describe(password.name, () => {
  function createPasswordForm(initial: string): PasswordFormModelPair {
    const model = signal(initial);
    const f = form(model, (path) => {
      password(path);
    }, { injector: TestBed.inject(Injector) });
    
    return {
      model: model,
      passwordForm: f
    };
  }

  function extractActualErrorKinds(passwordForm: FieldTree<string>): PasswordErrorKind[] {
    return passwordForm()
      .errors()
      .map((validationError) => validationError.kind) as PasswordErrorKind[];
  }

  it('should return all of the error kinds for the empty string input', () => {
    const { passwordForm } = createPasswordForm('');

    const expectedErrorKinds: PasswordErrorKind[] = ['requireDigit', 'requiredMinLength', 'requireNonAlphanumeric', 'requireUppercase', 'requireLowercase', 'requiredUniqueChars'];
    
    const actualErrorKinds = extractActualErrorKinds(passwordForm);
    
    expect(actualErrorKinds).toEqual(expectedErrorKinds);
  });

  it.for<[string, PasswordErrorKind[]]>(
    [
      [
        ' ',
        ['requireDigit', 'requiredMinLength', 'requireUppercase', 'requireLowercase', 'requiredUniqueChars']
      ],
      [
        'Google123',
        ['requiredMinLength', 'requireNonAlphanumeric']
      ],
      [
        'SuperStrong_Pssword123',
        []
      ],
      [
        'ONLY_UPPERCASE',
        ['requireDigit', 'requireLowercase']
      ],
      [
        'lets test only lowercase',
        ['requireDigit', 'requireUppercase']
      ],
      [
        '12345678901212',
        ['requireNonAlphanumeric', 'requireUppercase', 'requireLowercase']
      ]
    ]
  )('returns expected error kinds for the "%s" password', ([password, expectedErrorKinds]) => {
    const { passwordForm } = createPasswordForm(password);

    const actualErrorKinds = extractActualErrorKinds(passwordForm);

    expect(actualErrorKinds).toEqual(expectedErrorKinds);
  });
});