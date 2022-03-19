import React, { ReactElement } from "react";
import { signOut } from "next-auth/react";
import AdminLayout, { useSessionContext } from "~/src/components/layout/AdminLayout";
import "antd/dist/antd.css";
import { Table } from "antd";

const columns = [
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
	{
		title: "Xác nhận số điện thoại",
		dataIndex: "phoneNumberConfirmed",
		key: "phoneNumberConfirmed",
		width: 300,
		minWidth: 120,
	},
	{
		title: "Hành động",
		dataIndex: "phoneNumberConfirmed",
		key: "phoneNumberConfirmed",
		width: 300,
		minWidth: 120,
		render: (text: any, record: any) => (
			<button className="bg-blue-500 text-white font-bold py-2 px-4 rounded" onClick={() => console.log(record)}>
				{"Button Text"}
			</button>
		),
	},
];

const UserAccount = () => {
	const context = useSessionContext();
	const dataSource = [
		{
			fullName: "Phạm Ngọc Danh",
			email: "phamngocdanhhcm@gmail.com",
			active: true,
			userName: "phamngocdanhhcm",
			phoneNumber: "0927140859",
			phoneNumberConfirmed: true,
		},
		{
			fullName: "Phạm Ngọc Kiên",
			email: "phamngocdanhhcm@gmail.com",
			active: true,
			userName: "phamngockienhcm",
			phoneNumber: "0932621181",
			phoneNumberConfirmed: false,
		},
	];
	return (
		<div>
			<div className="relative h-20 r-0">
				<button className="absolute top-0 right-0 h-10 w-25 bg-blue-500 text-white font-bold py-2 px-4 rounded" onClick={() => {}}>
					{"Tạo mới"}
				</button>
			</div>
			<Table style={{ width: "auto" }} dataSource={dataSource} columns={columns} />
		</div>
	);
};

UserAccount.getLayout = function getLayout(page: ReactElement) {
	return <AdminLayout>{page}</AdminLayout>;
};

export default UserAccount;

// const UserAccount = () => {
// 	const [checkbox, setCheckbox] = useState({
// 		1: false,
// 		2: false,
// 		3: false,
// 	});
// 	const [value, setValue] = useState("");
// 	const [keywords, setKeywords] = useState("");
// 	const [searchDataSource, setSearchDataSource] = useState<any>([]);
//
// 	const onChangeCheckbox = (value: boolean, name: number) => {
// 		setCheckbox({ ...checkbox, [name]: value });
// 	};
// 	const onChangeRadio = (_value: any) => {
// 		// value will be item value
// 	};
// 	const submitHandle = (sentValue: string) => setValue(sentValue);
// 	const status: Status[] = ["Info", "Success", "Danger", "Primary", "Warning", "Basic", "Control"];
//

