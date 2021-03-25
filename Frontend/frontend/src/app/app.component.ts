import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  updated: boolean = false;
  title = 'frontend';

  onListUpdated(isUpdated: boolean) {
    this.updated = isUpdated;
  }
}
