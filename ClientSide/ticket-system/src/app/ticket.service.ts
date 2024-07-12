import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PagedResultVm } from './paged-result-vm';
import { TicketReadVm } from './ticket-read-vm';
import { TicketFilterInputVm } from './ticket-filter-input-vm';

@Injectable({
  providedIn: 'root'
})
export class TicketService {

  private apiUrl = 'https://localhost:7023/api/Tickets';

  constructor(private http: HttpClient) { }

  filterTickets(model: TicketFilterInputVm) {
    return this.http.post<PagedResultVm<TicketReadVm>>(`${this.apiUrl}/filter`, model);
  }

  handleTicket(ticketId: number): Observable<any> {
    const url = `${this.apiUrl}/changeStatus/${ticketId}`;
    return this.http.post<any>(url, {}); // POST request with an empty body
  }
}
