import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {FormControl} from "@angular/forms";
import {Post} from "../../models/post";
import {ActivatedRoute} from "@angular/router";
import {PostApiService} from "../../services/post-api.service";
import {MatDialog} from "@angular/material/dialog";
import {SettingsDialogComponent} from "../../components/settings-dialog/settings-dialog.component";

@Component({
  selector: 'app-blog-builder-page',
  templateUrl: './blog-builder-page.component.html',
  styleUrls: ['./blog-builder-page.component.scss']
})
export class BlogBuilderPageComponent implements OnInit {

  public sourceControl = new FormControl<string>('');
  public isViewMode = false;
  private selectionStart: number = 0;
  private post: Post = {content: ''};

  @ViewChild('source') sourceTextArea?: ElementRef;

  constructor(
    private route: ActivatedRoute,
    private postApi: PostApiService,
    public dialog: MatDialog) {

  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      const parentId = +(params['parentId'] ?? -1);
      if (parentId != -1) {
        this.post.parentId = parentId;
      }
    })
    this.route.paramMap
      .subscribe(params => {
          const id = +(params.get('id') ?? -1);
          const parentId = +(params.get('parentId') ?? -1);
          if (parentId != -1) {
            this.post.parentId = parentId;
          }
          if (id != -1) {
            this.postApi.GetPostById(id).subscribe(x => {
              this.post = x;
              this.sourceControl.setValue(this.post.content);
              setTimeout(() => this.updateSourceHeight(this.sourceTextArea?.nativeElement), 10);
            });
          }
        }
      );
  }

  public handleKeydown(event: any) {
    if (event.key == 'Tab') {
      event.preventDefault();
      const start = event.target.selectionStart;
      const end = event.target.selectionEnd;
      event.target.value = event.target.value.substring(0, start) + '\t' + event.target.value.substring(end);
      event.target.selectionStart = event.target.selectionEnd = start + 1;
    }
  };

  public onCodeSelection(value: string) {
    if (this.sourceTextArea) {
      let newValue: string = this.sourceTextArea.nativeElement.value;
      newValue = newValue.slice(0, this.selectionStart) + '\n```' + value + '\n\n```' + newValue.slice(this.selectionStart);
      this.sourceControl.setValue(newValue)
      this.updateSourceHeight(this.sourceTextArea.nativeElement);
    }
  }

  public onViewModeChange(value: boolean) {
    this.isViewMode = value;
    setTimeout(() => this.updateSourceHeight(this.sourceTextArea?.nativeElement), 10);
  }

  public onHorizontalLine(): void {
    if (this.sourceTextArea) {
      let newValue: string = this.sourceTextArea.nativeElement.value;
      newValue = newValue.slice(0, this.selectionStart) + '\n---' + newValue.slice(this.selectionStart);
      this.sourceControl.setValue(newValue)
      this.updateSourceHeight(this.sourceTextArea.nativeElement);
    }
  }

  public onSourceClick(event: any): void {
    this.selectionStart = event.target.selectionStart;
  }

  public onSourceInput(event: any): void {
    this.updateSourceHeight(event.target);
  }

  public onSettings(): void {
    const dialogRef = this.dialog.open(SettingsDialogComponent, {
      data: {
        title: this.post?.title,
        accessType: this.post?.accessType
      },
    });

    dialogRef.afterClosed().subscribe(result => {
      if (this.post && result) {
        this.post.accessType = result.accessType;
        this.post.title = result.title;
      }
    });
  }

  public onSave(): void {
    if (!this.post?.title || this.post?.accessType == undefined) {
      this.onSettings();
    } else {
      this.post.content = this.sourceControl.value ?? '';
      this.postApi.SavePost(this.post).subscribe(console.log);
    }
  }

  private updateSourceHeight(target: any) {
    if (target) {
      this.selectionStart = target.selectionStart;
      target.style.height = "";
      target.style.height = (target?.scrollHeight + 20) + "px"
    }
  }
}
