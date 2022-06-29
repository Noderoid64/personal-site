import { Component} from '@angular/core';
import {ThemeService} from "../../services/theme.service";
import {Observable} from "rxjs";

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']
})
export class MainPageComponent {

  public theme$: Observable<string>;

  constructor(private themeService: ThemeService) {
    this.theme$ = themeService.theme$;
  }

  public onThemeChangeClick(): void {
    this.themeService.changeTheme();
  }
}
