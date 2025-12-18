import { Component, OnInit } from '@angular/core';
import {CommonModule, Location} from '@angular/common';
import {ActivatedRoute, Router} from '@angular/router';
import { Complaint } from '../../../models/complaint.model';
import { Staff } from '../../../models/staff.model';
import { ComplaintService } from '../../../services/complaint.service';
import { StaffService } from '../../../services/staff.service';
import { HttpClientModule } from '@angular/common/http';
import {ConsumerSidebarComponent} from '../consumer-sidebar/consumer-sidebar.component';
import {
  SkeletonViewComplaintComponent
} from '../../skeleton-fields/skeleton-view-complaint/skeleton-view-complaint.component';

@Component({
  selector: 'app-consumer-view-complaint',
  standalone: true,
  imports: [CommonModule, HttpClientModule, ConsumerSidebarComponent, SkeletonViewComplaintComponent],
  templateUrl: './consumer-view-complaint.component.html',
  styleUrls: ['./consumer-view-complaint.component.scss']
})
export class ConsumerViewComplaintComponent implements OnInit {

  complaint: Complaint;
  complaintId: number;
  assignedStaff?: Staff | null = null;
  attachments: any[] = []

  tabs = ["Details", "Communication History", "Attachments", "Resolution"];
  selectedTab = "Details";

  loading: boolean = true;

  constructor(private route: ActivatedRoute, private complaintService: ComplaintService, private staffService: StaffService, private location: Location, private router: Router) {}

  ngOnInit(): void {
    this.complaintId = Number(this.route.snapshot.paramMap.get('complaintId'));

    setTimeout(() => {
      this.loadComplaint();
    }, 600);
  }

  loadComplaint() {
    this.loading = true;
    this.complaintService.GetComplaint(this.complaintId).subscribe({
      next: (data) => {
        this.complaint = data;
        this.loading = false;

        if (this.complaint.assignedToId) {
          this.staffService.GetStaffByStaffId(this.complaint.assignedToId).subscribe({
            next: (data) => {
              this.assignedStaff = data;
              this.loading = false;
            },
            error: (error) => {
              console.error("Error loading staff details", error);
              this.loading = false;
            }
          })
        }
      },
      error: (error) => {
        console.error("Failed to load complaint", error)
        this.loading = false;
      }
    });

    this.complaintService.GetAttachments(this.complaintId).subscribe(files => {
      this.attachments = files;
    });
  }

  selectTab(tab: string) {
    this.selectedTab = tab;
  }

  goBack(): void {
    if (window.history.length > 1) {
      this.location.back();
    }
    else {
      this.router.navigate(['/view-all-complaints']);
    }
  }

  updateComplaintStatus(newStatus: string) {
    this.complaintService.UpdateComplaintStatus(this.complaint.complaintId, newStatus)
      .subscribe({
        next: () => {
          alert('Complaint cancelled successfully');
          this.loadComplaint();
        },
        error: err => {
          console.error('Cancel failed', err);
          alert('Failed to cancel complaint');
        }
      });
  }
}
