import {NgModule} from '@angular/core';
import {MarkdownModule as MD} from "ngx-markdown";
import {MdRendererComponent} from "./components/md-renderer.component";
import {CommonModule} from "@angular/common";


@NgModule({
  imports: [MD.forRoot(), CommonModule],
  exports: [MdRendererComponent],
  declarations: [MdRendererComponent],
  providers: [],
})
export class MarkdownModule {
}
