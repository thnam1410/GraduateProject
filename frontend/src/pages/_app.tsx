import React, { ReactElement, ReactNode } from "react";
import { SessionProvider } from "next-auth/react";
import { AppProps } from "next/app";
import "../styles/globals.scss";
import { NextPage } from "next";

export default function MyApp({ Component, pageProps }: AppPropsWithLayout) {
	const getLayout = Component.getLayout ?? ((page) => page);
	return <SessionProvider session={pageProps.session}>{getLayout(<Component {...pageProps} />)}</SessionProvider>;
}

type NextPageWithLayout = NextPage & {
	getLayout?: (page: ReactElement) => ReactNode;
};

type AppPropsWithLayout = AppProps & {
	Component: NextPageWithLayout;
};
