import { NgModule } from '@angular/core';
import { MainPageComponent } from './pages/main-page/main-page.component';
import {MaterialModule} from "../shared/modules/material.module";
import {BrowserModule} from "@angular/platform-browser";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { AboutComponent } from './pages/main-page/about/about.component';
import { OtherServiceLinkComponent } from './pages/main-page/about/other-service-link/other-service-link.component';



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
