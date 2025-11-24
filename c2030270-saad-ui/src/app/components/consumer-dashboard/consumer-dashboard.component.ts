import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {Complaint} from '../../models/complaint.model'
import {ComplaintService} from '../../services/complaint.service';
import {HttpClientModule} from '@angular/common/http';
@Component({
  selector: 'app-consumer-dashboard',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  templateUrl: './consumer-dashboard.component.html',
  styleUrls: ['./consumer-dashboard.component.scss']
})
export class ConsumerDashboardComponent implements OnInit {
  consumerId = 1; //example value, will be replaced by JWT userId claim later
  complaints: Complaint[] = [];
  sidebarOpen: boolean = true;
  username: string = 'User';
  openCount: number = 0;
  inProgressCount: number = 0;
  resolvedCount: number = 0;

  constructor(private complaintService: ComplaintService) {}

  ngOnInit(): void {
    this.loadComplaints();
  }

  loadComplaints() {
    this.complaintService.
    GetComplaintsByConsumerId(this.consumerId).subscribe({
      next: (data: Complaint[]) => {
        this.complaints = data;

        this.openCount = data.filter(c => c.status === 'Open').length;
        this.inProgressCount = data.filter(c => c.status === 'In Progress').length;
        this.resolvedCount = data.filter(c => c.status === 'Resolved').length;
      },
      error: err => {
        console.error('Failed to load complaints for dashboard', err);
      }
    });
  }

  toggleSidebar(): void {
    this.sidebarOpen = !this.sidebarOpen;
  }
}
