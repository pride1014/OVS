import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { OpenDialogComponent } from 'src/app/Dialog/open-dialog/open-dialog.component';
import { Observable } from 'rxjs';
import { ServicesService } from 'src/app/service/Services/services.service';
import { Discount } from 'src/app/service/Interface/interfaces.service';

@Component({
  selector: 'app-add-customer-type',
  templateUrl: './add-customer-type.component.html',
  styleUrls: ['./add-customer-type.component.scss']
})
export class AddCustomerTypeComponent implements OnInit {

  title: string = "Add Customer Type";
  Id!: number;
  errorMessage: any;
  branchList: Array<any> = [];
  form!: FormGroup;


  constructor(private service: ServicesService, private fb: FormBuilder,
    private snack: MatSnackBar,
    private router: Router, private _avRoute: ActivatedRoute) {
      if (this._avRoute.snapshot.params["id"]) {
        this.Id = this._avRoute.snapshot.params["id"];
      }

      this.form = this.fb.group({
        CustomerTypeID: [''],
        CustomerTypeDescription: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
        DiscountID: ['', Validators.compose([Validators.required])],
      });

     }

     observeData: Observable<Discount[]> = this.service.getDiscount();
     DiscountData!: Discount[];
   Params: Discount = {
     DiscountID: 0,
     DiscountName: '',
     DiscountDescription: '',
     DiscountPercentage: 0
   }
  ngOnInit(): void {

    this.observeData.subscribe(res => {
      this.DiscountData = res;
    })

    if (this.Id > 0) {
      this.title = "Edit Customer Type";
      this.service.GetCustomerTypeByID(this.Id)
        .subscribe(resp => {
          console.log(resp)
          this.form = this.fb.group({
            CustomerTypeID: [resp.Customer_Type_ID],
            CustomerTypeDescription: [resp.Customer_Type_Description, Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
            DiscountID: [resp.Discount_ID, Validators.compose([Validators.required])],
          });
        })
    }
  }


  RegisterCustomerType()
  {
    if (!this.form.valid) {
      return;
    }

    if (this.title == "Add Customer Type") {
      this.service.CreateCustomerType(this.form.value).subscribe((res: any) => {

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
          this.router.navigateByUrl("CustomerType")
        }
      }, (error: HttpErrorResponse) => {
        if (error.status === 403) {
          this.snack.open('This Customer Type has already been registered.', 'OK', {
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

    else if (this.title == "Edit Customer Type") {
      this.service.UpdateCustomerType(this.form.value)
        .subscribe((data: any) => {
          console.log(data);
          if (data.Success === false) {
            this.snack.open('Customer Type Not Updated.', 'OK', {
              verticalPosition: 'bottom',
              horizontalPosition: 'center',
              duration: 3000
            });
            this.router.navigate(['/CustomerType']);
            this.form.reset();
            return;
          }
          else if (data.Success === true) {
            this.snack.open('Successful Updated Customer Type', 'OK', {
              horizontalPosition: 'center',
              verticalPosition: 'bottom',
              duration: 3000
  
  
            });
            this.router.navigate(['/CustomerType']);
          }
         
        }, error => this.errorMessage = error)
    }
  }

  back()
  {
    this.router.navigate(['/CustomerType']);
  }

}
