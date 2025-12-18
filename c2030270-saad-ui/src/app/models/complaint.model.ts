export interface Complaint {
  complaintId: number;
  title: string;
  description: string;
  status:string;
  priority:string;
  createdAt: string;
  updatedAt?: string;
  assignedToId?: number;
  resolutionNotes?: string;
}
