import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-dashboard',
  imports: [MatIconModule, MatButtonModule],
  templateUrl: './dashboard.component.html'
})

export class DashboardComponent {}