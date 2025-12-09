import { Component, OnInit } from '@angular/core';
import {CommonModule, Location} from '@angular/common';
import {ActivatedRoute, Router} from '@angular/router';
import { Complaint } from '../../../models/complaint.model';
import { Staff } from '../../../models/staff.model';
import { ComplaintService } from '../../../services/complaint.service';
import { StaffService } from '../../../services/staff.service';
import { HttpClientModule } from '@angular/common/http';
import {ConsumerSidebarComponent} from '../consumer-sidebar/consumer-sidebar.component';

@Component({
  selector: 'app-consumer-view-complaint',
  standalone: true,
  imports: [CommonModule, HttpClientModule, ConsumerSidebarComponent],
  templateUrl: './consumer-view-complaint.component.html',
  styleUrls: ['./consumer-view-complaint.component.scss']
})
export class ConsumerViewComplaintComponent implements OnInit {

  complaint?: Complaint;
  assignedStaff?: Staff | null = null;
  attachments: any[] = []

  tabs = ["Details", "Communication History", "Attachments", "Resolution"];
  selectedTab = "Details";

  constructor(
      private route: ActivatedRoute,
      private complaintService: ComplaintService,
      private staffService: StaffService,
      private location: Location,
      private router: Router
  ) {}

  ngOnInit(): void {
    const complaintId = Number(this.route.snapshot.paramMap.get('complaintId'));

    this.complaintService.GetComplaint(complaintId).subscribe({
      next: (data) => {
        this.complaint = data;

        if (this.complaint.assignedToId) {
          this.staffService.GetStaffByStaffId(this.complaint.assignedToId).subscribe({
            next: staff => this.assignedStaff = staff,
            error: err => console.error("Failed to load staff", err)
          });
        }
      }
    });

    this.complaintService.GetAttachments(complaintId).subscribe(files => {
      this.attachments = files;
    });
  }

  selectTab(tab: string) {
    this.selectedTab = tab;
  }

  goBack(): void {
    if (window.history.length > 1) {
      this.location.back();
    } else {
      this.router.navigate(['/view-all-complaints']);
    }
  }
}
