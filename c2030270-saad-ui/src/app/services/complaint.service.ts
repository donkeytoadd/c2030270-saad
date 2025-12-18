import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable} from 'rxjs';
import {Complaint} from '../models/complaint.model'
import {environment} from '../../environments/environment';
import {ComplaintAttachment} from '../models/complaint-attachment.model';

@Injectable({
  providedIn: 'root'
})
export class ComplaintService {

  private apiUrl = environment.apiUrl.concat("Complaint");

  constructor(private http: HttpClient) {}

  GetComplaint(complaintId: number): Observable<Complaint>{
    return this.http.get<Complaint>(`${this.apiUrl}/GetComplaint?complaintId=${complaintId}`)
  }

  GetComplaintsByConsumerId(consumerId: number): Observable<Complaint[]> {
    return this.http.get<Complaint[]>(`${this.apiUrl}/GetComplaintsByConsumerId?consumerId=${consumerId}`);
  }

  CreateComplaint(formData: FormData): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/CreateComplaint`, formData);
  }

  GetAttachments(complaintId: number): Observable<ComplaintAttachment[]> {
    return this.http.get<ComplaintAttachment[]>(`${this.apiUrl}/GetAttachments?complaintId=${complaintId}`);
  }

  UploadAttachment(complaintId: number, tenantId: number, file: File[]) {
    const formData = new FormData();

    formData.append('ComplaintId', complaintId.toString());
    formData.append('TenantId', tenantId.toString());
    formData.append('File', file);

    return this.http.post(`${this.apiUrl}/UploadAttachment`, formData);
  }


  GetComplaintsByAssignedToId(): Observable<Complaint[]> {
    return this.http.get<Complaint[]>(`${this.apiUrl}/GetComplaintsByAssignedToId`);
  }

  UpdateComplaintStatus(complaintId: number, newStatus: string, notes?: string) {
    return this.http.post(`${this.apiUrl}/UpdateComplaintStatus`, { complaintId, newStatus, notes});
  }
}
