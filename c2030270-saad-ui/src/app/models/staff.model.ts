import {Role} from "./role.model";

export interface Staff {
    staffId: number;
    firstName: string;
    lastName: string;
    email: string;
    phone: string;
    roleName: string;
    role?: Role;
}