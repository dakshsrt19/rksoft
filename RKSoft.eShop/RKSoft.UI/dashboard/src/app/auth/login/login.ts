import { Component, inject, Inject } from "@angular/core";
import { Router } from "@angular/router";

@Component({
  selector: "app-login",
  imports: [],
  templateUrl: "./login.html",
  styleUrl: "./login.css",
})
export class Login {
  router = inject(Router);
  
  onSignIn() {
    this.router.navigateByUrl("/dashboard");
  }
}
