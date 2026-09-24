import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RegisterModal } from './register-modal';
import { MatDialogRef, MatDialogTitle } from '@angular/material/dialog';
import { HarnessLoader } from '@angular/cdk/testing';
import { TestbedHarnessEnvironment } from '@angular/cdk/testing/testbed';
import { By } from '@angular/platform-browser';
import { TranslateTestingModule } from '@shared/utils/testing-helpers/translate-module';
import { getFirstErrorTextFromFormFieldHarness } from '@shared/utils/testing-helpers/getFirstErrorTextFromFormFieldHarness';
import { getFormFieldHarnessPredicateWithInputAttribute } from '@shared/utils/testing-helpers/getFormFieldWithInputAttribute';
import { MatFormFieldHarness } from '@angular/material/form-field/testing';
import { MatInputHarness } from '@angular/material/input/testing';
import en from '@public/i18n/en.json';

describe(RegisterModal.name, () => {
  let component: RegisterModal;
  let fixture: ComponentFixture<RegisterModal>;
  let loader: HarnessLoader;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RegisterModal,
        TranslateTestingModule
      ],
      providers: [
        {
          // Faking MatDialogRef cause if i were to import a real one, it would break the app (contains too many deps).
          provide: MatDialogRef,
          useValue: {
            close: () => {}
          }
        }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterModal);

    component = fixture.componentInstance;
    loader = TestbedHarnessEnvironment.loader(fixture);

    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display correct translation for the title', () => {
    const titleElement = fixture.debugElement.query(By.directive(MatDialogTitle)).nativeElement as HTMLHeadingElement;

    expect(titleElement).toBeDefined();
    expect(titleElement.textContent.trim()).toBe(en.authentication.register.title);
  });

  describe('email field', () => {
    let emailFieldHarness: MatFormFieldHarness;
    let emailInputHarness: MatInputHarness;

    beforeEach(async () => {
      emailFieldHarness = await loader.getHarness(getFormFieldHarnessPredicateWithInputAttribute('type', 'email'));
      emailInputHarness = (await emailFieldHarness.getControl()) as MatInputHarness;
    });

    it('has correct label text', async () => {
      expect(await emailFieldHarness.getLabel()).toBe(en.shared.email.label);
    });

    it('has correct placeholder for the input', async () => {
      expect(await emailInputHarness.getPlaceholder()).toBe(en.shared.email.placeholder);
    });

    it('has to be required', async () => {
      expect(await emailInputHarness.isRequired()).toBeTruthy();
      expect(await getFirstErrorTextFromFormFieldHarness(emailFieldHarness)).toBe('');

      await emailInputHarness.focus();
      await emailInputHarness.blur();

      expect(await getFirstErrorTextFromFormFieldHarness(emailFieldHarness)).toBe(en.shared.email.errors.required);
    });

    it('shows error message on wrong email format', async () => {
      expect(await getFirstErrorTextFromFormFieldHarness(emailFieldHarness)).toBe('');

      await emailInputHarness.focus();
      await emailInputHarness.setValue('Incorrect email');
      await emailInputHarness.blur();

      expect(await getFirstErrorTextFromFormFieldHarness(emailFieldHarness)).toBe(en.shared.email.errors.format);
    });

    it('removes error text after correct input is provided', async () => {
      expect(await getFirstErrorTextFromFormFieldHarness(emailFieldHarness)).toBe('');

      await emailInputHarness.focus();
      await emailInputHarness.setValue('Incorrect email');
      await emailInputHarness.blur();

      expect(await getFirstErrorTextFromFormFieldHarness(emailFieldHarness)).toBe(en.shared.email.errors.format);

      const value = 'example@gmail.com';
      await emailInputHarness.focus();
      await emailInputHarness.setValue(value);
      await emailInputHarness.blur();

      expect(await getFirstErrorTextFromFormFieldHarness(emailFieldHarness)).toBe('');
      expect(await emailInputHarness.getValue()).toBe(value);
    });
  });

  describe('password field', () => {
    let passwordFieldHarness: MatFormFieldHarness;
    let passwordInputHarness: MatInputHarness;

    beforeEach(async () => {
      passwordFieldHarness = await loader.getHarness(getFormFieldHarnessPredicateWithInputAttribute('type', 'password'));
      passwordInputHarness = (await passwordFieldHarness.getControl()) as MatInputHarness;
    });

    it('has correct label text', async () => {
      expect(await passwordFieldHarness.getLabel()).toBe(en.shared.password.label);
    });

    it('has correct placeholder for the input', async () => {
      expect(await passwordInputHarness.getPlaceholder()).toBe(en.shared.password.placeholder);
    });

    it('has to be required', async () => {
      expect(await passwordInputHarness.isRequired()).toBeTruthy();
      expect(await getFirstErrorTextFromFormFieldHarness(passwordFieldHarness)).toBe('');

      await passwordInputHarness.focus();
      await passwordInputHarness.blur();

      expect(await getFirstErrorTextFromFormFieldHarness(passwordFieldHarness)).toBe(en.shared.password.errors.required);
    });
  });
});
