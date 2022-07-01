import { Component } from '@angular/core';
import {FirebaseService} from "../../../services/firebase.service";

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss']
})
export class ProjectsComponent {

  constructor(private authService: FirebaseService) {
  }
}


