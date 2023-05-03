import React, {FC, useEffect} from "react";
import { Formik, Field, Form, FormikHelpers } from 'formik';
import { Header, Button, Input, Icon} from 'semantic-ui-react'
import { useAppDispatch, useAppSelector} from '../app/hooks'
import {login, UserLogin}from '../features/userSlice'
import { userService } from "../services/userService";
import './Form.css';
import { useNavigate } from "react-router-dom";

const LoginForm:FC = ({}) => {
    const dispatch = useAppDispatch();
    const authdata = useAppSelector((state) => state.user.authdata)
    const navigate = useNavigate();
    const Login = (user:UserLogin) => {
        userService.Login(user).then(data => {
            dispatch(login(data ?? ""))
        })
    }

    useEffect(() => {
      if(authdata.length > 0) 
        navigate("/")
    }, [authdata]);

    return(
        
        <Formik
        initialValues={{ userName: "", password: "" }}
        onSubmit={(value: UserLogin, {setSubmitting}:FormikHelpers<UserLogin>) => {
            Login(value)
            setSubmitting(false)
          }
        }
      >
        {({ handleSubmit, isSubmitting }) => (
          <Form
            onSubmit={handleSubmit}
            autoComplete="off"
            className="form"
          >
            
            <Header
              as="h2"
              icon ="user"
              textAlign="center"
              content="Sign in"
              className="formHeader"
            />
            <div className="userAndPassword">
              <label htmlFor="userName"><b>User Name: </b></label>
              <Field id="userName" name="userName" placeholder="John" />

              <label htmlFor="password"><b>Password: </b></label>
              <Field id="password" name="password" type="password" />
            </div>
            <Button
              loading={isSubmitting}
              className="loginButton"
              content="Login"
              type="submit"
              fluid
            />
          </Form>
        )}
      </Formik>
    )
}

export default LoginForm;