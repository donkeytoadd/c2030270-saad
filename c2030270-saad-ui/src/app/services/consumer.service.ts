import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {environment} from '../../environments/environment';
import {Consumer} from '../models/consumer.model';

@Injectable({
  providedIn: 'root'
})
export class ConsumerService {

  private apiUrl = environment.apiUrl.concat("Consumer");

  constructor(private http: HttpClient) {}

  getConsumerByConsumerId(consumerId: number): Observable<Consumer> {
    return this.http.get<Consumer>(`${this.apiUrl}/GetConsumerByConsumerId?consumerId=${consumerId}`);
  }
}
