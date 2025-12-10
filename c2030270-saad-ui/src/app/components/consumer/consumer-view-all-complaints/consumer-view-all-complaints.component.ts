import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Complaint } from '../../../models/complaint.model';
import { ComplaintService } from '../../../services/complaint.service';
import { ConsumerSidebarComponent } from '../consumer-sidebar/consumer-sidebar.component';
import { AuthService } from '../../../services/auth.service';
import {SkeletonTableComponent} from '../../skeleton-fields/skeleton-table/skeleton-table.component';

@Component({
  selector: 'app-consumer-view-all-complaints',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, ConsumerSidebarComponent, SkeletonTableComponent],
  templateUrl: './consumer-view-all-complaints.component.html',
  styleUrls: ['./consumer-view-all-complaints.component.scss']
})
export class ConsumerViewAllComplaintsComponent implements OnInit {

  consumerId: number;
  complaints: Complaint[] = [];
  filteredComplaints: Complaint[] = [];

  pagedComplaints: Complaint[] = [];

  searchTerm: string = "";
  statusFilter: string = "All";
  priorityFilter: string = "All";
  sortDirection: string = "newest";

  statuses = ["All", "Open", "In Progress", "Resolved"];
  priorities = ["All", "Low", "Medium", "High"];

  currentPage: number = 1;
  pageSize: number = 10;
  totalPages: number = 1;
  totalPagesArray: number[] = [];
  loading: boolean = true;

  constructor(private complaintService: ComplaintService, private authService: AuthService) {}

  ngOnInit(): void {
    this.consumerId = this.authService.getUserId();
    setTimeout(() => {
      this.loadComplaints();
    }, 600);
  }

  loadComplaints(): void {
    this.loading = true;
    this.complaintService.GetComplaintsByConsumerId(this.consumerId).subscribe({
      next: (data) => {
        this.complaints = data;
        this.applyFilters();
        setTimeout(() => this.loading = false, 400);
      },
      error: (error) => {
        console.error("Failed to load complaints", error)
        this.loading = false;
      }
    });
  }

  applyFilters(): void {
    this.loading = true;

    setTimeout(() => {
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
      this.currentPage = 1;
      this.updatePagination();
      this.loading = false;
    }, 300);
  }

  updatePagination(): void {
    this.totalPages = Math.ceil(this.filteredComplaints.length / this.pageSize);
    this.totalPagesArray = Array.from({ length: this.totalPages }, (_, i) => i + 1);

    const start = (this.currentPage - 1) * this.pageSize;
    const end = start + this.pageSize;

    this.pagedComplaints = this.filteredComplaints.slice(start, end);
  }

  goToPage(page: number): void {
    this.currentPage = page;
    this.updatePagination();
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updatePagination();
    }
  }

  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePagination();
    }
  }
}
