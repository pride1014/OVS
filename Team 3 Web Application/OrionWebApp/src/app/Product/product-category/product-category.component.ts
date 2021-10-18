import { ProductCategory } from './../../service/Interface/interfaces.service';
import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/service/Services/services.service';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-product-category',
  templateUrl: './product-category.component.html',
  styleUrls: ['./product-category.component.scss']
})
export class ProductCategoryComponent implements OnInit {

  observeData: Observable<ProductCategory[]> = this.service.getCategories();
  CategoryData!: ProductCategory[];

  constructor(private service: ServicesService, private fb: FormBuilder, 
    private snack: MatSnackBar, private dialogRef: MatDialog,
    private router: Router) { }

  ngOnInit(): void {
    this.observeData.subscribe(res => {
      this.CategoryData = res;
    })
  }

  AddCategory(){
    this.router.navigateByUrl("AddCategory")
  }

  deleteCatagory(CatagoryID: number){
    this.service.DeleteCategory(CatagoryID).subscribe((res: any) => {
      console.log(res);
      if (res.Success === false) {
        this.snack.open('Catagory not deleted.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });

        return;
      }

      else if (res.Success === true) {
        this.snack.open('Successful Deleted Catagory', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
        });
      }
      window.location.reload();
    });
  }

}
