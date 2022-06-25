import React, { useRef, useState } from "react";
import GooglePlacesAutocomplete, { geocodeByPlaceId } from "react-google-places-autocomplete";
import { GoogleAddress } from "~/src/types/Common";
import { Button } from "antd";
import { ApiUtil, BASE_API_PATH } from "~/src/utils/ApiUtil";
import { RoutePathDto, useMapControlStore } from "~/src/zustand/MapControlStore";
import { isEmpty } from "lodash";
import BusTripView from "./BusTripView";
import Overlay, { OverlayRef } from "~/src/components/Overlay/Overlay";
import { MapControlStoreV2, useMapControlStoreV2 } from "~/src/zustand/MapControlStoreV2";

const SearchView = () => {
	const [startPosition, setStartPosition] = useState<GoogleAddress | null>(null);
	const [endPosition, setEndPosition] = useState<GoogleAddress | null>(null);
	const [isDisableButton, setIsDisableButton] = useState<boolean>(false);
	const setPath = useMapControlStoreV2((state) => state.setPath);
	const overlayRef = useRef<OverlayRef>(null);
	const onChangePoint = (setStateFn: React.Dispatch<React.SetStateAction<GoogleAddress | null>>) => (address: GoogleAddress) => {
		setStateFn(address);
		if (isDisableButton) setIsDisableButton(!isDisableButton);
	};

	const onFindRoute = () => {
		if (!startPosition || !endPosition) return ApiUtil.ToastError("Vui lòng nhập đầy đủ các điểm đi và điểm đến!");
		overlayRef.current?.open("Hệ thống đang xử lý, vui lòng chờ trong giây lát...");
		Promise.all([geocodeByPlaceId(startPosition?.value?.place_id), geocodeByPlaceId(endPosition?.value?.place_id)])
			.then(([resStart, resEnd]) => {
				const formBody = {
					startPoint: {
						lat: resStart[0]?.geometry?.location?.lat(),
						lng: resStart[0]?.geometry?.location?.lng(),
					},
					endPoint: {
						lat: resEnd[0]?.geometry?.location?.lat(),
						lng: resEnd[0]?.geometry?.location?.lng(),
					},
				};
				ApiUtil.Axios.post(BASE_API_PATH + "/route/find-path-v2", formBody)
					.then((res) => {
						const result = res?.data?.result as Pick<MapControlStoreV2, "positions" | "stops">;
						if (!isEmpty(result)) {
							console.log('result',result)
							setPath(result);
						}
					})
					.catch((err) => {
						throw err;
					})
					.finally(() => {
						overlayRef.current?.close();
					});
			})
			.catch((err) => {
				console.log("err", err);
				ApiUtil.ToastError("Có lỗi xảy ra!");
			});
	};

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
				<Button type="primary" className="rounded self-center" style={{ width: 170 }} onClick={onFindRoute}>
					Tìm tuyến đường
				</Button>
			</div>
			<div className="container mb-2  w-full items-center justify-center">
				<BusTripView />
			</div>
			<Overlay ref={overlayRef} />
		</div>
	);
};

export default SearchView;
