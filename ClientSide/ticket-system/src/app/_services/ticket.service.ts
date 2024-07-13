import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PagedResultVm } from '../_models/paged-result-vm';
import { TicketReadVm } from '../_models/ticket-read-vm';
import { environment } from 'src/environments/environment';
import { TicketFilterInputVm } from '../_models/ticket-filter-input-vm';

@Injectable({
  providedIn: 'root'
})
export class TicketService {

  private apiUrl = environment.baseUrl;

  constructor(private http: HttpClient) { }

  filterTickets(model: TicketFilterInputVm) {
    return this.http.post<PagedResultVm<TicketReadVm>>(`${this.apiUrl}/filter`, model);
  }

  handleTicket(ticketId: number): Observable<any> {
    const url = `${this.apiUrl}/changeStatus/${ticketId}`;
    return this.http.post<any>(url, {}); // POST request with an empty body
  }

  addTicket(ticket: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/add`, ticket);
  }
  
}
