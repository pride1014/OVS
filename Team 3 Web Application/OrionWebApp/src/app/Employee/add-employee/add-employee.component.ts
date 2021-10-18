import { EmployeeType } from './../../service/Interface/interfaces.service';
import { ServicesService } from './../../service/Services/services.service';
import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { OpenDialogComponent } from 'src/app/Dialog/open-dialog/open-dialog.component';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.scss']
})
export class AddEmployeeComponent implements OnInit {

  title: string = "Register Employee";
  employeeId!: number;
  errorMessage: any;
  branchList: Array<any> = [];
  form!: FormGroup;




  constructor(private service: ServicesService, private fb: FormBuilder,
    private snack: MatSnackBar, private dialogRef: MatDialogRef<AddEmployeeComponent>,
    private router: Router, private _avRoute: ActivatedRoute) {

    if (this._avRoute.snapshot.params["id"]) {
      this.employeeId = this._avRoute.snapshot.params["id"];
    }

    this.form = this.fb.group({
      EmployeeID: [''],
      EmployeeName: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
      EmployeeSurname: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
      EmployeePhoneNumber: ['', Validators.compose([Validators.required, Validators.maxLength(10), Validators.minLength(10)])],
      EmployeeEmailAddress: ['', Validators.compose([Validators.required, Validators.email])],
      EmployeeTypeID: ['', Validators.compose([Validators.required])],
    });
  }

  observeData: Observable<EmployeeType[]> = this.service.getEmployeeType();
  EmployeeTypeData!: EmployeeType[];
  EmployeeTypeParams: EmployeeType = {
    EmployeeTypeID: 0,
    EmployeeTypeDescription: '',
  }

  ngOnInit(): void {
    this.observeData.subscribe(res => {
      this.EmployeeTypeData = res;
    })

    if (this.employeeId > 0) {
      this.title = "Edit Branch";
      this.service.GetEmployeeByID(this.employeeId)
        .subscribe(resp => {
          console.log(resp)
          this.form = this.fb.group({
            EmployeeID: [resp.Employee_ID],
            EmployeeName: [resp.Employee_Name, Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
            EmployeeSurname: [resp.Employee_Surname, Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
            EmployeePhoneNumber: [resp.Employee_Phone_Number, Validators.compose([Validators.required, Validators.maxLength(10), Validators.minLength(10)])],
            EmployeeEmailAddress: [resp.Employee_Email_Address, Validators.compose([Validators.required, Validators.email])],
            EmployeeTypeID: [resp.Employee_Type_ID, Validators.compose([Validators.required])],
          });
        })
    }
  }

  AddEmployeeType(){
    this.router.navigateByUrl("AddEmployeeType")
  }

  RegisterEmployee() {

    if (!this.form.valid) {
      return;
    }

    if (this.title == "Register Employee") {
      this.service.RegisterEmployee(this.form.value).subscribe((res: any) => {

        if (res.Success === false) {
          this.snack.open('Registration cancelled.', 'OK', {
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
            duration: 3000
          });
          this.form.reset();
          return;
        }

        else if (res.Success === true) {
          this.snack.open('Successful registration', 'OK', {
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
            duration: 3000


          });
          this.router.navigateByUrl("AddUser")
        }
      }, (error: HttpErrorResponse) => {
        if (error.status === 403) {
          this.snack.open('This user has already been registered.', 'OK', {
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
            duration: 3000
          });
        }
        this.snack.open('An error occurred on our servers, try again', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
        });

      })
    }

    else if (this.title == "Edit Branch") {
      this.service.UpdateEmployee(this.form.value)
        .subscribe((data: any) => {
          console.log(data);
          if (data.Success === false) {
            this.snack.open('Employee Not Updated.', 'OK', {
              verticalPosition: 'bottom',
              horizontalPosition: 'center',
              duration: 3000
            });
            this.router.navigate(['/Employee']);
            this.form.reset();
            return;
          }
          else if (data.Success === true) {
            this.snack.open('Successful Updated Employee', 'OK', {
              horizontalPosition: 'center',
              verticalPosition: 'bottom',
              duration: 3000
  
  
            });
            this.router.navigate(['/Employee']);
          }
         
        }, error => this.errorMessage = error)
    }
  }

  back() {
    this.router.navigateByUrl("Employee")
  }

}
