import { NgModule } from '@angular/core';
import { DeveloperPageComponent } from './pages/developer-page/developer-page.component';
import {MaterialModule} from "../shared/modules/material.module";
import {BrowserModule} from "@angular/platform-browser";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { AboutComponent } from './pages/developer-page/about/about.component';
import { OtherServiceLinkComponent } from './pages/developer-page/about/other-service-link/other-service-link.component';
import { ProjectsComponent } from './pages/developer-page/projects/projects.component';
import { ProjectCardComponent } from './pages/developer-page/projects/project-card/project-card.component';
import { ProfileComponent } from './components/profile/profile.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import { MainPageComponent } from './pages/main-page/main-page.component';
import {CoreRoutingModule} from "./core-routing.module";
import {JwtInterceptor} from "./services/token.interceptor";


@NgModule({
  declarations: [
    DeveloperPageComponent,
    AboutComponent,
    OtherServiceLinkComponent,
    ProjectsComponent,
    ProjectCardComponent,
    ProfileComponent,
    MainPageComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule,
    CoreRoutingModule
  ],
  bootstrap: [
    MainPageComponent
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ]
})
export class CoreModule { }
