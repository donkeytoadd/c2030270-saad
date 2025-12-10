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

  email = "";
  password = "";
  tenants: any[] = [];
  selectedTenantId: number;

  isEmailVerified: boolean = false;
  emailError = "";
  errorMessage = "";
  showPassword = false;

  constructor(private router: Router, private auth: AuthService) {}

  findTenants() {
    this.emailError = "";

    if (!this.email.trim()) {
      this.emailError = "Email is required.";
      return;
    }

    this.auth.findTenants(this.email).subscribe({
      next: tenants => {
        this.tenants = tenants;
        this.isEmailVerified = true;

        if (tenants.length === 1) {
          this.selectedTenantId = tenants[0].tenantId;
        }
      },
      error: () => {
        this.emailError = "No accounts found with this email.";
        this.isEmailVerified = false;
      }
    });
  }

  login() {
    if (!this.isEmailVerified) {
      this.errorMessage = "Please verify your email before logging in.";
      return;
    }

    if (!this.canShowPassword) {
      this.errorMessage = "Please select an organisation.";
      return;
    }

    if (!this.password.trim()) {
      this.errorMessage = "Password is required.";
      return;
    }

    this.auth.login(this.email, this.password, this.selectedTenantId).subscribe({
      next: data => {
        if (data.role === "Consumer") this.router.navigate(['/consumer-dashboard']);
        else if (data.role === "SupportPerson") this.router.navigate(['/support-person-dashboard']);
        else this.errorMessage = "Unknown role.";
      },
      error: () => {
        this.errorMessage = "Invalid password.";
      }
    });
  }

  onEmailChange() {
    this.isEmailVerified = false;
    this.tenants = [];
    this.selectedTenantId = 0;
    this.password = "";
    this.errorMessage = "";
  }

  canShowPassword() {
    return (this.tenants.length === 1 || (this.tenants.length > 1 && this.selectedTenantId));
  }

  togglePassword() {
    this.showPassword = !this.showPassword;
  }
}
