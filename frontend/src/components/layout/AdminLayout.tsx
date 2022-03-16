import React, { useContext, useMemo, useRef } from "react";
import { useSession } from "next-auth/react";
import { UserSession } from "~/src/types/UserInfo";
import AdminNavbar from "~/src/components/navbars/AdminNavbar";
import SideBar from "~/src/components/SideBar/SideBar";

interface SessionContext {
	userInfo: UserSession;
}
export type SideBarRef = {
	setCollapsed: (val: boolean) => void;
	collapsed: boolean;
};
export const SessionContext = React.createContext<SessionContext | null>(null);

const AdminLayout: React.FC = ({ children }) => {
	const sideBarRef = useRef<SideBarRef>(null);
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
				<div className="flex">
					<SideBar ref={sideBarRef} />
					<div className="flex flex-col w-full h-screen">
						<AdminNavbar sideBarRef={sideBarRef} />
						<div className="px-4 md:px-10 mx-auto w-full">{children}</div>
					</div>
				</div>
			</SessionContext.Provider>
		</>
	);
};
export const useSessionContext = () => useContext(SessionContext);
export default AdminLayout;
