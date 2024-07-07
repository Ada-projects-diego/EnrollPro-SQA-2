import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'; 
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class SharedService {
  
  constructor(private http:HttpClient) { }

  getStuList():Observable<any[]>{
    return this.http.get<any>(environment.APIUrl+'/Students')
  }

  addStudent(val:any){
    return this.http.post(environment.APIUrl+'/Students', val)
  }

  deleteStudent(val:any){
    return this.http.delete(environment.APIUrl+'/Students/'+val)
  }
  
  getCouList():Observable<any[]>{
    return this.http.get<any>(environment.APIUrl+'/Courses')
  }

  addCourse(val:any){
    return this.http.post(environment.APIUrl+'/Courses', val)
  }

  deleteCourse(val:any){
    return this.http.delete(environment.APIUrl+'/Courses/'+val)
  }
  
  getSubList():Observable<any[]>{
    return this.http.get<any>(environment.APIUrl+'/Subjects')
  }

  addSubject(val:any){
    return this.http.post(environment.APIUrl+'/Subjects', val)
  }

  deleteSubject(val:any){
    return this.http.delete(environment.APIUrl+'/Subjects/'+val)
  }
}
