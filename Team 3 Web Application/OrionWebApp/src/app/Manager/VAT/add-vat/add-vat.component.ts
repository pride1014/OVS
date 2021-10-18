import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/service/Services/services.service';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
@Component({
  selector: 'app-add-vat',
  templateUrl: './add-vat.component.html',
  styleUrls: ['./add-vat.component.scss']
})
export class AddVATComponent implements OnInit {

  form: FormGroup = this.fb.group({
    VATDate: ['', Validators.compose([Validators.required])],
    VATPercentage: ['', Validators.compose([Validators.required, Validators.maxLength(2), Validators.minLength(1)])],
  });

  constructor(private service: ServicesService, private fb: FormBuilder, 
    private snack: MatSnackBar, private dialogRef: MatDialogRef<AddVATComponent>,
    private router: Router) { }

  ngOnInit(): void {
  }

  CreateVAT(){
    this.service.CreateVAT(this.form.value).subscribe((res:any) => {
      //this.dialogRef.close();
console.log(res);
      if (res.Success===false)
      {
        console.log(res);
        this.snack.open('VAT not added.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });
        this.form.reset();
        return;
      }

      else if (res.Success===true)
      {
        this.snack.open('Successful Added VAT', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
          
          
        });
        this.router.navigateByUrl("VAT")
      }
    }, (error: HttpErrorResponse) => {
      if (error.status === 403) {
        this.snack.open('This user has already been registered.', 'OK', {
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

  back(){
    this.router.navigateByUrl("VAT")
  }

}
