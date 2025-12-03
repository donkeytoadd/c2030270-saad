import {Role} from "./role.model";

export interface Staff {
    staffId: number;
    fName: string;
    lName: string;
    email: string;
    phone: string;
    roleName: string;
    role?: Role;
}
