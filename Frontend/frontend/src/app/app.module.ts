import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { CourseInstanceListComponent } from './components/course/course-instance-list/course-instance-list.component';
import { FormatDatePipe } from './pipes/format-date.pipe';
import { FileUploadComponent } from './components/file-upload/file-upload.component';
import { FormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    AppComponent,
    CourseInstanceListComponent,
    FormatDatePipe,
    FileUploadComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: ':year/:week', component: CourseInstanceListComponent },
    ]),
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
