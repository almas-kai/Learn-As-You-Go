import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Footer } from '@layout-components/footer/footer';
import { Header } from '@layout-components/header/header';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    Header,
    Footer
  ],
  templateUrl: './app.html',
  styleUrl: './app.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class App {
}
