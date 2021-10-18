import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ServicesService } from 'src/app/service/Services/services.service';
import { OpenDialogComponent } from 'src/app/Dialog/open-dialog/open-dialog.component';
import { Observable } from 'rxjs';
import { UserAccess } from 'src/app/service/Interface/interfaces.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss']
})
export class AddUserComponent implements OnInit {

  title: string = "Add User";
  userId!: number;
  errorMessage: any;
  branchList: Array<any> = [];
  form!: FormGroup;



  observeUserAccess: Observable<UserAccess[]> = this.service.getUserPermission();
  UserAccessData!: UserAccess[];
  UserAccessparams: UserAccess =
    {
      UserAccessPermissionID: 0,
      UserRoleName: "",
      UserRoleDescription: "",
    }

  constructor(private service: ServicesService, private fb: FormBuilder,
    private snack: MatSnackBar, private dialogRef: MatDialogRef<AddUserComponent>,
    private router: Router, private _avRoute: ActivatedRoute) {

    if (this._avRoute.snapshot.params["id"]) {
      this.userId = this._avRoute.snapshot.params["id"];
    }

    this.form = this.fb.group({
      UserID: [''],
      UserName: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
      UserPassword: ['', Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
      UserAccessPermissionID: ['', Validators.compose([Validators.required])]
    });
  }

  ngOnInit(): void {
    this.observeUserAccess.subscribe(res => {
      this.UserAccessData = res;
      console.log(res);

    })

    if (this.userId > 0) {
      this.title = "Edit User";
      this.service.GetUserByID(this.userId)
        .subscribe(resp => {
          console.log(resp)
          this.form = this.fb.group({
            UserID: [resp.User_ID],
            UserName: [resp.User_Name, Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
            UserPassword: [resp.User_Password, Validators.compose([Validators.required, Validators.maxLength(20), Validators.minLength(2)])],
            UserAccessPermissionID: [resp.User_Access_Permission_ID, Validators.compose([Validators.required])]
          });

        })
    }
  }


  RegisterUser() {


    if (!this.form.valid) {
      return;
    }
    if (this.title == "Add User") {
      this.service.AddUser(this.form.value).subscribe((res: any) => {
        // this.dialogRef.close();
        console.log(res);
        this.snack.open('Successful registration', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
        });

        if (res.Success === false) {
          this.snack.open('Enter valid details.', 'OK', {
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
            duration: 3000
          });
          this.form.reset();
          return;
        }

        else if (res.Success === true) {
          this.snack.open('This user has already been registered.', 'OK', {
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
            duration: 3000

          });
          this.router.navigateByUrl("Login")
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
        // this.dialogRef.close();
      })
    }

    else if (this.title == "Edit User") {
      this.service.UpdateUser(this.form.value)
        .subscribe((data: any) => {
          console.log(data);
          
          if (data.Success === false) {
            this.snack.open('User not Updated.', 'OK', {
              verticalPosition: 'bottom',
              horizontalPosition: 'center',
              duration: 3000
            });
         
            this.router.navigate(['/Login']);
            return;
          }
  
          else if (data.Success === true) {
            this.snack.open('Successful Updated User', 'OK', {
              horizontalPosition: 'center',
              verticalPosition: 'bottom',
              duration: 3000
            });
          this.router.navigate(['/Login']);
          }
        }, error => this.errorMessage = error)
    }

  }



  back() {
    this.router.navigateByUrl("Login")
  }

  backLogin() {
    this.router.navigateByUrl("Login")
  }
}
