import {Component} from '@angular/core';
import {CommonModule} from '@angular/common';
import {Router, RouterLink, RouterModule} from '@angular/router';

@Component({
  selector: 'app-support-person-sidebar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterModule],
  templateUrl: './support-person-sidebar.component.html',
  styleUrls: ['./support-person-sidebar.component.scss']
})

export class SupportPersonSidebarComponent{
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

    this.router.navigate(['/login']);
  }
}
