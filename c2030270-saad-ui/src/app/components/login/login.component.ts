import { Component } from '@angular/core';
import { Router } from '@angular/router';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [
    FormsModule
  ],
  templateUrl: 'login.component.html',
  styleUrl: 'login.component.scss'
})
export class LoginComponent {
  email = '';
  password = '';
  loading = false;

  constructor(private router: Router) {}

  onSubmit() {
    this.loading = true;

    // Simulated login delay
    setTimeout(() => {
      this.loading = false;

      // Store a fake flag to indicate the user is logged in
      localStorage.setItem('isLoggedIn', 'true');

      this.router.navigate(['/dashboard']);
    }, 1000);
  }
}
