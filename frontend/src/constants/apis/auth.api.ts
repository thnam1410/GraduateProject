import { BASE_API_PATH } from "~/src/pages/utils/ApiUtil";

export const LOGIN_API = BASE_API_PATH + "/identity/login";
export const REGISTER_API = BASE_API_PATH + "/identity/register";
export const LOGOUT_API = BASE_API_PATH + "/identity/logout";
export const CHECK_LOGIN_API = BASE_API_PATH + "/identity/check-login";


// UserAccount
export const LOAD_USER_ACCOUNT = BASE_API_PATH + "/user-account/user-account-list"