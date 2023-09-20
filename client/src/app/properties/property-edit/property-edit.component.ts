import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, NgForm, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Property } from 'src/app/_models/property';
import { Photo } from 'src/app/_models/photo';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { PropertiesService } from 'src/app/_services/properties.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-property-edit',
  templateUrl: './property-edit.component.html',
  styleUrls: ['./property-edit.component.css']
})
export class PropertyEditComponent implements OnInit {
  // @ViewChild('editForm') editForm: NgForm;
  property: Property;
  editForm: UntypedFormGroup;
  validationErrors: string[] = [];
  user: User;

  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if(this.editForm.dirty){
      $event.returnValue = true;
    }
  }

  constructor(private acountService: AccountService, private propertyService: PropertiesService, 
    private toastr: ToastrService, private fb: UntypedFormBuilder, private route: ActivatedRoute) {
    // this.acountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

  ngOnInit(): void {
    this.loadProperty();

    }
  
    initializeForm(){
      this.editForm = this.fb.group({
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

  loadProperty(){
    
    this.propertyService.getProperty(this.route.snapshot.params['id']).subscribe(property => {
      this.property = property;
      this.initializeForm();
    })
  }

  updateProperty(){
    this.propertyService.updateProperty(this.route.snapshot.params['id'], this.editForm.value).subscribe(() => {
      this.toastr.success("Changes saved successfully");
      // this.editForm.reset(this.property);
    })

  }

}
