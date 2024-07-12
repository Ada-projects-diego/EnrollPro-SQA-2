import { Component, OnInit } from '@angular/core';
import { ConnectableObservable } from 'rxjs';
import { SharedService } from 'src/app/shared.service';
@Component({
  selector: 'app-display-cou',
  templateUrl: './display-cou.component.html',
  styleUrls: ['./display-cou.component.css']
})
export class DisplayCouComponent implements OnInit {

  constructor(private service:SharedService) {}
  courseList:any=[];
  ModalTitle!: string;
  ActivateAddEditCouComp:boolean=false;
  cou:any;

  ngOnInit(): void {
    this.refreshCouList();
  }

  addClick(){
    this.cou={
      courseId: 0,
      courseName:"",
      courseDescription:"", 
    }
    this.ModalTitle="Add Course";
    this.ActivateAddEditCouComp=true;
  }

  deleteClick(item:any){
    this.service.deleteCourse(item.courseId).subscribe(data =>{
      this.refreshCouList();
  });
  }
  
  closeClick(){
    this.ActivateAddEditCouComp=false;
    this.refreshCouList();
  }

  refreshCouList(){
    this.service.getCouList().subscribe(data=>{
      this.courseList = data
    });
  }

}

