import { Routes } from '@angular/router';
import { GiftsPage } from './features/gifts/gifts-page/gifts-page';
import { Register } from './features/auth/register/register';
import { Login } from './features/auth/login/login';

export const routes: Routes = [
    // {path:'', component:Home},
    { path: 'gifts', component: GiftsPage },
    { path: 'register', component: Register },
    { path: 'login', component: Login },
    // {
    //     path: 'cars', component: CarsWrapper,
    //     children: [
    //         { path: '', redirectTo: 'list', pathMatch: 'full' },
    //         { path: 'list', component: CarList },
    //         { path: 'add', component: CarReactive },
    //         { path: 'edit/:id', component: CarReactive }
    //     ]
    // },
    // { path: '**', component: NotFound }
];
