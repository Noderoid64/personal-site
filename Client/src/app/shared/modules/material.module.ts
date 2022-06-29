import {NgModule} from '@angular/core';
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from "@angular/material/card";
import {MatTabsModule} from "@angular/material/tabs";

@NgModule({
  exports: [
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatCardModule,
    MatTabsModule
  ],
})
export class MaterialModule {
}
