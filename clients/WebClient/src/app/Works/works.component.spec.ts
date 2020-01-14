import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { WorksComponent } from './works.component';

describe('WorksComponent', () => {
     let component: WorksComponent;
     let fixture: ComponentFixture<WorksComponent>;

     beforeEach(async(() => {
          TestBed.configureTestingModule({
               declarations: [WorksComponent]
          })
               .compileComponents();
     }));

     beforeEach(() => {
          fixture = TestBed.createComponent(WorksComponent);
          component = fixture.componentInstance;
          fixture.detectChanges();
     });

     it('should create', () => {
          expect(component).toBeTruthy();
     });
});

describe('Common', () => {
     const kvPairX: { [key: number]: string } = { 2: 'foo', 1: 'bar' };
     const map = new Map([[2, 'foo'], [1, 'bar']]);

     it('resolve', () => {
           
          expect(map.keys.length).toEqual(1, 'aaaaaargh!!!!!!!');
     });
});
