import { Component, OnInit, OnDestroy } from '@angular/core'; 
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';
import { MenuModule } from 'primeng/menu';
import { ButtonModule } from 'primeng/button';
import { AuthService } from '../../../core/services/auth-service';
import { Subscription } from 'rxjs'; 

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [CommonModule, MenubarModule, MenuModule, ButtonModule],
  templateUrl: './nav.html',
  styleUrl: './nav.scss',
})
export class Nav implements OnInit, OnDestroy { 

  menuItems: MenuItem[] = [];
  userMenuItems: MenuItem[] = [];

  isLoggedIn = false;

  private authSub!: Subscription; 

  constructor(private router: Router, private authService: AuthService) {}

  ngOnInit(): void {
    this.buildMainMenu();

    //  מאזינים לשינויים במצב התחברות
    this.authSub = this.authService.loggedIn$.subscribe(isLoggedIn => {
      this.isLoggedIn = isLoggedIn;
      this.buildUserMenu();
    });
  }

  ngOnDestroy(): void { 
    this.authSub?.unsubscribe();
  }

//     MAIN MENU

  private buildMainMenu(): void {
    this.menuItems = [
      {
        label: 'Home',
        icon: 'pi pi-home',
        routerLink: '/'
      },
      {
        label: 'Gifts',
        icon: 'pi pi-gift',
        routerLink: '/gifts'
      }
    ];
  }

//     USER MENU

  private buildUserMenu(): void {
    this.userMenuItems = this.isLoggedIn
      ? [
          {
            label: 'Logout',
            icon: 'pi pi-sign-out',
            command: () => this.logout()
          }
        ]
      : [
          {
            label: 'Register',
            icon: 'pi pi-user-plus',
            routerLink: '/register'
          },
          {
            label: 'Login',
            icon: 'pi pi-sign-in',
            routerLink: '/login'
          }
        ];
  }

    // ACTIONS

  private logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
