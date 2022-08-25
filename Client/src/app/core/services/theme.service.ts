import {Injectable} from '@angular/core';
import {BehaviorSubject} from "rxjs";
import {OverlayContainer} from "@angular/cdk/overlay";

@Injectable({providedIn: 'root'})
export class ThemeService {

  theme$ = new BehaviorSubject<'dark' | 'light'>('dark');
  private readonly ThemeKey = "Theme";

  public constructor(private overlay: OverlayContainer) {
    this.overlay.getContainerElement().classList.add('dark-theme');
  }

  public tryGetThemeFromSettings() {
    var storedTheme = localStorage.getItem(this.ThemeKey);
    if (!!storedTheme && storedTheme !== this.theme$.value) {
      this.changeTheme();
    }

  }

  public changeTheme(): void {
    if (this.theme$.value === 'light') {
      this.theme$.next( 'dark');
      this.overlay.getContainerElement().classList.add('dark-theme');
      this.overlay.getContainerElement().classList.remove('light-theme');
    } else {
      this.theme$.next('light');
      this.overlay.getContainerElement().classList.remove('dark-theme');
      this.overlay.getContainerElement().classList.add('light-theme');
    }
    localStorage.setItem(this.ThemeKey, this.theme$.value);


  }
}
