import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ProductType, CashRegister} from 'src/app/service/Interface/interfaces.service';
import { ServicesService } from 'src/app/service/Services/services.service';

@Component({
  selector: 'app-product-type',
  templateUrl: './product-type.component.html',
  styleUrls: ['./product-type.component.scss']
})
export class ProductTypeComponent implements OnInit {

  observeData: Observable<ProductType[]> = this.service.getProductTypes();
  ProductTypeData!: ProductType[];

  constructor(private service: ServicesService, private fb: FormBuilder, 
    private snack: MatSnackBar, private dialogRef: MatDialog,
    private router: Router) { }

  ngOnInit(): void {

    this.observeData.subscribe(res => {
      this.ProductTypeData = res;
      console.log(res);
     
    })
  }

  AddProductType(){
    this.router.navigateByUrl("AddProductType")
  }

 
  deleteProductType(TypeID: number) {
    this.service.DeleteProductType(TypeID).subscribe((res: any) => {
      console.log(res);
      if (res.Success === false) {
        this.snack.open('Product Type not deleted.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });

        return;
      }

      else if (res.Success === true) {
        this.snack.open('Successful Deleted Product Type', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
        });
      }
      window.location.reload();
    });

  }
}
