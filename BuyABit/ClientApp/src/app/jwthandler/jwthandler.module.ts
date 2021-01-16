import { NgModule } from '@angular/core';
import { JwtModule } from '@auth0/angular-jwt';

export function getToken() {
  return localStorage.getItem("token");
}

const jwtConfig = JwtModule.forRoot({
  config: {
    tokenGetter: getToken,
    allowedDomains: ["https://localhost:5001/"],
    disallowedRoutes: ["https://localhost:5002/"],
  }
})

@NgModule({
  imports: [jwtConfig],
  exports: [JwtModule]
})

export class JwthandlerModule { }
