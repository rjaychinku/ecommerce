import { Injectable } from "@angular/core";
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from "@angular/common/http";
import { Observable } from "rxjs";
import { UseraccountService } from "../shared/useraccount.service";

@Injectable()
export class MycustomInterceptor implements HttpInterceptor {
  constructor(private useraccountService: UseraccountService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> | any {

    console.log("MyCustomInterceptor ran!");

    return next.handle(request);
  }
}
