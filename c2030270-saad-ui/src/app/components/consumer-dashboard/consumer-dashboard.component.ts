import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../../services/api-service/api-service';
import { Consumer } from '../../models/consumer.model';
import { Complaint } from '../../models/complaint.model';

@Component({
  selector: 'app-consumer-dashboard',
  templateUrl: 'consumer-dashboard.component.html',
  imports: [],
  styleUrl: 'consumer-dashboard.component.scss'
})
export class ConsumerDashboardComponent implements OnInit {
  consumer: Consumer | null = null;
  complaints: Complaint[] = [];
  loadingConsumer = false;
  loadingComplaints = false;
  errorMessage = '';

  private readonly demoConsumerId = 1;

  constructor(private api: ApiService, private router: Router) {}

  ngOnInit(): void {
    //this.loadConsumerData();
    this.loadComplaints();
  }

  // loadConsumerData(): void {
  //   this.loadingConsumer = true;
  //   this.errorMessage = '';
  //   this.api.get<Consumer>(`Consumer/GetComplaintsByConsumerId`)
  //     .pipe(finalize(() => (this.loadingConsumer = false)))
  //     .subscribe({
  //       next: (data) => {
  //         this.consumer = data;
  //       },
  //       error: (err) => {
  //         console.error('Error loading consumer info', err);
  //         this.consumer = { id: this.demoConsumerId, name: 'John Doe' };
  //         this.errorMessage = 'Unable to load consumer profile';
  //       }
  //     });
  // }

  loadComplaints(): void {
    this.loadingComplaints = true;
    this.errorMessage = '';
    this.api.get<Complaint[]>(`Complaint/GetComplaintsByConsumerId?consumerId=${this.demoConsumerId}`)
      .subscribe({
        next: (data) => {
          this.complaints = (data || []).map(c => ({
            ...c,
            createdDate: c.createdDate || new Date().toISOString()
          }));
        },
        error: (err) => {
          console.error('Error loading complaints', err);
          this.complaints = [];
          this.errorMessage = 'Unable to load complaints';
        }
      });
  }

  logout(): void {
    localStorage.removeItem('isLoggedIn');
    // if you later store JWT, remove it too: localStorage.removeItem('jwt');
    this.router.navigate(['/login']);
  }
}
