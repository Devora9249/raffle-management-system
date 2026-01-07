// // src/app/app.config.ts
// import { ApplicationConfig, ErrorHandler, Injectable } from '@angular/core';
// import { provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
// import { provideRouter } from '@angular/router';
// import { HTTP_INTERCEPTORS, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpErrorResponse } from '@angular/common/http';
// import { catchError, throwError, Observable } from 'rxjs';

// import { routes } from './app.routes';
// import { providePrimeNG } from 'primeng/config';
// import mainDesign from './theme/mainDesign';
// import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
// import { AuthInterceptor } from './core/Interceptors/authInterceptor';
// import { HttpErrorInterceptor } from './core/Interceptors/HTTPErrorInterceptor';
// import { GlobalErrorHandler } from './core/Errors/globalErrorHendler';






// // Application Config

// export const appConfig: ApplicationConfig = {
//   providers: [
//     provideAnimationsAsync(),
//     // Global error handling
//     provideBrowserGlobalErrorListeners(),
//     provideZoneChangeDetection({ eventCoalescing: true }),
//     //error handler
//     { provide: ErrorHandler, useClass: GlobalErrorHandler },

//     // HTTP interceptor
//     { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true },
//     //auth Interceptor
//     { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },

//     // Router
//     provideRouter(routes),
//     providePrimeNG({
//       theme: {
//         preset: mainDesign,
//         options: {
//           darkModeSelector: false
//         }
//       }
//     })
//   ]
// };



import { ApplicationConfig, ErrorHandler } from '@angular/core';
import { provideRouter } from '@angular/router';
import {
  provideHttpClient,
  withInterceptorsFromDi,
  HTTP_INTERCEPTORS
} from '@angular/common/http';
import { providePrimeNG } from 'primeng/config';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';

import { routes } from './app.routes';
import mainDesign from './theme/mainDesign';

import { AuthInterceptor } from './core/Interceptors/authInterceptor';
import { HttpErrorInterceptor } from './core/Interceptors/HTTPErrorInterceptor';
import { GlobalErrorHandler } from './core/Errors/globalErrorHendler';

export const appConfig: ApplicationConfig = {
  providers: [
    // ----------------------
    // Animations
    // ----------------------
    provideAnimationsAsync(),

    // ----------------------
    // HttpClient + Interceptors
    // ----------------------
    provideHttpClient(withInterceptorsFromDi()),
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true },

    // ----------------------
    // Global Error Handling
    // ----------------------
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    { provide: ErrorHandler, useClass: GlobalErrorHandler },

    // ----------------------
    // Router
    // ----------------------
    provideRouter(routes),

    // ----------------------
    // UI / Theme
    // ----------------------
    providePrimeNG({
      theme: {
        preset: mainDesign,
        options: {
          darkModeSelector: false
        }
      }
    })
  ]
};
