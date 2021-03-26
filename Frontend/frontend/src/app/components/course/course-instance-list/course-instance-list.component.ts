import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { DatePipe } from '@angular/common';
import { CourseInstance } from 'src/app/models/course-instance';
import { CourseInstanceService } from 'src/app/services/course-instance.service';


@Component({
  selector: 'course-instance-list',
  templateUrl: './course-instance-list.component.html',
  styleUrls: ['./course-instance-list.component.css'],
})
export class CourseInstanceListComponent implements OnInit, OnChanges {
  @Input() update: boolean;
  @Output() reset: EventEmitter<boolean> = new EventEmitter();

  courseInstances: CourseInstance[] = [];
  weekInstances: CourseInstance[] = [];
  week: number;
  year: number;

  constructor(
    private courseInstanceService: CourseInstanceService,
    private datepipe: DatePipe
  ) {}

  ngOnInit(): void {
    this.week = +this.datepipe.transform(new Date(), 'w');
    this.year = +this.datepipe.transform(new Date(), 'yyyy');

    this.getAllInstances();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(changes.update.currentValue) {
      this.getAllInstances();
    }
  }

  getAllInstances() {
    this.courseInstanceService.getAll().subscribe((courseInstances) => {
      if(courseInstances) {
        this.courseInstances = courseInstances;
        this.getWeekInstances();
      }
      this.reset.emit(false);
    });
  }

  getWeekInstances() {
    this.weekInstances = this.courseInstances.filter((value) => {
      return (
        +this.datepipe.transform(value.startDate, 'w') === this.week &&
        +this.datepipe.transform(value.startDate, 'yyyy') === this.year
      );
    });
  }

  getPreviousWeek() {
    if(this.week === 1) {
      this.year--;
      this.week = +this.datepipe.transform(`${this.year}-12-27`, 'w');
    } else {
      this.week--;
    }
    this.getWeekInstances();
  }

  getNextWeek() {
    if(this.week === +this.datepipe.transform(`${this.year}-12-27`, 'w')) {
      this.week = 1;
      this.year++;
    } else {
      this.week++;
    }
    this.getWeekInstances();
  }
}
