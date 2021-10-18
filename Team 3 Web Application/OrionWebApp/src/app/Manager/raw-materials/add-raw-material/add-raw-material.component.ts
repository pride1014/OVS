import { RawMaterial, Unit } from './../../../service/Interface/interfaces.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { ServicesService } from 'src/app/service/Services/services.service';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-add-raw-material',
  templateUrl: './add-raw-material.component.html',
  styleUrls: ['./add-raw-material.component.scss']
})
export class AddRawMaterialComponent implements OnInit {

  title: string = "Add Raw Material";
  rawMaterialId!: number;
  errorMessage: any;
  branchList: Array<any> = [];
  form!: FormGroup;



  observeData: Observable<Unit[]> = this.service.getUnit();
  UnitData!: Unit[];
  Unitparams: Unit =
    {
      UnitID: 0,
      UnitMeasurement: "",

    }

  constructor(private service: ServicesService, private fb: FormBuilder,
    private snack: MatSnackBar, private dialogRef: MatDialogRef<AddRawMaterialComponent>,
    private router: Router, private _avRoute: ActivatedRoute) {

    if (this._avRoute.snapshot.params["id"]) {
      this.rawMaterialId = this._avRoute.snapshot.params["id"];
    }

    this.form = this.fb.group({
      RawMaterialID: [''],
      RawMaterialName: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
      QuantityOnhand: ['', Validators.compose([Validators.required, Validators.maxLength(50), Validators.minLength(2)])],
      Rawmaterialdescription: ['', Validators.compose([Validators.required])],
      
      UnitMeasurement:  ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(1)])],
    });
  }

  ngOnInit(): void {
    this.observeData.subscribe(res => {
      this.UnitData = res;
      console.log(res);

    })

    if (this.rawMaterialId > 0) {
      this.title = "Edit Raw Material";
      this.service.GetRawMaterialByID(this.rawMaterialId)
        .subscribe(resp => {
          console.log(resp)
          this.form = this.fb.group({
            RawMaterialID: [resp.Raw_Material_ID],
            RawMaterialName: [resp.Raw_Material_Name, Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
            QuantityOnhand: [resp.Quantity_on_hand, Validators.compose([Validators.required, Validators.maxLength(50), Validators.minLength(2)])],
            Rawmaterialdescription: [resp.Raw_material_description, Validators.compose([Validators.required])],
            
          });

        })
    }
  }

  CreateRawMaterial() {

    if (!this.form.valid) {
      return;
    }
    if (this.title == "Add Raw Material") {

      this.service.CreateRawMaterials(this.form.value).subscribe((res: any) => {

        if (res.Success === false) {
          this.snack.open('Raw Material not added.', 'OK', {
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
            duration: 3000
          });
          this.form.reset();
          return;
        }

        else if (res.Success === true) {
          this.snack.open('Successful Added Raw Material ', 'OK', {
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
            duration: 3000
          });
          this.router.navigateByUrl("RawMaterials")
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

    else if (this.title == "Edit Raw Material") {
      this.service.UpdateRawMaterials(this.form.value)
        .subscribe((data: any) => {
          console.log(data);
          
          if (data.Success === false) {
            this.snack.open('Raw Material not Updated.', 'OK', {
              verticalPosition: 'bottom',
              horizontalPosition: 'center',
              duration: 3000
            });
         
            this.router.navigate(['/RawMaterials']);
            return;
          }
  
          else if (data.Success === true) {
            this.snack.open('Successful Updated Raw Material', 'OK', {
              horizontalPosition: 'center',
              verticalPosition: 'bottom',
              duration: 3000
            });
          this.router.navigate(['/RawMaterials']);
          }
        }, error => this.errorMessage = error)
    }
  }

  back() {
    this.router.navigateByUrl("RawMaterials")
  }

}
