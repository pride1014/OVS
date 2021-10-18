import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Product, RawMaterial } from 'src/app/service/Interface/interfaces.service';
import { ServicesService } from 'src/app/service/Services/services.service';

@Component({
  selector: 'app-add-recipe',
  templateUrl: './add-recipe.component.html',
  styleUrls: ['./add-recipe.component.scss']
})
export class AddRecipeComponent implements OnInit {

  title: string = "Add Recipe";
  recipeId!: number;
  errorMessage: any;
  branchList: Array<any> = [];
  form!: FormGroup;

  observeData: Observable<RawMaterial[]> = this.service.getRawMaterials();
  MaterialData!: RawMaterial[];
  RawParams: RawMaterial = {
    RawMaterialID: 0,
    RawMaterialName: '',
    QuantityOnhand: 0,
    Rawmaterialdescription: '',
    UnitID: 0,
    UnitMeasurement: ''
  }

  observePData: Observable<Product[]> = this.service.getProducts();
  ProductData!: Product[];
  ProductParams: Product = {
    ProductID: 0,
    ProductName: '',
    ProductDescription: '',
    ProductImage: undefined,
    ProductTypeID: 0,
    Quantityonhand: 0,
    ProductTypeName: ''
  }

  constructor(private service: ServicesService, private fb: FormBuilder,
    private snack: MatSnackBar, private dialogRef: MatDialogRef<AddRecipeComponent>,
    private router: Router, private _avRoute: ActivatedRoute) {

    if (this._avRoute.snapshot.params["id"]) {
      this.recipeId = this._avRoute.snapshot.params["id"];
    }

    this.form = this.fb.group({
      RecipeID: [''],
      ProductID: [''],
      RawMaterialID: [''],
      RecipeName: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
      RecipeDescription: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
      QuantityProduced: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
    });
  }

  ngOnInit(): void {

    this.observePData.subscribe(res => {
      this.ProductData = res;
    })
    this.observeData.subscribe(res => {
      this.MaterialData = res;
      })

    if (this.recipeId > 0) {
      this.title = "Edit Recipe";
      this.service.GetRecipeByID(this.recipeId)
        .subscribe(resp => {
          console.log(resp)
          this.form = this.fb.group({
            RecipeID: [resp.Recipe_ID],
            RecipeName: [resp.Recipe_Name, Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
            RecipeDescription: [resp.Recipe_Description, Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
            QuantityProduced: [resp.Quantity_produced, Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
          });

        })
    }
  }
  CreateRecipe() {

    if (!this.form.valid) {
      return;
    }
    if (this.title == "Add Recipe") {

      this.service.CreateRecipe(this.form.value).subscribe((res: any) => {
        if (res.Success === false) {
          this.snack.open('Recipe not added.', 'OK', {
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
            duration: 3000
          });
          console.log(res);
          this.form.reset();
          return;
        }

        else if (res.Success === true) {
          this.snack.open('Successful Added Recipe', 'OK', {
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
            duration: 3000
          });
          this.router.navigateByUrl("Recipe")
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
      })


    }

    else if (this.title == "Edit Recipe") {
      this.service.UpdateRecipe(this.form.value)
        .subscribe((data: any) => {
          console.log(data);
          
          if (data.Success === false) {
            this.snack.open('Recipe not Updated.', 'OK', {
              verticalPosition: 'bottom',
              horizontalPosition: 'center',
              duration: 3000
            });
         
            this.router.navigate(['/Recipe']);
            return;
          }
  
          else if (data.Success === true) {
            this.snack.open('Successful Updated Recipe', 'OK', {
              horizontalPosition: 'center',
              verticalPosition: 'bottom',
              duration: 3000
            });
          this.router.navigate(['/Recipe']);
          }
        }, error => this.errorMessage = error)
    }
  }

  back() {
    this.router.navigateByUrl("Recipe")
  }

}
