export interface UpdateComplaintStatus {
  complaintId: number;
  newStatus: string;
  notes?: string;
}
