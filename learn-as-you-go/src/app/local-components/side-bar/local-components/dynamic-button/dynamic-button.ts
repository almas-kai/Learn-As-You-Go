import { Component, input, output } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltip } from '@angular/material/tooltip';

@Component({
  selector: 'app-dynamic-button',
  imports: [
    TranslateModule,
    MatIconModule,
    MatButtonModule,
    MatTooltip
  ],
  templateUrl: './dynamic-button.html',
  styleUrl: './dynamic-button.scss'
})
export class DynamicButton {
  public readonly isExtended = input.required<boolean>();
  public readonly iconName = input.required<string>();
  public readonly labelKey = input.required<string>();
  public readonly activate = output<void>();

  protected onClick(): void {
    this.activate.emit();
  }
}
