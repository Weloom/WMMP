import { Component, OnInit } from '@angular/core';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { WorksService } from 'src/app/Core/Services/works-service.service';
import { IWork, IWorksColl, IAspect } from 'src/app/models/work.model';
import { Observable, Observer } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
     selector: 'mmp-WorkSettings',
     templateUrl: './WorkSettings.component.html',
     styleUrls: ['./WorkSettings.component.css']
})
export class WorkSettingsComponent implements OnInit {
     private works$: Observable<IWorksColl>;
     private worksObs: Observer<IWorksColl>;

     treeControl = new NestedTreeControl<IAspect>(node => node.Aspects);
     dataSource = new MatTreeNestedDataSource<IAspect>();

     constructor(public worksService: WorksService, private route: ActivatedRoute) {
          this.works$ = this.worksService.GetWorks();
          this.worksObs = {
               next: works => {
                    if (works.Works.length > 0) {
                         //this.worksService.SetEditWork(works.Works[0]);
                         let id = this.route.snapshot.paramMap.get("id")
                         let item1 = works.Works.find(i => i.Id == id);
                         this.dataSource.data = item1.Aspects;
                    }
               }
          } as Observer<IWorksColl>;
          let subscription = this.works$.subscribe(this.worksObs);
     }

     ngOnInit() { }

     hasChild = (_:any, node: IAspect) => !!node.Aspects && node.Aspects.length > 0;
}
