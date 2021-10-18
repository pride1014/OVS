import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Product, ProductCategory, ProductType } from 'src/app/service/Interface/interfaces.service';
import { ServicesService } from 'src/app/service/Services/services.service';


@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.scss']
})
export class AddProductComponent implements OnInit {
  image: any;
  title: string = "Add Product";
  productId!: number;
  errorMessage: any;
  productList: Array<any> = [];
  form!: FormGroup;


  observeData: Observable<ProductType[]> = this.service.getProductTypes();
  ProductTypeData!: ProductType[];
  typeparams: ProductType =
    {
    ProductTypeID: 0,
    ProductTypeName: '',
    ProductCategoryID: 0,
    ProductCategoryName: ''
  }


  constructor(private service: ServicesService, private fb: FormBuilder,
    private snack: MatSnackBar, private dialogRef: MatDialogRef<AddProductComponent>,
    private router: Router, private _avRoute: ActivatedRoute) {

    if (this._avRoute.snapshot.params["id"]) {
      this.productId = this._avRoute.snapshot.params["id"];
    }

    this.form = this.fb.group({
      ProductID: [''],
      ProductName: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
      ProductDescription: ['', Validators.compose([Validators.required, Validators.maxLength(50), Validators.minLength(2)])],
      Quantityonhand: ['', Validators.compose([Validators.required])],
      ProductTypeID: ['', Validators.compose([Validators.required])],
      ProductImage: ['', Validators.compose([Validators.required])],
    });

  }

  ngOnInit(): void {

    this.observeData.subscribe(res => {
      this.ProductTypeData = res;
      console.log(res);

    })

    if (this.productId > 0) {
      this.title = "Edit Prouduct";
      this.service.GetProductByID(this.productId)
        .subscribe(resp => {
          console.log(resp)
          this.form = this.fb.group({
            ProductID: [resp.Product_ID],
            ProductName: [resp.Product_Name, Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
            ProductDescription: [resp.Product_Description, Validators.compose([Validators.required, Validators.maxLength(50), Validators.minLength(2)])],
            Quantityonhand: [resp.Quantity_on_hand, Validators.compose([Validators.required])],
            ProductTypeID: [resp.Product_Type_ID, Validators.compose([Validators.required])],
            ProductImage: [resp.Product_Image, Validators.compose([Validators.required])],
          });

        })
    }
  }

  AddProductType(){
    this.router.navigateByUrl("AddProductType")
  }

  CreateProduct() {
    if (!this.form.valid) {
      return;
    }

    console.log(this.image)
    const form = this.form.value
    const formData = new FormData();
    formData.append('ProductName', form.ProductName)
    formData.append('ProductDescription', form.ProductDescription)
    formData.append('Quantityonhand', form.Quantityonhand)
    formData.append('ProductTypeID', form.ProductTypeID)
    formData.append('ProductImage', form.ProductImage)
    formData.append('image', this.image)

    if (this.title == "Add Product") {
      this.service.CreateProduct(formData).subscribe((res: any) => {

        if (res.Success === false) {
          this.snack.open('Product not created.', 'OK', {
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
            duration: 3000
          });
          this.form.reset();
          return;
        }

        else if (res.Success === true) {
          this.snack.open('Successful Added Product ', 'OK', {
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
            duration: 3000
          });
          this.router.navigateByUrl("ManageProducts")
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
    else if (this.title == "Edit Branch") {
      this.service.UpdateProduct(form)
        .subscribe((data: any) => {
          console.log(data);
          this.router.navigate(['/ManageProducts']);
        }, error => this.errorMessage = error)
    }

  }

  SelectFile(event: any) {
    if (event.target.files) {
      this.image = event.target.files[0];
    }
  }

  back() {
    this.router.navigateByUrl("ManageProducts")
  }

}
