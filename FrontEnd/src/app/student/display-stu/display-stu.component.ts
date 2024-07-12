import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
@Component({
  selector: 'app-display-stu',
  templateUrl: './display-stu.component.html',
  styleUrls: ['./display-stu.component.css']
})
export class DisplayStuComponent implements OnInit {

  constructor(private service:SharedService) { }
  studentList:any =[];
  ModalTitle!: string;
  ActivateAddEditStuComp:boolean=false;
  stu:any;

  ngOnInit(): void {
    this.refreshStuList();
  }

  addClick(){
    this.stu={
      studentId: 0,
      firstName:"",
      lastName:"", 
    }
    this.ModalTitle="Add Student";
    this.ActivateAddEditStuComp=true;
  }

  deleteClick(item:any){
    this.service.deleteStudent(item.studentId).subscribe(data =>{
        this.refreshStuList();
    });
  }

  closeClick() {
    this.ActivateAddEditStuComp = false;
    this.refreshStuList();
  }
  
  refreshStuList(){
    this.service.getStuList().subscribe(data=>{
      this.studentList = data
    });
  }
}
