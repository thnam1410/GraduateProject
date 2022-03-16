import React, { ReactElement } from "react";
import { signOut } from "next-auth/react";
import AdminLayout, { useSessionContext } from "~/src/components/layout/AdminLayout";

const AdminPage = () => {
	const context = useSessionContext();
	return <div>abc</div>;
};

AdminPage.getLayout = function getLayout(page: ReactElement) {
	return <AdminLayout>{page}</AdminLayout>;
};

export default AdminPage;
