export const setAuthToken = (token: string) => {
    localStorage.setItem("authToken", token);
}