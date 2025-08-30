import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes';
import { APP_INITIALIZER } from '@angular/core';
import { App } from './app/app';
import { AppConfigService } from './app/pages/services/app-config.service';

export function initializeApp(appConfig: AppConfigService) {
  return () => appConfig.loadConfig();
}

bootstrapApplication(App, {
  providers: [
    provideRouter(routes),
    provideHttpClient(),
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [AppConfigService],
      multi: true
    }
  ]
}).catch(err => console.error(err));
