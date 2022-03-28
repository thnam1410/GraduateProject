import React from "react";
import {useRouter} from "next/router";
import Link from "next/link";

export default function Index() {
	const router = useRouter();
	return <div>
		<span>Homepage</span>
		<Link href={"/auth/login"}>
			<a>Login</a>
		</Link>
	</div>
}
