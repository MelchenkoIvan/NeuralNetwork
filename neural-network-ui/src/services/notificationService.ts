import { toast } from 'react-toastify';

const notificationService = {
    Successful() {
        toast.success(`Successful!`);
    },
    BadRequest () {
        toast.error(`Bad Request!`);
    },
    Unauthorised () {
        toast.error(`Unauthorised!`);
    },
    NotFound () {
        toast.error(`Not Found!`);
    },
    SomethingWasWrong (message: string) {
        toast.error(`Something was wrong! Message: ${message}`);
    },
    WrongPasswordOrLogin(){
        toast.error(`Wrong password or login!`)
    },
    PredictionRequestSuccess(){
        toast.success(`The symptoms were correctly loaded into the queue, please wait for the result to appear in the table!`);
    }
};

export default notificationService;