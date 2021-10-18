import { ManageCustomersComponent } from './Manager/manager/manage-customers/manage-customers.component';
import { ProductQuoteComponent } from './Product/product-quote/product-quote.component';
import { SaleComponent } from './Product/sale/sale.component';
import { AddCustomerTypeComponent } from './Customer/customers/customer-type/add-customer-type/add-customer-type.component';
import { AddPriceComponent } from './Product/price/add-price/add-price.component';
import { AddProductSizeComponent } from './Product/product-size/add-product-size/add-product-size.component';
import { SizeComponent } from './Product/size/size.component';
import { ProductDetailsComponent } from './Product/product-details/product-details.component';
import { QuoteComponent } from './Employee/employee-duties/quote/quote.component';
import { EmployeeDutiesComponent } from './Employee/employee-duties/employee-duties.component';
import { AddSupplierComponent } from './Manager/supplier/add-supplier/add-supplier.component';
import { SupplierComponent } from './Manager/supplier/supplier.component';
import { ManagerUsersComponent } from './Manager/manager/manager-users/manager-users.component';
import { EditRecipeComponent } from './Manager/recipe/edit-recipe/edit-recipe.component';
import { AddRecipeComponent } from './Manager/recipe/add-recipe/add-recipe.component';
import { RecipeComponent } from './Manager/recipe/recipe.component';
import { EmployeeTypeComponent } from './Manager/EmployeeType/employee-type/employee-type.component';
import { DeleteRawMaterialComponent } from './Manager/raw-materials/delete-raw-material/delete-raw-material.component';
import { UpdateRawMaterialComponent } from './Manager/raw-materials/update-raw-material/update-raw-material.component';
import { AddRawMaterialComponent } from './Manager/raw-materials/add-raw-material/add-raw-material.component';
import { RawMaterialsComponent } from './Manager/raw-materials/raw-materials.component';
import { EditProductComponent } from './Product/products/editProduct/edit-product/edit-product.component';
import { ProductCategoryComponent } from './Product/product-category/product-category.component';
import { AddVATComponent } from './Manager/VAT/add-vat/add-vat.component';
import { DeleteBranchComponent } from './Manager/Branch/branches/deleteBranch/delete-branch/delete-branch.component';
import { EditBranchComponent } from './Manager/Branch/branches/editBranch/edit-branch/edit-branch.component';
import { EditEmployeeComponent } from './Employee/edit-employee/edit-employee.component';
import { VATComponent } from './Manager/VAT/vat/vat.component';
import { ShiftsComponent } from './Manager/Shift/shifts/shifts.component';
import { ReportsComponent } from './Manager/Reports/reports/reports.component';
import { JobsComponent } from './Manager/Jobs/jobs/jobs.component';
import { CashRegisterComponent } from './Manager/CashRegister/cash-register/cash-register.component';
import { BranchesComponent } from './Manager/Branch/branches/branches.component';
import { AddEmployeeComponent } from './Employee/add-employee/add-employee.component';
import { RegisterComponent } from './Customer/RegisterCusomter/register/register.component';
import { CustomersComponent } from './Customer/customers/customers.component';
import { LoginComponent } from './Login/login/login.component';
import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './Product/products/products.component';
import { EmployeesComponent } from './Employee/employees/employees.component';
import { AddUserComponent } from './User/add-user/add-user.component';
import { ManagerComponent } from './Manager/manager/manager.component';
import { AddBranchComponent } from './Manager/Branch/branches/AddBranch/add-branch/add-branch.component';
import { AddCashRegisterComponent } from './Manager/CashRegister/add-cash-register/add-cash-register.component';
import { EditCashRegisterComponent } from './Manager/CashRegister/edit-cash-register/edit-cash-register.component';
import { AddProductCategoryComponent } from './Product/product-category/add-product-category/add-product-category.component';
import { ProductTypeComponent } from './Product/product-type/product-type.component';
import { AddProductTypeComponent } from './Product/product-type/add-product-type/add-product-type.component';
import { AddProductComponent } from './Product/products/add-product/add-product.component';
import { ManageProductsComponent } from './Manager/manager/manage-products/manage-products.component';
import { DiscountComponent } from './Manager/discount/discount.component';
import { AddEditDiscountComponent } from './Manager/discount/add-edit-discount/add-edit-discount.component';
import { AddEmployeeTypeComponent } from './Manager/EmployeeType/add-employee-type/add-employee-type.component';
import { AddSizesComponent } from './Product/size/add-sizes/add-sizes.component';
import { ProductSizeComponent } from './Product/product-size/product-size.component';
import { PriceComponent } from './Product/price/price.component';
import { CustomerTypeComponent } from './Customer/customers/customer-type/customer-type.component';

