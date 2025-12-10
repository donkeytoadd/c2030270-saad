import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ComplaintService } from '../../services/complaint.service';
import { AuthService } from '../../services/auth.service';
import { ConsumerService } from '../../services/consumer.service';
import { ConsumerSidebarComponent } from '../consumer/consumer-sidebar/consumer-sidebar.component';
import { Router } from '@angular/router';
import {CreateComplaint} from '../../models/create-complaint.model';
import {
  SupportPersonSidebarComponent
} from '../staff/support-person/support-person-sidebar/support-person-sidebar.component';

@Component({
  selector: 'app-create-complaint',
  standalone: true,
  imports: [CommonModule, FormsModule, ConsumerSidebarComponent, SupportPersonSidebarComponent],
  templateUrl: './create-complaint.component.html',
  styleUrls: ['./create-complaint.component.scss']
})
export class CreateComplaintComponent implements OnInit {
  step = 1;

  createComplaintModel: CreateComplaint = {
    consumerId: 0,
    title: "",
    description: "",
    priority: "",
    files: []
  };

  titleError = "";
  priorityError = "";
  consumerError = "";
  descriptionError = "";
  fileError = "";

  isStaff = false;

  searchTerm = "";
  consumerResults: any[] = [];
  showConsumerList = false;
  selectedConsumerName = "";

  selectedFiles: File[] = [];

  constructor(private complaintService: ComplaintService, private auth: AuthService, private consumerService: ConsumerService, private router: Router,) {}

  ngOnInit(): void {
    const role = this.auth.getRole();
    this.isStaff = role !== "Consumer";

    if (!this.isStaff) {
      this.createComplaintModel.consumerId = this.auth.getUserId();
    }
  }

  validateStep1(): boolean {
    this.titleError = this.createComplaintModel.title.trim() ? "" : "Title is required.";
    this.priorityError = this.createComplaintModel.priority ? "" : "Priority must be selected.";

    if (this.isStaff) {
      this.consumerError = this.createComplaintModel.consumerId > 0 ? "" : "A consumer must be selected.";
    }

    return !this.titleError && !this.priorityError && !this.consumerError;
  }

  validateStep2(): boolean {
    this.descriptionError = this.createComplaintModel.description.trim() ? "" : "Description is required.";

    return !this.descriptionError;
  }

  validateStep3(): boolean {
    return true;
  }

  nextStep() {
    if (this.step === 1 && !this.validateStep1()) return;
    if (this.step === 2 && !this.validateStep2()) return;

    this.step++;
  }

  prevStep() {
    if (this.step > 1) this.step--;
  }

  searchConsumers(): void {
    if (!this.searchTerm.trim()) {
      this.consumerResults = [];
      return;
    }

    this.consumerService.searchConsumers(this.searchTerm).subscribe(results => {
      this.consumerResults = results;
      this.showConsumerList = true;
    });
  }

  selectConsumer(c: any): void {
    this.createComplaintModel.consumerId = c.consumerId;
    this.selectedConsumerName = `${c.fName} ${c.lName}`;
    this.showConsumerList = false;
  }

  onFilesSelected(event: any) {
    const files = event.target.files;

    for (let i = 0; i < files.length; i++) {
      this.selectedFiles.push(files[i]);
    }
  }

  removeFile(index: number) {
    this.selectedFiles.splice(index, 1);
  }

  submitComplaint() {
    if (!this.validateStep3()) return;

    const formData = new FormData();

    formData.append("ConsumerId", this.createComplaintModel.consumerId.toString());
    formData.append("Title", this.createComplaintModel.title);
    formData.append("Description", this.createComplaintModel.description);
    formData.append("Priority", this.createComplaintModel.priority);

    for (let file of this.selectedFiles) {
      formData.append("Files", file);
    }

    this.complaintService.CreateComplaint(formData).subscribe({
      next: () => {
        alert("Complaint submitted successfully!");

        const role = this.auth.getRole();

        if (role === "Consumer") {
          this.router.navigate(['/consumer-dashboard']);
        } else {
          this.router.navigate(['/support-person-dashboard']);
        }
      },
      error: () => {
        alert("Failed to submit complaint.");
      }
    });
  }
}
