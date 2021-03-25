import { Course } from "./course";

export interface CourseInstance {
  courseInstanceId?: number;
  startDate: Date;
  course: Course;
}
