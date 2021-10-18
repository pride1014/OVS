import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ServicesService } from 'src/app/service/Services/services.service';

@Component({
  selector: 'app-add-branch',
  templateUrl: './add-branch.component.html',
  styleUrls: ['./add-branch.component.scss']
})
export class AddBranchComponent implements OnInit {

  title: string = "Create Branch";
  branchId!: number;
  errorMessage: any;
  branchList: Array<any> = [];
  branchForm!: FormGroup;



  constructor(private service: ServicesService, private fb: FormBuilder,
    private snack: MatSnackBar, private dialogRef: MatDialogRef<AddBranchComponent>,
    private router: Router, private _avRoute: ActivatedRoute) {

    if (this._avRoute.snapshot.params["id"]) {
      this.branchId = this._avRoute.snapshot.params["id"];
    }

    this.branchForm = this.fb.group({
      BranchID: [''],
      BranchName: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
      BranchLocationStorageCapacity: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
      BranchAddress: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
    });
  }




  ngOnInit(): void {

    this.service.getBranch().subscribe(
      data => this.branchList = data
    )
    //  this.branchForm.patchValue({ first: "BranchName" });
    if (this.branchId > 0) {
      this.title = "Edit Branch";
      this.service.GetBranchByID(this.branchId)
        .subscribe(resp => {
          console.log(resp)
          this.branchForm = this.fb.group({
            BranchID: [resp.Branch_ID],
            BranchName: [resp.Branch_Name, Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
            BranchLocationStorageCapacity: [resp.Branch_Location_Storage_Capacity, Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
            BranchAddress: [resp.Branch_Address, Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
          });

        })
    }

  }

  save() {

    if (!this.branchForm.valid) {
      return;
    }

    if (this.title == "Create Branch") {
      this.service.CreateBranch(this.branchForm.value)
        .subscribe((data: any) => {

          if (data.Success === false) {
            this.snack.open('Branch not added.', 'OK', {
              verticalPosition: 'bottom',
              horizontalPosition: 'center',
              duration: 3000
            });
            this.branchForm.reset();
            return;
          }

          else if (data.Success === true) {
            this.snack.open('Successful Added Branch', 'OK', {
              horizontalPosition: 'center',
              verticalPosition: 'bottom',
              duration: 3000
            });
            this.router.navigate(['Branch']);
            console.log(data);

          }

        }, error => this.errorMessage = error)
    }
    else if (this.title == "Edit Branch") {
      this.service.UpdateBranch(this.branchForm.value)
        .subscribe((data: any) => {
          console.log(data);
          this.router.navigate(['/Branch']);
        }, error => this.errorMessage = error)
    }
  }



  // CreateBranch(){
  //   this.service.CreateBranch(this.form.value).subscribe((res:any) => {
  //    // this.dialogRef.close();

  //     if (res.Success===false)
  //     {
  //       this.snack.open('Branch not added.', 'OK', {
  //         verticalPosition: 'bottom',
  //         horizontalPosition: 'center',
  //         duration: 3000
  //       });
  //       this.form.reset();
  //       return;
  //     }

  //     else if (res.Success===true)
  //     {
  //       this.snack.open('Successful Added Branch', 'OK', {
  //         horizontalPosition: 'center',
  //         verticalPosition: 'bottom',
  //         duration: 3000
  //       });
  //       this.router.navigateByUrl("Branch")
  //       console.log(res);

  //     }
  //   }, (error: HttpErrorResponse) => {
  //     if (error.status === 403) {
  //       this.snack.open('This branch has already exists.', 'OK', {
  //         horizontalPosition: 'center',
  //         verticalPosition: 'bottom',
  //         duration: 3000
  //       });
  //     }
  //     this.snack.open('An error occurred on our servers, try again', 'OK', {
  //       horizontalPosition: 'center',
  //       verticalPosition: 'bottom',
  //       duration: 3000
  //     });
  //    //this.dialogRef.close();
  //   })
  // }

  back() {
    this.router.navigateByUrl("Branch")
  }



  get BranchName() { return this.branchForm.get('BranchName'); }
  get BranchLocationStorageCapacity() { return this.branchForm.get('BranchLocationStorageCapacity'); }
  get BranchAddress() { return this.branchForm.get('BranchAddress'); }

}
