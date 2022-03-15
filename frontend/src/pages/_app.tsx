import React from "react";
import {SessionProvider} from "next-auth/react";
import {AppProps} from "next/app";
import "../styles/globals.scss"

export default function MyApp({ Component, pageProps }: AppProps) {
	return (
		<SessionProvider session={pageProps.session}>
			<Component {...pageProps} />
		</SessionProvider>
	);
}
