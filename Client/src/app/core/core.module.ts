import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainPageComponent } from './pages/main-page/main-page.component';
import {MaterialModule} from "../shared/modules/material.module";
import {BrowserModule} from "@angular/platform-browser";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";



@NgModule({
  declarations: [
    MainPageComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MaterialModule
  ],
  bootstrap: [
    MainPageComponent
  ]
})
export class CoreModule { }
