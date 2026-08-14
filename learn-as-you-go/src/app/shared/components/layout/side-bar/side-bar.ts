import { Component, inject, signal } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { TranslateModule } from '@ngx-translate/core';
import { DynamicButton } from './local-components/dynamic-button/dynamic-button';
import { MatDialog } from '@angular/material/dialog';
import { RegisterModal } from '@features/authentication/register-modal/register-modal';

@Component({
  selector: 'app-side-bar',
  imports: [
    MatIconModule,
    MatButtonModule,
    TranslateModule,
    DynamicButton
  ],
  templateUrl: './side-bar.html',
  styleUrl: './side-bar.scss',
  host: {
    '[class.expanded]': 'isExpanded()'
  }
})
export class SideBar {
  private readonly dialog = inject(MatDialog);

  protected readonly isExpanded = signal(false);

  protected toggleMenu(): void {
    this.isExpanded.update(prevValue => !prevValue);
  }

  protected authenticate(): void {
    // TODO: Right now it is just registration, when login is added we shall call LogIn? Or dynamically decide which one to call, for that we need to know if a user is registered or not...
    this.dialog.open(RegisterModal);
  }
}
