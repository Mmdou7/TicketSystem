import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TicketService } from '../ticket.service';

@Component({
  selector: 'app-ticket-add',
  templateUrl: './ticket-add.component.html',
  styleUrls: ['./ticket-add.component.css']
})
export class TicketAddComponent implements OnInit {

  constructor(
    private fb: FormBuilder,
    private ticketService: TicketService,
    private router: Router
  ) 
  {
    this.addTicketForm = this.fb.group({
      phoneNumber: ['', Validators.required],
      governorate: ['', Validators.required],
      city: ['', Validators.required],
      district: ['', Validators.required],
      creationDateTime: ['', Validators.required]
    });
  }
   addTicketForm: FormGroup;
    governorates = ['Governorate1', 'Governorate2', 'Governorate3'];
    cities = ['City1', 'City2', 'City3'];
    districts = ['District1', 'District2', 'District3'];

  ngOnInit() {
  }

  onSubmit(): void {
    const creationDate = this.addTicketForm.get('creationDateTime').value;

    if (new Date(creationDate) > new Date()) {
      alert('Creation date cannot be in the future.');
      return;
    }
    if (this.addTicketForm.valid) {
      this.ticketService.addTicket(this.addTicketForm.value).subscribe(() => {
        this.router.navigate(['/']);
      });
    }
  }

}
