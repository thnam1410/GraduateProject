import { Checkbox } from "antd";

export default class UserAccountColumn {
	static columns = [
		{
			title: "Họ và tên",
			dataIndex: "fullName",
			key: "fullName",
			width: 500,
			minWidth: 120,
		},
		{
			title: "Email",
			dataIndex: "email",
			key: "email",
			width: 300,
			minWidth: 120,
		},
		{
			title: "Kích hoạt",
			dataIndex: "active",
			key: "active",
			width: 300,
			minWidth: 120,
			render: (text: any, record: any) => {
				const active = record.active;
				return (
					<div className="flex justify-center">
						<Checkbox checked={active} />
					</div>
				);
			},
		},
		{
			title: "Tên tài khoản",
			dataIndex: "userName",
			key: "userName",
			width: 300,
			minWidth: 120,
		},
		{
			title: "Số điện thoại",
			dataIndex: "phoneNumber",
			key: "phoneNumber",
			width: 300,
			minWidth: 120,
		},
		// {
		// 	title: "Hành động",
		// 	dataIndex: "phoneNumberConfirmed",
		// 	key: "phoneNumberConfirmed",
		// 	width: 300,
		// 	minWidth: 120,
		// 	render: (text: any, record: any) => (
		// 		<div className="flex">
		// 			<button
		// 				data-tooltip-target="tooltip-dark"
		// 				className="flex-none hover:bg-sky-700 bg-blue-500 text-white font-bold py-2 px-2 rounded mr-2"
		// 				onClick={onCreate}
		// 				type="submit"
		// 			>
		// 				<svg className="w-5 h-5" viewBox="0 0 183.792 183.792">
		// 					<path d="M54.734 9.053C39.12 18.067 27.95 32.624 23.284 50.039c-4.667 17.415-2.271 35.606 6.743 51.22 12.023 20.823 34.441 33.759 58.508 33.759a67.31 67.31 0 0 0 22.287-3.818l30.364 52.592 21.65-12.5-30.359-52.583c10.255-8.774 17.638-20.411 21.207-33.73 4.666-17.415 2.27-35.605-6.744-51.22C134.918 12.936 112.499 0 88.433 0 76.645 0 64.992 3.13 54.734 9.053zm70.556 37.206c5.676 9.831 7.184 21.285 4.246 32.25-2.938 10.965-9.971 20.13-19.802 25.806a42.466 42.466 0 0 1-21.199 5.703c-15.163 0-29.286-8.146-36.857-21.259-5.676-9.831-7.184-21.284-4.245-32.25 2.938-10.965 9.971-20.13 19.802-25.807A42.47 42.47 0 0 1 88.433 25c15.164 0 29.286 8.146 36.857 21.259z" />
		// 				</svg>
		// 			</button>

		// 			<button
		// 				data-tooltip-target="tooltip-dark"
		// 				className="flex-none hover:bg-sky-700 bg-red-500 text-white font-bold py-2 px-2 rounded mr-2"
		// 				onClick={showDeleteConfirm}
		// 				type="button"
		// 			>
		// 				<svg className="w-5 h-5 mr-1">
		// 					<path d="M6 19c0 1.1.9 2 2 2h8c1.1 0 2-.9 2-2V7H6v12zM19 4h-3.5l-1-1h-5l-1 1H5v2h14V4z" />
		// 				</svg>
		// 			</button>
		// 		</div>
		// 	),
		// },
	];
}
