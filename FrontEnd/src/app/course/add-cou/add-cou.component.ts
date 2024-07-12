import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-add-cou',
  templateUrl: './add-cou.component.html',
  styleUrls: ['./add-cou.component.css']
})
export class AddCouComponent implements OnInit {

  constructor(private service:SharedService) { }
  @Input() cou:any;
  @Output() courseAdded: EventEmitter<any> = new EventEmitter();

  courseId!:string;
  courseName!: string;
  courseDescription!: string;

  ngOnInit(): void {
    this.courseId=this.cou.courseId;
    this.courseName=this.cou.courseName;
    this.courseDescription=this.cou.courseDescription;
  }

  addCourse(){ 
    var val = {courseName:this.courseName,
      courseDescription:this.courseDescription}
    this.service.addCourse(val).subscribe(res=>{
      this.courseAdded.emit();
    })
  }

}
