import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { RegisterAccountModel } from './types';
import { Observable } from 'rxjs';
import { environment } from '@environments/environment';

@Service()
export class AccountManager {
  private readonly _httpClient = inject(HttpClient);

  public register(registerAccountModel: RegisterAccountModel): Observable<void> {
    return this._httpClient.post<void>(
      environment.api.domain + environment.api.account.register,
      registerAccountModel
    );
  }
}
