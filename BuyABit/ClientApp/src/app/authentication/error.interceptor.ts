import { Injectable } from "@angular/core";
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";

import { UseraccountService } from "../shared/useraccount.service";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private UseraccountService: UseraccountService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((err) => {
        if ([400, 403].includes(err.status)) {
          // auto logout if 401 or 403 response returned from api
          console.log("not authenticated!!!!");
          this.UseraccountService.logout();
        }

        const error = (err && err.error && err.error.message) || err.statusText;
        console.error(err);
        return throwError(error);
      })
    );
  }
}
