import {Component, Input, OnInit} from '@angular/core';
import {Comment} from "../../../../models/comment";

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss']
})
export class CommentComponent implements OnInit {

  @Input() public Comment?: Comment;

  constructor() { }

  ngOnInit(): void {
  }

}
