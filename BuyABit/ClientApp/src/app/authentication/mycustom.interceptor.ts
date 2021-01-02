import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UseraccountService } from '../shared/useraccount.service';

@Injectable()
export class MycustomInterceptor implements HttpInterceptor {
    constructor(private useraccountService: UseraccountService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> | any {
        // add auth header with jwt if user is logged in and request is to the api url
       // const isLoggedIn = this.useraccountService.isAuthenticated();
      const jwtToken = this.useraccountService.getToken();
      console.log('MyCustomInterceptor ran!');
        //const isLoggedIn = user && user.jwtToken;
       // const isApiUrl = request.url.startsWith(environment.apiUrl);
        //if (jwtToken) {
        //    request = request.clone({
        //        setHeaders: { Authorization: `Bearer ${jwtToken}` }
        //    });
        //}

        return next.handle(request);
    }
}
