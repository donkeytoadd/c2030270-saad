import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Staff } from '../models/staff.model';
import {environment} from "../../environments/environment";

@Injectable({ providedIn: 'root' })
export class StaffService {
    private apiUrl = environment.apiUrl.concat("Staff");


    constructor(private http: HttpClient) {}

    GetStaffByStaffId(staffId: number): Observable<Staff> {
        return this.http.get<Staff>(`${this.apiUrl}/GetStaffByStaffId?staffId=${staffId}`);
    }
}