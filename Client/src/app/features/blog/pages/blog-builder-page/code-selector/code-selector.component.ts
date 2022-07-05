import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-code-selector',
  templateUrl: './code-selector.component.html',
  styleUrls: ['./code-selector.component.scss']
})
export class CodeSelectorComponent implements OnInit {

  @Output() selected = new EventEmitter<string>();

  constructor() {
  }

  ngOnInit(): void {
  }

}
