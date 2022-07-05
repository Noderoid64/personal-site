import {Component, ElementRef, ViewChild} from '@angular/core';
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-blog-builder-page',
  templateUrl: './blog-builder-page.component.html',
  styleUrls: ['./blog-builder-page.component.scss']
})
export class BlogBuilderPageComponent {

  public sourceControl = new FormControl<string>('');
  private selectionStart: number = 0;
  @ViewChild('source') sourceTextArea?: ElementRef;

  public handleKeydown(event:any) {
    if (event.key == 'Tab') {
      event.preventDefault();
      const start = event.target.selectionStart;
      const end = event.target.selectionEnd;
      event.target.value = event.target.value.substring(0, start) + '\t' + event.target.value.substring(end);
      event.target.selectionStart = event.target.selectionEnd = start + 1;
    }
  };

  public onCodeSelection(value: string) {
    if (this.sourceTextArea) {
      let newValue: string = this.sourceTextArea.nativeElement.value;
      newValue = newValue.slice(0, this.selectionStart) + '\n```' + value + '\n\n```' + newValue.slice(this.selectionStart);
      this.sourceControl.setValue(newValue)
      this.updateSourceHeight(this.sourceTextArea.nativeElement);
    }
  }

  public onSourceClick(event: any): void {
    this.selectionStart = event.target.selectionStart;
  }

  public onSourceInput(event: any): void {
    this.updateSourceHeight(event.target);
  }

  private updateSourceHeight (target: any) {
    this.selectionStart = target.selectionStart;
    target.style.height = "";
    target.style.height = (target?.scrollHeight + 20) + "px"
  }
}
