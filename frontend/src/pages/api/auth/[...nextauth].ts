import CredentialsProvider from "next-auth/providers/credentials";
import NextAuth, { NextAuthOptions } from "next-auth";
import { ApiUtil } from "../../utils/ApiUtil";
import { LOGIN_API } from "../../../constants/apis/auth.api";
import { ApiResponse } from "~/src/pages/types/api.type";
import { NextApiRequest, NextApiResponse } from "next";
import { Cookie } from "next-auth/core/lib/cookie";

const nextAuthOptions = (req: NextApiRequest, res: NextApiResponse): NextAuthOptions => {
	return {
		providers: [
			CredentialsProvider({
				name: "Credentials",
				credentials: {
					userName: { label: "Username", type: "text", placeholder: "Username..." },
					password: { label: "Password", type: "password" },
				},
				authorize: async (credentials, req) => {
					const formValues = { userName: credentials?.userName, password: credentials?.password };
					const response = await ApiUtil.Axios.post<ApiResponse>(LOGIN_API, formValues, { withCredentials: true });
					if (response.data.success) {
						const cookies = response.headers["set-cookie"] as string | number | readonly string[];
						res.setHeader("Set-Cookie", cookies);
						return response?.data?.result;
					} else {
						console.log("response?.data", response?.data);
						throw new Error(response?.data?.message);
					}
				},
			}),
		],
		session:{
			maxAge: 60 * 60 * 12
		},
		callbacks: {
			async jwt({ token, user }) {
				if (user) token.user = user;
				return token;
			},
			async session({ session, token }) {
				if (token?.user) session.user = token?.user as any;
				session.accessToken = token.accessToken;
				return session;
			},
		},
		pages: {
			signIn: "/auth/login",
			error: "/auth/login",
		},
	};
};

export default (req: NextApiRequest, res: NextApiResponse) => {
	return NextAuth(req, res, nextAuthOptions(req, res));
};
