import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Branches, Product, Size } from 'src/app/service/Interface/interfaces.service';
import { ServicesService } from 'src/app/service/Services/services.service';

@Component({
  selector: 'app-add-product-size',
  templateUrl: './add-product-size.component.html',
  styleUrls: ['./add-product-size.component.scss']
})
export class AddProductSizeComponent implements OnInit {
  [x: string]: any;

  title: string = "Add Product Size";
  productSizeId!: number;
  errorMessage: any;
  branchList: Array<any> = [];
  form!: FormGroup;

  constructor(private service: ServicesService, private fb: FormBuilder,
    private snack: MatSnackBar,
    private router: Router, private _avRoute: ActivatedRoute) {
    if (this._avRoute.snapshot.params["id"]) {
      this.productSizeId = this._avRoute.snapshot.params["id"];
    }

    this.form = this.fb.group({
      ProductSizeID: [''],
      ProductID: ['', Validators.compose([Validators.required])],
      SizeID: ['', Validators.compose([Validators.required])],
      PriceAmount :  ['', Validators.compose([Validators.required])],
    });

  }

  observeData: Observable<Product[]> = this.service.getProducts();
  ProductData!: Product[];

  productParams: Product = {
    ProductID: 0,
    ProductName: '',
    ProductDescription: '',
    ProductImage: '',
    ProductTypeID: 0,
    Quantityonhand: 0,
    ProductTypeName: ''
  }



  observeSData: Observable<Size[]> = this.service.getSizes();
  SizesData!: Size[];

  sizesParams: Size = {
    SizeID: 0,
    SizeDescription: '',
  }

  ngOnInit(): void {

    this.observeSData.subscribe(res => {
      this.SizesData = res;
    })

    this.observeData.subscribe(res => {
      this.ProductData = res;
    })

    if (this.productSizeId > 0) {
      this.title = "Edit Product Size";
      this.service.GetProductSizeByID(this.productSizeId)
        .subscribe(resp => {
          console.log(resp)
          this.form = this.fb.group({
            ProductSizeID: [resp.Product_Size_ID],
            ProductID: [resp.Product_ID, Validators.compose([Validators.required])],
            SizeID: [resp.Size_ID, Validators.compose([Validators.required])],
            PriceAmount :  [resp.Price_Amount, Validators.compose([Validators.required])],
          });

        })
    }
  }

  AddSize(){
    this.router.navigateByUrl("AddSize")
  }


  Save() {

    if (!this.form.valid) {
   
      return;
      
    }
    
    if (this.title == "Add Product Size") {
      this.service.CreateProductSize(this.form.value).subscribe((res: any) => {
        
        if (res.Success === false) {
          this.snack.open('Cash Register not created.', 'OK', {
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
            duration: 3000
          });
          this.form.reset();
          return;
        }

        else if (res.Success === true) {
          this.snack.open('Successful Added Product Size', 'OK', {
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
            duration: 3000
          });
          this.router.navigateByUrl("ProductSize")
          console.log(res);

        }
      }, (error: HttpErrorResponse) => {
        if (error.status === 403) {
          this.snack.open('This Product has already exists.', 'OK', {
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

    else if (this.title == "Edit Product Size") {
      this.service.UpdateProductSize(this.form.value)
        .subscribe((data: any) => {
          console.log(data);

          if (data.Success === false) {
            this.snack.open('Product Size not Updated.', 'OK', {
              verticalPosition: 'bottom',
              horizontalPosition: 'center',
              duration: 3000
            });

            this.router.navigate(['/ProductSize']);
            return;
          }

          else if (data.Success === true) {
            this.snack.open('Successful Updated Product Size', 'OK', {
              horizontalPosition: 'center',
              verticalPosition: 'bottom',
              duration: 3000
            });
            this.router.navigate(['/ProductSize']);
          }
        }, error => this.errorMessage = error)
    }
  }

}
