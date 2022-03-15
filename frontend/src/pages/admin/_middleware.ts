import type { NextFetchEvent, NextRequest } from "next/server";
import { useSession } from "next-auth/react";
import { getToken } from "next-auth/jwt";
import { NextApiRequest } from "next";
import { NextResponse } from "next/server";
import {UserSession} from "~/src/types/UserInfo";

export async function middleware(req: NextApiRequest | Pick<NextApiRequest, "cookies" | "headers">, event: NextFetchEvent) {
	const session = await getToken({ req, secret: process.env.NEXTAUTH_SECRET }) || {};
	const userInfo = session?.user as UserSession
	console.log('userInfo',userInfo)
	if(!userInfo) return NextResponse.redirect("/");
	return NextResponse.next();
}
