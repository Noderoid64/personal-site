import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-other-service-link',
  templateUrl: './other-service-link.component.html',
  styleUrls: ['./other-service-link.component.scss']
})
export class OtherServiceLinkComponent {

  @Input() public iconPath?: string;
  @Input() public title?: string;
  @Input() public link?: string;

}
