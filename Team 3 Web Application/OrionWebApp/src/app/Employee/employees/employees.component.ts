import { ServicesService } from './../../service/Services/services.service';
import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Route, Router } from '@angular/router';
import { OpenDialogComponent } from 'src/app/Dialog/open-dialog/open-dialog.component';
import { Employee } from 'src/app/service/Interface/interfaces.service';
import { Observable } from 'rxjs';
import { jsPDF } from "jspdf";
import { autoTable } from 'jspdf-autotable'; 
import html2canvas from 'html2canvas';
import * as XLSX from 'xlsx'; 

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.scss']
})
export class EmployeesComponent implements OnInit {

   /*name of the excel-file which will be downloaded. */ 
fileName= 'ExcelSheet.xlsx';

exportexcel(): void 
    {
       /* table id is passed over here */   
       let element = document.getElementById('printIt'); 
       const ws: XLSX.WorkSheet =XLSX.utils.table_to_sheet(element);

       /* generate workbook and add the worksheet */
       const wb: XLSX.WorkBook = XLSX.utils.book_new();
       XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

       /* save to file */
       XLSX.writeFile(wb, this.fileName);
			
    }

  observeEmployees: Observable<Employee[]> = this.service.getEmployee();
  EmployeeData!: Employee[];

  constructor(private router: Router,
    private service: ServicesService,
    private fb: FormBuilder,
    private snack: MatSnackBar,
    private dialogRef: MatDialog,) { }

  ngOnInit(): void {
    this.observeEmployees.subscribe(res => {
      this.EmployeeData = res;
    })
  }

  AddEmployee() {


    this.router.navigateByUrl("AddEmployee")
  }

  

  
  editEmployee() {
    this.router.navigateByUrl("EditEmployee")
  }

  getPDF(){
 
    html2canvas(document.querySelector(".printformClass")!).then(function(canvas) {
    canvas.getContext('2d');
     var HTML_Width = canvas.width;
    var HTML_Height = canvas.height;
    var top_left_margin = 15;
    var PDF_Width = HTML_Width+(top_left_margin*2);
    var PDF_Height = (PDF_Width*1.5)+(top_left_margin*2);
    var canvas_image_width = HTML_Width;
    var canvas_image_height = HTML_Height;
    
    var totalPDFPages = Math.ceil(HTML_Height/PDF_Height)-1;
    console.log(canvas.height+"  "+canvas.width);
    
    
    var imgData = canvas.toDataURL("image/jpeg", 1.0);
    var pdf = new jsPDF('p', 'pt',  [PDF_Width, PDF_Height]);
        pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin,canvas_image_width,canvas_image_height);
    
    
    for (var i = 1; i <= totalPDFPages; i++) { 
    pdf.addPage([PDF_Width, PDF_Height]);
    let margin=-(PDF_Height*i)+(top_left_margin*4);
    if(i>1)
    {
    margin=margin+i*8;
    }
    console.log(top_left_margin);
    console.log(top_left_margin);
    console.log(-(PDF_Height*i)+(top_left_margin*4));
    pdf.addImage(imgData, 'JPG', top_left_margin, margin,canvas_image_width,canvas_image_height);
    
    }
    
        pdf.save("HTML-Document.pdf");
           });
    };
  deleteEmployee(EmployeeID: number) {
    this.service.deleteEmployee(EmployeeID).subscribe((res: any) => {
      console.log(res);
      if (res.Success === false) {
        this.snack.open('Employee not deleted.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });

        return;
      }

      else if (res.Success === true) {
        this.snack.open('Successful Deleted Employee', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
        });
      }
      window.location.reload();
    });

  }
}