// 	const onSearch = async (info: { values: any; page: number; pageSize: number }) => {
// 		return {
// 			dataSource: [],
// 			total: 10,
// 		};
// 	};
// 	return (
// 		<Col breakPoint={{ xs: 12 }}>
// 			<Card>
// 				<Tabs activeIndex={0} fullWidth>
// 					<Tab
// 						title="Lưới thông tin"
// 						icon="icon ion-ios-star-outline"
// 						badge={{ status: "Info", title: "11", position: "bottomEnd" }}
// 						responsive
// 					>
// 						<div
// 							style={{
// 								display: "block",
// 								padding: 30,
// 							}}
// 						>
// 							<Row>
// 								<Col key={"Success"} style={style} breakPoint={{ xs: true }}>
// 									<Button appearance="hero" status={"Success"}>
// 										{"Tạo mới"}
// 									</Button>
// 								</Col>
// 							</Row>
// 							<Table style={{ width: "auto" }} dataSource={dataSource} columns={columns} />
// 						</div>
// 					</Tab>
// 					<Tab
// 						title="Thông tin"
// 						icon="icon ion-ios-home"
// 						badge={{ status: "Danger", title: "11", position: "topStart" }}
// 						responsive
// 					>
// 						<Row>
// 							<Col breakPoint={{ xs: 12 }} key={""}>
// 								<Card>
// 									<header> Tìm kiếm thông tin</header>
// 									<CardBody>
// 										<Search
// 											submit={(v) => submitHandle(v)}
// 											type={"column-curtain"}
// 											placeholder="Search..."
// 											hint="Hit Enter to search"
// 										/>
// 									</CardBody>
// 									<footer>You Search for: </footer>
// 								</Card>
// 							</Col>
// 						</Row>
// 						<Row>
// 							<Col breakPoint={{ xs: 12 }}>
// 								<Card>
// 									<header>Thông tin tài khoản</header>
// 									<CardBody>
// 										<Row>
// 											<Col style={{}} breakPoint={{ xs: 12, md: 2 }}>
// 												<Input>
// 													<p> Họ và tên </p>
// 												</Input>
// 											</Col>
// 											<Col breakPoint={{ xs: 12, md: 10 }}>
// 												<Input fullWidth shape="SemiRound">
// 													<input type="text" placeholder="" />
// 												</Input>
// 											</Col>
// 										</Row>
// 										<Row>
// 											<Col style={{}} breakPoint={{ xs: 12, md: 2 }}>
// 												<Input>
// 													<p> Email </p>
// 												</Input>
// 											</Col>
// 											<Col breakPoint={{ xs: 12, md: 10 }}>
// 												<Input fullWidth shape="SemiRound">
// 													<input type="text" placeholder="" />
// 												</Input>
// 											</Col>
// 										</Row>
// 										<Row>
// 											<Col style={{}} breakPoint={{ xs: 12, md: 2 }}>
// 												<Input>
// 													<p> Kích hoạt </p>
// 												</Input>
// 											</Col>
// 											<Col breakPoint={{ xs: 12, md: 10 }}>
// 												<Radio
// 													name="radio"
// 													onChange={onChangeRadio}
// 													options={[
// 														{
// 															value: "value 1",
// 															label: "Hoạt động",
// 															checked: true,
// 														},
// 														{
// 															value: "value 2",
// 															label: "Không hoạt động",
// 															status: "Info",
// 														},
// 													]}
// 												/>
// 											</Col>
// 										</Row>
// 										<Row>
// 											<Col style={{}} breakPoint={{ xs: 12, md: 2 }}>
// 												<Input>
// 													<p> Tên tài khoản </p>
// 												</Input>
// 											</Col>
// 											<Col breakPoint={{ xs: 12, md: 10 }}>
// 												<Input fullWidth shape="SemiRound">
// 													<input type="text" placeholder="" />
// 												</Input>
// 											</Col>
// 										</Row>
// 										<Row>
// 											<Col style={{}} breakPoint={{ xs: 12, md: 2 }}>
// 												<Input>
// 													<p> Số điện thoại </p>
// 												</Input>
// 											</Col>
// 											<Col breakPoint={{ xs: 12, md: 10 }}>
// 												<Input fullWidth shape="SemiRound">
// 													<input type="number" placeholder="" />
// 												</Input>
// 											</Col>
// 										</Row>
// 										<Row>
// 											<Col style={{}} breakPoint={{ xs: 12, md: 2 }}>
// 												<Input>
// 													<p> Kích hoạt xác thận 2 lớp </p>
// 												</Input>
// 											</Col>
// 											<Col breakPoint={{ xs: 12, md: 10 }}>
// 												<Radio
// 													name="radio-two-factor"
// 													onChange={onChangeRadio}
// 													options={[
// 														{
// 															value: "value 1",
// 															label: "Kích hoạt",
// 															checked: true,
// 														},
// 														{
// 															value: "value 2",
// 															label: "Không kích hoạt",
// 															status: "Info",
// 														},
// 													]}
// 												/>
// 											</Col>
// 										</Row>
// 									</CardBody>
// 								</Card>
// 							</Col>
// 						</Row>
// 					</Tab>
//
// 					<Tab
// 						title="Lưới thông tin"
// 						icon="icon ion-ios-switch"
// 						badge={{ status: "Success", title: "11", position: "topStart" }}
// 						responsive
// 					>
// 						<h1>Content 3</h1>
// 					</Tab>
// 				</Tabs>
// 			</Card>
// 		</Col>
// 	);
// };
// export default UserAccount;
