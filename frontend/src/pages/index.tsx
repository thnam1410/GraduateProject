import React from "react";
import { useRouter } from "next/router";
import Link from "next/link";
import dynamic from "next/dynamic";
import useBrowser from "~/src/hooks/useBrowser";
import BusMap from "./bus-map/index";
export default function Index() {
	const router = useRouter();
	const checkBrowser = useBrowser();
	if (!checkBrowser) return null;
	return <div>{/* <BusMap></BusMap> */}</div>;
}
