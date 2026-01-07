// import { bootstrapApplication } from '@angular/platform-browser';
// import { App } from './app/app';
// import { provideHttpClient } from '@angular/common/http';
// import { providePrimeNG } from 'primeng/config';
// import { ApplicationConfig } from '@angular/core';
// import Aura from '@primeuix/themes/aura';
// import { routes } from './app/app.routes';
// import { provideRouter } from '@angular/router';
// import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

// bootstrapApplication(App, {
//   providers: [
//     provideAnimationsAsync(),
//     provideHttpClient(),
//     provideRouter(routes), 
//     providePrimeNG({
//       theme: {
//         preset: Aura
//       }
//     })
//   ]
// }).catch(err => console.error(err));

import { bootstrapApplication } from '@angular/platform-browser';
import { App } from './app/app';
import { appConfig } from './app/app.config';

bootstrapApplication(App, appConfig)
  .catch(err => console.error(err));
