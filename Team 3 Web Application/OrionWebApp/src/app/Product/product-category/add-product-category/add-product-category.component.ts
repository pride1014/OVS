import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ServicesService } from 'src/app/service/Services/services.service';

@Component({
  selector: 'app-add-product-category',
  templateUrl: './add-product-category.component.html',
  styleUrls: ['./add-product-category.component.scss']
})
export class AddProductCategoryComponent implements OnInit {

  title: string = "Add Product Category ";
  CatagoryId!: number;
  errorMessage: any;
  branchList: Array<any> = [];
  form!: FormGroup;



  constructor(private service: ServicesService, private fb: FormBuilder,
    private snack: MatSnackBar, private dialogRef: MatDialogRef<AddProductCategoryComponent>,
    private router: Router, private _avRoute: ActivatedRoute) {
    if (this._avRoute.snapshot.params["id"]) {
      this.CatagoryId = this._avRoute.snapshot.params["id"];
    }

    this.form = this.fb.group({
      ProductCategoryID: [''],
      ProductCategoryName: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
    });

  }

  ngOnInit(): void {

    if (this.CatagoryId > 0) {
      this.title = "Edit Product Category";
      this.service.GetProductCategoryByID(this.CatagoryId)
        .subscribe(resp => {
          console.log(resp)
          this.form = this.fb.group({
            ProductCategoryID: [resp.Product_Category_ID],
            ProductCategoryName: [resp.Product_Category_Name, Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
          });

        })
    }
  }

  CreateCategory() {

    if (!this.form.valid) {
      return;
    }
    if (this.title == "Add Product Category ") {
      this.service.CreateCategory(this.form.value).subscribe((res: any) => {
        if (res.Success === false) {
          this.snack.open('Category not added.', 'OK', {
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
            duration: 3000
          });
          this.form.reset();
          return;
        }

        else if (res.Success === true) {
          this.snack.open('Successful Added Product Category', 'OK', {
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
            duration: 3000
          });
          this.router.navigateByUrl("ProductCategory")
          console.log(res);

        }
      }, (error: HttpErrorResponse) => {
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

    else if (this.title == "Edit Product Category") {
      this.service.UpdateProductCategory(this.form.value)
        .subscribe((data: any) => {
          console.log(data);
          
          if (data.Success === false) {
            this.snack.open('Product Category not Updated.', 'OK', {
              verticalPosition: 'bottom',
              horizontalPosition: 'center',
              duration: 3000
            });
         
            this.router.navigate(['/ProductCategory']);
            return;
          }
  
          else if (data.Success === true) {
            this.snack.open('Successful Updated Product Category', 'OK', {
              horizontalPosition: 'center',
              verticalPosition: 'bottom',
              duration: 3000
            });
          this.router.navigate(['/ProductCategory']);
          }
        }, error => this.errorMessage = error)
    }
  }

  back() {
    this.router.navigateByUrl("ProductCategory")
  }

}
