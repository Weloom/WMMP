import { Component, OnInit, ViewChild, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';

import { Validators } from '@angular/forms';
import { FieldConfig } from '../../DynamicForm/field.interface';
import { DynamicFormComponent } from '../../DynamicForm/dynamic-form/dynamic-form.component';
import { WorksService } from 'src/app/Core/Services/works-service.service';
import { IWork } from 'src/app/models/work.model';
import {Router} from '@angular/router';

@Component({
     selector: 'mmp-edit-works',
     templateUrl: './edit-works.component.html',
     styleUrls: ['./edit-works.component.less'],
     changeDetection: ChangeDetectionStrategy.Default//.OnPush
})

export class EditWorksComponent implements OnInit {

     @ViewChild(DynamicFormComponent, { static: true }) form: DynamicFormComponent;

     constructor(public worksService: WorksService, private cd: ChangeDetectorRef, private router: Router) {
          this.worksService.WorkBeingEdited$.subscribe(data => {
               if (this.form != null) {
                    this.form.reset(this.loadControls(data));
               }
          });
     }

     ngOnInit() {
     }

     submit(value: any) {
          //...
     }

     loadControls(work: IWork) {
          return [
               {
                    type: 'input',
                    label: 'Title',
                    inputType: 'text',
                    name: 'Title',
                    value: this.PassPropertyValues(work, 'Title')
               },
               {
                    type: 'input',
                    label: 'Description',
                    inputType: 'text',
                    name: 'Description',
                    value: this.PassPropertyValues(work, 'Description')
               },
               {
                    type: 'select',
                    label: 'Type',
                    name: 'Type',
                    value: this.PassPropertyValues(work, 'Type'),
                    options: ['work', 'journal', 'template']
               },
               {
                    type: 'select',
                    label: 'Language',
                    name: 'Language',
                    value: this.PassPropertyValues(work, 'Language'),
                    options: ['Danish', 'English', 'Other']
               },
               {
                    type: 'button',
                    label: 'Delete',
                    name: 'Delete',
                    action: () => {
                         this.worksService.Delete(this.worksService.WorkBeingEdited);
                    }
               },
               {
                    type: 'button',
                    label: 'Settings',
                    name: 'Settings',
                    action: () => {
                         this.router.navigateByUrl('settings/' + this.worksService.WorkBeingEdited.Id );
                    }
               },
               {
                    type: 'button',
                    label: 'Desktop',
                    name: 'Desktop',
                    action: () => {
                         this.router.navigateByUrl('desktop/' + this.worksService.WorkBeingEdited.Id );
                    }
               }
          ];
     }

     PassPropertyValues(source: object, propertyName: string): object {
          //Object.keys(obj2).forEach(key=>obj1[key]=obj2[key]);
          let foundValue: object;
          Object.keys(source).forEach(key => {
               console.log("Looking for: " + key)
               if (key == propertyName) {
                    foundValue = source[key];
                    console.log("Found: " + key)
               }
          });
          return foundValue;
     }
}
