import { Branches, CashRegister, Discount, EmployeeType, Product, RawMaterial, Unit, Recipe, Supplier, JobStatus, Job, Size, ProductSize, Price, CartLine,  CustomerType, CartItem } from './../Interface/interfaces.service';
import { BehaviorSubject } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User, UserAccess, Customer, ProductType, ProductCategory, Employee, VAT } from '../Interface/interfaces.service';

@Injectable({
  providedIn: 'root'
})
export class ServicesService {
  [x: string]: any;
  public itemCount!:number;
  cart: CartItem[]=[];
  //Local host server
  server = 'https://localhost:44387/api/';

  httpOptions = {
    headers: new HttpHeaders({
      ContentType: 'application/json'
    })
  };
  constructor(private http: HttpClient) { }
  //

  AddUser(user: User) {
    return this.http.post(`${this.server}User/CreateUser`, user, this.httpOptions);
  }

  GetUserByID(ID: number) {
    return this.http.get<any>(`${this.server}User/getUserByID/ ${ID}`).pipe(map(res => res));

  }

  //Update User
  UpdateUser(branches: User): Observable<User> {
    return this.http.put<User>(`${this.server}User/UpdateUser`, branches, this.httpOptions);
  }

  GetUser(): Observable<User[]> {
    return this.http.get<User[]>(`${this.server}User/GetUser`).pipe(map(res => res));
  }
  //Login
  sendUserLogin(user: User) {
    return this.http.post(`${this.server}User/Login`, user, this.httpOptions);
  }

  //UserPermission
  getUserPermission(): Observable<UserAccess[]> {
    return this.http.get<UserAccess[]>(`${this.server}UserAccess/GetUserAccess`)
      .pipe(map(res => res));
  }

  //get ID
  GetUserAccessByID(UserAccessID: number) {
    return this.http.post(`${this.server}UserAccess/getUserAccessByID/` + UserAccessID, this.httpOptions);
  }

  DeleteUserAccess(userAccessId: number) {

    return this.http.delete(`${this.server}UserAccess/DeleteUserAccess/` + userAccessId,
      this.httpOptions);
  }

  //Customer

  //Register Customer 
  RegisterCustomer(customer: Customer) {
    return this.http.post(`${this.server}Customer/CreateCustomer`, customer, this.httpOptions);
  }

  //Get Customer
  getCustomer(): Observable<Customer[]> {
    return this.http.get<Customer[]>(`${this.server}Customer/GetCustomer`)
      .pipe(map(res => res));
  }

  //Update Customers
  updateCustomer(customer: Customer): Observable<any> {
    return this.http.post(`${this.server}Customer/UpdateCustomer`, customer, this.httpOptions);
  }

  //Delete Employee
  deleteCusotmer(toRemove: Customer): void {
    let _bs = new BehaviorSubject<Customer[]>([]);
    let customer = [..._bs.getValue()];
    const index = customer.findIndex(x => x.CustomerId === toRemove.CustomerId);

    if (index > -1) {
      customer.splice(index, 1);
      _bs.next(customer);
    }
  }



