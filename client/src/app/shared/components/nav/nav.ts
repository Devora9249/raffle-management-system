import { Component, OnInit, OnDestroy } from '@angular/core'; 
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';
import { MenuModule } from 'primeng/menu';
import { ButtonModule } from 'primeng/button';
import { AuthService } from '../../../core/services/auth-service';
import { Subscription } from 'rxjs'; 
import { CartDrawerService } from '../../../core/services/CartDrawerService ';
import { NotificationService } from '../../../core/services/notification-service';
@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [CommonModule, MenubarModule, MenuModule, ButtonModule, RouterModule],
  templateUrl: './nav.html',
  styleUrl: './nav.scss',
})
export class Nav implements OnInit, OnDestroy { 

  menuItems: MenuItem[] = [];
  userMenuItems: MenuItem[] = [];

  isLoggedIn = false;
  isAdmin = false;   
isDonor = false; 

  private authSub!: Subscription; 
  private roleSub!: Subscription;

  constructor(private router: Router, private authService: AuthService, private cartDrawerService: CartDrawerService,private notificationService: NotificationService) {}

  ngOnInit(): void {
    this.buildMainMenu();

    //  מאזינים לשינויים במצב התחברות
    this.authSub = this.authService.loggedIn$.subscribe(isLoggedIn => {
      this.isLoggedIn = isLoggedIn;
      if (isLoggedIn) {
      this.subscribeToRoles();   
    } else {
      this.isAdmin = false;      
      this.isDonor = false;      
      this.buildMainMenu();      
    }

      this.buildUserMenu();
    });
  }

ngOnDestroy(): void {
  this.authSub?.unsubscribe();
  this.roleSub?.unsubscribe();
}


//     MAIN MENU

private buildMainMenu(): void {
  const items: MenuItem[] = [
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

  if (this.isLoggedIn && this.isAdmin) {
    items.push({
      label: 'Admin',
      icon: 'pi pi-cog',
      routerLink: '/admin'
    });
  }

  if (this.isLoggedIn && this.isDonor) {
    items.push({
      label: 'Donor',
      icon: 'pi pi-user',
      routerLink: '/donor'
    });
  }

  this.menuItems = items;
}


//USER MENU

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
    this.notificationService.showSuccess('Logged out successfully');
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  private subscribeToRoles(): void {
  this.roleSub?.unsubscribe();

  this.roleSub = new Subscription();


    
  this.roleSub.add(
    this.authService.isAdmin$.subscribe(isAdmin => {
      this.isAdmin = isAdmin;
      this.buildMainMenu();
    })
  );

  this.roleSub.add(
    this.authService.isDonor$.subscribe(isDonor => {
      this.isDonor = isDonor;
      this.buildMainMenu();
    })
  );
}

openCart(): void {
  this.cartDrawerService.open();
}

}
