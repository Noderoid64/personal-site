import {Component, EventEmitter, Input, Output} from '@angular/core';
import {AuthService} from "../../../../../core/services/auth.service";
import {User} from "../../../../../core/models/user";
import {EMPTY, filter, Observable, tap} from "rxjs";
import {CommentApiService} from "../../../services/comment-api.service";
import {Comment} from "../../../models/comment";
import {map} from "rxjs/operators";

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.scss']
})
export class CommentsComponent {

  @Input() public set postId (value: number | undefined) {
    if (value && value != 0) {
      this._postId = value;
      this.loadComments(this._postId!);
    }
  }
  @Output() public CommentsLoaded = new EventEmitter<number>();

  public _postId?: number;
  public user$?: Observable<User | null>;
  public comments: Comment[] = [];
  public comments$: Observable<Comment[]> = EMPTY;


  constructor(private auth: AuthService, private commentApi: CommentApiService) {
    this.user$ = auth.user$;
  }

  public onCommentSend(comment: string): void {
    if (this._postId)
    this.commentApi.PostComment(this._postId, comment).subscribe(() => this.postId = this._postId);
  }

  public onCommentRemove(commentId: number): void {
    if (this._postId)
      this.commentApi.DeleteComment(commentId).subscribe(() => {
        this.comments = this.comments.filter(x => x.id !== commentId);
        this.CommentsLoaded.emit(this.comments.length);
      });
  }

  identify(index: number, item: Comment) {
    return item.id;
  }

  private loadComments(postId: number) {
    this.commentApi.GetComments(postId)
      .pipe(
        tap(x => this.CommentsLoaded.emit(x.length))
      )
      .subscribe(x => this.comments = x);
  }

}
