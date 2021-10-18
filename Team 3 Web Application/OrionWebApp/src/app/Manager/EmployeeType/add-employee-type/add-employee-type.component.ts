import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ServicesService } from 'src/app/service/Services/services.service';

@Component({
  selector: 'app-add-employee-type',
  templateUrl: './add-employee-type.component.html',
  styleUrls: ['./add-employee-type.component.scss']
})
export class AddEmployeeTypeComponent implements OnInit {

  title: string = "Add Employee Type";
  employeeTypeId!: number;
  errorMessage: any;
  branchList: Array<any> = [];
  form!: FormGroup;



  constructor(private service: ServicesService, private fb: FormBuilder,
    private snack: MatSnackBar, private dialogRef: MatDialogRef<AddEmployeeTypeComponent>,
    private router: Router, private _avRoute: ActivatedRoute) {

    if (this._avRoute.snapshot.params["id"]) {
      this.employeeTypeId = this._avRoute.snapshot.params["id"];
    }

    this.form = this.fb.group({
      EmployeeTypeID: [''],
      EmployeeTypeDescription: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],

    });
  }

  ngOnInit(): void {

    if (this.employeeTypeId > 0) {
      this.title = "Edit Employee Type";
      this.service.GetEmployeeTypeByID(this.employeeTypeId)
        .subscribe(resp => {
          console.log(resp)
          this.form = this.fb.group({
            EmployeeTypeID: [resp.Employee_Type_ID],
            EmployeeTypeDescription: [resp.Employee_Type_Description, Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],

          });

        })
    }
  }

  CreateEmployeeType() {

    if (!this.form.valid) {
      return;
    }
    if (this.title == "Add Employee Type") {
      this.service.RegisterEmployeeType(this.form.value).subscribe((res: any) => {
        // this.dialogRef.close();

        if (res.Success === false) {
          this.snack.open('Employee Type not added.', 'OK', {
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
            duration: 3000
          });
          this.form.reset();
          return;
        }

        else if (res.Success === true) {
          this.snack.open('Successful Added Employee Type', 'OK', {
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
            duration: 3000
          });
          this.router.navigateByUrl("EmployeeType")
          console.log(res);

        }
      },
        (error: HttpErrorResponse) => {
          if (error.status === 403) {
            this.snack.open('This branch has already exists.', 'OK', {
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
          //this.dialogRef.close();
        })
    }

    else if (this.title == "Edit Employee Type") {
      this.service.UpdateEmployeeType(this.form.value)
        .subscribe((data: any) => {
          console.log(data);
          
          if (data.Success === false) {
            this.snack.open('Employee Type not Updated.', 'OK', {
              verticalPosition: 'bottom',
              horizontalPosition: 'center',
              duration: 3000
            });
         
            this.router.navigate(['/EmployeeType']);
            return;
          }
  
          else if (data.Success === true) {
            this.snack.open('Successful Updated Employee Type', 'OK', {
              horizontalPosition: 'center',
              verticalPosition: 'bottom',
              duration: 3000
            });
          this.router.navigate(['/EmployeeType']);
          }
        }, error => this.errorMessage = error)
    }

  }

  back() {
    this.router.navigateByUrl("EmployeeType")
  }

}
