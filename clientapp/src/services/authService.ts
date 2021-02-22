import axios from 'axios'

const API_URL ='https://localhost:5001/api/';

interface AuthData{
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
    login(authData: AuthData){
        return axios
        .post<ResponseData>(API_URL + 'account/login',authData)
        .then(responce=>{
            if(responce.data){
                localStorage.setItem('user',responce.data.token)
            }

            return responce.data
        })
    }

    register(userData:SignUpData){
        return axios.post(API_URL + 'account/sign-up', userData)
    }

    logout(){
        localStorage.removeItem('user')
    }
}

export default new AuthService()