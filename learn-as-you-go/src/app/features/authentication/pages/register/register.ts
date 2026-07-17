import { Component, inject } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { TranslateModule } from '@ngx-translate/core';


@Component({
  selector: 'app-register',
  imports: [
    TranslateModule
  ],
  templateUrl: './register.html',
  styleUrl: './register.scss'
})
export class Register {
  private readonly dialogRef = inject(MatDialogRef);
}
