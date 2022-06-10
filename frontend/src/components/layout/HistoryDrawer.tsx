import { NextPage } from "next";
import { useEffect } from "react";
import { ApiUtil, BASE_API_PATH } from "~/src/utils/ApiUtil";
import { useStore } from "~/src/zustand/store";
import ItemDrawer from "./ItemDrawer";

const HisotryDrawer: NextPage<any> = (props) => {
    const {children} = props;
	const isOpen = useStore((state) => state.isOpen);
	const setIsOpen = useStore((state) => state.setIsOpen);
	const infoRouteSearch: any = useStore((state) => state.infoRouteSearch);

  useEffect(() => {
		
	}, [infoRouteSearch]);


  const handleOnChangeRoute = (userId: number) => () => {
		ApiUtil.Axios.get(BASE_API_PATH + `/route/get-route-info-search/` + userId)
			.then((res) => {
				if (res.data?.success) {
            console.log("res",res)
                    
				}
			})
			.catch((err) => {
				console.log("err", err);
			});
	};


    return (<>
   <main
      className={
        " absolute overflow-hidden z-10 bg-gray-900 bg-opacity-35 inset-0 transform ease-in-out  transition-opacity opacity-100 duration-500 translate-x-0"
      }
    >
        <article className="relative w-screen max-w-lg flex flex-col space-y-6 overflow-y-scroll h-full">
          <p className="text-violet-50 p-4 font-bold text-lg">Lịch sử tìm kiếm</p>
          <ItemDrawer />
        </article>
    </main>
    </>)
}
export default HisotryDrawer;
