import { Component } from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {MatDialog} from "@angular/material/dialog";
import {LoginDialogComponent} from "../login-dialog/login-dialog.component";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {

  constructor(public auth: AuthService, public dialog: MatDialog) {

  }

  public onLogin(): void {
    const dialogRef = this.dialog.open(LoginDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result === 'google') {
        this.auth.signInByGoogle();
      }
    });
  }

  public onLogOut(): void {
    this.auth.logout();
  }

}
