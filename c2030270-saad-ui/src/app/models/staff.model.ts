import {Role} from "./role.model";

export interface Staff {
    staffId: number;
    fName: string;
    lName: string;
    email: string;
    contactNumber: string;
    roleName: string;
    role?: Role;
}
