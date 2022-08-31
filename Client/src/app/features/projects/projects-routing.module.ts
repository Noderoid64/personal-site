import {NgModule} from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {AuthGuardService} from "../../core/services/guards/auth-guard.service";
import {ProjectsListComponent} from "./components/projects-list/projects-list.component";
import {PersonalSiteProjectComponent} from "./components/personal-site-project/personal-site-project.component";

const routes : Routes = [
  {
    path: 'personal-site',
    component: PersonalSiteProjectComponent,
  },
  {
    path: "**",
    component: ProjectsListComponent,
    canActivate: [AuthGuardService]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [],
})
export class ProjectsRoutingModule {
}
