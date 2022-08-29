import {Component, Inject, OnInit} from '@angular/core';
import {AccessType} from "../../models/access-type.enum";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {MAT_DIALOG_DATA} from "@angular/material/dialog";

@Component({
  selector: 'app-settings-dialog',
  templateUrl: './settings-dialog.component.html',
  styleUrls: ['./settings-dialog.component.scss']
})
export class SettingsDialogComponent {

  public AccessTypes = AccessType;

  public fg = new FormGroup({
    title: new FormControl(this.data.title, [Validators.minLength(4), Validators.maxLength(30), Validators.required]),
    accessType: new FormControl(this.data.accessType, [Validators.required])
  });

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {  }

}
