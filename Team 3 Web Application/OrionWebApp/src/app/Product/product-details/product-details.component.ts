import { Component, OnInit } from '@angular/core';
import { Router , ActivatedRoute} from '@angular/router';
import { ServicesService } from 'src/app/service/Services/services.service';
import { InterfacesService, Product, ProductCategory, ProductSize } from 'src/app/service/Interface/interfaces.service';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
 count: number=0;
  productID!: number;
  product_id!: string;
  ProductName!: string;
 observeProductD: Observable<any> = this.service.GetProductByID(this.productID);
 ProductdData!:any ;
 ProductSizes : ProductSize[]= [];
// product!: Product[];

 observeProduct: Observable<ProductSize[]> = this.service.getProductSizes();
 ProductData!: any;


  constructor(private route: Router , 
    private service : ServicesService ,
     private interfaces : InterfacesService,
     private actRoute: ActivatedRoute) { 
      if (this.actRoute.snapshot.params["id"]) {  
        this.productID = this.actRoute.snapshot.params["id"];  
    } 
    this.product_id = this.actRoute.snapshot.params.id;
    this.ProductName = this.actRoute.snapshot.params.id;
     }

  ngOnInit(): void {
    this.count = this.service.itemCount;
    this.service.GetProductSizeByProductID(this.productID).subscribe(x=>
      {
        console.log(x);
        this.ProductData=x;
    
      })
  
  }

  DisplayProductDetails(catid : number)
  {
    this.service.GetProductByID(catid).subscribe(x=>
    {
      console.log(x);
      this.ProductData=x;
  
    })
 
  }

  // select(category:string){
  //   this.service.getProducts().subscribe( product =>{
  //     this.ProductdData = product;
  //     if(category==="All"){
  //       this.category = this.ProductSizes;  
  //     }else
  //     {
  //     this.category = this.ProductdData.filter(pro =>(pro.category=== category));
  //     }
  //     console.log(this.category);
  //   }) 
  // }

  addItem(product:ProductSize){
    // alert(product.name+' has been added to Cart');
   // console.log(this.count);
    this.count = this.service.addProduct(product);
    console.log(this.count);
    
  }
  // addToWishlist(product:Product){
  //   // alert(product.name+' has been added to wishlist');
  //   this.wishlistCount = this.wishlistServise.addWishlist(product);
  // }
 
  // openCart(): void {
  //   const dialogRef = this.dialog.open(CartComponent, 
  //     {
  //       width: '80%',
  //      });
  //   dialogRef.afterClosed().subscribe(res => {
  //     this.count = this.dataService.itemCount;
  //   });

  // } 
  // openWishList(): void {
  //   const dialogRef = this.dialog.open(WishlistComponent, 
  //     {
  //       width: '80%',
  //      });
  //   dialogRef.afterClosed().subscribe(res => {
  //     this.wishlistCount = this.wishlistServise.wishlistCount;
  //   });

  // }

  


  openLogin(){
    this.route.navigateByUrl("Login")
  }

}
