import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/service/Services/services.service';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AddVATComponent } from '../add-vat/add-vat.component';
import { VAT } from 'src/app/service/Interface/interfaces.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-vat',
  templateUrl: './vat.component.html',
  styleUrls: ['./vat.component.scss']
})
export class VATComponent implements OnInit {

  observeVAT: Observable<VAT[]> = this.service.getVAT();
  VatData!: VAT[];

  constructor(private service: ServicesService, private fb: FormBuilder, 
    private snack: MatSnackBar, private dialogRef: MatDialog,
    private router: Router) { }

  ngOnInit(): void {
    this.observeVAT.subscribe(res => {
      this.VatData = res;
    })
  }


  AddVAT() {
    this.router.navigateByUrl("AddVAT")
  }
}
