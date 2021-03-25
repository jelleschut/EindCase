import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CourseInstanceService } from 'src/app/services/course-instance.service';
import { CourseInstanceServiceStub } from 'src/app/stubs/course-instanceservicestub';
import { CourseInstanceListComponent } from './course-instance-list.component';
import { FormatDatePipe } from 'src/app/pipes/format-date.pipe'

describe('CourseInstanceListComponent', () => {
  let sut: CourseInstanceListComponent;
  let fixture: ComponentFixture<CourseInstanceListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CourseInstanceListComponent, FormatDatePipe ],
      providers: [{provide: CourseInstanceService, useClass: CourseInstanceServiceStub}]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CourseInstanceListComponent);
    sut = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('Should get all instances', () => {
    sut.ngOnInit();
    expect(sut.courseInstances).toBeDefined();
  });

});
