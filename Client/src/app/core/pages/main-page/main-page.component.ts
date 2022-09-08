import { Component } from '@angular/core';
import {Observable} from "rxjs";
import {ThemeService} from "../../services/theme.service";
import {AuthService} from "../../services/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']
})
export class MainPageComponent {

  public theme$: Observable<string>;

  constructor(private themeService: ThemeService, authService: AuthService, public router : Router) {
    this.themeService.tryGetThemeFromSettings();
    this.theme$ = themeService.theme$;
    authService.tryContinueGoogleSignIn();
    console.log("Console");
  }

  public onThemeChangeClick(): void {
    this.themeService.changeTheme();
  }
}
