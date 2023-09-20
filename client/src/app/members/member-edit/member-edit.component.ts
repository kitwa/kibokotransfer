import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { Photo } from 'src/app/_models/photo';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  member: Member;
  user: User;
  // galleryImages: Photo[];
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if(this.editForm.dirty){
      $event.returnValue = true;
    }
  }

  constructor(private acountService: AccountService, private memberService: MembersService, 
    private toastr: ToastrService) {
    this.acountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

  ngOnInit(): void {
    this.loadMember();
  }

  // getImages(): Photo[]{
  //   const imageUrls: any = [];
  //   for(const photo of this.member.photos){
  //     imageUrls.push(photo.url);
  //   }

  //   return imageUrls;
  // }

  loadMember(){
    this.memberService.getMember(this.user.email).subscribe(member => {
      this.member = member;
      // this.galleryImages = this.getImages();
    })
  }

  updateMember(){

    this.memberService.updateMember(this.member).subscribe(() => {
      this.toastr.success("Changes saved successfully");
      this.editForm.reset(this.member);
    })

  }

}
