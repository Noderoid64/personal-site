import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from "@angular/router";
import { BlogBuilderPageComponent } from './pages/blog-builder-page/blog-builder-page.component';

const routes: Routes = [
  {
    path: "**",
    component: BlogBuilderPageComponent
  }
]

@NgModule({
  declarations: [
    BlogBuilderPageComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class BlogModule { }
