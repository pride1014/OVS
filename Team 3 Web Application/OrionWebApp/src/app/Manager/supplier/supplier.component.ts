import { Supplier } from './../../service/Interface/interfaces.service';
import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/service/Services/services.service';
import { Observable } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-supplier',
  templateUrl: './supplier.component.html',
  styleUrls: ['./supplier.component.scss']
})
export class SupplierComponent implements OnInit {

  observeData: Observable<Supplier[]> = this.service.getSupplier();
  SupplierData!: Supplier[];

  supplierForm: any;

  constructor(private service: ServicesService, private fb: FormBuilder,
    private snack: MatSnackBar, private dialogRef: MatDialog,
    private router: Router) { }

  ngOnInit(): void {
    this.observeData.subscribe(res => {
      this.SupplierData = res;
    })


    this.supplierForm = this.fb.group({
      BranchName: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
      BranchLocationStorageCapacity: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
      BranchAddress: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
    });
  }

  AddSupplier() {
    this.router.navigateByUrl("AddSupplier")
  }

  editSupplier() {
    this.router.navigateByUrl("Supplier")
    // this.router.navigateByUrl("editBranch/" + this.service.GetBranchByID(this.b.BranchID).subscribe((branch : any)=>{
    //   console.log(branch);
    //   this.supplierForm.controls['BranchName'].setValue(branch.BranchName);
    // }))
  }

  deleteSupplier(Supplierid: number) {

    this.service.deleteSupplier(Supplierid).subscribe((res: any) => {
      console.log(res);
      if (res.Success === false) {
        this.snack.open('Supplier not Deleted.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });

        return;
      }

      else if (res.Success === true) {
        this.snack.open('Successful Deleted Supplier', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
        });
      }
      window.location.reload();
    });

  }

}
