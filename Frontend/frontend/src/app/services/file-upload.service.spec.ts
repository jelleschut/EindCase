import { TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { UpdateResult } from '../models/update-result';
import { FileUploadService } from './file-upload.service';

describe('FileUploadService', () => {
  let mockHttpClient: { post: jasmine.Spy };
  let service: FileUploadService;

  beforeEach(() => {
    mockHttpClient = jasmine.createSpyObj('HttpClient', ['post']);
    service = new FileUploadService(mockHttpClient as any);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('Should return regular once', () => {
    let formData: FormData = new FormData();
    const expected: UpdateResult = {
      newCourses: 2,
      newInstances: 4,
      duplicates: 7,
      error: false,
      errorMessage: '',
    };

    mockHttpClient.post.and.returnValue(of(expected));

    service.upload(formData).subscribe((res) => {
      expect(res).toEqual(expected), fail;
    }, fail);

    expect(mockHttpClient.post.calls.count()).toBe(1);
  });

  it('Should return error once', () => {
    let formData: FormData = new FormData();
    const expected: UpdateResult = {
      newCourses: 0,
      newInstances: 0,
      duplicates: 0,
      error: true,
      errorMessage: 'This is an error',
    };

    mockHttpClient.post.and.returnValue(of(expected));

    service.upload(formData).subscribe((res) => {
      expect(res).toEqual(expected);
    }, fail);

    expect(mockHttpClient.post.calls.count()).toBe(1);
  });
});
