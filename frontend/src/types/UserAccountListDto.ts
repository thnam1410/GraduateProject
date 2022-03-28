import { Identity } from "./Common";
import { Role } from "./Role";

interface UserRole {
	roleId: string;
	role: Role;
}

export type UserAccountListDto = {
	fullName: string;
	email: string;
	active: string;
	userName: string;
	phoneNumber: string;
	userRoles: UserRole[];
};

export type UserAccountTable = {
	fullName: string;
	email: string;
	active: string;
	userName: string;
	phoneNumber: string;
	roleId: string;
	code: string;
};
