import {Injectable} from '@angular/core';
import {BehaviorSubject} from "rxjs";

@Injectable({providedIn: 'root'})
export class ThemeService {

  theme$ = new BehaviorSubject<'dark' | 'light'>('dark');

  public changeTheme(): void {
    this.theme$.next(this.theme$.value === 'light' ? 'dark' : 'light');
  }
}
