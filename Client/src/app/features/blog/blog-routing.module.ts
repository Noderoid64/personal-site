import {NgModule} from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {BlogBuilderPageComponent} from "./pages/blog-builder-page/blog-builder-page.component";
import {AuthGuardService} from "../../core/services/guards/auth-guard.service";
import {BlogSearchPageComponent} from "./pages/blog-search-page/blog-search-page.component";
import {BlogMyPostsPageComponent} from "./pages/blog-my-posts-page/blog-my-posts-page.component";
import {BlogViewComponent} from "./pages/blog-view/blog-view.component";
import {BlogMainPageComponent} from "./pages/blog-main-page/blog-main-page.component";

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
    path: "search",
    component: BlogSearchPageComponent,
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
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [],
})
export class BlogRoutingModule {
}
