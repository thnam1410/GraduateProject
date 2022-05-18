import React, { useState } from "react";
import GooglePlacesAutocomplete from "react-google-places-autocomplete";
import { GoogleAddress } from "~/src/types/Common";
import { Button } from "antd";

const SearchView = () => {
	const [startPosition, setStartPosition] = useState<GoogleAddress | null>(null);
	const [endPosition, setEndPosition] = useState<GoogleAddress | null>(null);

	const onChangePoint = (setStateFn: React.Dispatch<React.SetStateAction<GoogleAddress | null>>) => (address: GoogleAddress) => {
		setStateFn(address);
	};
	
	const onFindRoute = () => {
		console.log('startPosition',startPosition)
		console.log('endPosition',endPosition)
	}
	
	return (
		<div className="flex flex-col">
			<div className={"search-form flex flex-col justify-between"}>
				<div className="flex items-center mb-2">
					<label className="mr-1">Điểm đón</label>
					<div className="flex-1">
						<GooglePlacesAutocomplete
							apiKey={process.env.NEXT_PUBLIC_GG_PLACE_API_KEY}
							debounce={300}
							minLengthAutocomplete={3}
							apiOptions={{
								language: "VN",
								region: "VN",
							}}
							selectProps={{
								placeholder: "Nhập điểm đón",
								isClearable: true,
								onChange: onChangePoint(setStartPosition),
							}}
						/>
					</div>
				</div>
				<div className="flex items-center mb-2">
					<label className="mr-1">Điểm đến</label>
					<div className="flex-1">
						<GooglePlacesAutocomplete
							apiKey={process.env.NEXT_PUBLIC_GG_PLACE_API_KEY}
							debounce={300}
							minLengthAutocomplete={3}
							apiOptions={{
								language: "VN",
								region: "VN",
							}}
							selectProps={{
								placeholder: "Nhập điểm đến",
								isClearable: true,
								onChange: onChangePoint(setEndPosition),
							}}
						/>
					</div>
				</div>
				<Button type="primary"  className='rounded self-center' style={{width: 170}} onClick={onFindRoute}>Tìm tuyến đường</Button>
			</div>
		</div>
	);
};

export default SearchView;
