import { OpenDialogComponent } from './../../../Dialog/open-dialog/open-dialog.component';
import { ServicesService } from './../../../service/Services/services.service';
import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CustomerType } from 'src/app/service/Interface/interfaces.service';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {


  form: FormGroup = this.fb.group({
    CustomerName: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
    CustomerSurname: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
    CustomerCellphoneNumber: ['', Validators.compose([Validators.required])],
    CustomerEmailAddress: ['', Validators.compose([Validators.required, Validators.email])],
    CustomerPhysicalAddress: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
    CustomerDateOfBirth: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
 
  });

  observeCustomerType: Observable<CustomerType[]> = this.service.getCustomerType();
  CustomerTypeData!: CustomerType[];
  parameter : CustomerType={
    CustomerTypeID: 0,
    CustomerTypeDescription: '',
    DiscountID: 0,
    DiscountName: ''
  };

  constructor(private service: ServicesService, private fb: FormBuilder, 
    private snack: MatSnackBar, private dialogRef: MatDialogRef<RegisterComponent>,
    private router: Router) { }

  ngOnInit(): void {
  }

  AddCustomerType() {
    this.router.navigateByUrl("AddCustomerType")
  }

  Register() {
    this.service.RegisterCustomer(this.form.value).subscribe((res:any)=> {
     
      console.log(res);
    

      if (res.Success===false)
      {
        this.snack.open('Registration cancelled.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });
        this.form.reset();
        return;
      }

      else if (res.Success===true)
      {
        this.snack.open('Successful registration', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
          
          
        });
 
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

  back(){
    this.router.navigateByUrl("Login")
  }

  addUser(){
    this.router.navigateByUrl("AddUser")
  }

}
