import {Component, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-horizontal-line',
  templateUrl: './horizontal-line.component.html',
  styleUrls: ['./horizontal-line.component.scss']
})
export class HorizontalLineComponent {

  @Output() clicked = new EventEmitter();

}
