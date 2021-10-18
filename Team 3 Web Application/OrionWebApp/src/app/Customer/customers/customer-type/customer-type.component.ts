import { CustomerType } from './../../../service/Interface/interfaces.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ServicesService } from 'src/app/service/Services/services.service';

@Component({
  selector: 'app-customer-type',
  templateUrl: './customer-type.component.html',
  styleUrls: ['./customer-type.component.scss']
})
export class CustomerTypeComponent implements OnInit {

  observeCustomerType: Observable<CustomerType[]> = this.service.getCustomerType();
  CustomerTypeData!: CustomerType[];

  constructor(private router: Router,
    private service: ServicesService,
    private fb: FormBuilder,
    private snack: MatSnackBar,
    private dialogRef: MatDialog,) { }

  ngOnInit(): void {
    this.observeCustomerType.subscribe(res => {
      this.CustomerTypeData = res;
    })
  }

  AddCustomerType() {
    this.router.navigateByUrl("AddCustomerType")
  }

  deleteCustomerType(id: number) {
    this.service.DeleteCustomerType(id).subscribe((res: any) => {
      console.log(res);
      if (res.Success === false) {
        this.snack.open('Customer Type not deleted.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });

        return;
      }

      else if (res.Success === true) {
        this.snack.open('Successful Deleted Customer Type', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
        });
      }
      window.location.reload();
    });

  }

}
