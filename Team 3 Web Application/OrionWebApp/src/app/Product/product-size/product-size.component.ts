import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import {  Product, ProductSize, Size} from 'src/app/service/Interface/interfaces.service';
import { ServicesService } from 'src/app/service/Services/services.service';

@Component({
  selector: 'app-product-size',
  templateUrl: './product-size.component.html',
  styleUrls: ['./product-size.component.scss']
})
export class ProductSizeComponent implements OnInit {
  [x: string]: any;

  observeData: Observable<ProductSize[]> = this.service.getProductSizes();
  productSizeData!: ProductSize[];



  constructor(private service: ServicesService, private fb: FormBuilder, 
    private snack: MatSnackBar, private dialogRef: MatDialog,
    private router: Router) { }


    observePData: Observable<Product[]> = this.service.getProducts();
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

    this.observeData.subscribe(res => {
      this.productSizeData = res;
      
     
    })


    this.observeSData.subscribe(res => {
      this.SizesData = res;
    })

    this.observePData.subscribe(res => {
      this.ProductData = res;
    })
  }


  AddProductSize()
  {
    this.router.navigateByUrl("AddProductSize")
  }


  deleteProductSize(id:number){
  
    this.service.DeleteProductSize(id).subscribe((res:any)=>{
      console.log(res);
      if (res.Success===false)
      {
        this.snack.open('Product Size not Deleted.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });
      
        return;
      }

      else if (res.Success===true)
      {
        this.snack.open('Successful Deleted Product Size', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
        });
        console.log(res);
      }
      window.location.reload();
    });
    
  }
}
