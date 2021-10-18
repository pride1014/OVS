import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ProductCategory, ProductType } from 'src/app/service/Interface/interfaces.service';
import { ServicesService } from 'src/app/service/Services/services.service';

@Component({
  selector: 'app-add-product-type',
  templateUrl: './add-product-type.component.html',
  styleUrls: ['./add-product-type.component.scss']
})
export class AddProductTypeComponent implements OnInit {

  title: string = "Add Product Type";
  prouductTypeId!: number;
  errorMessage: any;
  branchList: Array<any> = [];
 form!: FormGroup;




  constructor(private service: ServicesService, private fb: FormBuilder,
    private snack: MatSnackBar, private dialogRef: MatDialogRef<AddProductTypeComponent>,
    private router: Router, private _avRoute: ActivatedRoute) { 

      if (this._avRoute.snapshot.params["id"]) {
        this.prouductTypeId = this._avRoute.snapshot.params["id"];
      }

      this.form= this.fb.group({
        ProductTypeID: [''],
        ProductTypeName: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
        ProductCategoryID: ['', Validators.compose([Validators.required])],
      });
    }


  observeData: Observable<ProductCategory[]> = this.service.getCategories();
  CategoryData!: ProductCategory[];
  CategoryParams: ProductCategory = {
    ProductCategoryName: '',
    ProductCategoryID: 0,
  }

  ngOnInit(): void {
    this.observeData.subscribe(res => {
      this.CategoryData = res;
    })


    if (this.prouductTypeId > 0) {
      this.title = "Edit Product Type";
      this.service.GetTypeByID(this.prouductTypeId)
        .subscribe(resp => {
          console.log(resp)
          this.form= this.fb.group({
            ProductTypeID: [resp.Product_Type_ID],
            ProductTypeName: [resp.Product_Type_Name, Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
            ProductCategoryID: [resp.Product_Category_ID, Validators.compose([Validators.required])],
          });

        })
    }
  }

  AddCategory(){
    this.router.navigateByUrl("AddCategory")
  }

  CreateProductType()
  {

    if (!this.form.valid) {
      return;
    }

    if (this.title == "Add Product Type") {
    this.service.CreateProductType(this.form.value).subscribe((res:any) => {
      console.log(res);
      if (res.Success===false)
      {
        this.snack.open('Product Type not created.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });
        this.form.reset();
        return;
      }

      else if (res.Success===true)
      {
        this.snack.open('Successful Added Product Type', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
        });
        this.router.navigateByUrl("ProductType")
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
  else if (this.title == "Edit Product Type") {
    this.service.UpdateProductType(this.form.value)
      .subscribe((data: any) => {
        console.log(data);
        this.router.navigate(['/ProductType']);
      }, error => this.errorMessage = error)
  }
}


  back(){
    this.router.navigateByUrl("ProductType")
  }
}
