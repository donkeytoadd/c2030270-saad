import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {Complaint} from '../../../models/complaint.model'
import {ComplaintService} from '../../../services/complaint.service';
import {HttpClientModule} from '@angular/common/http';
import {RouterLink, RouterModule} from "@angular/router";
import {ConsumerSidebarComponent} from '../consumer-sidebar/consumer-sidebar.component';
import {ConsumerService} from '../../../services/consumer.service';
import {Consumer} from '../../../models/consumer.model';
import {AuthService} from '../../../services/auth.service';

@Component({
  selector: 'app-consumer-dashboard',
  standalone: true,
  imports: [CommonModule, HttpClientModule, RouterLink, RouterModule, ConsumerSidebarComponent],
  templateUrl: './consumer-dashboard.component.html',
  styleUrls: ['./consumer-dashboard.component.scss']
})

export class ConsumerDashboardComponent implements OnInit {
  consumerId: number;
  complaints: Complaint[] = [];
  consumer: Consumer;
  openCount: number = 0;
  inProgressCount: number = 0;
  resolvedCount: number = 0;

  constructor(private complaintService: ComplaintService, private consumerService: ConsumerService, private authService: AuthService) {}

  ngOnInit(): void {
    this.consumerId = this.authService.getUserId();
    this.loadConsumerDetails(this.consumerId);
    this.loadComplaints();
  }

  loadComplaints() {
    this.complaintService.GetComplaintsByConsumerId(this.consumerId).subscribe({
      next: (data: Complaint[]) => {

        this.complaints = data.sort(
          (a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
        );

        this.openCount = data.filter(c => c.status === 'Open').length;
        this.inProgressCount = data.filter(c => c.status === 'In Progress').length;
        this.resolvedCount = data.filter(c => c.status === 'Resolved').length;
      },
      error: err => {
        console.error('Failed to load complaints for dashboard', err);
      }
    });
  }

  loadConsumerDetails(consumerId: number) {
    this.consumerService.getConsumerByConsumerId(consumerId).subscribe({
      next: (data: Consumer) => {
        this.consumer = data;
      },
      error: err => {
        console.error('Failed to load consumer details', err)
      }
    })
  }
}
