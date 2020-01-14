// tslint:disable: no-debugger
import { TestBed } from '@angular/core/testing';
import { WorksService } from './works-service.service';
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from '@angular/platform-browser-dynamic/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('WorksServiceService', () => {
  TestBed.resetTestEnvironment();
  TestBed.initTestEnvironment(BrowserDynamicTestingModule, platformBrowserDynamicTesting());
  beforeEach(() => TestBed.configureTestingModule({
     declarations: [],
     imports: [HttpClientTestingModule],
    }));
  it('should be created xxx', () => {

    const service: WorksService = TestBed.get(WorksService);

    expect(service).toBeTruthy();
  });
});
