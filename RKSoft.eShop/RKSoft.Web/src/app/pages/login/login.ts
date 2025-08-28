import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "../services/auth.service";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

@Component({
  selector: "app-login",
  imports: [CommonModule, FormsModule],
  templateUrl: "./login.html",
  styleUrls: ["./login.scss"],
})
export class Login {
  username: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) { }

  onLogin(): void {
    if (!this.username || !this.password) {
      this.errorMessage = 'UserName and password are required.';
      return;
    }

    this.errorMessage = '';
    this.authService.login(this.username, this.password).subscribe({
      next: () => {
        console.log('Login success, navigating to dashboard...');
        this.router.navigate(['/dashboard']);
      },
      error: (err: { error: string }) => {
        this.errorMessage = err?.error || 'Login failed';
      }
    });
  }
}
