// tslint:disable: no-debugger
import { Injectable } from '@angular/core';
import { IWork, IWorksColl, IAspect, ISectionLevel } from '../../models/work.model';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { interval } from 'rxjs';
import { map } from 'rxjs/operators';

export class Work implements IWork {
     Id: string;
     Title: string;
     Type: string;
     Language: string;
     Aspects: Array<IAspect>;
     SectionLevels: Array<ISectionLevel>;

     constructor(work: IWork) {
     }
}

@Injectable({ providedIn: 'root' })
export class WorksService {
     works: IWork[];

     private behaviorSubject$;
     WorkBeingEdited: IWork;
     WorkBeingEdited$: Observable<IWork>;


     SetEditWork(work: IWork) {
          this.WorkBeingEdited= work;
          this.behaviorSubject$.next(work);
     }

     private REST_API_SERVER = 'http://localhost:3000';

     public simpleObservable: Observable<string>;
     constructor(private httpClient: HttpClient) {
          this.behaviorSubject$ = new BehaviorSubject(new Array() as Array<IWork>); //The channel - hide this (BehaviorSubject, ReplaySubject and AsyncSubject)
          this.WorkBeingEdited$ = this.behaviorSubject$.asObservable(); //Point to listen to - expose this
     }

     // Setup observables when service is instantiated
     init(): void {
          this.works = [];
          const obs = this.GetWorks();
          obs.toPromise().then((data) => {
               this.works = data.Works;
          });
     }

     // Fetches works from backend service
     GetWorks(): Observable<IWorksColl> {
          return this.httpClient.get<IWorksColl>(this.REST_API_SERVER + '/WMMP/Works');
     }

     Delete(work: IWork){
          //TODO: Delete
          alert("Delete work: " + work.Title);
     }
}
