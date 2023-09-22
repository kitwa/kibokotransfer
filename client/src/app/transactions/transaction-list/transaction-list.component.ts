import { Component, OnInit } from '@angular/core';
import { Pagination } from 'src/app/_models/pagination';
import { Transaction } from 'src/app/_models/transaction';
import { TransactionsService } from 'src/app/_services/transactions.service';

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.component.html',
  styleUrls: ['./transaction-list.component.css']
}) 
export class TransactionListComponent implements OnInit {

  transactions: Transaction[];
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 10;


  constructor(private transactionsService: TransactionsService) { }

  ngOnInit(): void {
    this.loadTransactions();
  }

  loadTransactions(){
    this.transactionsService.getTransactions(this.pageNumber, this.pageSize).subscribe(response => {
      this.transactions = response.result;
      this.pagination = response.pagination;

    })
  }
  
  pageChanged(event: any){
    this.pageNumber = event.page;
    this.loadTransactions();
  }

}

