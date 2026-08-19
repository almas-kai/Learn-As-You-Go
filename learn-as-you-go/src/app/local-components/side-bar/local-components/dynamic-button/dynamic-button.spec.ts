import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DynamicButton } from './dynamic-button';
import { ComponentRef } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';

describe(DynamicButton.name, () => {
  let component: DynamicButton;
  let fixture: ComponentFixture<DynamicButton>;
  let componentRef: ComponentRef<DynamicButton>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        DynamicButton,
        TranslateModule.forRoot()
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DynamicButton);
    component = fixture.componentInstance;
    componentRef = fixture.componentRef;

    componentRef.setInput('isExtended', false);
    componentRef.setInput('iconName', 'test-icon');
    componentRef.setInput('labelKey', 'some-random-key');

    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
