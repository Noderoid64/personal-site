import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from "@angular/router";
import {BlogBuilderPageComponent} from './pages/blog-builder-page/blog-builder-page.component';
import {MaterialModule} from "../../shared/modules/material.module";
import {ReactiveFormsModule} from "@angular/forms";
import {CodeSelectorComponent} from './pages/blog-builder-page/code-selector/code-selector.component';
import {HorizontalLineComponent} from './pages/blog-builder-page/horizontal-line/horizontal-line.component';
import {BlogMainPageComponent} from './pages/blog-main-page/blog-main-page.component';
import {PostApiService} from "./services/post-api.service";
import { ViewModeComponent } from './pages/blog-builder-page/view-mode/view-mode.component';
import { SettingsDialogComponent } from './components/settings-dialog/settings-dialog.component';
import { BlogMyPostsPageComponent } from './pages/blog-my-posts-page/blog-my-posts-page.component';
import { AuthGuardService } from './services/auth-guard.service';
import { BlogViewComponent } from './pages/blog-view/blog-view.component';
import {MarkdownModule} from "../../shared/modules/markdown/markdown.module";

const routes: Routes = [
  {
    path: "builder/:id",
    component: BlogBuilderPageComponent,
    canActivate: [AuthGuardService]
  }, {
    path: "builder",
    component: BlogBuilderPageComponent,
    canActivate: [AuthGuardService]
  }, {
    path: "my",
    component: BlogMyPostsPageComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: "view/:id",
    component: BlogViewComponent
  },
  {
    path: "**",
    component: BlogMainPageComponent
  }
]

@NgModule({
  declarations: [
    BlogBuilderPageComponent,
    CodeSelectorComponent,
    HorizontalLineComponent,
    BlogMainPageComponent,
    ViewModeComponent,
    SettingsDialogComponent,
    BlogMyPostsPageComponent,
    BlogViewComponent
  ],
  imports: [
    CommonModule,
    MarkdownModule,
    RouterModule.forChild(routes),
    MaterialModule,
    ReactiveFormsModule
  ],
  providers: [
    PostApiService
  ]
})
export class BlogModule {
}
