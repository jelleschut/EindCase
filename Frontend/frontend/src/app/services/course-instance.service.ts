import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CourseInstance } from '../models/course-instance';

@Injectable({
  providedIn: 'root'
})
export class CourseInstanceService {
  private url = 'https://localhost:5001/api/courseinstances'

  constructor(private httpClient: HttpClient) { }

  getAll(): Observable<CourseInstance[]> {
    return this.httpClient.get<CourseInstance[]>(this.url);
  }
}
