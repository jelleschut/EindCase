import { Observable, of } from 'rxjs';
import { CourseInstance } from '../models/course-instance';

export class CourseInstanceServiceStub {
  result: CourseInstance[] = [
    {
      startDate: new Date(),
      course: {
        title: 'Course1',
        code: 'C1',
        lengthInDays: 5,
      },
    },
    {
      startDate: new Date(),
      course: {
        title: 'Course2',
        code: 'C2',
        lengthInDays: 3,
      },
    },
  ];

  getAll(): Observable<CourseInstance[]> {
    return of(this.result);
  }
}
