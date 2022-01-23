import axios from "axios";

const AxiosClient = axios.create({
	baseURL: process.env.NEXT_PUBLIC_API_URL,
});

export default AxiosClient;
// export const GLOBAL_DOMAIN_PATH = window.location.protocol + "//" + window.location.host;
export const BASE_API_PATH = "/api";
