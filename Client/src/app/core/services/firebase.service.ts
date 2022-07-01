import { initializeApp } from "firebase/app";
import { GoogleAuthProvider, signInWithPopup, getAuth } from "firebase/auth";

import {Injectable} from '@angular/core';
import {environment} from "../../../environments/environment";

@Injectable({providedIn: 'root'})
export class FirebaseService {

  private app = initializeApp(environment.firebaseConfig);
  private provider = new GoogleAuthProvider();
  private auth = getAuth();

  constructor() {
  }

  public async SignInViaGoogle(): Promise<any> {
    try {
      var result = await signInWithPopup(this.auth, this.provider);
      const credential = GoogleAuthProvider.credentialFromResult(result);
      if (credential) {
        return result.user;
      }
    } catch (e) {
      console.error(e);
    }
    return null;
  }
}
