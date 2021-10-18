import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Branches, CashRegister} from 'src/app/service/Interface/interfaces.service';
import { ServicesService } from 'src/app/service/Services/services.service';

@Component({
  selector: 'app-cash-register',
  templateUrl: './cash-register.component.html',
  styleUrls: ['./cash-register.component.scss']
})
export class CashRegisterComponent implements OnInit {

  observeData: Observable<CashRegister[]> = this.service.getCashRegister();
  RegisterData!: CashRegister[];


  //TRY

  observeBData: Observable<Branches[]> = this.service.getBranch();
  BranchData!: Branches[];

  constructor(private service: ServicesService, private fb: FormBuilder, 
    private snack: MatSnackBar, private dialogRef: MatDialog,
    private router: Router) { }

  ngOnInit(): void {
    this.observeData.subscribe(res => {
      this.RegisterData = res;
     
    })


    //TRY

    this.observeBData.subscribe(res => {
      this.BranchData = res;
    })
  }

  AddCashRegster(){
    this.router.navigateByUrl("AddCashRegster")
  }


  EditCashRegster(){
    this.router.navigateByUrl("EditCashRegster")
  }

  deleteCashRegister(CashRegisterID: number) {
    this.service.removeCashRegister(CashRegisterID).subscribe((res: any) => {
      console.log(res);
      if (res.Success === false) {
        this.snack.open('Cash Register not deleted.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });

        return;
      }

      else if (res.Success === true) {
        this.snack.open('Successful Deleted Cash Register', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
        });
      }
      window.location.reload();
    });

  }

}
