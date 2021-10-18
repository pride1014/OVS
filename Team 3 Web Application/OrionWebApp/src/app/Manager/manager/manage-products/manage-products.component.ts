import { Product } from 'src/app/service/Interface/interfaces.service';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { ServicesService } from 'src/app/service/Services/services.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { jsPDF } from "jspdf";
import { autoTable } from 'jspdf-autotable'; 
import html2canvas from 'html2canvas';
import * as XLSX from 'xlsx'; 



@Component({
  selector: 'app-manage-products',
  templateUrl: './manage-products.component.html',
  styleUrls: ['./manage-products.component.scss']
})
export class ManageProductsComponent implements OnInit {
  name = 'Angular';

 

  @ViewChild('content', { static: false })
  content!: ElementRef;
  observeData: Observable<Product[]> = this.service.getProducts();
  ProductData!: Product[];

  constructor(private service: ServicesService, private fb: FormBuilder, 
    private snack: MatSnackBar, private dialogRef: MatDialog,
    private router: Router) { }

  ngOnInit(): void {
    this.observeData.subscribe(res => {
      this.ProductData = res;
    })
  }
  convertToPdf() {
    //WORKING EXAMPLE IS HERE
    let html1 = document.querySelector('.printformClass');
    html2canvas(document.querySelector(".printformClass")!).then(canvas => {

      var pdf = new jsPDF('p', 'pt', [canvas.width, canvas.height]);

      var imgData = canvas.toDataURL("image/jpeg", 1.0);
      pdf.addImage(imgData, 0, 0, canvas.width, canvas.height);
      pdf.save('converteddoc.pdf');
});}



  AddProduct(){
    this.router.navigateByUrl("AddProduct")
  }

  public downloadPDF() {
    const doc = new jsPDF();

    const specialElementHandlers = {
      '#editor': function (element: any, renderer : any) {
        return true;
      }
    };

    const content = this.content.nativeElement;

    // doc.fromHTML(content.innerHTML, 15, 15, {
    //   width: 190,
    //   'elementHandlers': specialElementHandlers
    // });

    // for (let i = 0; i < length; i++) {
    //   doc.text(`${names[i]} (Average: ${averages[i]})`, (pageWidth / 3) - 25, finalY + 23);

    //   doc.autoTable({
    //     startY: finalY + 25,
    //     html: `#testing${i}`,
    //     useCss: true,
    //     head: [
    //       [
    //         'Order Date',
    //         'Date Shipped',
    //         'Order Quantity'
    //       ]
    //     ]
    //   });

    doc.save('test.pdf');
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

  editProduct(){
    this.router.navigateByUrl("EditProduct")
  }

  deleteProduct(productId:number){
    this.service.DeleteProducts(productId).subscribe((res: any) => {
      console.log(res);
      if (res.Success === false) {
        this.snack.open('Product not deleted.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });

        return;
      }

      else if (res.Success === true) {
        this.snack.open('Successful Deleted Product', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
        });
      }
      window.location.reload();
    });
  }

}
