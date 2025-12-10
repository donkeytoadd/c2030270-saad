import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { ConsumerDashboardComponent } from './components/consumer/consumer-dashboard/consumer-dashboard.component';
import { ConsumerViewComplaintComponent } from "./components/consumer/consumer-view-complaint/consumer-view-complaint.component";
import { ConsumerViewAllComplaintsComponent } from './components/consumer/consumer-view-all-complaints/consumer-view-all-complaints.component';
import {AuthGuard} from './guards/auth.guard';
import {SupportPersonDashboardComponent} from './components/staff/support-person/support-person-dashboard/support-person-dashboard.component';
import {CreateComplaintComponent} from './components/create-complaint/create-complaint.component';
import {
  SupportPersonViewComplaintComponent
} from './components/staff/support-person/support-person-view-complaint/support-person-view-complaint.component';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'consumer-dashboard', component: ConsumerDashboardComponent, canActivate: [AuthGuard] },
  {path: 'support-person-dashboard', component: SupportPersonDashboardComponent, canActivate: [AuthGuard] },
  {path: 'support-person-view-complaint/:complaintId', component: SupportPersonViewComplaintComponent, canActivate: [AuthGuard] },
  { path: 'view-complaint/:complaintId', component: ConsumerViewComplaintComponent },
  {path: 'view-all-complaints', component: ConsumerViewAllComplaintsComponent},
  {path: 'create-complaint', component: CreateComplaintComponent },
];
