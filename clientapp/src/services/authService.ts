import axios from 'axios'

const API_URL = 'https://localhost:5001/api/';

interface SignInData {
    email: string,
    password: string
}

interface SignUpData {
    userEmail: string,
    firstName: string,
    lastName: string,
    password: string
}

interface SignInResponseData {
    email: string,
    message: string,
    name: string,
    token: string
}

class AuthService {
    signIn(authData: SignInData) {
        return axios.post<SignInResponseData>(API_URL + 'User/signin', authData, { withCredentials: true })
    }

    signUp(userData: SignUpData) {
        return axios.post(API_URL + 'User/signup', userData, { withCredentials: true }).
            then(
                response => {
                    const message = 'Registration was successful! A message was sent to you with a link to confirm your email address. Follow the link and log in to start using the service!'
                    return Promise.resolve(message)
                },
                error => {
                    const message = (error.response && error.response.data && error.response.data.error.Message) ||
                        error.message ||
                        error.toString()
                    return Promise.reject(message);
                })
    }

    forgotPassword(userEmail: String) {
        return axios.post(API_URL + 'User/forgotpassword', { email: userEmail })
            .then(response => {
                let message = "A message was sent to your email address with the order of further actions"
                if (response.data.message) {
                    message = response.data.message
                }
                return Promise.resolve(message)
            }, error => {
                const message = (error.response && error.response.data && error.response.data.error.Message) ||
                    error.message ||
                    error.toString()
                return Promise.reject(message);
            })
    }

    resetPassword(newPassword: string) {
        return axios.post(API_URL + 'User/resetpassword/', { newPassword: newPassword }, { withCredentials: true })
            .then(response => {
                const message = "Password changed successfully"
                return Promise.resolve(message)
            }, error => {
                const message = (error.response && error.response.data && error.response.data.error.Message) ||
                    error.message ||
                    error.toString()
                return Promise.reject(message);
            })
    }

    logout() {
        localStorage.removeItem('user')
    }
}

export default new AuthService()