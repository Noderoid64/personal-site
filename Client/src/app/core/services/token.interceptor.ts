import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import {Observable, take} from 'rxjs';
import {AuthService} from "./auth.service";

@Injectable({providedIn: 'root'})
export class JwtInterceptor implements HttpInterceptor {
  constructor(private auth: AuthService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // add auth header with jwt if account is logged in and request is to the api url
    let token;
    this.auth.user$.pipe(take(1)).subscribe(x => token = x?.token);
    console.log('sdf');
    if (token) {
        request = request.clone({
          setHeaders: { Authorization: `Bearer ${token}` }
        });
    }

    return next.handle(request);
  }
}
