import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../_models/pagination';
import { Transaction } from '../_models/transaction';
import { SearchTransactionData } from '../_models/searchTransactionData';

@Injectable({
  providedIn: 'root'
})
export class TransactionsService {

  baseUrl = environment.apiUrl;
  transactions: Transaction[] = [];
  paginatedResult: PaginatedResult<Transaction[]> = new PaginatedResult<Transaction[]>();

  constructor(private http: HttpClient ) {

  } 

  getTransactions(page? : number, itemsPerPage?: number) {

    let params = new HttpParams();

    if(page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('PageSize', itemsPerPage.toString());
    }

    return this.http.get<Transaction[]>(this.baseUrl + 'transactions', {observe: 'response', params}).pipe(

      map(response => {
        this.paginatedResult.result = response.body;
        if(response.headers.get('Pagination') !== null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return this.paginatedResult;
      })

    );
  }

  searchTransaction(searchTransactionData: SearchTransactionData, page? : number, itemsPerPage?: number) {

    let params = new HttpParams();

    if(page !== null && itemsPerPage !== null) {

      params = params.append('pageNumber', page.toString());
      params = params.append('PageSize', itemsPerPage.toString());
    }

    return this.http.post<Transaction[]>(this.baseUrl + 'transactions/search-transactions', searchTransactionData, {observe: 'response', params}).pipe(
      map(response => {
        this.paginatedResult.result = response.body;
        if(response.headers.get('Pagination') !== null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return this.paginatedResult;
      })
    );
  }

  getTransaction(id: number) {
    const transaction = this.transactions.find(x => x.id === id);
    if(transaction !== undefined) return of(transaction);
    return this.http.get<Transaction>(this.baseUrl + 'transactions/' + id);
  }

  addTransaction(transaction: Transaction) {
    return this.http.post<Transaction>(this.baseUrl + 'transactions', transaction);
    
  }

  deletePhoto(transactionId: Number, photoId: Number) {
    return this.http.delete(this.baseUrl + 'transactions/' + transactionId + '/delete');
  }

}
