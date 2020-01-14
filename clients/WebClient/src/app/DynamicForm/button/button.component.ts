import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FieldConfig } from '../field.interface';

@Component({
     selector: 'mmp-button',
     template: `
<div class="demo-full-width margin-top" [formGroup]="group">
<button type="submit" mat-raised-button color="primary"  (click)="onclick()" >{{field.label}}</button>
</div>
`,
     styles: []
})
export class ButtonComponent implements OnInit {
     field: FieldConfig;
     group: FormGroup;
     constructor() { }
     ngOnInit() { }
     onclick() {
          if (this.field.action) {
               this.field.action();
          }
          else{
               alert("No implementation!");
          }
     }
}
