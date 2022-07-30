import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-my-comment',
  templateUrl: './my-comment.component.html',
  styleUrls: ['./my-comment.component.scss']
})
export class MyCommentComponent implements OnInit {

  @Input() public profilePicture?: string;

  @Output() public sendComment = new EventEmitter<string>();

  public commentForm = new FormControl();

  constructor() { }

  ngOnInit(): void {
  }

}
