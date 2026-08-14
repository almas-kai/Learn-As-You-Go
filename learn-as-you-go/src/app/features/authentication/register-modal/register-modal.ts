import { Component, inject, signal } from '@angular/core';
import { MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { TranslateModule } from '@ngx-translate/core';
import { MatAnchor } from "@angular/material/button";
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIcon } from '@angular/material/icon';
import { RegisterAccountModel } from '@core/account-manager/types';
import { email, form, FormField, FormRoot, required } from '@angular/forms/signals';
import { AccountManager } from '@core/account-manager/account-manager';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-register-modal',
  imports: [
    TranslateModule,
    MatDialogModule,
    MatAnchor,
    MatFormFieldModule,
    MatInputModule,
    MatIcon,
    FormField,
    FormRoot
],
  templateUrl: './register-modal.html',
  styleUrl: './register-modal.scss'
})
export class RegisterModal {
  private static _counter = 0;

  private readonly dialogRef = inject(MatDialogRef<RegisterModal>);
  private readonly accountManager = inject(AccountManager);
  private readonly formModel = signal<RegisterAccountModel>({
    email: '',
    password: ''
  });

  protected readonly formId = `register-modal-form-id-${RegisterModal._counter++}`;

  protected readonly registerForm = form(this.formModel, (schemaPath) => {
    // TODO: Add password validation using global service registration like in .NET.
    required(schemaPath.email, { message: 'shared.email.errors.required' });
    email(schemaPath.email, { message: 'shared.email.errors.format' });
    required(schemaPath.password, { message: 'shared.password.errors.required' });
  }, {
    submission: {
      action: async (data) => {
        console.log(data().value());
        const result = await firstValueFrom(this.accountManager.register(data().value()));
        /*
          TODO:
            1) Add HttpIntercepters for try again logic if 503 returns.
            2) Add notification if some error happens, to improve UX.
            3) Add proper error handling.
        */
        if(!result.ok) {
          // TODO: Some error happened. We could define a type for ProblemDetails and show errors? Or is it too overwhelming for a user? Maybe we could filter errors that are 400 and related to validation errors?
        }
        // TODO: Great now u are good to go? Should i redirect to login? For now i will just close the modal.
        this.cancel();
      }
    }
  });

  protected cancel(): void {
    this.dialogRef.close();
  }
}
