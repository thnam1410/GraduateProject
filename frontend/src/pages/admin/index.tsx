import React, { ReactElement, useContext } from "react";
import { getSession, signOut, useSession } from "next-auth/react";
import AdminLayout, { SessionContext } from "~/src/components/layout/AdminLayout";

const AdminPage = () => {
	const context = useContext(SessionContext);
	// console.log('context',context);
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

AdminPage.getLayout = function getLayout(page: ReactElement) {
	return <AdminLayout>{page}</AdminLayout>;
};

export default AdminPage;
