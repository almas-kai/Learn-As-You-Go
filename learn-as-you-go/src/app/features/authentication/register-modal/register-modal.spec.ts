import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RegisterModal } from './register-modal';
import { MatDialogRef, MatDialogTitle } from '@angular/material/dialog';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { HarnessLoader } from '@angular/cdk/testing';
import { TestbedHarnessEnvironment } from '@angular/cdk/testing/testbed';
import { By } from '@angular/platform-browser';

const TRANSLATION_MOCKS = {
  authentication: {
    register: {
      title: 'Register'
    }
  }
} as const;

describe(RegisterModal.name, () => {
  let component: RegisterModal;
  let fixture: ComponentFixture<RegisterModal>;
  let loader: HarnessLoader;
  let translateService: TranslateService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RegisterModal,
        TranslateModule.forRoot()
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

    translateService = TestBed.inject(TranslateService);
    translateService.setTranslation('en', TRANSLATION_MOCKS);
    translateService.use('en');

    component = fixture.componentInstance;
    loader = TestbedHarnessEnvironment.loader(fixture);

    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display correct title text', () => {
    const titleElement = fixture.debugElement.query(By.directive(MatDialogTitle)).nativeElement as HTMLHeadingElement;

    expect(titleElement).toBeDefined();
    expect(titleElement.textContent.trim()).toBe(translateService.instant('authentication.register.title'));
  });
});
