import {Injectable} from '@angular/core';
import {FirebaseService} from "./firebase.service";
import {User} from "../models/user";
import {BehaviorSubject} from "rxjs";

@Injectable({providedIn: 'root'})
export class AuthService {

  public user$ = new BehaviorSubject<User | null>(null);

  constructor(private firebase: FirebaseService) {
  }

  public signIn(): void {
    this.firebase.SignInViaGoogle().then(x => {
      this.user$.next(x);
      console.log(x);
    }).catch(e => {
      this.user$.next(null);
      console.error(e);
    })
  }

  public logout(): void {
    this.user$.next(null);
  }
}
