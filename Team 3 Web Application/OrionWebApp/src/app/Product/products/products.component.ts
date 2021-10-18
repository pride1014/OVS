import { Product, ProductType } from './../../service/Interface/interfaces.service';
import { OpenDialogComponent } from './../../Dialog/open-dialog/open-dialog.component';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ServicesService } from 'src/app/service/Services/services.service';
import { InterfacesService , ProductCategory } from 'src/app/service/Interface/interfaces.service';
import { Observable } from 'rxjs';
import {MatMenuModule, MatMenuTrigger} from '@angular/material/menu';


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {
  observeCategories: Observable<ProductCategory[]> = this.service.getCategories();
  CategoryData!: ProductCategory[];

  observeProductType: Observable<ProductType[]> = this.service.getProductTypes();
  TypeData!: ProductType[];

  observeProduct: Observable<Product[]> = this.service.getProducts();
  ProductData!: Product[];
  productId!: number; 

  observeProductD: Observable<any> = this.service.GetProductByID(this.productId);
  ProductdData!:any ;

  


  ProductDetailsData!: any;

  check: boolean = true;
  
  constructor(private route: Router , private service : ServicesService , private interfaces : InterfacesService,private _avRoute: ActivatedRoute) {
    if (this._avRoute.snapshot.params["id"]) {  
      this.productId = this._avRoute.snapshot.params["id"];  
  } 
   }

  ngOnInit(): void {
    this.observeCategories.subscribe(res => {
      this.CategoryData = res;
    })

    this.observeProductType.subscribe(res => {
      this.TypeData = res;
    })

    this.observeProduct.subscribe(res => {
      this.ProductData = res;
      console.log(res);
      
    })
  }
  
  @ViewChild('menuuTrigger')
  clickHoverMenuTrigger!: MatMenuTrigger;

  openOnMouseOver() {
    this.clickHoverMenuTrigger.openMenu();
  }

  openLogin(){
    this.route.navigateByUrl("Login")
  }

  DisplayProductDetails(id : number)
  {

    this.service.GetProductByID(id).subscribe(x=>
    {
      console.log(x);
      this.ProductdData=x;
  
    })
 
  }



  DisplayProductCat(catid : number)
  {
    let ProductTypeID: number[] =  [];
    let ProductTypeName: string[] =  [];
    let CatagoryID: number[] =  [];
    this.service.GetProductTypesByCatID(catid).subscribe(x=>
    {
      console.log(x);
      this.TypeData=x;
  ProductTypeID= x.map(x => x.ProductTypeID);
    ProductTypeName = x.map(x => x.ProductTypeName);
 CatagoryID= x.map(x => x.ProductCategoryID);
    })
  }





  DisplayProducts(catid : number)
  {
    let ProductTypeID: number[] =  [];
    let ProductTypeName: string[] =  [];
    let CatagoryID: number[] =  [];
    this.service.GetProductByTypeID(catid).subscribe(x=>
    {
      console.log(x);
      this.ProductData=x;

    })
  }


  
}
