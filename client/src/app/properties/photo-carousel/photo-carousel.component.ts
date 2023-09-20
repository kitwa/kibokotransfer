import { Component, HostListener, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, NgForm, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Property } from 'src/app/_models/property';
import { Photo } from 'src/app/_models/photo';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { PropertiesService } from 'src/app/_services/properties.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Pagination } from 'src/app/_models/pagination';

@Component({
  selector: 'app-photo-carousel',
  templateUrl: './photo-carousel.component.html',
  styleUrls: ['./photo-carousel.component.css']
})
export class PhotoCarouselComponent implements OnInit {
  // @ViewChild('addForm') addForm: NgForm;
  searchForm: UntypedFormGroup;
  validationErrors: string[] = [];
  property: Property;
  properties: Property[] = [];
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 5;
  searchClicked = false;
  loading = false;
  


  constructor(private acountService: AccountService, private propertyService: PropertiesService, 
    private toastr: ToastrService, private fb: UntypedFormBuilder, private router: Router) {

   }

   ngOnInit(): void {
    this.initializeForm();
    this.loadProperties();
  }

  loadProperties(){
    this.propertyService.getProperties(this.pageNumber, this.pageSize).subscribe(response => {
      this.properties = response.result;
      this.pagination = response.pagination;

    })
  }
  
  pageChanged(event: any){
    this.pageNumber = event.page;
    this.loadProperties();
  }

  initializeForm(){
    this.searchForm = this.fb.group({
      minPrice: [10000, Validators.nullValidator],
      maxPrice: [100000000, Validators.nullValidator],
      bathRooms: [0, Validators.nullValidator],
      bedRooms: [0, Validators.nullValidator],
      propertyType: ['house', Validators.required],
      city: ['lubumbashi', Validators.required],

    })
  }

  searchProperty(){
    this.loading = true;
    this.propertyService.searchProperty(this.searchForm.value, this.pageNumber, this.pageSize).subscribe(response => {

      this.properties = response.result;
      this.searchClicked = true;
      if(response.result.length > 1){
        this.pagination = response.pagination;
      }
      this.loading= false;
    }, error => {
      this.validationErrors = error;
    });




  }

}
