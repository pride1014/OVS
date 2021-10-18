import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './Login/login/login.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import jsPDF from 'jspdf';

import { HttpClientModule } from '@angular/common/http';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatDialogModule , MatDialogRef} from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule} from '@angular/material/card';
import { MatNativeDateModule } from '@angular/material/core';
import {MatButtonModule} from '@angular/material/button';
import { ProductsComponent } from './Product/products/products.component';
import { CustomersComponent } from './Customer/customers/customers.component';
import {MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import { OpenDialogComponent } from './Dialog/open-dialog/open-dialog.component';
import { RegisterComponent } from './Customer/RegisterCusomter/register/register.component';
import { EmployeesComponent } from './Employee/employees/employees.component';
import { AddEmployeeComponent } from './Employee/add-employee/add-employee.component';
import { AddUserComponent } from './User/add-user/add-user.component';
import { ManagerComponent } from './Manager/manager/manager.component';
import { BranchesComponent } from './Manager/Branch/branches/branches.component';
import { CashRegisterComponent } from './Manager/CashRegister/cash-register/cash-register.component';
import { JobsComponent } from './Manager/Jobs/jobs/jobs.component';
import { ReportsComponent } from './Manager/Reports/reports/reports.component';
import { ShiftsComponent } from './Manager/Shift/shifts/shifts.component';
import { VATComponent } from './Manager/VAT/vat/vat.component';
import { EditEmployeeComponent } from './Employee/edit-employee/edit-employee.component';
import { AddVATComponent } from './Manager/VAT/add-vat/add-vat.component';
import { AddBranchComponent } from './Manager/Branch/branches/AddBranch/add-branch/add-branch.component';
import { EditBranchComponent } from './Manager/Branch/branches/editBranch/edit-branch/edit-branch.component';
import { DeleteBranchComponent } from './Manager/Branch/branches/deleteBranch/delete-branch/delete-branch.component';
import { AddCashRegisterComponent } from './Manager/CashRegister/add-cash-register/add-cash-register.component';
import { EditCashRegisterComponent } from './Manager/CashRegister/edit-cash-register/edit-cash-register.component';
import { ProductCategoryComponent } from './Product/product-category/product-category.component';
import { ProductTypeComponent } from './Product/product-type/product-type.component';
import { AddProductCategoryComponent } from './Product/product-category/add-product-category/add-product-category.component';
import { AddProductTypeComponent } from './Product/product-type/add-product-type/add-product-type.component';
import { AddProductComponent } from './Product/products/add-product/add-product.component';
import { ManageProductsComponent } from './Manager/manager/manage-products/manage-products.component';
import { EditProductComponent } from './Product/products/editProduct/edit-product/edit-product.component';

import { RawMaterialsComponent } from './Manager/raw-materials/raw-materials.component';
import { AddRawMaterialComponent } from './Manager/raw-materials/add-raw-material/add-raw-material.component';
import { UpdateRawMaterialComponent } from './Manager/raw-materials/update-raw-material/update-raw-material.component';
import { DeleteRawMaterialComponent } from './Manager/raw-materials/delete-raw-material/delete-raw-material.component';
import { DiscountComponent } from './Manager/discount/discount.component';
import { AddEditDiscountComponent } from './Manager/discount/add-edit-discount/add-edit-discount.component';
import { EmployeeTypeComponent } from './Manager/EmployeeType/employee-type/employee-type.component';
import { AddEmployeeTypeComponent } from './Manager/EmployeeType/add-employee-type/add-employee-type.component';
import { RecipeComponent } from './Manager/recipe/recipe.component';
import { AddRecipeComponent } from './Manager/recipe/add-recipe/add-recipe.component';
import { EditRecipeComponent } from './Manager/recipe/edit-recipe/edit-recipe.component';
import { ManagerUsersComponent } from './Manager/manager/manager-users/manager-users.component';
import { SupplierComponent } from './Manager/supplier/supplier.component';
import { AddSupplierComponent } from './Manager/supplier/add-supplier/add-supplier.component';
import { EmployeeDutiesComponent } from './Employee/employee-duties/employee-duties.component';
import { QuoteComponent } from './Employee/employee-duties/quote/quote.component';
import { AddJobComponent } from './Manager/Jobs/add-job/add-job.component';
import { ProductDetailsComponent } from './Product/product-details/product-details.component';
import { EditEmployeeTypeComponent } from './Product/product-type/edit-employee-type/edit-employee-type.component';
import { SizeComponent } from './Product/size/size.component';
import { AddSizesComponent } from './Product/size/add-sizes/add-sizes.component';
import { ProductSizeComponent } from './Product/product-size/product-size.component';
import { AddProductSizeComponent } from './Product/product-size/add-product-size/add-product-size.component';
import { PriceComponent } from './Product/price/price.component';
import { AddPriceComponent } from './Product/price/add-price/add-price.component';
import { CustomerTypeComponent } from './Customer/customers/customer-type/customer-type.component';
import { AddCustomerTypeComponent } from './Customer/customers/customer-type/add-customer-type/add-customer-type.component';
import { SaleComponent } from './Product/sale/sale.component';
import { ProductQuoteComponent } from './Product/product-quote/product-quote.component';
import { AddQuoteComponent } from './Product/product-quote/add-quote/add-quote.component';
import { ViewQoutesComponent } from './Product/product-quote/view-qoutes/view-qoutes.component';
import { ManageCustomersComponent } from './Manager/manager/manage-customers/manage-customers.component';


//import { ScheduleModule, RecurrenceEditorModule, DayService, WeekService, WorkWeekService, MonthService, MonthAgendaService } from '@syncfusion/ej2-angular-schedule';
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ProductsComponent,
    CustomersComponent,
    OpenDialogComponent,
    RegisterComponent,
    EmployeesComponent,
    AddEmployeeComponent,
    AddUserComponent,
    ManagerComponent,
    BranchesComponent,
    CashRegisterComponent,
    JobsComponent,
    ReportsComponent,
    ShiftsComponent,
    VATComponent,
    EditEmployeeComponent,
    AddVATComponent,
    AddBranchComponent,
    EditBranchComponent,
    DeleteBranchComponent,
    AddCashRegisterComponent,
    EditCashRegisterComponent,
    ProductCategoryComponent,
    ProductTypeComponent,
    AddProductCategoryComponent,
    AddProductTypeComponent,
    AddProductComponent,
    ManageProductsComponent,
    EditProductComponent,

    RawMaterialsComponent,
    AddRawMaterialComponent,
    UpdateRawMaterialComponent,
    DeleteRawMaterialComponent,
    DiscountComponent,
    AddEditDiscountComponent,
    EmployeeTypeComponent,
    AddEmployeeTypeComponent,
    RecipeComponent,
    AddRecipeComponent,
    EditRecipeComponent,
    ManagerUsersComponent,
    SupplierComponent,
    AddSupplierComponent,
    EmployeeDutiesComponent,
    QuoteComponent,
    AddJobComponent,
    ProductDetailsComponent,
    EditEmployeeTypeComponent,
    SizeComponent,
    AddSizesComponent,
    ProductSizeComponent,
    AddProductSizeComponent,
    PriceComponent,
    AddPriceComponent,
    CustomerTypeComponent,
    AddCustomerTypeComponent,
    SaleComponent,
    ProductQuoteComponent,
    AddQuoteComponent,
    ViewQoutesComponent,
    ManageCustomersComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule, 
    MatAutocompleteModule,
    MatDialogModule,
    CommonModule,
    MatDatepickerModule,
    MatSelectModule,
    MatInputModule,
    MatMenuModule,
    MatDatepickerModule,
    MatIconModule,
    FormsModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    MatSnackBarModule,
    MatDialogModule,
    MatCardModule,
    MatButtonModule, 
    HttpClientModule
  //  ScheduleModule,
   // RecurrenceEditorModule
  ],
  providers: [{provide: MatDialogRef,
    useValue: {}}, 
   // DayService, WeekService, WorkWeekService, MonthService, MonthAgendaService
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
