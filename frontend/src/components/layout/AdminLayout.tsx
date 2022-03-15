import React, { useMemo } from "react";
import { useSession } from "next-auth/react";
import { UserSession } from "~/src/types/UserInfo";

interface SessionContext {
	userInfo: UserSession;
}

export const SessionContext = React.createContext<SessionContext | null>(null);

const AdminLayout: React.FC = ({ children }) => {
	const session = useSession();
	const userInfo = session?.data?.user as UserSession;
	const value = useMemo(
		() => ({
			userInfo,
		}),
		[userInfo]
	);
	return (
		<>
			<SessionContext.Provider value={value}>
				<div>Navbar</div>
				<main>{children}</main>
			</SessionContext.Provider>
		</>
	);
};

export default AdminLayout;
