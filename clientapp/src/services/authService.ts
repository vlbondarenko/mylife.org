import axios from 'axios'

const API_URL ='http://localhost:5000/api/';

interface AuthData{
    email: string,
    password: string
}

interface UserData extends AuthData{
    firstName: string,
    lastName: string
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

    register(userData:UserData){
        return axios.post<ResponseData>(API_URL + 'account/register',userData)
    }

    logout(){
        localStorage.removeItem('user')
    }
}

export default new AuthService()