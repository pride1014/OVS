import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Size } from 'src/app/service/Interface/interfaces.service';
import { ServicesService } from 'src/app/service/Services/services.service';

@Component({
  selector: 'app-size',
  templateUrl: './size.component.html',
  styleUrls: ['./size.component.scss']
})
export class SizeComponent implements OnInit {

  observeData: Observable<Size[]> = this.service.getSizes();
  SizesData!: Size[];
  form!: FormGroup;

  constructor(private service: ServicesService, private fb: FormBuilder, 
    private snack: MatSnackBar, private dialogRef: MatDialog,
    private router: Router) { }

  ngOnInit(): void {
    this.observeData.subscribe(res => {
      this.SizesData = res;
    })

    
    this.form = this.fb.group({
      SizeID: [''],
      SizeDescription: ['', Validators.compose([Validators.required, Validators.maxLength(15), Validators.minLength(2)])],
    });
  }


  AddSize(){
    this.router.navigateByUrl("AddSize")
  }

  deleteSize(Sizeid: number) {

    this.service.DeleteSize(Sizeid).subscribe((res: any) => {
      console.log(res);
      if (res.Success === false) {
        this.snack.open('Size not Deleted.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });

        return;
      }

      else if (res.Success === true) {
        this.snack.open('Successful Deleted Size', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
        });
      }
      window.location.reload();
    });

  }
}
