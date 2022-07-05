import {Component} from '@angular/core';
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-blog-builder-page',
  templateUrl: './blog-builder-page.component.html',
  styleUrls: ['./blog-builder-page.component.scss']
})
export class BlogBuilderPageComponent {

  public sourceControl = new FormControl<string>('');

  public onSourceInput(event: any): void {
    console.log(event);
    var styles = event.target?.style;
    styles.height = "";
    styles.height = (event.target?.scrollHeight + 20) + "px"
  }

  public handleKeydown(event:any) {
    if (event.key == 'Tab') {
      event.preventDefault();
      var start = event.target.selectionStart;
      var end = event.target.selectionEnd;
      event.target.value = event.target.value.substring(0, start) + '\t' + event.target.value.substring(end);
      event.target.selectionStart = event.target.selectionEnd = start + 1;
    }
  };
}
