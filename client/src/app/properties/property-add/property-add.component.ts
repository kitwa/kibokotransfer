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

@Component({
  selector: 'app-property-add',
  templateUrl: './property-add.component.html',
  styleUrls: ['./property-add.component.css']
})
export class PropertyAddComponent implements OnInit {

  addForm: UntypedFormGroup;
  validationErrors: string[] = [];
  property: Property;
  user: User;
  // @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
  //   if(this.addForm.dirty){
  //     $event.returnValue = true;
  //   }
  // }

  constructor(private acountService: AccountService, private propertyService: PropertiesService, 
    private toastr: ToastrService, private fb: UntypedFormBuilder, private router: Router) {
    this.acountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

   ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.addForm = this.fb.group({
      price: ['', Validators.required],
      bathRooms: ['', Validators.required],
      bedRooms: ['', Validators.required],
      garage: ['', Validators.required],
      propertyType: ['', Validators.required],
      description: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      youtubeLink: ['', Validators.nullValidator],
      appUserId: ['', Validators.required]

    })
  }

  addProperty(){
    this.propertyService.addProperty(this.addForm.value).subscribe(property => {
      this.toastr.success("saved successfully");
      this.router.navigateByUrl('/property/edit/' + property.id);
    }, error => {
      this.validationErrors = error;
    });

  }

}
