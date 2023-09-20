import { Component, Input, OnInit } from '@angular/core';
import { Property } from '../_models/property';
import { Pagination } from '../_models/pagination';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.css']
})
export class TransactionsComponent implements OnInit {

  @Input() properties: Property[];
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 10;
  
  constructor() { }

  ngOnInit(): void {
  }

}
