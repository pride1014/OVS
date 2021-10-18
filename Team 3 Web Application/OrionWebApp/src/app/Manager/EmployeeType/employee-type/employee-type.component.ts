import { EmployeeType } from './../../../service/Interface/interfaces.service';
import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/service/Services/services.service';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-employee-type',
  templateUrl: './employee-type.component.html',
  styleUrls: ['./employee-type.component.scss']
})
export class EmployeeTypeComponent implements OnInit {

  observeData: Observable<EmployeeType[]> = this.service.getEmployeeType();
  typeData!: EmployeeType[];

  constructor(private service: ServicesService, private fb: FormBuilder, 
    private snack: MatSnackBar, private dialogRef: MatDialog,
    private router: Router) { }

  ngOnInit(): void {

    this.observeData.subscribe(res => {
      this.typeData = res;
    })
  }


  AddEmployeeType(){
    this.router.navigateByUrl("AddEmployeeType")
  }


  deleteAddEmployeeType(typeId:number){
  
    this.service.DeleteEmployeeType(typeId).subscribe((res:any)=>{
      console.log(res);
      if (res.Success===false)
      {
        this.snack.open('Employee Type not deleted.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });
      
        return;
      }

      else if (res.Success===true)
      {
        this.snack.open('Successful deleted Employee  Type', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
        });
        
       // this.router.navigateByUrl("EmployeeType")
        console.log(res);
        
      }
      window.location.reload();
    });
    
  }

}
