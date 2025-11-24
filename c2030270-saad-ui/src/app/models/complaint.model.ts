export interface Complaint {
  complaintId: number;
  title: string;
  status:string;
  priority:string;
  createdAt: string;
  updatedAt?: string;
  assignedTo?: string;
}
