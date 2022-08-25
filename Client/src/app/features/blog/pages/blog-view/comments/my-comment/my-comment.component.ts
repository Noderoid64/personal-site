import {Component, EventEmitter, Input, Output} from '@angular/core';
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-my-comment',
  templateUrl: './my-comment.component.html',
  styleUrls: ['./my-comment.component.scss']
})
export class MyCommentComponent {

  @Input() public profilePicture?: string;

  @Output() public sendComment = new EventEmitter<string>();

  public commentForm = new FormControl();

  constructor() { }

  public onSend() {
    this.sendComment.emit(this.commentForm.value)
    this.commentForm.reset();
  }

}
