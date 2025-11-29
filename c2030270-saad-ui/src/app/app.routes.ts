import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { ConsumerDashboardComponent } from './components/consumer-dashboard/consumer-dashboard.component';
import {ConsumerViewComplaintComponent} from "./components/consumer-view-complaint/consumer-view-complaint.component";

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'consumer-dashboard', component: ConsumerDashboardComponent },
  { path: 'complaint/:complaintId', component: ConsumerViewComplaintComponent }
];