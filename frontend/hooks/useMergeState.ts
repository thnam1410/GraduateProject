import { useState } from "react";

const useMergeState = <T>(initialState: T): [T, (newState: Partial<T>) => void] => {
	const [state, setState] = useState<T>(initialState);

	const mergeState = (newState: Partial<T>) => {
		setState((prev) => ({ ...prev, ...newState }));
	};
	return [state, mergeState];
};

export default useMergeState;
