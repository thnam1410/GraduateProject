import {useForm} from "react-hook-form";
import {ApiUtil} from "~/src/utils/ApiUtil";

const ControllerNumberPost = (props: any) => {
	const {
		register,
		formState: { errors },
		handleSubmit,
	} = useForm();

	const onFinish = handleSubmit((values: any) => {
		const password = values.password;
		const confirmPassword = values.confirmPassword;
		if (password !== confirmPassword) return ApiUtil.ToastError("Mật khẩu xác nhận không trùng nhau ! Vui lòng kiểm tra lại");
		props.onFinishModal;
	});
	return (
		<>
			<div className="max-w-md mx-auto space-y-6">
				<form onSubmit={onFinish}>
					<label className="text-sm font-bold opacity-70" htmlFor="fullName">
						Họ và tên
					</label>
					<input type="text" className="p-3 mt-2 mb-4 w-full bg-slate-200 rounded" id="fullName" placeholder="Họ và tên" />
					<label className=" text-sm font-bold opacity-70" htmlFor="postNumber">
						Số lượng bài viết
					</label>
					<input
						type="number"
						className="p-3 mt-2 mb-4 w-full bg-slate-200 rounded"
						id="postNumber"
						placeholder="Số lượng"
						// {...register("email", {
						// 	required: {
						// 		value: true,
						// 		message: "Vui lòng nhập email",
						// 	},
						// 	minLength: {
						// 		value: 8,
						// 		message: "Ký tự không đủ cho địa chỉ email",
						// 	},
						// 	maxLength: {
						// 		value: 120,
						// 		message: "Ký tự vượt quá ký tự cho phép dành cho địa chỉ email",
						// 	},
						// 	pattern: {
						// 		value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
						// 		message: "Vui lòng kiểm tra lại các ký tự không đúng cho email",
						// 	},
						// })}
					/>
					{/* <ErrorMessage
						errors={errors}
						name="email"
						render={({ message }) => (message ? <p className="text-red-400">{message}</p> : null)}
					/> */}
					{/* <div className="mb-6 text-center">
						<button
							className="w-full px-4 py-2 font-bold text-white bg-blue-500 rounded-full hover:bg-blue-700 focus:outline-none focus:shadow-outline"
							type="submit"
						>
							Đăng ký
						</button>
					</div> */}
				</form>
			</div>
		</>
	);
};
export default ControllerNumberPost;
