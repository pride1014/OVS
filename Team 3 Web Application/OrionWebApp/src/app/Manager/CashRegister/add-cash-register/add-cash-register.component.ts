import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Branches } from 'src/app/service/Interface/interfaces.service';
import { ServicesService } from 'src/app/service/Services/services.service';

@Component({
  selector: 'app-add-cash-register',
  templateUrl: './add-cash-register.component.html',
  styleUrls: ['./add-cash-register.component.scss']
})
export class AddCashRegisterComponent implements OnInit {

  title: string = "Add Cash Register";
  registerId!: number;
  errorMessage: any;
  branchList: Array<any> = [];
  form!: FormGroup;

 


  constructor(private service: ServicesService, private fb: FormBuilder,
    private snack: MatSnackBar, private dialogRef: MatDialogRef<AddCashRegisterComponent>,
    private router: Router, private _avRoute: ActivatedRoute) { 

      if (this._avRoute.snapshot.params["id"]) {
        this.registerId = this._avRoute.snapshot.params["id"];
      }

      this.form = this.fb.group({
        RegisterID: [''],
        CashRegisterName: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
        BranchID: ['', Validators.compose([Validators.required])],
      });
    }

  observeData: Observable<Branches[]> = this.service.getBranch();
  BranchData!: Branches[];
  BranchParams: Branches = {
    BranchName: '',
    BranchLocationStorageCapacity: 0,
    BranchAddress: '',
    BranchID: 0,
  }

  ngOnInit(): void {
    this.observeData.subscribe(res => {
      this.BranchData = res;
    })

    if (this.registerId > 0) {
      this.title = "Edit Cash Register";
      this.service.GetCashRegisterByID(this.registerId)
        .subscribe(resp => {
          console.log(resp)
          this.form = this.fb.group({
            RegisterID: [resp.Register_ID],
            CashRegisterName: [resp.Cash_Register_Name, Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
            BranchID: [resp.Branch_ID, Validators.compose([Validators.required])],
          });

        })
    }
  }

  AddBranch(){
    this.router.navigateByUrl("AddBranch")
  }

  CreateCashRegister() {

    if (!this.form.valid) {
      return;
    }
    if (this.title == "Add Cash Register") {
    this.service.CreateCashRegister(this.form.value).subscribe((res: any) => {
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
        this.snack.open('Successful Added Cash Register', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
        });
        this.router.navigateByUrl("CashRegster")
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
  else if (this.title == "Edit Cash Register") {
    this.service.UpdateCashRegister(this.form.value)
      .subscribe((data: any) => {
        console.log(data);
        
        if (data.Success === false) {
          this.snack.open('Cash Register not Updated.', 'OK', {
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
            duration: 3000
          });
       
          this.router.navigate(['/CashRegster']);
          return;
        }

        else if (data.Success === true) {
          this.snack.open('Successful Updated Cash Register', 'OK', {
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
            duration: 3000
          });
        this.router.navigate(['/CashRegster']);
        }
      }, error => this.errorMessage = error)
  }

}


  back() {
    this.router.navigateByUrl("CashRegster")
  }

}
