import { Component, OnInit } from '@angular/core';
import { ComplaintService } from '../../../../services/complaint.service';
import { AuthService } from '../../../../services/auth.service';
import {RouterLink} from '@angular/router';
import {SupportPersonSidebarComponent} from '../support-person-sidebar/support-person-sidebar.component';
import {Complaint} from '../../../../models/complaint.model';
import {DatePipe} from '@angular/common';
import {StaffService} from '../../../../services/staff.service';
import {Staff} from '../../../../models/staff.model';
import {SkeletonHeaderComponent} from '../../../skeleton-fields/skeleton-header/skeleton-header.component';
import {SkeletonCardsComponent} from '../../../skeleton-fields/skeleton-cards/skeleton-cards.component';
import {SkeletonTableComponent} from '../../../skeleton-fields/skeleton-table/skeleton-table.component';

@Component({
  selector: 'app-support-person-dashboard',
  templateUrl: './support-person-dashboard.component.html',
  imports: [DatePipe, RouterLink, SupportPersonSidebarComponent, SkeletonTableComponent, SkeletonHeaderComponent, SkeletonCardsComponent, SkeletonTableComponent],
  styleUrls: ['./support-person-dashboard.component.scss']
})
export class SupportPersonDashboardComponent implements OnInit {

  staffId = 0;
  staff: Staff;
  complaints: Complaint[] = [];

  assignedCount = 0;
  inProgressCount = 0;
  resolvedCount = 0;

  loading = true;
  loadingCards = true;
  loadingTable = true;

  constructor(private complaintService: ComplaintService, private auth: AuthService, private staffService: StaffService) {}

  ngOnInit(): void {
    this.staffId = this.auth.getUserId();

    this.loading = true;
    this.loadingCards = true;
    this.loadingTable = true;

    setTimeout(() => {
      this.loadStaffDetails();
      this.loadAssignedComplaints();
    }, 400);
  }

  loadAssignedComplaints(): void {
    this.complaintService.GetComplaintsByAssignedToId().subscribe({
      next: (data) => {
        this.complaints = data;

        this.assignedCount = this.complaints.length;
        this.inProgressCount = this.complaints.filter(c => c.status === "In Progress").length;
        this.resolvedCount = this.complaints.filter(c => c.status === "Resolved").length;

        this.loadingCards = false;
        this.loadingTable = false;
      },
      error: (error) => {
        console.error("Error loading assigned complaints", error);
        this.loadingCards = false;
        this.loadingTable = false;
      }
    });
  }

  loadStaffDetails(): void {
    this.staffService.GetStaffByStaffId(this.staffId).subscribe({
      next: (data: Staff) => {
        this.staff = data;
        this.loading = false;
      },
      error: error => {
        console.error('Failed to load staff details', error)
        this.loading = false;
      }
    })
  }
}
