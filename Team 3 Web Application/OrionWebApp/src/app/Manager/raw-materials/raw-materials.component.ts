import { RawMaterial } from './../../service/Interface/interfaces.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ServicesService } from 'src/app/service/Services/services.service';

@Component({
  selector: 'app-raw-materials',
  templateUrl: './raw-materials.component.html',
  styleUrls: ['./raw-materials.component.scss']
})
export class RawMaterialsComponent implements OnInit {

  observeData: Observable<RawMaterial[]> = this.service.getRawMaterials();
  MaterialData!: RawMaterial[];

  constructor(private service: ServicesService, private fb: FormBuilder, 
    private snack: MatSnackBar, private dialogRef: MatDialog,
    private router: Router) { }

  ngOnInit(): void {
    this.observeData.subscribe(res => {
    this.MaterialData = res;
    })
  }

  AddRawMaterial(){
    this.router.navigateByUrl("AddRawMaterial")
  }

  editRawMaterial(){
    this.router.navigateByUrl("editRawMaterial")
  }

  deleteRawMaterial(RawMaterialsID: number){
    this.service.removeRM(RawMaterialsID).subscribe((res: any) => {
      console.log(res);
      if (res.Success === false) {
        this.snack.open('Raw Material not deleted.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });

        return;
      }

      else if (res.Success === true) {
        this.snack.open('Successful Deleted Raw Material', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
        });
      }
      window.location.reload();
    });
  }

}
