import React from "react";
//import { reduxForm, Field, InjectedFormProps} from "redux-form";

interface LoginFormValuesType{
    userName: string,
    password: string
}

const LoginForm:React.FC<InjectedFormProps<LoginFormValuesType>> = ({handleSubmit}) => {

    return(
        <form onSubmit={handleSubmit}>
            <div>
                <label htmlFor="username">Username</label>
                <Field
                    name="username"
                    component="input"
                    type="text"
                />
            </div>

            <div>
                <label htmlFor="password">Password</label>
                <Field
                    name="password"
                    component="input"
                    type="password"
                />
            </div>
            <button type="submit">Submit</button>
        </form>
    )
}

export default reduxForm<LoginFormValuesType>({
    form: 'userLogin',
})(LoginForm)