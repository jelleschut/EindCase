import { HttpErrorResponse } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { CourseInstance } from '../models/course-instance';

import { CourseInstanceService } from './course-instance.service';

describe('CourseInstanceService', () => {
  let mockHttpClient: { get: jasmine.Spy };
  let service: CourseInstanceService;

  beforeEach(() => {
    mockHttpClient = jasmine.createSpyObj('HttpClient', ['get']);
    service = new CourseInstanceService(mockHttpClient as any);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call once', () => {
    const expected: CourseInstance[] = [
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

    mockHttpClient.get.and.returnValue(of(expected));

    service.getAll().subscribe((result) => {
      expect(result).toEqual(expected);
    });

    expect(mockHttpClient.get.calls.count()).toBe(1);
  });
});
