import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

//import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { CarouselComponent } from './carousel/carousel.component';
import { FooterComponent } from './footer/footer.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { ProductComponent } from './product/product.component';
import { UseraccountComponent } from './useraccount/useraccount.component';
import { MycustomInterceptor } from './authentication/mycustom.interceptor';
import { ErrorInterceptor } from './authentication/error.interceptor';
import { AuthGuard } from './authentication/auth.guard';
import { JwtModule } from '@auth0/angular-jwt';

export function getToken() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    CarouselComponent,
    FooterComponent,
    CheckoutComponent,
    ProductComponent,
    UseraccountComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    //ToastrModule.forRoot({
    //  progressBar: true
    //}),
    FormsModule,
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
         tokenGetter: getToken,
        allowedDomains: ["https://localhost:5001/"],
        disallowedRoutes: ["https://localhost:5002/"],
      },
    }),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full'},
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'checkout', component: CheckoutComponent, canActivate: [AuthGuard] },
      { path: 'account', component: UseraccountComponent },
      { path: 'home', component: HomeComponent },
          // otherwise redirect to home
      { path: '**', redirectTo: '' }
    ])
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: MycustomInterceptor, multi: true }],
    // { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
