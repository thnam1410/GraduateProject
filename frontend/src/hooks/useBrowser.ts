import React, { useEffect, useState } from "react";

const useBrower = () => {
	const [isBrowser, setIsBrowser] = useState(false);
	useEffect(() => {
		setIsBrowser(true);
	}, []);

	return isBrowser;
};

export default useBrower;
