import React from "react";
import { getSession, signOut, useSession } from "next-auth/react";

const AdminPage = () => {
	const session = useSession();
	return (
		<button
			onClick={() => {
				signOut();
			}}
		>
			Logout
		</button>
	);
};

export default AdminPage;
