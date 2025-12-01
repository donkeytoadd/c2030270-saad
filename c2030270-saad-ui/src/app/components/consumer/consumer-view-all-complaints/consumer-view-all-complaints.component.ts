import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Complaint } from '../../../models/complaint.model';
import { ComplaintService } from '../../../services/complaint.service';
import {ConsumerSidebarComponent} from '../consumer-sidebar/consumer-sidebar.component';

@Component({
  selector: 'app-consumer-view-all-complaints',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, ConsumerSidebarComponent],
  templateUrl: './consumer-view-all-complaints.component.html',
  styleUrls: ['./consumer-view-all-complaints.component.scss']
})
export class ConsumerViewAllComplaintsComponent implements OnInit {
  sidebarOpen: boolean = true;
  consumerId: number = 1; // replaced by JWT later

  complaints: Complaint[] = [];
  filteredComplaints: Complaint[] = [];

  searchTerm: string = "";
  statusFilter: string = "All";
  priorityFilter: string = "All";
  sortDirection: string = "newest";

  statuses = ["All", "Open", "In Progress", "Resolved"];
  priorities = ["All", "Low", "Medium", "High"];

  constructor(private complaintService: ComplaintService) {}

  ngOnInit(): void {
    this.loadComplaints();
  }

  toggleSidebar(): void {
    this.sidebarOpen = !this.sidebarOpen;
  }

  loadComplaints(): void {
    this.complaintService.GetComplaintsByConsumerId(this.consumerId).subscribe({
      next: (data) => {
        this.complaints = data;
        this.applyFilters();
      },
      error: err => console.error("Failed to load complaints", err)
    });
  }

  applyFilters(): void {
    let list = [...this.complaints];

    if (this.searchTerm.trim() !== "") {
      const term = this.searchTerm.toLowerCase();
      list = list.filter(c =>
        c.title.toLowerCase().includes(term) ||
        c.description.toLowerCase().includes(term)
      );
    }

    if (this.statusFilter !== "All") {
      list = list.filter(c => c.status === this.statusFilter);
    }

    if (this.priorityFilter !== "All") {
      list = list.filter(c => c.priority === this.priorityFilter);
    }

    list.sort((a, b) => {
      const dateA = new Date(a.createdAt).getTime();
      const dateB = new Date(b.createdAt).getTime();

      return this.sortDirection === "newest" ? dateB - dateA : dateA - dateB;
    });

    this.filteredComplaints = list;
  }
}
