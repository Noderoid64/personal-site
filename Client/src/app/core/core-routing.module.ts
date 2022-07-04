import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {DeveloperPageComponent} from "./pages/developer-page/developer-page.component";

const routes: Routes = [
  {
    path: 'blog',
    loadChildren: () => import('../features/blog/blog.module').then(m => m.BlogModule)
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
