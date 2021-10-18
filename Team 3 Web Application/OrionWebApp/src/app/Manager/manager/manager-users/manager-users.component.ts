import { User } from './../../../service/Interface/interfaces.service';
import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/service/Services/services.service';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Input } from '@angular/core';

@Component({
  selector: 'app-manager-users',
  templateUrl: './manager-users.component.html',
  styleUrls: ['./manager-users.component.scss']
})
export class ManagerUsersComponent implements OnInit {

  observeData: Observable<User[]> = this.service.GetUser();
  userData!: User[];

  constructor(private service: ServicesService, private fb: FormBuilder, 
    private snack: MatSnackBar, private dialogRef: MatDialog,
    private router: Router) { }

  ngOnInit(): void {
    this.observeData.subscribe(res => {
      this.userData = res;
    })}


    AddUser(){
      this.router.navigateByUrl("AddUser")
    }
  



}
