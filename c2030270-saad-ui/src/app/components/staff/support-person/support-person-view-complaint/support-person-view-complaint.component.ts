import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ComplaintService } from '../../../../services/complaint.service';
import {FormsModule, NgModel} from '@angular/forms';
import { SupportPersonSidebarComponent } from '../support-person-sidebar/support-person-sidebar.component';
import { DatePipe } from '@angular/common';
import { Complaint } from '../../../../models/complaint.model';
import { SkeletonViewComplaintComponent } from '../../../skeleton-fields/skeleton-view-complaint/skeleton-view-complaint.component';
import {AuthService} from '../../../../services/auth.service';

@Component({
  selector: 'app-support-person-view-complaint',
  standalone: true,
  templateUrl: './support-person-view-complaint.component.html',
  imports: [
    FormsModule,
    SupportPersonSidebarComponent,
    DatePipe,
    SkeletonViewComplaintComponent
  ],
  styleUrls: ['./support-person-view-complaint.component.scss']
})
export class SupportPersonViewComplaintComponent implements OnInit {

  complaintId!: number;
  complaint!: Complaint;
  attachments: any[] = [];

  tabs: string[] = ['Details', 'Attachments', 'Update Status'];
  selectedTab: string = 'Details';

  selectedFiles: File[] = [];
  loading = true;

  updateModel = {
    status: '',
    notes: ''
  };

  constructor(private route: ActivatedRoute, private router: Router, private complaintService: ComplaintService, private auth: AuthService) {}

  ngOnInit(): void {
    this.complaintId = Number(this.route.snapshot.paramMap.get('complaintId'));

    setTimeout(() => this.loadComplaint(), 600);
  }

  goBack() {
    this.router.navigate(['/support-person-dashboard']);
  }

  selectTab(tab: string) {
    this.selectedTab = tab;
  }

  loadComplaint() {
    this.loading = true;

    this.complaintService.GetComplaint(this.complaintId).subscribe({
      next: (data) => {
        this.complaint = data;

        this.updateModel.status = data.status;
        this.updateModel.notes = data.resolutionNotes ?? '';

        this.loading = false;
      },
      error: () => {
        alert('Failed to load complaint');
        this.loading = false;
      }
    });

    this.complaintService.GetAttachments(this.complaintId).subscribe(files => {
      this.attachments = files;
    });
  }

  selectFiles(event: any) {
    this.selectedFiles = Array.from(event.target.files);
  }

  uploadAttachments() {
    if (this.selectedFiles.length === 0) {
      alert('Please select files first');
      return;
    }

    const tenantId = this.auth.getTenantId();

    this.complaintService
      .UploadAttachment(this.complaintId, tenantId, this.selectedFiles)
      .subscribe({
        next: () => {
          alert('Files uploaded successfully');
          this.selectedFiles = [];
          this.loadComplaint();
        },
        error: err => {
          console.error('Upload failed', err);
          alert('Failed to upload files');
        }
      });
  }



  saveStatusUpdate() {
    if (!this.updateModel.status || !this.updateModel.notes.trim()) {
      alert('Status and resolution notes are required');
      return;
    }

    this.complaintService
      .UpdateComplaintStatus(
        this.complaint.complaintId,
        this.updateModel.status,
        this.updateModel.notes
      )
      .subscribe({
        next: () => {
          alert('Complaint updated successfully');
          this.loadComplaint();
          this.selectedTab = 'Details';
        },
        error: (err) => {
          console.error('Update failed', err);
          alert('Failed to update complaint');
        }
      });
  }

  protected readonly NgModel = NgModel;
}
