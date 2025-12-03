import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import {NgClass} from '@angular/common';
import {AuthService} from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  imports: [FormsModule, NgClass],
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  email: string = "";
  password: string = "";
  rememberMe: boolean = false;
  showPassword: boolean = false;
  emailError: string = "";
  passwordError: string = "";
  errorMessage: string = "";
  formValid: boolean = false;

  constructor(private router: Router, private authService: AuthService) { }

  togglePassword(): void {
    this.showPassword = !this.showPassword;
  }

  validateEmail() {
    if (!this.email) {
      this.emailError = "Email is required.";
    } else if (!/^\S+@\S+\.\S+$/.test(this.email)) {
      this.emailError = "Please enter a valid email address.";
    } else {
      this.emailError = "";
    }
    this.updateFormValidity();
  }

  validatePassword() {
    if (!this.password) {
      this.passwordError = "Password is required.";
    } else if (this.password.length < 6) {
      this.passwordError = "Password must be at least 6 characters.";
    } else {
      this.passwordError = "";
    }
    this.updateFormValidity();
  }

  updateFormValidity() {
    this.formValid = !this.emailError && !this.passwordError && !!this.email && !!this.password;
  }

  onSubmit(): void {
    this.validateEmail();
    this.validatePassword();

    if (!this.formValid) return;

    this.authService.login(this.email, this.password).subscribe({
      next: (response) => {
        const role = response.role;

        if (role === 'Consumer')
          this.router.navigate(['/consumer-dashboard']);

        else if (role === 'SupportPerson')
          this.router.navigate(['/support-person-dashboard']);

        // else if (role === 'Admin')
        //   this.router.navigate(['/admin-dashboard']);

        else
          this.errorMessage = "Unknown role returned by server.";
      },

      error: () => {
        this.errorMessage = "Invalid email or password.";
      }
    });
  }
}
