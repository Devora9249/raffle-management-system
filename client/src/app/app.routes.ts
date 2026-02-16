import { Routes } from '@angular/router';
import { GiftsPage } from './features/gifts/gifts-page/gifts-page';
import { Register } from './features/auth/register/register';
import { LoginComponent } from './features/auth/login/login';
import { DonorPage } from './features/donors/donor-page/donor-page';
import { HomePage } from './features/home/home-page/home-page';
import { AdminPage } from './features/admin/admin-page/admin-page';
import { DonorGuard } from './core/guards/DonorGuard';
import { AdminGuard } from './core/guards/AdminGuard';
import { PaymentPage } from './features/cart/payment-page/payment-page';
import { WinnigsPage } from './features/winnigs-page/winnigs-page';
export const routes: Routes = [
    { path: '', component: HomePage },
    { path: 'gifts', component: GiftsPage },
    { path: 'register', component: Register },
    { path: 'login', component: LoginComponent },
    { path: 'donor', component: DonorPage, canActivate: [DonorGuard] },
    { path: 'admin', component: AdminPage, canActivate: [AdminGuard] },
    { path: 'payment', component: PaymentPage },
    { path: 'winnings', component: WinnigsPage },
];