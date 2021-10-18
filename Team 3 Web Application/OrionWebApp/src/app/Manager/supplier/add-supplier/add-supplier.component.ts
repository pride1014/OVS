import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ServicesService } from 'src/app/service/Services/services.service';

@Component({
  selector: 'app-add-supplier',
  templateUrl: './add-supplier.component.html',
  styleUrls: ['./add-supplier.component.scss']
})
export class AddSupplierComponent implements OnInit {

  title: string = "Add Supplier";
  supplierId!: number;
  errorMessage: any;
  branchList: Array<any> = [];
  form!: FormGroup;



  constructor(private service: ServicesService, private fb: FormBuilder,
    private snack: MatSnackBar, private dialogRef: MatDialogRef<AddSupplierComponent>,
    private router: Router, private _avRoute: ActivatedRoute) {

    if (this._avRoute.snapshot.params["id"]) {
      this.supplierId = this._avRoute.snapshot.params["id"];
    }

    this.form = this.fb.group({
      SupplierID: [''],
      SupplierName: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
      SupplierPhoneNumber: ['', Validators.compose([Validators.required, Validators.maxLength(10), Validators.minLength(10)])],
      SupplierAddress: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
    });
  }

  ngOnInit(): void {

    if (this.supplierId > 0) {
      this.title = "Edit Supplier";
      this.service.GetSupplierByID(this.supplierId)
        .subscribe(resp => {
          console.log(resp)
          this.form = this.fb.group({
            SupplierID: [resp.Supplier_ID],
            SupplierName: [resp.Supplier_Name, Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
            SupplierPhoneNumber: [resp.Supplier_Phone_Number, Validators.compose([Validators.required, Validators.maxLength(10), Validators.minLength(10)])],
            SupplierAddress: [resp.Supplier_Address, Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
          });

        })
    }
  }

  CreateSupplier() {

    if (!this.form.valid) {
      return;
    }
    if (this.title == "Add Supplier") {
    this.service.RegisterSupplier(this.form.value).subscribe((res: any) => {
      // this.dialogRef.close();
      console.log(res)
      if (res.Success === false) {
        this.snack.open('Supplier not added.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });
        this.form.reset();
        return;
      }

      else if (res.Success === true) {
        this.snack.open('Successful Added Supplier', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
        });
        this.router.navigateByUrl("Suppliers")
        console.log(res);

      }
    }, (error: HttpErrorResponse) => {
      if (error.status === 403) {
        this.snack.open('This Supplier has already exists.', 'OK', {
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

  else if (this.title == "Edit Supplier") {
    this.service.UpdateSupplier(this.form.value)
      .subscribe((data: any) => {
        console.log(data);
        
        if (data.Success === false) {
          this.snack.open('Supplier not Updated.', 'OK', {
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
            duration: 3000
          });
       
          this.router.navigate(['/Suppliers']);
          return;
        }

        else if (data.Success === true) {
          this.snack.open('Successful Updated Supplier', 'OK', {
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
            duration: 3000
          });
        this.router.navigate(['/Suppliers']);
        }
      }, error => this.errorMessage = error)
  }

}

  back() {
    this.router.navigateByUrl("Supplier")
  }

}
