import { Observable, of } from 'rxjs';
import { UpdateResult } from '../models/update-result';

export class FileUploadServiceStub {
  result: UpdateResult = {
    newCourses: 1,
    newInstances: 3,
    duplicates: 5,
    error: false,
    errorMessage: '',
  };

  upload(formData: FormData): Observable<UpdateResult> {
    return of(this.result);
  }
}
