import { Identity } from "~/src/types/Common";

interface UserInfo extends Identity {
	userName: string;
	fullName: string | null;
	email: string | null;
}
export interface UserSession {
	user: UserInfo;
	rights: Array<"MANAGER" | "ADMIN" | "USER">;
	isAdmin: boolean;
}
