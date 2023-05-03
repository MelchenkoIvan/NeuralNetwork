import * as conf from './defaultConfig';
import {UserLogin} from '../features/userSlice';
import notificationService from './notificationService';

export const userService = {
    async Login(user: UserLogin) {
        try {
            let data = await conf.api.post<string>('User/login', user)
                .then(response => response.data);

                if(data.length > 0) {
                    data = window.btoa(user.userName + ':' + user.password);
                    notificationService.Successful();
                }
                else notificationService.WrongPasswordOrLogin();
                
                return data;
        }
        catch (e) {
            console.log(e);
        }
    }
};
