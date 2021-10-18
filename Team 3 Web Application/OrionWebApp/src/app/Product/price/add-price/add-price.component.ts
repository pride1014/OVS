import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Branches, ProductSize } from 'src/app/service/Interface/interfaces.service';
import { ServicesService } from 'src/app/service/Services/services.service';

@Component({
  selector: 'app-add-price',
  templateUrl: './add-price.component.html',
  styleUrls: ['./add-price.component.scss']
})
export class AddPriceComponent implements OnInit {

  title: string = "Add Price";
  priceId!: number;
  errorMessage: any;
  branchList: Array<any> = [];
  form!: FormGroup;

  constructor(private service: ServicesService, private fb: FormBuilder,
    private snack: MatSnackBar,
    private router: Router, private _avRoute: ActivatedRoute) {
    if (this._avRoute.snapshot.params["id"]) {
      this.priceId = this._avRoute.snapshot.params["id"];
    }

    this.form = this.fb.group({
      PriceID: [''],
      PriceAmount: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
      PriceDate: ['', Validators.compose([Validators.required])],
      ProductSizeID: ['', Validators.compose([Validators.required])],
    });
  }

  observeData: Observable<ProductSize[]> = this.service.getProductSizes();
  productSizeData!: ProductSize[];



  ngOnInit(): void {
    this.observeData.subscribe(res => {
      this.productSizeData = res;
    })

    if (this.priceId > 0) {
      this.title = "Edit Price";
      this.service.GetCashRegisterByID(this.priceId)
        .subscribe(resp => {
          console.log(resp)
          this.form = this.fb.group({
            PriceID: [resp.Price_ID],
            PriceAmount: [resp.Price_Amount, Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
            PriceDate: [resp.Product_ID, Validators.compose([Validators.required])],
            ProductSizeID: [resp.Product_Size_ID, Validators.compose([Validators.required])],
          });

        })
    }
  }


  CreatePrice() {

    if (!this.form.valid) {
      return;
    }
    if (this.title == "Add Price") {
    this.service.CreatePrice(this.form.value).subscribe((res: any) => {
      if (res.Success === false) {
        this.snack.open('Price not created.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });
        this.form.reset();
        return;
      }

      else if (res.Success === true) {
        this.snack.open('Successful Added Price', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
        });
        this.router.navigateByUrl("price")
        console.log(res);

      }
    }, (error: HttpErrorResponse) => {
      if (error.status === 403) {
        this.snack.open('This Price has already exists.', 'OK', {
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
  else if (this.title == "Edit Price") {
    this.service.UpdatePrice(this.form.value)
      .subscribe((data: any) => {
        console.log(data);
        
        if (data.Success === false) {
          this.snack.open('Price not Updated.', 'OK', {
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
            duration: 3000
          });
       
          this.router.navigate(['/price']);
          return;
        }

        else if (data.Success === true) {
          this.snack.open('Successful Updated price', 'OK', {
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
            duration: 3000
          });
        this.router.navigate(['/price']);
        }
      }, error => this.errorMessage = error)
  }

}

}
