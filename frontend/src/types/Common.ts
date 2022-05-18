export interface Identity {
	id: string;
}
export type Position = {
	lat: number;
	lng: number
}

export type GoogleAddress = {
	label: string;
	value: GoogleAddressValue
}
export type GoogleAddressValue = {
	description: string;
	place_id: string;
	reference: string;
}