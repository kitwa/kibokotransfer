import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { Property } from 'src/app/_models/property';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { PropertiesService } from 'src/app/_services/properties.service';

@Component({
  selector: 'app-property-detail',
  templateUrl: './property-detail.component.html',
  styleUrls: ['./property-detail.component.css']
})
export class PropertyDetailComponent implements OnInit {
  property: Property;
  member: Member;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[] = [];
  user: User;
  youtubeVideo: SafeResourceUrl;

  constructor(private propertyService: PropertiesService, private route: ActivatedRoute, 
      private accountService: AccountService, private sanitizer: DomSanitizer) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

  ngOnInit(): void {
    this.loadProperty();
    this.galleryOptions = [
      {
        width: '700px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 10,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false,
        imageArrows: true,
        imageAutoPlay: true,
        imageArrowsAutoHide : true,
        imageSwipe: true,
        imageAutoPlayInterval: 4000
      }

    ]
  }

  getImages(): NgxGalleryImage[]{
    const imageUrls: any = [];
    for(const photo of this.property.photos){
      imageUrls.push({
        small: photo.url,
        medium: photo.url,
        big: photo.url
      });
    }

    return imageUrls;
  }

  loadProperty(){
    
    this.propertyService.getProperty(this.route.snapshot.params['id']).subscribe(property => {
      this.property = property;
      this.youtubeVideo = this.sanitizer.bypassSecurityTrustResourceUrl(property.youtubeLink);
      this.galleryImages = this.getImages();
      this.member = this.property.appUser;
    })
  }

}
