import { Identity } from "./Common";

export interface Role extends Identity {
	code: string;
	displayName: string;
	name: string;
}
