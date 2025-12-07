export interface LoginResponse {
  token: string;
  userId: number;
  role: string;
  refreshToken: string;
  tenantId: number;
}
