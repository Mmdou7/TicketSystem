import { Component, ElementRef, OnInit } from '@angular/core';
import { TicketService } from '../ticket.service';
import { TicketReadVm } from '../ticket-read-vm';
import { PagedResultVm } from '../paged-result-vm';
import { TicketFilterInputVm } from '../ticket-filter-input-vm';

@Component({
  selector: 'app-ticket-list',
  templateUrl: './ticket-list.component.html',
  styleUrls: ['./ticket-list.component.css']
})
export class TicketListComponent implements OnInit {
  tickets: PagedResultVm<TicketReadVm>;
  model: TicketFilterInputVm = {
    pageSize: 5,
    pageNumber: 1,
  };

  constructor(private ticketService: TicketService) { }

  ngOnInit() {
    this.loadTickets();
  }

  loadTickets(): void {
    this.ticketService.filterTickets(this.model).subscribe((data) => {
      this.tickets = data;
    }, (error) => {
      console.error('Error fetching tickets:', error);
    });
  }


  handleTicket(ticketId: number): void {
    this.ticketService.handleTicket(ticketId).subscribe(
      () => {
        this.loadTickets();
      },
      (error) => {
        console.error('Error handling ticket:', error);
      }
    );
  }

  onPageChange(pageNumber: number): void {
    this.model.pageNumber = pageNumber;
    this.loadTickets();
  }

  getPaginationArray(): number[] {
    if (!this.tickets || !this.tickets.totalCount) {
      return [];
    }
    
    const totalPages = Math.ceil(this.tickets.totalCount / this.model.pageSize);
    return Array.from({ length: totalPages }, (_, index) => index + 1);
  }

  totalPages(): number {
    if (!this.tickets || !this.tickets.totalCount) {
      return 0;
    }
    return Math.ceil(this.tickets.totalCount / this.model.pageSize);
  }

  getTicketColorClass(ticket: TicketReadVm): string {
    const creationDate = new Date(ticket.creationDateTime);
    const now = new Date();
    const minutesAgo = Math.floor((now.getTime() - creationDate.getTime()) / (1000 * 60));
  
    if (minutesAgo <= 15) {
      return 'table-warning';
    } else if (minutesAgo <= 30) {
      return 'table-success';
    } else if (minutesAgo <= 45) {
      return 'table-info';
    } else {
      return 'table-danger';
    }
  }
}
