import { Discount } from './../../service/Interface/interfaces.service';
import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/service/Services/services.service';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-discount',
  templateUrl: './discount.component.html',
  styleUrls: ['./discount.component.scss']
})
export class DiscountComponent implements OnInit {

  observeData: Observable<Discount[]> = this.service.getDiscount();
  DiscountData!: Discount[];

  constructor(private service: ServicesService, private fb: FormBuilder, 
    private snack: MatSnackBar, private dialogRef: MatDialog,
    private router: Router) { }

  ngOnInit(): void {

    this.observeData.subscribe(res => {
      this.DiscountData = res;
    })
  }

  AddDiscount(){
    this.router.navigateByUrl("AddEditDiscount")
  }

  deleteDiscount(DiscountID:number){
  
    this.service.DeleteDiscount(DiscountID).subscribe((res:any)=>{
      console.log(res);
      if (res.Success===false)
      {
        this.snack.open('Discont not deleted.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });
      
        return;
      }

      else if (res.Success===true)
      {
        this.snack.open('Successful Deleted Discount', 'OK', {
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
