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

     checkEmailForUniqueness(email:string){
        return axios.get(API_URL+'account/check-email', {params:email})
        .then( async response=>
            {
                return Promise.resolve(Boolean(await response.data.json()))
                // if(response.data.result=='true')
                //     return Promise.resolve(true)
                // else
                //     return Promise.resolve(false)
            })
    }
}

export default new AuthService()