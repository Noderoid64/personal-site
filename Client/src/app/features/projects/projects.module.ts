import {NgModule} from '@angular/core';
import { ProjectsListComponent } from './components/projects-list/projects-list.component';
import {ProjectsRoutingModule} from "./projects-routing.module";
import { PersonalSiteProjectComponent } from './components/personal-site-project/personal-site-project.component';
import {MarkdownModule} from "../../shared/modules/markdown/markdown.module";
import {MaterialModule} from "../../shared/modules/material.module";

@NgModule({
  imports: [
    ProjectsRoutingModule,
    MaterialModule,
    MarkdownModule
  ],
  exports: [],
  declarations: [
    ProjectsListComponent,
    PersonalSiteProjectComponent
  ],
  providers: [],
})
export class ProjectsModule {
}