const routes: Routes = [
  {
    path: '',
    component: ProductsComponent
  },
  {
    path: 'Login',
    component: LoginComponent
  },
  {
    path: 'AddCustomer',
    component: RegisterComponent
  },
  {
    path: 'Customers',
    component: ManageCustomersComponent
  },
  { path: 'Customers/edit/:id', component: RegisterComponent }
  ,
  {
    path: 'Register',
    component: RegisterComponent
  },
  {
    path: 'Employee',
    component: EmployeesComponent
  },
  {
    path: 'AddEmployee',
    component: AddEmployeeComponent
  },
  { path: 'Employee/edit/:id', component: AddEmployeeComponent }
  ,
  {
    path: 'ManageUsers/edit/:id',
    component: AddUserComponent
  },
  {
    path: 'AddUser',
    component: AddUserComponent
  },
  {
    path: 'Manager',
    component: ManagerComponent
  },
  {
    path: 'Branch',
    component: BranchesComponent
  },
  {
    path: 'AddBranch',
    component: AddBranchComponent
  },
  {
    path: 'CashRegster',
    component: CashRegisterComponent
  },
  {
    path: 'CustomerType',
    component: CustomerTypeComponent
  },
  {
    path: 'AddCustomerType',
    component: AddCustomerTypeComponent
  },
  {
    path: 'CustomerType/edit/:id',
    component: AddCustomerTypeComponent
  },
  {
    path: 'AddCashRegster',
    component: AddCashRegisterComponent
  },
  {
    path: 'CashRegster/edit/:id',
    component: AddCashRegisterComponent
  },
  {
    path: 'EditCashRegster',
    component: EditCashRegisterComponent
  },
  {
    path: 'Jobs',
    component: JobsComponent
  },
  {
    path: 'Reports',
    component: ReportsComponent
  },
  {
    path: 'Shift',
    component: ShiftsComponent
  },
  {
    path: 'VAT',
    component: VATComponent
  },
  {
    path: 'AddVAT',
    component: AddVATComponent
  },
  {
    path: 'EditEmployee',
    component: EditEmployeeComponent
  },
  {
    path: 'editBranch/:BranchID',
    component: EditBranchComponent
  },
  {
    path: 'deleteBranch',
    component: DeleteBranchComponent
  },
  {
    path: 'ProductCategory',
    component: ProductCategoryComponent
  },
  {
    path: 'AddCategory',
    component: AddProductCategoryComponent
  },
  {
    path: 'ProductCategory/edit/:id',
    component: AddProductCategoryComponent
  },
  {
    path: 'ProductType',
    component: ProductTypeComponent
  },
  {
    path: 'AddProductType',
    component: AddProductTypeComponent
  },
  {
    path: 'ProductType/edit/:id',
    component: AddProductTypeComponent
  },
  {
    path: 'Product',
    component: ProductsComponent
  },
  {
    path: 'AddProduct',
    component: AddProductComponent
  },
  
  { path: 'ManageProducts/edit/:id', component: AddProductComponent }
  ,
  {
    path: 'EditProduct',
    component: EditProductComponent
  },

  {
    path: 'ManageProducts',
    component: ManageProductsComponent
  },
  {
    path: 'RawMaterials',
    component: RawMaterialsComponent
  },
  {
    path: 'AddRawMaterial',
    component: AddRawMaterialComponent
  },
  {
    path: 'RawMaterials/edit/:id',
    component: AddRawMaterialComponent
  },
  {
    path: 'editRawMaterial',
    component: UpdateRawMaterialComponent
  },
  {
    path: 'deleteRawMaterial',
    component: DeleteRawMaterialComponent
  },
  {
    path: 'Discount',
    component: DiscountComponent

  },
  {
    path: 'AddEditDiscount',
    component: AddEditDiscountComponent

  },

  {
    path: 'EmployeeType',
    component: EmployeeTypeComponent

  },

  {
    path: 'AddEmployeeType',
    component: AddEmployeeTypeComponent

  },
  { path: 'EmployeeType/edit/:id', component: AddEmployeeTypeComponent }
  ,
  {
    path: 'Recipe',
    component: RecipeComponent
  },
  {
    path: 'AddRecipe',
    component: AddRecipeComponent
  },
  {
    path: 'Recipe/edit/:id',
    component: AddRecipeComponent
  },
  {
    path: 'EditRecipe',
    component: EditRecipeComponent
  },
  {
    path: 'ManageUsers',
    component: ManagerUsersComponent
  },
  {
    path: 'Suppliers',
    component: SupplierComponent
  },
  {
    path: 'AddSupplier',
    component: AddSupplierComponent
  },
  {
    path: 'Suppliers/edit/:id',
    component: AddSupplierComponent
  },
  {
    path: 'EditSuppliers',
    component: SupplierComponent
  },
  {
    path: 'EmployeeDuties',
    component: EmployeeDutiesComponent
  },
  {
    path: 'Quote',
    component: QuoteComponent
  },
  {
    path: 'Product-Details/:id',
    component: ProductDetailsComponent
  },
  { path: 'branch/edit/:id', component: AddBranchComponent }
  ,
  { path: 'size', component: SizeComponent },
  {
    path: 'AddSize',
    component: AddSizesComponent
  },
  { path: 'size/edit/:id', component: AddSizesComponent }
  ,
  { path: 'ProductSize', component: ProductSizeComponent },
  {
    path: 'AddProductSize',
    component: AddProductSizeComponent
  },
  { path: 'ProductSize/edit/:id', component: AddProductSizeComponent }
  ,
  { path: 'price', component: PriceComponent },
  {
    path: 'AddPrice',
    component: AddPriceComponent
  },
  {
    path: 'price/edit/:id',
    component: AddPriceComponent
  },
  { path: 'sale', component: SaleComponent }
  ,
  { path: 'productquote', component: ProductQuoteComponent }

]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
