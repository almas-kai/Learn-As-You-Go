import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RegisterModal } from './register-modal';
import { MatDialogRef } from '@angular/material/dialog';
import { TranslateModule } from '@ngx-translate/core';

describe(RegisterModal.name, () => {
  let component: RegisterModal;
  let fixture: ComponentFixture<RegisterModal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RegisterModal,
        TranslateModule.forRoot()
      ],
      providers: [
        {
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
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