  //Product 

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.server}Product/GetProduct`)
      .pipe(map(res => res));
  }

  GetProductByTypeID(typeID: number) {
    return this.http.get<Product[]>(`${this.server}Product/getProductByCatTypeID/ ${typeID}`).pipe(map(res => res));
  }

  GetProductByID(ID: number) {
    return this.http.get<any>(`${this.server}Product/GetProductsByID/ ${ID}`).pipe(map(res => res));
    //return this.http.post(`${this.server}Product/GetProductsByID/`+ ID ,this.httpOptions);
  }

  //Update Product
  UpdateProduct(products: Product): Observable<Product> {
    return this.http.put<Product>(`${this.server}Product/UpdateProduct`, products, this.httpOptions);
  }

  DeleteProducts(productId: number) {
    return this.http.delete(`${this.server}Product/DeleteProduct/` + productId,
      this.httpOptions);
  }

  getTestProduct(product: FormData) {
    return this.http.get(`${this.server}Product/CreateProduct`)
      .pipe(map(res => res));
  }

  //Product Category

  getCategories(): Observable<ProductCategory[]> {
    return this.http.get<ProductCategory[]>(`${this.server}ProductCategory/GetProductCategory`)
      .pipe(map(res => res));
  }



  //AddCategory
  CreateCategory(category: ProductCategory) {
    return this.http.post(`${this.server}ProductCategory/CreateProductCategory`, category, this.httpOptions);
  }

  // Get  Category by ID
  GetProductCategoryByID(ID: number) {
    return this.http.get<any>(`${this.server}ProductCategory/getProductCategoryByID/ ${ID}`).pipe(map(res => res));

  }

  UpdateProductCategory(type: ProductCategory): Observable<any> {
    return this.http.put(`${this.server}ProductCategory/UpdateProductCategory`, type, this.httpOptions);
  }


  DeleteCategory(catId: number) {
    return this.http.delete(`${this.server}ProductCategory/DeleteProductCatagory/` + catId,
      this.httpOptions);
  }

  //Product Type 

  getProductTypes(): Observable<ProductType[]> {
    return this.http.get<ProductType[]>(`${this.server}ProductType/GetProductType`)
      .pipe(map(res => res));
  }
  // get Product Types by Cat id
  GetProductTypesByCatID(CatID: number) {
    return this.http.get<ProductType[]>(`${this.server}ProductType/getProductTypeCategoryID/ ${CatID}`).pipe(map(res => res));
  }
  //Create Product type
  CreateProductType(type: ProductType) {
    return this.http.post(`${this.server}ProductType/CreateProductType`, type, this.httpOptions);
  }

  GetTypeByID(ID: number) {
    //return this.http.post(`${this.server}Branch/GetBranchByID/` + BranchID, this.httpOptions);
    return this.http.get<any>(`${this.server}ProductType/getProductTypeByID/ ${ID}`).pipe(map(res => res));
  }

  DeleteProductType(typeId: number) {
    return this.http.delete(`${this.server}ProductType/DeleteProductType/` + typeId,
      this.httpOptions);
  }

  UpdateProductType(type: ProductType): Observable<any> {
    return this.http.put(`${this.server}ProductType/UpdateProductType`, type, this.httpOptions);
  }


  //Employee Typescript
  getEmployeeType(): Observable<EmployeeType[]> {
    return this.http.get<EmployeeType[]>(`${this.server}EmployeeType/GetEmployeeType`)
      .pipe(map(res => res));
  }

  RegisterEmployeeType(employee: EmployeeType) {
    return this.http.post(`${this.server}EmployeeType/CreateEmployeeType`, employee, this.httpOptions);
  }

  DeleteEmployeeType(typeId: number) {
    return this.http.delete(`${this.server}EmployeeType/DeleteEmployeeType/` + typeId,
      this.httpOptions);
  }

  //Update EmployeeType
  UpdateEmployeeType(employee: EmployeeType): Observable<any> {
    return this.http.put(`${this.server}EmployeeType/EditEmployeeType`, employee, this.httpOptions);
  }

  // Get an employee type ID
  GetEmployeeTypeByID(ID: number) {
    return this.http.get<any>(`${this.server}EmployeeType/GetEmployeeTypeByID/ ${ID}`).pipe(map(res => res));
    //return this.http.post(`${this.server}Product/GetProductsByID/`+ ID ,this.httpOptions);
  }

  //Employee

  //Get Employee
  getEmployee(): Observable<Employee[]> {
    return this.http.get<Employee[]>(`${this.server}Employee/GetEmployee`)
      .pipe(map(res => res));
  }

  //Create EmployeesComponent
  RegisterEmployee(employee: Employee) {
    return this.http.post(`${this.server}Employee/CreateEmployee`, employee, this.httpOptions);
  }

  GetEmployeeByID(ID: number) {
    return this.http.get<any>(`${this.server}Employee/GetEmployeeByID/ ${ID}`).pipe(map(res => res));
    //return this.http.post(`${this.server}Product/GetProductsByID/`+ ID ,this.httpOptions);
  }

  //Update Employee
  UpdateEmployee(employee: Employee): Observable<any> {
    return this.http.put(`${this.server}Employee/UpdateEmployee`, employee, this.httpOptions);
  }

  //Delete Employee
  deleteEmployee(employeeid: number) {
    return this.http.delete(`${this.server}Employee/DeleteEmployee/` + employeeid,
      this.httpOptions);
  }


  //VAT 
  //Get VAT
  getVAT(): Observable<VAT[]> {
    return this.http.get<VAT[]>(`${this.server}VAT/GetVAT`)
      .pipe(map(res => res));
  }

  //Create VAT 
  CreateVAT(vat: VAT) {
    return this.http.post(`${this.server}VAT/AddVAT`, vat, this.httpOptions);
  }

  //Branch
  //Get Branch
  getBranch(): Observable<Branches[]> {
    return this.http.get<Branches[]>(`${this.server}Branch/GetBranch`)
      .pipe(map(res => res));
  }
  // get branch by id
  GetBranchByID(BranchID: number) {
    //return this.http.post(`${this.server}Branch/GetBranchByID/` + BranchID, this.httpOptions);
    return this.http.get<any>(`${this.server}Branch/GetBranchByID/ ${BranchID}`).pipe(map(res => res));
  }

  //Create Branch
  CreateBranch(branches: Branches) {
    return this.http.post(`${this.server}Branch/CreateBranch`, branches, this.httpOptions);
  }

  //Update Branch
  UpdateBranch(branches: Branches): Observable<Branches> {
    return this.http.put<Branches>(`${this.server}Branch/UpdateBranch`, branches, this.httpOptions);
  }

  //Remove Branch
  removeBranch(branchid: number) {

    return this.http.delete(`${this.server}Branch/DeleteBranch/` + branchid,
      this.httpOptions);
  }

  //Cash Register

  //get cash -register
  getCashRegister(): Observable<CashRegister[]> {
    return this.http.get<CashRegister[]>(`${this.server}CashRegister/GetCashRegister`)
      .pipe(map(res => res));
  }


  //Create Cash Register
  CreateCashRegister(register: CashRegister) {
    return this.http.post(`${this.server}CashRegister/CreateCashRegister`, register, this.httpOptions);
  }

  // Get  get Cash Register By ID
  GetCashRegisterByID(ID: number) {
    return this.http.get<any>(`${this.server}CashRegister/GetCashRegisterByID/ ${ID}`).pipe(map(res => res));

  }

  //Update Branch
  UpdateCashRegister(register: CashRegister): Observable<any> {
    return this.http.put(`${this.server}CashRegister/UpdateCashRegister`, register, this.httpOptions);
  }

  //Remove Cash Register
  removeCashRegister(CashRegisterID: number) {
    return this.http.delete(`${this.server}CashRegister/DeleteCashRegister/` + CashRegisterID,
      this.httpOptions);
  }

  //AddCategory
  CreateProduct(product: FormData) {
    return this.http.post(`${this.server}Product/CreateProduct`, product, this.httpOptions);
  }

  //Raw Materials
  //Get Raw Materials
  getRawMaterials(): Observable<RawMaterial[]> {
    return this.http.get<RawMaterial[]>(`${this.server}rawmaterials/Getrawmaterials`)
      .pipe(map(res => res));
  }

  //Create Raw Materials
  CreateRawMaterials(rawmaterials: RawMaterial) {
    return this.http.post(`${this.server}rawmaterials/CreateRawMaterial`, rawmaterials, this.httpOptions);
  }

  // Get  ge tRawMaterial By ID
  GetRawMaterialByID(ID: number) {
    return this.http.get<any>(`${this.server}rawmaterials/getRawMaterialByID/ ${ID}`).pipe(map(res => res));

  }

  //Update Raw Materials
  UpdateRawMaterials(rawmaterials: RawMaterial): Observable<any> {
    return this.http.put(`${this.server}rawmaterials/UpdateRawMaterial`, rawmaterials, this.httpOptions);
  }

  //Delete Raw Materials 
  removeRM(RMID: number) {
    return this.http.delete(`${this.server}rawmaterials/DeleteRawMaterial/` + RMID,
      this.httpOptions);
  }

  getUnit(): Observable<Unit[]> {
    return this.http.get<Unit[]>(`${this.server}unit/GetUnit`)
      .pipe(map(res => res));
  }



  //Discount
  //Get Discount
  getDiscount(): Observable<Discount[]> {
    return this.http.get<Discount[]>(`${this.server}discount/GetDiscount`)
      .pipe(map(res => res));
  }
  // get Discount by id
  GetDiscountByID(DiscountID: number) {
    return this.http.post(`${this.server}discount/getDiscountByID/` + DiscountID, this.httpOptions);
  }

  //Create Discount
  CreateDiscount(Discounts: Discount) {
    return this.http.post(`${this.server}discount/CreateDiscount`, Discounts, this.httpOptions);
  }

  //Update Discount
  UpdateDiscount(Discounts: Discount): Observable<Discount> {
    return this.http.put<Discount>(`${this.server}discount/updatediscount`, Discounts, this.httpOptions);
  }


  DeleteDiscount(DiscountID: number) {

    return this.http.delete(`${this.server}discount/DeleteDiscount/` + DiscountID,
      this.httpOptions);
  }

  //Recipe
  //Get Recipe
  getRecipe(): Observable<Recipe[]> {
    return this.http.get<Recipe[]>(`${this.server}Recipe/GetRecipe`)
      .pipe(map(res => res));
  }
  // get Recipe by id

  GetRecipeByID(ID: number) {
    return this.http.get<any>(`${this.server}Recipe/getRecipeByID/ ${ID}`).pipe(map(res => res));

  }
  //Create Recipe
  // CreateRecipe(Recipe: Recipe) {
  //   return this.http.post(`${this.server}Recipe/CreateRecipe`, Recipe, this.httpOptions);
  // }

  CreateRecipe(Recipe: Recipe) {
    return this.http.post(`${this.server}RecipeLine/CreateRecipeLine`, Recipe, this.httpOptions);
  }

  //Update Discount
  UpdateRecipe(Recipe: Recipe): Observable<Recipe> {
    return this.http.put<Recipe>(`${this.server}Recipe/UpdateRecipe`, Recipe, this.httpOptions);
  }

  //Delete Recipe
  DeleteRecipe(RecipeID: number) {
    return this.http.delete(`${this.server}Recipe/DeleteRecipe/` + RecipeID,
      this.httpOptions);
  }


  //Get Supplier
  getSupplier(): Observable<Supplier[]> {
    return this.http.get<Supplier[]>(`${this.server}Supplier/GetSupplierr`)
      .pipe(map(res => res));
  }

  GetSupplierByID(ID: number) {
    return this.http.get<any>(`${this.server}Supplier/getSupplierByID/ ${ID}`).pipe(map(res => res));

  }

  //Create Supplier
  RegisterSupplier(Supplier: Supplier) {
    return this.http.post(`${this.server}Supplier/CreateSupplier`, Supplier, this.httpOptions);
  }

  //Update Supplier
  UpdateSupplier(Supplier: Supplier): Observable<any> {
    return this.http.put(`${this.server}Supplier/UpdateSupplier`, Supplier, this.httpOptions);
  }

  //Delete Supplier
  deleteSupplier(Supplierid: number) {
    return this.http.delete(`${this.server}Supplier/DeleteSupplier/` + Supplierid,
      this.httpOptions);
  }

  //Job Status
  getJobStatus(): Observable<JobStatus[]> {
    return this.http.get<JobStatus[]>(`${this.server}JobStatus/GetJobStatus`)
      .pipe(map(res => res));
  }


  //Create Job
  CreateJob(job: Job) {
    return this.http.post(`${this.server}Job/CreateJob`, job, this.httpOptions);
  }


  //Get Sizes
  getSizes(): Observable<Size[]> {
    return this.http.get<Size[]>(`${this.server}Size/GetSize`)
      .pipe(map(res => res));
  }
  // get Size by id

  GetSizeByID(ID: number) {
    return this.http.get<any>(`${this.server}Size/getSizeByID/ ${ID}`).pipe(map(res => res));

  }
  //Create Size
  CreateSize(size: Size) {
    return this.http.post(`${this.server}Size/CreateSize`, size, this.httpOptions);
  }

  //Update Size
  UpdateSize(size: Size): Observable<Size> {
    return this.http.put<Size>(`${this.server}Size/UpdateSize`, size, this.httpOptions);
  }

  //Delete Size
  DeleteSize(ID: number) {
    return this.http.delete(`${this.server}Size/DeleteSize/` + ID,
      this.httpOptions);
  }


  //Get Product Sizes
  getProductSizes(): Observable<ProductSize[]> {
    return this.http.get<ProductSize[]>(`${this.server}ProductSize/GetProductSize`)
      .pipe(map(res => res));
  }

  //Create Product Size
  CreateProductSize(Psize: ProductSize) {
    return this.http.post(`${this.server}ProductSize/CreateProductSize`, Psize, this.httpOptions);
  }
//Get Product Size by ID
  GetProductSizeByID(ID: number) {
    return this.http.get<any>(`${this.server}ProductSize/getProductSizeByID/ ${ID}`).pipe(map(res => res));

  }

  GetProductSizeByProductID(ID: number) {
    return this.http.get<any>(`${this.server}ProductSize/getProductSIzeByProductID/ ${ID}`).pipe(map(res => res));

  }

   //Update Size
   UpdateProductSize(size: ProductSize): Observable<ProductSize> {
    return this.http.put<ProductSize>(`${this.server}ProductSize/UpdateProductSize`, size, this.httpOptions);
  }


    //Delete Product  Size
    DeleteProductSize(ID: number) {
      return this.http.delete(`${this.server}ProductSize/DeleteProductSize/` + ID,
        this.httpOptions);
    }


    //Get Price
    getPrices(): Observable<Price[]> {
      return this.http.get<Price[]>(`${this.server}Price/GetPrice`)
        .pipe(map(res => res));
    }
  
    //Create Price
    CreatePrice(Psize: Price) {
      return this.http.post(`${this.server}Price/CreatePrice`, Psize, this.httpOptions);
    }

    UpdatePrice(Price: Price): Observable<Price> {
      return this.http.put<Price>(`${this.server}Price/UpdatePrice`, Price, this.httpOptions);
    }


      // Get ProductSizes By SizeID
  GetProductSizesBySizeID(ID: number) {
    return this.http.get<ProductSize[]>(`${this.server}ProductSize/getProductSIzeBySizeID/ ${ID}`).pipe(map(res => res));
  }


    //Add to Cart
    AddtoCart(cart: CartLine) {
      return this.http.post(`${this.server}cartline/CreateCartLine`, cart, this.httpOptions);
    }


      //Create Customer Type
  CreateCustomerType(type: CustomerType) {
    return this.http.post(`${this.server}CustomerType/CreateCustomerType`, type, this.httpOptions);
  }

  //Update Customer Type
  UpdateCustomerType(type: CustomerType): Observable<CustomerType> {
    return this.http.put<CustomerType>(`${this.server}CustomerType/updatecustomertype`, type, this.httpOptions);
  }

  //Delete Customer Type
  DeleteCustomerType(ID: number) {
    return this.http.delete(`${this.server}CustomerType/DeleteCustomerType/` + ID,
      this.httpOptions);
  }

   //Get Customer Type
   getCustomerType(): Observable<CustomerType[]> {
    return this.http.get<CustomerType[]>(`${this.server}CustomerType/GetCustomerType`)
      .pipe(map(res => res));
  }

  GetCustomerTypeByID(ID: number) {
    return this.http.get<any>(`${this.server}CustomerType/getCustomerTypeByID/ ${ID}`).pipe(map(res => res));
   
  }

  addProduct(product: ProductSize):number{
    let added = false;
    for (let p of this.cart) {
      if (p.ProductID === product.ProductID) {
        p.Quantityonhand += 1;
        product.Quantityonhand -=1;
        added = true;
        break;
      }
    }
    if (!added) {
      this.cart.push(
        {ProductName: product.ProductName,
        ProductID: product.ProductID,
        PriceAmount: product.PriceAmount,
        Quantityonhand: product.Quantityonhand,
        ProductImage: product.ProductImage,
        // category: product.,
        ProductDescription: product.ProductDescription,
        total_price: product.PriceAmount,
        SizeDescription : product.SizeDescription,
        SizeID: product.SizeID,
        ProductSizeID: product.ProductSizeID,
        PriceID: product.PriceID,
       });
      console.log(product.ProductName +  ' - ' + product.Quantityonhand + ' added to cart');
    }
    this.itemCount +=1;
    this.cartItemCount.next(this.cartItemCount.value + 1);
    console.log(this.itemCount);
    return this.itemCount;
  }
  removeItem(product: Product){
   // alert(product.ProductName + " has been deleted successfully!");
    for (let [index, p] of this.cart.entries()) {
      if (p.ProductID === product.ProductID) {
        this.itemCount -= p.Quantityonhand;
        this.cartItemCount.next(this.cartItemCount.value - p.Quantityonhand);
        this.cart.splice(index, 1);
      }
    }
    console.log(this.itemCount);
    return this.itemCount;
  }
  getCart(){
    return this.cart;
  }
  getTotal(){
    return this.cart.reduce((i : any, j: any) => i + j.price * j.quantity, 0);
  }
  resetCart(){
    this.cartItemCount.next(this.cartItemCount.value * 0);
    this.itemCount = 0;
    return this.cart=[];
  }
}
