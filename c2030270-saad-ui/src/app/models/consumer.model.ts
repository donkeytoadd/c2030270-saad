export interface Consumer {
  consumerId: number;
  tenantId: number;
  roleId: number;
  fName: string;
  lName: string;
  email: string;
  contactNumber: string;
  isActive: boolean;
  createdAt: Date;
}
