import { bootstrapApplication } from '@angular/platform-browser';
import { App } from './app/app';
import { provideHttpClient } from '@angular/common/http';
import { providePrimeNG } from 'primeng/config';
import { ApplicationConfig } from '@angular/core';
import Aura from '@primeuix/themes/aura';

bootstrapApplication(App, {
  providers: [
    provideHttpClient(),
    providePrimeNG({
      theme: {
        preset: Aura
      }
    })
  ]
}).catch(err => console.error(err));

