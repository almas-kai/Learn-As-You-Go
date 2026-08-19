import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SideBar } from './side-bar';
import { TranslateModule } from '@ngx-translate/core';

describe(SideBar.name, () => {
  let component: SideBar;
  let fixture: ComponentFixture<SideBar>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        SideBar,
        TranslateModule.forRoot()
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SideBar);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
