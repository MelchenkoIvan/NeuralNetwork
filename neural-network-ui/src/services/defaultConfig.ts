import axios, { AxiosError, AxiosResponse} from 'axios';
import notificationService from './notificationService';
import { store } from '../app/store';


export const api = axios.create({
    baseURL: 'http://localhost:5124/'
}); 
api.interceptors.request.use(async config => {
    const authdata = store ? store.getState().user.authdata : ""
    if(authdata.length > 0){
        config.headers['Authorization'] = 'Basic ' + authdata
    }
    return await config;
},
error => {
  Promise.reject(error)
})
  
api.interceptors.response.use(async response => {
    return await response;
}, (error: AxiosError) => {
    const {data, status}: AxiosResponse = error.response!;
    
    const message = data.message;
    switch(status){
        case 400:
            notificationService.BadRequest();
            break;
        case 401 :
            notificationService.Unauthorised();
            break;
        case 404:
            notificationService.NotFound();
            break;
        case 500:
            notificationService.SomethingWasWrong(message)
            break;
        default:
            break;
    }
    return Promise.reject(error);
});