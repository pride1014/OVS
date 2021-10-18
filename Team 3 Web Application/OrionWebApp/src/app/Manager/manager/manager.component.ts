import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-manager',
  templateUrl: './manager.component.html',
  styleUrls: ['./manager.component.scss']
})
export class ManagerComponent implements OnInit {

  constructor(private router: Router) {}

  ngOnInit(): void {
  }

  customers()
  {
    this.router.navigateByUrl("Customers")
  }

  customertype()
  {
    this.router.navigateByUrl("CustomerType")
  }

  employee()
  {
    this.router.navigateByUrl("Employee")
  }
  branch()
  {
    this.router.navigateByUrl("Branch")
  }

  ManageProducts()
  {
    this.router.navigateByUrl("ManageProducts")
  }
  CashRegister()
  {
    this.router.navigateByUrl("CashRegster")
  }
  jobs()
  {
    this.router.navigateByUrl("Jobs")
  }
  reports()
  {
    this.router.navigateByUrl("Reports")
  }
  shift()
  {
    this.router.navigateByUrl("Shift")
  }
  VAT()
  {
    this.router.navigateByUrl("VAT")
  }
  RawMaterials(){
    this.router.navigateByUrl("RawMaterials")
  }
  Discount(){
    this.router.navigateByUrl("Discount")
  }
  EmployeeType(){
    this.router.navigateByUrl("EmployeeType")
  }
  ProductCatagory(){
    this.router.navigateByUrl("ProductCategory")
  }
  ProductType(){
    this.router.navigateByUrl("ProductType")
  }
  Recipe(){
    this.router.navigateByUrl("Recipe")
  }

  ManageUsers(){
    this.router.navigateByUrl("ManageUsers")
  }

  Suppliers(){
    this.router.navigateByUrl("Suppliers")
  }

  Sizes(){
    this.router.navigateByUrl("size")
  }


  ProductSizes(){
    this.router.navigateByUrl("ProductSize")
  }

}
