import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FileUploadModule } from 'ng2-file-upload';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TimeagoModule } from 'ngx-timeago';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    PaginationModule.forRoot(),
    FileUploadModule,
    NgxGalleryModule,
    ModalModule.forRoot(),
    TimeagoModule.forRoot()
  ],
  exports: [
    ToastrModule,
    PaginationModule,
    FileUploadModule,
    NgxGalleryModule,
    ModalModule,
    TimeagoModule
  ]
})
export class SharedModule { }
