import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { ConsumerDashboardComponent } from './components/consumer/consumer-dashboard/consumer-dashboard.component';
import { ConsumerViewComplaintComponent } from "./components/consumer/consumer-view-complaint/consumer-view-complaint.component";
import { ConsumerViewAllComplaintsComponent } from './components/consumer/consumer-view-all-complaints/consumer-view-all-complaints.component';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'consumer-dashboard', component: ConsumerDashboardComponent },
  { path: 'view-complaint/:complaintId', component: ConsumerViewComplaintComponent },
  {path: 'view-all-complaints', component: ConsumerViewAllComplaintsComponent}
];
