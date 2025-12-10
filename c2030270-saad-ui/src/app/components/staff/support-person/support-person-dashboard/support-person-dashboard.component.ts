import { Component, OnInit } from '@angular/core';
import { ComplaintService } from '../../../../services/complaint.service';
import { AuthService } from '../../../../services/auth.service';
import {RouterLink} from '@angular/router';
import {
  SupportPersonSidebarComponent
} from '../support-person-sidebar/support-person-sidebar.component';
import {Complaint} from '../../../../models/complaint.model';
import {DatePipe} from '@angular/common';
import {Consumer} from '../../../../models/consumer.model';
import {StaffService} from '../../../../services/staff.service';
import {Staff} from '../../../../models/staff.model';

@Component({
  selector: 'app-support-person-dashboard',
  templateUrl: './support-person-dashboard.component.html',
  imports: [DatePipe,
    RouterLink,
    SupportPersonSidebarComponent
  ],
  styleUrls: ['./support-person-dashboard.component.scss']
})
export class SupportPersonDashboardComponent implements OnInit {

  staffId = 0;
  staff: Staff;
  complaints: Complaint[] = [];

  // Summary card counts
  assignedCount = 0;
  inProgressCount = 0;
  resolvedCount = 0;

  constructor(
    private complaintService: ComplaintService,
    private auth: AuthService,
    private staffService: StaffService
  ) {}

  ngOnInit(): void {
    this.staffId = this.auth.getUserId();

    this.loadStaffDetails();
    this.loadAssignedComplaints();
  }

  loadAssignedComplaints(): void {
    this.complaintService.GetComplaintsByAssignedToId().subscribe({
      next: (data) => {
        this.complaints = data;
        this.updateSummaryCounts();
      },
      error: (err) => {
        console.error("Error loading assigned complaints", err);
      }
    });
  }

  updateSummaryCounts(): void {
    this.assignedCount = this.complaints.length;
    this.inProgressCount = this.complaints.filter(c => c.status === "In Progress").length;
    this.resolvedCount = this.complaints.filter(c => c.status === "Resolved").length;
  }

  loadStaffDetails(): void {
    this.staffService.GetStaffByStaffId(this.staffId).subscribe({
      next: (data: Staff) => {
        this.staff = data;
      },
      error: err => {
        console.error('Failed to load staff details', err)
      }
    })
  }
}
