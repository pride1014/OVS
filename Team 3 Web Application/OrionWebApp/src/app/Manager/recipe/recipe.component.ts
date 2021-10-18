import { Recipe } from './../../service/Interface/interfaces.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Branches } from 'src/app/service/Interface/interfaces.service';
import { ServicesService } from 'src/app/service/Services/services.service';

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.scss']
})
export class RecipeComponent implements OnInit {

  observeData: Observable<Recipe[]> = this.service.getRecipe();
  RecipeData!: Recipe[];

  constructor(private service: ServicesService, private fb: FormBuilder, 
    private snack: MatSnackBar, private dialogRef: MatDialog,
    private router: Router) { }

  ngOnInit(): void {
    this.observeData.subscribe(res => {
      this.RecipeData = res;
    })
  }
  AddRecipe(){
    this.router.navigateByUrl("AddRecipe")
  }

  editRecipe(){
    this.router.navigateByUrl("EditRecipe")
  }

deleteRecipe(RecipeID:number){
  
    this.service.DeleteRecipe(RecipeID).subscribe((res:any)=>{
      console.log(res);
      if (res.Success===false)
      {
        this.snack.open('Recipe not Deleted.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });
      
        return;
      }

      else if (res.Success===true)
      {
        this.snack.open('Successful Deleted Recipe', 'OK', {
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
