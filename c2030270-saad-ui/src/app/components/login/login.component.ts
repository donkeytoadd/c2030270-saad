import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { NgClass } from '@angular/common';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, NgClass],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  step: number = 1;

  email: string = "";
  password: string  = "";

  emailError: string = "";
  errorMessage: string = "";

  tenants: any[] = [];
  selectedTenantId: number | null = null;

  showPassword: boolean = false;

  constructor(private router: Router, private auth: AuthService) {}

  findTenants() {
    this.emailError = "";

    if (!this.email.trim()) {
      this.emailError = "Email is required.";
      return;
    }

    this.auth.findTenants(this.email).subscribe({
      next: (tenants) => {
        this.tenants = tenants;
        this.step = 2;
      },
      error: () => {
        this.emailError = "No accounts found with this email.";
      }
    });
  }

  login() {
    if (!this.selectedTenantId) {
      this.errorMessage = "Please select which organisation you want to log into.";
      return;
    }

    if (!this.password.trim()) {
      this.errorMessage = "Password is required.";
      return;
    }

    this.auth.login(this.email, this.password, this.selectedTenantId).subscribe({
      next: res => {
        if (res.role === "Consumer") {
          this.router.navigate(['/consumer-dashboard']);
        }
        else if (res.role === "Staff") {
          this.router.navigate(['/support-person-dashboard']);
        }
        else {
          this.errorMessage = "Unknown role returned.";
        }
      },
      error: () => {
        this.errorMessage = "Invalid password.";
      }
    });
  }

  togglePassword() {
    this.showPassword = !this.showPassword;
  }

  goBack() {
    this.step = 1;
    this.password = "";
    this.errorMessage = "";
  }
}
