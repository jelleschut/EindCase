export interface UpdateResult {
  newCourses: number;
  newInstances: number;
  duplicates: number;
  error: boolean;
  errorMessage: string;
}
