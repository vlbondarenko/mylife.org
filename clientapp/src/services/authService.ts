import axios from 'axios'

const API_URL ='http://localhost:8080/api/auth/';

interface AuthData{
    email: string,
    password: string
}

interface UserData extends AuthData{
    firstName: string,
    lastName: string
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

    register(userData:UserData){
        return axios.post(API_URL + 'signup',userData)
    }

    logout(){
        localStorage.removeItem('user')
    }
}

export default new AuthService()