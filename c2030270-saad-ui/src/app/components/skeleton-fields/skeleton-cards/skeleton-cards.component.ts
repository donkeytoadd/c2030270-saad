import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-skeleton-cards',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './skeleton-cards.component.html',
  styleUrls: ['./skeleton-cards.component.scss']
})
export class SkeletonCardsComponent {
  @Input() count: number = 3;
}
