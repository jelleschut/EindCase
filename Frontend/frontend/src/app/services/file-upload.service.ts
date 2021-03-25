import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UpdateResult } from '../models/update-result';

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {
  private url = 'https://localhost:5001/api/fileupload'

  constructor(private httpClient: HttpClient) { }

  upload(formData: FormData): Observable<UpdateResult> {
    return this.httpClient.post<UpdateResult>(this.url, formData);
  }
}
