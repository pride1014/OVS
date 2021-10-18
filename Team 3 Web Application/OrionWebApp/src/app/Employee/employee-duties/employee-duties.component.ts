import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-duties',
  templateUrl: './employee-duties.component.html',
  styleUrls: ['./employee-duties.component.scss']
})
export class EmployeeDutiesComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  Quote()
  {
    this.router.navigateByUrl("Quote")
  }

}
