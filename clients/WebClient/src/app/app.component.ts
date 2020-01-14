import { Component } from '@angular/core';
import { WorksService } from './Core/Services/works-service.service';
import {WorksComponent} from './Works/works.component';


@Component({
  selector: 'mmp-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less'],
})
export class AppComponent {
  title = 'WMMPC';

  constructor(public worksService: WorksService) {
  }
}
