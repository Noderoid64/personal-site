import { NgModule } from '@angular/core';
import { MainPageComponent } from './pages/main-page/main-page.component';
import {MaterialModule} from "../shared/modules/material.module";
import {BrowserModule} from "@angular/platform-browser";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { AboutComponent } from './pages/main-page/about/about.component';
import { OtherServiceLinkComponent } from './pages/main-page/about/other-service-link/other-service-link.component';
import { ProjectsComponent } from './pages/main-page/projects/projects.component';
import { ProjectCardComponent } from './pages/main-page/projects/project-card/project-card.component';
import { ProfileComponent } from './components/profile/profile.component';
import { HttpClientModule} from "@angular/common/http";





@NgModule({
  declarations: [
    MainPageComponent,
    AboutComponent,
    OtherServiceLinkComponent,
    ProjectsComponent,
    ProjectCardComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule
  ],
  bootstrap: [
    MainPageComponent
  ]
})
export class CoreModule { }
