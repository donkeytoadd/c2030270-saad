export interface ComplaintAttachment {
  attachmentId :number;
  complaintId : number;
  fileName : string;
  originalName : string;
  filePath : string;
  uploadedAt: Date;
}
