import axios from 'axios'

const API_URL ='http://localhost:8080/api/auth/';

interface AuthData{
    email: string,
    password: string
}

class AuthService {
    login(authData: AuthData){
        return axios
        .post(API_URL + 'signup',authData)
        .then(responce=>{
            if(responce.data.accesToken){
                localStorage.setItem('user',responce.data)
            }

            return responce.data
        })
    }
}

export default new AuthService()