import { variable } from '@angular/compiler/src/output/output_ast';
import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}
export function getApplicationSettings() {
    var ex = {
    USER_IDLE_SECONDS: Number(3600),
    ANOTHER_SETTING: "WHATEVER"
  };

  return ex;
}

const providers = [
  {
    provide: 'BASE_URL', useFactory: getBaseUrl, deps: [],
  },
  { provide: 'Application_Settings', useFactory: getApplicationSettings, deps: [] }
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.log(err));
