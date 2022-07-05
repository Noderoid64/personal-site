import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from "@angular/router";
import { BlogBuilderPageComponent } from './pages/blog-builder-page/blog-builder-page.component';
import {MarkdownModule} from "ngx-markdown";
import {MaterialModule} from "../../shared/modules/material.module";
import {ReactiveFormsModule} from "@angular/forms";
import { CodeSelectorComponent } from './pages/blog-builder-page/code-selector/code-selector.component';
import { HorizontalLineComponent } from './pages/blog-builder-page/horizontal-line/horizontal-line.component';

const routes: Routes = [
  {
    path: "**",
    component: BlogBuilderPageComponent
  }
]

@NgModule({
  declarations: [
    BlogBuilderPageComponent,
    CodeSelectorComponent,
    HorizontalLineComponent
  ],
  imports: [
    CommonModule,
    MarkdownModule.forRoot(),
    RouterModule.forChild(routes),
    MaterialModule,
    ReactiveFormsModule
  ]
})
export class BlogModule { }
