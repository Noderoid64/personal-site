import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-project-card',
  templateUrl: './project-card.component.html',
  styleUrls: ['./project-card.component.scss']
})
export class ProjectCardComponent {

  @Input() public title?: string;
  @Input() public image?: string;
  @Input() public description?: string;
}
