import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-add-sub',
  templateUrl: './add-sub.component.html',
  styleUrls: ['./add-sub.component.css']
})
export class AddSubComponent implements OnInit {

  constructor(private service: SharedService) {}
  @Input() sub: any;
  @Output() subjectAdded: EventEmitter<any> = new EventEmitter();

  subjectId!: string;
  subjectName!: string;
  subjectDescription!: string;
  courseId!: number | null;
  courses: any[] = [];
  selectedCourseId: number | null = null;

  ngOnInit(): void {
    this.subjectId = this.sub.subjectId;
    this.subjectName = this.sub.subjectName;
    this.subjectDescription = this.sub.subjectDescription; 
    this.loadCourses();
  }

  loadCourses() {
    this.service.getCouList().subscribe({
      next: (data) => {
        this.courses = data;
      },
      error: (error) => {
        console.error('Failed to load courses:', error);
      }
    });
  }

  onCourseSelect() {
    this.courseId = this.selectedCourseId;
  }

  addSubject(){ 
    if (this.courseId === null) {
      console.error('No course selected');
      return;
    }

    var val = {
      subjectName: this.subjectName,
      subjectDescription: this.subjectDescription,
      courseId: this.courseId
    }
    this.service.addSubject(val).subscribe({
      next: (response) => {
        this.subjectAdded.emit(response);
      },
      error: (error) => {
        console.error('Failed to add subject:', error);
      }
    }); 
  }

  isValidForm(): boolean {
    return this.subjectName?.trim() !== '' && 
           this.subjectDescription?.trim() !== '' && 
           this.selectedCourseId !== null;
  }  
}