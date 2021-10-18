import { AddBranchComponent } from './AddBranch/add-branch/add-branch.component';
import { Branches } from './../../../service/Interface/interfaces.service';
import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/service/Services/services.service';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Input } from '@angular/core';

@Component({
  selector: 'app-branches',
  templateUrl: './branches.component.html',
  styleUrls: ['./branches.component.scss']
})
export class BranchesComponent implements OnInit {

  @Input()
  b!: Branches;

  observeData: Observable<Branches[]> = this.service.getBranch();
  BranchData!: Branches[];

  dataSaved = false;  
  branchForm : any;  
   allBranches!: Observable<Branches[]>;  
   branchIdUpdate = null;  
   massage = null; 

  constructor(private service: ServicesService, private fb: FormBuilder, 
    private snack: MatSnackBar, private dialogRef: MatDialog,
    private router: Router) { }

  ngOnInit(): void {
    this.observeData.subscribe(res => {
      this.BranchData = res;
    })

    this.branchForm = this.fb.group({
      BranchName: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
      BranchLocationStorageCapacity: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
      BranchAddress: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
    });
    //this.loadAllBranches(); 
  }

  // onFormSubmit() {  
  //   this.dataSaved = false;  
  //   const branch = this.branchForm.value;  
 
  //   this.branchForm.reset();  
  // }

  // loadAllBranches() {  
  //   this.allBranches = this.service.getBranch();
  // } 
  // loadBranchToEdit(BranchID : number) {   
  //   this.service.GetBranchByID(BranchID).subscribe((branch : any)=> {  
  //     this.massage = null;  
  //     this.dataSaved = false;  
  //     this.branchIdUpdate = branch.BranchID;    
  //     this.branchForm.controls['BranchName'].setValue(branch.BranchName);  
  //    this.branchForm.controls['BranchLocationStorageCapacity'].setValue(branch.BranchLocationStorageCapacity);  
  //     this.branchForm.controls['BranchAddress'].setValue(branch.BranchAddress);  
      
     
  //   });
  //   this.router.navigateByUrl('editBranch');
  // }
 
  AddBranch(){
    this.router.navigateByUrl("AddBranch")
  }

  editBranch(){
    
    this.router.navigateByUrl("editBranch/" + this.service.GetBranchByID(this.b.BranchID).subscribe((branch : any)=>{
      console.log(branch);
      this.branchForm.controls['BranchName'].setValue(branch.BranchName);
    }))
  }

deleteBranch(branchid:number){
  
    this.service.removeBranch(branchid).subscribe((res:any)=>{
      console.log(res);
      if (res.Success===false)
      {
        this.snack.open('Branch not Deleted.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });
      
        return;
      }

      else if (res.Success===true)
      {
        this.snack.open('Successful Deleted Branch', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
        });
      }
      window.location.reload();
    });
    
  }
}


