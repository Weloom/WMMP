//Angular
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

//Angular material
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatTreeModule} from '@angular/material/tree';
import {MatTableModule} from '@angular/material/table';

//Dynamic form
import { MaterialModule } from './DynamicForm/material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputComponent } from './DynamicForm/input/input.component';
import { ButtonComponent } from './DynamicForm/button/button.component';
import { SelectComponent } from './DynamicForm/select/select.component';
import { DateComponent } from './DynamicForm/date/date.component';
import { RadiobuttonComponent } from './DynamicForm/radiobutton/radiobutton.component';
import { CheckboxComponent } from './DynamicForm/checkbox/checkbox.component';
import { DynamicFieldDirective } from './DynamicForm/dynamic-field/dynamic-field.directive';
import { DynamicFormComponent } from './DynamicForm/dynamic-form/dynamic-form.component';

//Services
import { WorksService } from './Core/Services/works-service.service';

//Components
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { WorksComponent } from './Works/works.component';
import { EditWorksComponent } from './Works/edit-works/edit-works.component';
import { DesktopComponent } from './Desktop/desktop.component';
import { WorkSettingsComponent } from './Works/WorkSettings/WorkSettings.component';
import { PageNotFoundComponent } from './Standard/PageNotFound/PageNotFound.component';
import { SettingTitlePipe } from './Shared/SettingTitle.pipe';
import { TitleColorDirective } from './Shared/TitleColor.directive';

@NgModule({
     declarations: [
          AppComponent,
          DesktopComponent,
          PageNotFoundComponent,
          WorkSettingsComponent,

          WorksComponent,
          EditWorksComponent,

          DynamicFieldDirective,
          DynamicFormComponent,

          InputComponent,
          ButtonComponent,
          SelectComponent,
          DateComponent,
          RadiobuttonComponent,
          CheckboxComponent,

          SettingTitlePipe,

          TitleColorDirective


     ],
     imports: [
          MatToolbarModule,
          MatTreeModule,
          MatTableModule,
          BrowserModule,
          AppRoutingModule,
          HttpClientModule,
          BrowserAnimationsModule,
          MaterialModule,
          FormsModule,
          ReactiveFormsModule

     ],
     providers: [WorksService],
     bootstrap: [AppComponent],
     entryComponents: [EditWorksComponent,
          InputComponent,
          ButtonComponent,
          SelectComponent,
          DateComponent,
          RadiobuttonComponent,
          CheckboxComponent
     ]
})
export class AppModule { }
