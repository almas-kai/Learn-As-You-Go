import { Component, signal } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { TranslateModule } from '@ngx-translate/core';
import { DynamicButton } from './local-components/dynamic-button/dynamic-button';

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
  protected readonly isExpanded = signal(false);

  protected toggleMenu(): void {
    this.isExpanded.update(prevValue => !prevValue);
  }
}
