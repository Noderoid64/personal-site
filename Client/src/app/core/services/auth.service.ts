import {Injectable} from '@angular/core';
import {BehaviorSubject, empty, Observable, tap} from "rxjs";

import {User} from "../models/user";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {map} from "rxjs/operators";

@Injectable({providedIn: 'root'})
export class AuthService {

  public user$ = new BehaviorSubject<User | null>(null);
  private userKey = 'user';

  constructor(private http: HttpClient) {
  }

  public signIn(): void {

  }

  public refreshToken(): Observable<any> {
    const user = JSON.parse(localStorage.getItem(this.userKey) ?? '');
    if (user.refreshToken) {
      return this.http.post<User>(environment.serverUri + '/auth/refresh?refreshToken=' + user.refreshToken,null)
        .pipe(map(x => {
          localStorage.setItem(this.userKey, JSON.stringify(x));
          this.user$.next(x);
          return x.token;
        }));
    }
    return empty();
  }

  public signInByGoogle(): void {
    window.location.assign("https://accounts.google.com/o/oauth2/v2/auth?scope=profile&access_type=offline&response_type=code&redirect_uri=http://localhost:4200/auth&client_id=714351833041-ohb8v036d4efaoo6g0bs38us42ff4v8r.apps.googleusercontent.com");
  }

  public tryContinueGoogleSignIn(): void {
    const uri = window.location.search;
    const user = localStorage.getItem(this.userKey);
    if (uri.startsWith('?code') && !this.user$.value && !user) {
      const authCode = uri.substring(6, uri.indexOf('&'));
      this.http.post<User>(environment.serverUri + '/auth/google?code=' + authCode, null)
        .subscribe(obj=> {
          localStorage.setItem(this.userKey, JSON.stringify(obj));
            this.user$.next(obj);
        });
    } else if (user) {
      this.user$.next(JSON.parse(user));
    }
  }

  public logout(): void {
    localStorage.removeItem(this.userKey);
    this.user$.next(null);
  }
}
