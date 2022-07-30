import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import {catchError, Observable, of, switchMap, take, throwError} from 'rxjs';
import {AuthService} from "./auth.service";
import {MatSnackBar} from "@angular/material/snack-bar";

@Injectable({providedIn: 'root'})
export class JwtInterceptor implements HttpInterceptor {
  constructor(private auth: AuthService, private _snackBar: MatSnackBar) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // add auth header with jwt if account is logged in and request is to the api url
    let token;
    this.auth.user$.pipe(take(1)).subscribe(x => token = x?.token);
    if (token) {
        request = request.clone({
          setHeaders: { Authorization: `Bearer ${token}` }
        });
    }

    return next.handle(request).pipe(catchError((err) => {
      if (err instanceof HttpErrorResponse) {
        if (err.status === 401) {
          return this.auth.refreshToken().pipe(switchMap(x => {
            request = request.clone({
              setHeaders: { Authorization: `Bearer ` + x }
            })
            return next.handle(request);
          }), catchError((err) => {
            this._snackBar.open(err);
            this.auth.logout();
            return of(err);
          }));
        }
      }
      return throwError(err.error);
    }));
  }
}
