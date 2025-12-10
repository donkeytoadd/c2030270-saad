import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ComplaintService } from '../../../../services/complaint.service';
import { AuthService } from '../../../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { SupportPersonSidebarComponent } from '../support-person-sidebar/support-person-sidebar.component';
import {DatePipe} from '@angular/common';

@Component({
  selector: 'app-support-person-view-complaint',
  standalone: true,
  templateUrl: './support-person-view-complaint.component.html',
  imports: [FormsModule, SupportPersonSidebarComponent, DatePipe],
  styleUrls: ['./support-person-view-complaint.component.scss']
})
export class SupportPersonViewComplaintComponent implements OnInit {

  complaintId!: number;
  complaint: any;

  tabs: string[] = ['Details', 'Communication History', 'Attachments', 'Update Status'];
  selectedTab: string = 'Details';

  selectedFiles: File[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private complaintService: ComplaintService,
    private auth: AuthService
  ) {}

  ngOnInit(): void {
    this.complaintId = Number(this.route.snapshot.paramMap.get("complaintId"));
    this.loadComplaint();
  }

  goBack() {
    this.router.navigate(['/support-person-dashboard']);
  }

  selectTab(tab: string) {
    this.selectedTab = tab;
  }

  loadComplaint() {
    this.complaintService.GetComplaint(this.complaintId).subscribe({
      next: (data) => {
        this.complaint = data;
      },
      error: (err) => {
        console.error("Error loading complaint", err);
        alert("Failed to load complaint.");
      }
    });
  }

  selectFiles(event: any) {
    const files = event.target.files;

    for (let i = 0; i < files.length; i++) {
      this.selectedFiles.push(files[i]);
    }
  }

  uploadAttachments() {
    if (this.selectedFiles.length === 0) {
      alert("Please select at least one file.");
      return;
    }

    this.complaintService.UploadAttachment(this.complaintId, this.selectedFiles).subscribe({
      next: () => {
        alert("Files uploaded successfully.");
        this.selectedFiles = [];
        this.loadComplaint();
      },
      error: (err) => {
        console.error("Upload error", err);
        alert("Failed to upload files.");
      }
    });
  }
}
