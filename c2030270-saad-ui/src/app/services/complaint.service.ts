import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable} from 'rxjs';
import {Complaint} from '../models/complaint.model'
import {environment} from '../../environments/environment';

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
}
