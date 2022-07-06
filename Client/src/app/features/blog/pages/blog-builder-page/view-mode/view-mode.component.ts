import {Component, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-view-mode',
  templateUrl: './view-mode.component.html',
  styleUrls: ['./view-mode.component.scss']
})
export class ViewModeComponent {

  @Output() toggle = new EventEmitter();

  public value = false;

  constructor() { }

  public onClick(): void {
    this.value = !this.value;
    this.toggle.emit(this.value);
  }
}
