import type { NextFetchEvent, NextRequest } from "next/server";
import { NextResponse } from "next/server";
import { getToken } from "next-auth/jwt";
import { UserSession } from "~/src/types/UserInfo";
import { ApiUtil } from "~/src/pages/utils/ApiUtil";

export async function middleware(req: NextRequest, event: NextFetchEvent) {
	// @ts-ignore
	const session = (await getToken({ req, secret: process.env.NEXTAUTH_SECRET })) || {};
	const userInfo = session?.user as UserSession;
	if (!userInfo) {
		const nextPageName = req.page.name as string;
		if (nextPageName.includes("admin")) {
			return NextResponse.redirect("/auth/login?admin=1");
		}
		return NextResponse.redirect("/");
	}
	return NextResponse.next();
}
