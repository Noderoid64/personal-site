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
import { AuthGuardService } from '../../core/services/guards/auth-guard.service';
import { BlogViewComponent } from './pages/blog-view/blog-view.component';
import {MarkdownModule} from "../../shared/modules/markdown/markdown.module";
import { CommentsComponent } from './pages/blog-view/comments/comments.component';
import { MyCommentComponent } from './pages/blog-view/comments/my-comment/my-comment.component';
import { CommentComponent } from './pages/blog-view/comments/comment/comment.component';
import { BlogSearchPageComponent } from './pages/blog-search-page/blog-search-page.component';
import { RecentTableComponent } from './pages/blog-search-page/recent-table/recent-table.component';
import {BlogRoutingModule} from "./blog-routing.module";

@NgModule({
  declarations: [
    BlogBuilderPageComponent,
    CodeSelectorComponent,
    HorizontalLineComponent,
    BlogMainPageComponent,
    ViewModeComponent,
    SettingsDialogComponent,
    BlogMyPostsPageComponent,
    BlogViewComponent,
    CommentsComponent,
    MyCommentComponent,
    CommentComponent,
    BlogSearchPageComponent,
    RecentTableComponent
  ],
  imports: [
    CommonModule,
    MarkdownModule,
    BlogRoutingModule,
    MaterialModule,
    ReactiveFormsModule
  ],
  providers: [
    PostApiService
  ]
})
export class BlogModule {
}
