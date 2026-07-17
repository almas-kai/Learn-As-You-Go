import { TestBed } from '@angular/core/testing';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { AccountManager } from './account-manager';
import { RegisterAccountModel } from './types';
import { environment } from '@environments/environment';

const REGISTER_FAKES = {
  endpoint: environment.api.domain + environment.api.account.register,
  expectedMethod: 'POST',
  model: {
    email: 'fake@example.com',
    password: 'fake_password'
  } as RegisterAccountModel
} as const;

describe(AccountManager.name, () => {
  let service: AccountManager;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        provideHttpClientTesting()
      ]
    });
    service = TestBed.inject(AccountManager);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    TestBed.inject(HttpTestingController).verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should make register request to the correct endpoint, and with correct configs', () => {
    service.register(REGISTER_FAKES.model).subscribe();

    const request = httpTestingController.expectOne(REGISTER_FAKES.endpoint);
    
    expect(request.request.method).toBe(REGISTER_FAKES.expectedMethod);
  });
});
