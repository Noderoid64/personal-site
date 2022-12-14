import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {DeveloperPageComponent} from "./pages/developer-page/developer-page.component";
import {TestDevPageComponent} from "./pages/test-dev-page/test-dev-page.component";
import {CvComponent} from "./pages/cv/cv.component";

const routes: Routes = [
  {
    path: 'blog',
    loadChildren: () => import('../features/blog/blog.module').then(m => m.BlogModule)
  }, {
    path: 'projects',
    loadChildren: () => import('../features/projects/projects.module').then(m => m.ProjectsModule)
  }, {
    path: 'cv',
    component: CvComponent
  },
  {
    path: 'test',
    component: TestDevPageComponent
  },
  {
    path: "**",
    component: DeveloperPageComponent
  }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  declarations: [],
})
export class CoreRoutingModule {
}
