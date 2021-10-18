import { User, UserAccess } from './../../service/Interface/interfaces.service';
import { RegisterComponent } from './../../Customer/RegisterCusomter/register/register.component';
import { ServicesService } from './../../service/Services/services.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginGroup: FormGroup = this.fb.group({
    UserAccessPermissionID: [],
    UserName: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
    UserPassword: ['', Validators.compose([Validators.required, Validators.maxLength(12), Validators.minLength(8)])],
  });

  @Input()
  b!: UserAccess;

  observeUserAccess: Observable<UserAccess[]> = this.service.getUserPermission();
  UserAccessData!: UserAccess[];
  UserData!: User[];
  UserAccessID!: number;

  constructor(private fb: FormBuilder,
    private service: ServicesService, private snack: MatSnackBar,
    private router: Router, private dialog: MatDialog,) { }

  ngOnInit(): void {
    this.observeUserAccess.subscribe(res => {
      this.UserAccessData = res;
    })
  }

  Login(): void {
    this.service.sendUserLogin(this.loginGroup.value).subscribe((res: any) => {
      // route to home
      localStorage.setItem('user', JSON.stringify(res));
      console.log(res);

      if (res.Success === false) {
        this.snack.open('Invalid credentials.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });
        this.loginGroup.reset();
        return;
      }

      else if (res.Success === true) {
        
        if (res.UserAccessPermissionID == 1) {
          console.log(res);
          this.router.navigateByUrl("")
        }
        else if (res.UserAccessPermissionID == 2) {
          this.router.navigateByUrl("EmployeeDuties")
        }
        else if (res.UserAccessPermissionID == 3) {
          this.router.navigateByUrl("Manager")
        }

      }

    }, (error: HttpErrorResponse) => {

      if (error.status === 404) {
        this.snack.open('Invalid credentials.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });
        this.loginGroup.reset();
        return;
      }
      this.snack.open('An error occured on our servers. Try again later.', 'OK', {
        verticalPosition: 'bottom',
        horizontalPosition: 'center',
        duration: 3000
      });
      this.loginGroup.reset();
    });
  }

  // OpenRegister() {
  //   this.router.navigateByUrl("Register")
  // }

  OpenRegister() {
    this.router.navigateByUrl('Register')
  }

}
