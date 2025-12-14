import { Component, OnInit } from '@angular/core';
import { ComplaintService } from '../../../../services/complaint.service';
import { Complaint } from '../../../../models/complaint.model';
import {FormsModule} from '@angular/forms';
import {SkeletonTableComponent} from '../../../skeleton-fields/skeleton-table/skeleton-table.component';
import {SupportPersonSidebarComponent} from '../support-person-sidebar/support-person-sidebar.component';
import {DatePipe} from '@angular/common';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-support-person-view-all-complaints',
  standalone: true,
  templateUrl: './support-person-view-all-complaints.component.html',
  imports: [FormsModule, SkeletonTableComponent, SupportPersonSidebarComponent, DatePipe, RouterLink],
  styleUrls: ['./support-person-view-all-complaints.component.scss']
})
export class SupportPersonViewAllComplaintsComponent implements OnInit {

  complaints: Complaint[] = [];
  filteredComplaints: Complaint[] = [];
  pagedComplaints: Complaint[] = [];

  loading = true;

  searchTerm = '';
  statusFilter = 'All';
  priorityFilter = 'All';
  sortDirection = 'newest';

  statuses = ['All', 'Open', 'In Progress', 'Awaiting Consumer', 'Resolved'];
  priorities = ['All', 'Low', 'Medium', 'High', 'Urgent'];

  currentPage: number = 1;
  pageSize: number = 10;
  totalPages: number = 1;
  totalPagesArray: number[] = [];

  constructor(private complaintService: ComplaintService) {}

  ngOnInit(): void {
    setTimeout(() => {
      this.loadComplaints();
    }, 600);
  }

  loadComplaints(): void {
    this.complaintService.GetComplaintsByAssignedToId().subscribe({
      next: (data) => {
        this.complaints = data;
        this.applyFilters();
        this.loading = false;
      },
      error: (error) => console.error('Failed to load assigned complaints', error)
    });
  }

  applyFilters(): void {
    let list = [...this.complaints];

    const term = this.searchTerm.toLowerCase();
    if (term.trim() !== '') {
      list = list.filter(c =>
        c.title.toLowerCase().includes(term) ||
        c.description?.toLowerCase().includes(term)
      );
    }

    if (this.statusFilter !== 'All')
      list = list.filter(c => c.status === this.statusFilter);

    if (this.priorityFilter !== 'All')
      list = list.filter(c => c.priority === this.priorityFilter);

    list.sort((a, b) => {
      const dA = new Date(a.createdAt).getTime();
      const dB = new Date(b.createdAt).getTime();
      return this.sortDirection === 'newest' ? dB - dA : dA - dB;
    });

    this.filteredComplaints = list;
    this.updatePagination();
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

  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updatePagination();
    }
  }

  prevPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePagination();
    }
  }
}
