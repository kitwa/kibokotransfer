import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { Router } from '@angular/router';
import { Transaction } from 'src/app/_models/transaction';
import { TransactionsService } from 'src/app/_services/transactions.service';

@Component({
  selector: 'app-transaction-add',
  templateUrl: './transaction-add.component.html',
  styleUrls: ['./transaction-add.component.css']
})
export class TransactionAddComponent implements OnInit {

  addForm: UntypedFormGroup;
  validationErrors: string[] = [];
  transaction: Transaction;
  user: User;
  // @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
  //   if(this.addForm.dirty){
  //     $event.returnValue = true;
  //   }
  // }

  constructor(private acountService: AccountService, private transactionsService: TransactionsService, 
    private toastr: ToastrService, private fb: UntypedFormBuilder, private router: Router) {
    this.acountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

   ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.addForm = this.fb.group({
      sentAmount: ['', Validators.required],
      profit: ['', Validators.required],
      totalAmount: ['', Validators.required],
      senderName: ['', Validators.required],
      senderCityId: ['', Validators.required],
      recipientName: ['', Validators.required],
      recipientCityId: ['', Validators.required],
      code: ['', Validators.required],
      statusId: ['1', Validators.required],
      appUserId: ['', Validators.required]

    })
  }

  addTransaction(){
    this.transactionsService.addTransaction(this.addForm.value).subscribe(transaction => {
      this.toastr.success("saved successfully");
      // this.router.navigateByUrl('/transaction/details/' + transaction.id);
      this.router.navigateByUrl('/transactions');
    }, error => {
      this.validationErrors = error;
    });

  }

}
