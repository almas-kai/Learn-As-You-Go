import { DEFAULT_PASSWORD_VALIDATOR_CONFIGURATIONS, MIN_PASSWORD_LENGTH, MIN_UNIQUE_CHARS } from './default-configuration';

describe('DEFAULT_PASSWORD_VALIDATOR_CONFIGURATIONS', () => {
  describe('has at least one digit constraint', () => {
    const validateFn = DEFAULT_PASSWORD_VALIDATOR_CONFIGURATIONS.requireDigit.isValidPassword;

    test.for(
      [
        '1',
        '1password',
        'SomeText999',
        '1password9',
        'Some text 1 that contains number in the middle.',
        'Number at the end? 0'
      ]
    )('validator returns true for %s', (password: string) => {
      expect(validateFn(password)).toBeTruthy();
    });

    test.for(
      [
        '',
        'No number at all',
        'Some Text',
        'One Two Three',
        'It is a poor sort of memory that only works backwords.',
        'Make things as simple as possible, but not simpler.',
        'Simplicity is the ultimate sophistication.',
        'We give ourselves what we feel we deserve.'
      ]
    )('validator returns false for %s', (password: string) => {
      expect(validateFn(password)).toBeFalsy();
    });
  });

  describe('has minimum length constraint', () => {
    const validateFn = DEFAULT_PASSWORD_VALIDATOR_CONFIGURATIONS.requiredMinLength.isValidPassword;
    const constraintParam = DEFAULT_PASSWORD_VALIDATOR_CONFIGURATIONS.requiredMinLength.params;

    it(`should have length constraint as ${MIN_PASSWORD_LENGTH}`, () => {
      expect(MIN_PASSWORD_LENGTH).toBe(12);
    });

    it('should pass the length constraint in params', () => {
      expect(constraintParam).toBeDefined();
      expect(constraintParam!['value']).toBe(MIN_PASSWORD_LENGTH);
    });

    test.for(
      [
        'My length is greater than 12',
        '1234567890121',
        'High thoughts need a high language.',
        'Common sense will not accomplish great things. Simply become insane and desperate.'
      ]
    )('validator returns true for %s', (password: string) => {
      expect(validateFn(password)).toBeTruthy();
    });

    test.for(
      [
        '',
        '12345678901',
        'Some Text'
      ]
    )('validator returns false for %s', (password: string) => {
      expect(validateFn(password)).toBeFalsy();
    });
  });

  describe('has non-alpha numeric character constraint', () => {
    const validateFn = DEFAULT_PASSWORD_VALIDATOR_CONFIGURATIONS.requireNonAlphanumeric.isValidPassword;

    test.for(
      [
        'Super_Strong_Password',
        'Password@123',
        'SuperSecret!',
        '[Programming]',
        'It1sHard+'
      ]
    )('validator returns true for %s', (password: string) => {
      expect(validateFn(password)).toBeTruthy();
    });

    test.for(
      [
        'SuperStrongPassword',
        'Password123',
        'SuperSecret',
        'Programming',
        'It1sHard'
      ]
    )('validator returns false for %s', (password: string) => {
      expect(validateFn(password)).toBeFalsy();
    });
  });

  describe('has uppercase character constraint', () => {
    const validateFn = DEFAULT_PASSWORD_VALIDATOR_CONFIGURATIONS.requireUppercase.isValidPassword;
    test.for(
      [
        'Super_Strong_Password',
        'Password@123',
        'SuperSecret!',
        '[Programming]',
        'It1sHard+'
      ]
    )('validator returns true for %s', (password: string) => {
      expect(validateFn(password)).toBeTruthy();
    });

    test.for(
      [
        'only_lowercase',
        'password123',
        'password',
        'programming',
        'it1shard'
      ]
    )('validator returns false for %s', (password: string) => {
      expect(validateFn(password)).toBeFalsy();
    });
  });

  describe('has lowercase character constraint', () => {
    const validateFn = DEFAULT_PASSWORD_VALIDATOR_CONFIGURATIONS.requireLowercase.isValidPassword;
    test.for(
      [
        'Super_Strong_Password',
        'Password@123',
        'SuperSecret!',
        '[Programming]',
        'It1sHard+'
      ]
    )('validator returns true for %s', (password: string) => {
      expect(validateFn(password)).toBeTruthy();
    });

    test.for(
      [
        'ONLY_UPPERCASE',
        'SELECT * FROM SALESDB',
        'PASSWORD',
        'NO_LOWERCASE',
        ''
      ]
    )('validator returns false for %s', (password: string) => {
      expect(validateFn(password)).toBeFalsy();
    });
  });

  describe('has minimum number of unique characters constraint', () => {
    const validateFn = DEFAULT_PASSWORD_VALIDATOR_CONFIGURATIONS.requiredUniqueChars.isValidPassword;
    const constraintParam = DEFAULT_PASSWORD_VALIDATOR_CONFIGURATIONS.requiredUniqueChars.params;

    it(`should have minimum number of unique characters constraint as ${MIN_UNIQUE_CHARS}`, () => {
      expect(MIN_UNIQUE_CHARS).toBe(6);
    });

    it('should pass the minimum number of unique characters constraint in params', () => {
      expect(constraintParam).toBeDefined();
      expect(constraintParam!['value']).toBe(MIN_UNIQUE_CHARS);
    });

    test.for(
      [
        'Password123!',
        'uikjlp',
        'Super Secret',
        'The beginning of knowledge is the discovery of something we do not understand.'
      ]
    )('validator returns true for %s', (password: string) => {
      expect(validateFn(password)).toBeTruthy();
    });

    test.for(
      [
        'Pass',
        'PPPPPPAAAAAA',
        'SAMESAMESAME',
        ''
      ]
    )('validator returns false for %s', (password: string) => {
      expect(validateFn(password)).toBeFalsy();
    });
  });
});