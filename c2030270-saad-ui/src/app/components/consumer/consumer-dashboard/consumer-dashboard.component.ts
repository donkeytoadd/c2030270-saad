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
import {SkeletonTableComponent} from '../../skeleton-fields/skeleton-table/skeleton-table.component';
import {SkeletonHeaderComponent} from '../../skeleton-fields/skeleton-header/skeleton-header.component';
import {SkeletonCardsComponent} from '../../skeleton-fields/skeleton-cards/skeleton-cards.component';

@Component({
  selector: 'app-consumer-dashboard',
  standalone: true,
  imports: [CommonModule, HttpClientModule, RouterLink, RouterModule, ConsumerSidebarComponent, SkeletonTableComponent, SkeletonHeaderComponent, SkeletonCardsComponent],
  templateUrl: './consumer-dashboard.component.html',
  styleUrls: ['./consumer-dashboard.component.scss']
})

export class ConsumerDashboardComponent implements OnInit {
  consumerId: number;
  complaints: Complaint[] = [];
  consumer: Consumer;

  openCount = 0;
  inProgressCount = 0;
  resolvedCount = 0;

  loading = true;
  loadingCards = true;
  loadingTable = true;

  constructor(private complaintService: ComplaintService, private consumerService: ConsumerService, private authService: AuthService) {}

  ngOnInit(): void {
    this.consumerId = this.authService.getUserId();

    this.loading = true;
    this.loadingCards = true;
    this.loadingTable = true;

    setTimeout(() => {
      this.loadConsumerDetails(this.consumerId);
      this.loadComplaints();
    }, 400);
  }

  loadComplaints() {
    this.complaintService.GetComplaintsByConsumerId(this.consumerId).subscribe({
      next: (data: Complaint[]) => {
        this.complaints = data.sort((a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime());

        this.openCount = data.filter(c => c.status === 'Open').length;
        this.inProgressCount = data.filter(c => c.status === 'In Progress').length;
        this.resolvedCount = data.filter(c => c.status === 'Resolved').length;

        this.loadingCards = false;
        this.loadingTable = false;
      },
      error: (error) => {
        console.error("Error loading complaints", error);
        this.loading = false;
      }
    });
  }

  loadConsumerDetails(consumerId: number) {
    this.consumerService.getConsumerByConsumerId(consumerId).subscribe({
      next: (data: Consumer) => {
        this.consumer = data;
        this.loading = false;
      },
      error: (error) => {
        console.error("Error loading consumer details", error);
        this.loading = false;
      }
    });
  }
}
