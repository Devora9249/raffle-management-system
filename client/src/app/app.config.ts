import { ApplicationConfig, ErrorHandler } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptorsFromDi, HTTP_INTERCEPTORS } from '@angular/common/http';
import { providePrimeNG } from 'primeng/config';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { routes } from './app.routes';
import { AuthInterceptor } from './core/Interceptors/authInterceptor';
import { HttpErrorInterceptor } from './core/Interceptors/HTTPErrorInterceptor';
import { GlobalErrorHandler } from './core/Errors/globalErrorHendler';
import Lara from '@primeng/themes/lara';
import Material from '@primeng/themes/material';
import Nora from '@primeng/themes/nora';
import Aura from '@primeng/themes/aura';
import {mainDesign} from '../app/styles/mainDesign';
import { MessageService } from 'primeng/api';
import { ConfirmationService } from 'primeng/api';

export const appConfig: ApplicationConfig = {

  providers: [
    // Animations
    provideAnimationsAsync(),

    // HttpClient + Interceptors
    provideHttpClient(withInterceptorsFromDi()),
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true },

    // Global Error Handling
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    { provide: ErrorHandler, useClass: GlobalErrorHandler },

    // Router
    provideRouter(routes),

    // UI / Theme
    providePrimeNG({
      theme: mainDesign
    }),
    MessageService,
    ConfirmationService
  ]
};
