import {Component, Input} from '@angular/core';
import {Observable} from "rxjs";

@Component({
  selector: 'md-renderer',
  templateUrl: 'md-renderer.component.html'
})

export class MdRendererComponent {

  public _data$?: Observable<string>;
  public _data?: string;

  @Input() public set content(value: string | Observable<string>) {
    this._data$ = undefined;
    this._data = undefined;
    if (value instanceof Observable<string>) {
      this._data$ = value;
    } else {
      this._data = value;
    }
  }

  constructor() {
  }
}
