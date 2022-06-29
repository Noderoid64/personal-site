import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainPageComponent } from './pages/main-page/main-page.component';
import {MaterialModule} from "../shared/modules/material.module";
import {BrowserModule} from "@angular/platform-browser";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { AboutComponent } from './pages/about/about.component';
import { OtherServiceLinkComponent } from './pages/about/other-service-link/other-service-link.component';



@NgModule({
  declarations: [
    MainPageComponent,
    AboutComponent,
    OtherServiceLinkComponent
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
