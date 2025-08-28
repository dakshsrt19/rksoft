import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../pages/services/auth.service';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-header',
  imports: [RouterLink, CommonModule],
  templateUrl: './header.html',
  styleUrl: './header.scss'
})
export class Header implements OnInit {
user: any;
loggedIn: boolean = false;
constructor(private authService: AuthService, private router: Router  ) {}

  ngOnInit(): void {
    this.user = this.authService.getUserDetails();
    if(this.user != null)
    {
    this.loggedIn = true;
    }
  }
  logout(): void {
    // Clear localStorage/sessionStorage
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('user');

    // Optionally clear everything
    localStorage.clear();

    // Redirect to login page
    this.router.navigate(['/login']);
  }

}
