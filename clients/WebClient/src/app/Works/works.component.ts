import { Component, OnInit } from '@angular/core';
import { WorksService } from '../Core/Services/works-service.service';
import { IWork, IWorksColl } from '../models/work.model';
import { Observable, Observer } from 'rxjs';

@Component({
     selector: 'mmp-works',
     templateUrl: './works.component.html',
     styleUrls: ['./works.component.less']
})
export class WorksComponent implements OnInit {
     private works$: Observable<IWorksColl>;
     private worksObs: Observer<IWorksColl>;

     public works: any;
     public displayedColumns  :  string[] = ['Id','Title','Type'];

     constructor(public worksService: WorksService) {
          this.works$ = this.worksService.GetWorks();

          this.worksObs = {
               next: works => {
                    if (works.Works.length > 0) {
                         //select first for edit
                         this.worksService.SetEditWork(works.Works[0]);
                         this.works = works.Works;
                    }
               }
          } as Observer<IWorksColl>;

          let subscription = this.works$.subscribe(this.worksObs);
     }

     public select(work: IWork) {
          console.info("clicked " + work.Id);
          this.worksService.SetEditWork(work);
     }

     ngOnInit() { }
}
