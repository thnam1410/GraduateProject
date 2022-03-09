import type { NextPage } from "next";
import { ApiUtil } from "~/utils/ApiUtil";
import { CHECK_LOGIN_API, LOGOUT_API } from "~/constants/apis/auth.api";

const Home: NextPage = () => {
	return (
		<>
			<button
				onClick={async () => {
					await ApiUtil.Axios.get(CHECK_LOGIN_API).then((res) => console.log("res", res?.data?.result));
				}}
			>
				Check login
			</button>
			<br/>
			<br/>
			<br/>
			<button
				onClick={async () => {
					await ApiUtil.Axios.post(LOGOUT_API).then((res) => console.log("res", res));
				}}
			>
				Log out
			</button>
		</>
	);
};

export default Home;
