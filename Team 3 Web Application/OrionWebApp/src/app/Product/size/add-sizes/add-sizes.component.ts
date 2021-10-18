import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ServicesService } from 'src/app/service/Services/services.service';

@Component({
  selector: 'app-add-sizes',
  templateUrl: './add-sizes.component.html',
  styleUrls: ['./add-sizes.component.scss']
})
export class AddSizesComponent implements OnInit {

  title: string = "Add Size";
  sizeId!: number;
  errorMessage: any;
  sizeList: Array<any> = [];
  form!: FormGroup;

  constructor(private service: ServicesService, private fb: FormBuilder,
    private snack: MatSnackBar,
    private router: Router, private _avRoute: ActivatedRoute) {
    if (this._avRoute.snapshot.params["id"]) {
      this.sizeId = this._avRoute.snapshot.params["id"];
    }

    this.form = this.fb.group({
      SizeID: [''],
      SizeDescription: ['', Validators.compose([Validators.required, Validators.maxLength(15), Validators.minLength(2)])],
    });

  }

  ngOnInit(): void {

    if (this.sizeId > 0) {
      this.title = "Edit Size";
      this.service.GetSizeByID(this.sizeId)
        .subscribe(resp => {
          console.log(resp)
          this.form = this.fb.group({
            SizeID: [resp.Size_ID],
            SizeDescription: [resp.Size_Description, Validators.compose([Validators.required, Validators.maxLength(15), Validators.minLength(2)])],
          });

        })
    }
  }

  Save() {
    if (!this.form.valid) {
      return;
    }

    if (this.title == "Add Size") {
      this.service.CreateSize(this.form.value).subscribe((res: any) => {
        // this.dialogRef.close();
        console.log(res)
        if (res.Success === false) {
          this.snack.open('Size not added.', 'OK', {
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
            duration: 3000
          });
          this.form.reset();
          return;
        }

        else if (res.Success === true) {
          this.snack.open('Successful Added Size', 'OK', {
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
            duration: 3000
          });
          this.router.navigateByUrl("size")
          console.log(res);
  
        }
      }, (error: HttpErrorResponse) => {
        if (error.status === 403) {
          this.snack.open('This Size has already exists.', 'OK', {
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

    else if (this.title == "Edit Size") {
      this.service.UpdateSize(this.form.value)
        .subscribe((data: any) => {
          console.log(data);
          
          if (data.Success === false) {
            this.snack.open('Size not Updated.', 'OK', {
              verticalPosition: 'bottom',
              horizontalPosition: 'center',
              duration: 3000
            });
         
            this.router.navigate(['/size']);
            return;
          }
  
          else if (data.Success === true) {
            this.snack.open('Successful Updated Size', 'OK', {
              horizontalPosition: 'center',
              verticalPosition: 'bottom',
              duration: 3000
            });
          this.router.navigate(['/size']);
          }
        }, error => this.errorMessage = error)
    }
  
  


  }

  back() {
    this.router.navigateByUrl("size")
  }

}
