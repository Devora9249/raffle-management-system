// src/app/app.config.ts
import { ApplicationConfig, ErrorHandler, Injectable } from '@angular/core';
import { provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { HTTP_INTERCEPTORS, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { catchError, throwError, Observable } from 'rxjs';

import { routes } from './app.routes';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeuix/themes/aura';
import mainDesign from './theme/mainDesign';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';


// ----------------------
// Global Error Handler
// ----------------------
@Injectable({ providedIn: 'root' })
export class GlobalErrorHandler implements ErrorHandler {
  handleError(error: any): void {
    console.error('Global Error:', error);
  }
}

// ----------------------
// HTTP Error Interceptor
// ----------------------
@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((err: HttpErrorResponse) => {
        console.error('HTTP Error:', {
          status: err.status,
          statusText: err.statusText,
          url: err.url,
          error: err.error
        });
        return throwError(() => err);
      })
    );
  }
}

// ----------------------
// Application Config
// ----------------------
export const appConfig: ApplicationConfig = {
  providers: [
    provideAnimationsAsync(),
    // Global error handling
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    { provide: ErrorHandler, useClass: GlobalErrorHandler },

    // HTTP interceptor
    { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true },

    // Router
    provideRouter(routes),
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