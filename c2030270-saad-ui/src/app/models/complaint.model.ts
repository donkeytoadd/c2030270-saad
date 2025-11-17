export interface Complaint {
  id: number;
  subject: string;
  description?: string;
  status: 'Open' | 'InProgress' | 'Resolved' | 'Closed' | string;
  createdDate: string;
}
