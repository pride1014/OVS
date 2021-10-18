import { BranchesComponent } from './../../branches.component';
import { Branches } from 'src/app/service/Interface/interfaces.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ServicesService } from 'src/app/service/Services/services.service';

@Component({
  selector: 'app-edit-branch',
  templateUrl: './edit-branch.component.html',
  styleUrls: ['./edit-branch.component.scss']
})
export class EditBranchComponent implements OnInit {

  @ViewChild(BranchesComponent) edit: any;

  id!: number;

  dataSaved = false;
  branchForm: any;
  allBranches!: Observable<Branches[]>;
  branchIdUpdate = null;
  massage = null;
  constructor(private service: ServicesService, private fb: FormBuilder,
    private snack: MatSnackBar,
    private router: Router, private r: ActivatedRoute) { }

  ngOnInit(): void {
    this.branchForm = this.fb.group({
      BranchID: ['', Validators.compose([Validators.required])],
      BranchName: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
      BranchLocationStorageCapacity: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
      BranchAddress: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
    });
    this.loadAllBranches();
    this.id = +this.r.snapshot.params['id']
    
    this.r.paramMap.subscribe(parameterMap =>{
      const id = parameterMap.get('BranchID');
      //this.service.GetBranchByID(id);
    })
  }
  onFormSubmit() {
    this.dataSaved = false;
    const branch = this.branchForm.value;
   
this.loadBranchToEdit(branch);
    this.branchForm.reset();
  }

  loadAllEmployees() {  
    this.allBranches = this.service.getBranch();  
  }

  loadBranchToEdit(BranchID: number) {
    this.service.GetBranchByID(BranchID).subscribe((branch: any) => {
      console.log(branch);
      this.massage = null;
      this.dataSaved = false;
      this.branchIdUpdate = branch.BranchID;
      this.branchForm.controls['BranchName'].setValue(branch.BranchName);
      this.branchForm.controls['BranchLocationStorageCapacity'].setValue(branch.BranchLocationStorageCapacity);
      this.branchForm.controls['BranchAddress'].setValue(branch.BranchAddress);

      this.loadAllBranches(); 
    });
  }

  loadAllBranches() {
    this.allBranches = this.service.getBranch();
  }

  back() {
    this.router.navigateByUrl('Branch')
  }
}

