import { ChangeDetectionStrategy, Component, input, output } from '@angular/core';
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
  styleUrl: './dynamic-button.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DynamicButton {
  public isExtended = input.required<boolean>();
  public iconName = input.required<string>();
  public labelKey = input.required<string>();
  public activate = output<void>();

  protected onClick(): void {
    this.activate.emit();
  }
}
