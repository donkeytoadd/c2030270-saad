export interface CreateComplaint {
  consumerId: number;
  title: string;
  description: string;
  priority:string;
  files: File[];
}
