import {Component} from '@angular/core';
import {CommonModule} from '@angular/common';
import {Router, RouterLink, RouterModule} from '@angular/router';

@Component({
  selector: 'app-consumer-sidebar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterModule],
  templateUrl: './consumer-sidebar.component.html',
  styleUrls: ['./consumer-sidebar.component.scss']
})

export class ConsumerSidebarComponent{
  sidebarOpen: boolean = true;

  constructor(private router: Router) {
  }

  toggleSidebar(): void {
    this.sidebarOpen = !this.sidebarOpen;
  }

  logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("refreshToken");
    localStorage.removeItem("userId");
    localStorage.removeItem("role");
    localStorage.removeItem("tenantId");
  }
}
