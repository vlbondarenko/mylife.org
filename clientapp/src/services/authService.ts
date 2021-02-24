import axios from 'axios'

const API_URL ='https://localhost:5001/api/';

interface SignInData{
    email: string,
    password: string
}

interface SignUpData {
    userEmail:string,
    firstName: string,
    lastName: string,
    password:string
}

interface ResponseData {
    email:string,
    message:string,
    name:string,
    token:string
}

class AuthService {
    login(authData: SignInData){
        return axios.post<ResponseData>(API_URL + 'account/sign-in',authData)
    }

    register(userData:SignUpData){
        return axios.post(API_URL + 'account/sign-up', userData)
    }

    forgotPassword(userEmail: String){
        return axios.post(API_URL +'account/forgot-password', { email: userEmail})
        .then(response => {
            let message = "A message was sent to your email address with the order of further actions"
            if(response.data.message){
                message = response.data.message
            }   
            return Promise.resolve(message)
        }, (error) =>{
            const message = (error.response&&error.response.data&&error.response.data.error.Message)||
                error.message||
                error.toString()
                return Promise.reject(message);
        })
    }

    resetPassword(newPassword:string) {
        return axios.post(API_URL + 'account/reset-password', { newPassword: newPassword })
        .then(response => {
            const message = "Password changed successfully"
            return Promise.resolve(message)
        }, error =>{
            const message = (error.response&&error.response.data&&error.response.data.error.Message)||
                error.message||
                error.toString()
                return Promise.reject(message);
        })
    }

    logout(){
        localStorage.removeItem('user')
    }
}

export default new AuthService()