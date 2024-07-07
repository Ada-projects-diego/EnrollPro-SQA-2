import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { StudentComponent } from './student/student.component';
import { CourseComponent } from './course/course.component';
import { SubjectComponent } from './subject/subject.component';
const routes: Routes = [
  {path: '', component:HomeComponent},
  {path: "students", component:StudentComponent},
  {path: "courses", component:CourseComponent},
  {path: "subjects", component:SubjectComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
