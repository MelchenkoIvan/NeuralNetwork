import React, {FC, useEffect} from "react";
import { Formik, Field, Form, FormikHelpers } from 'formik';
import { Header, Button, Input, Icon} from 'semantic-ui-react'
import { useAppDispatch, useAppSelector} from '../app/hooks'
import './LoginForm.css';
import { useNavigate } from "react-router-dom";
import { Symptomes } from "../features/symptomesSlice";
import InputField from "./InputField";
import * as Yup from 'yup';

const initialValues: Symptomes = {
    Age: null,
    Sex: null,
    Trestbps: null,
    Chol: null,
    Fbs: null,
    Restecg: null,
    Thalach: null,
    Exang: null,
    Oldpeak: null,
    Slope: null,
    Ca: null,
    Thal: null
}
const SymptomesSchemaValidation = Yup.object().shape({
    Age: Yup.number().moreThan(0),
    Sex: Yup.number().moreThan(0).lessThan(1),
    Trestbps: Yup.number().moreThan(0),
    Chol: Yup.number().moreThan(0),
    Fbs: Yup.number().moreThan(0),
    Restecg: Yup.number().moreThan(0),
    Thalach: Yup.number().moreThan(0),
    Exang: Yup.number().moreThan(0),
    Oldpeak: Yup.number().moreThan(0),
    Slope: Yup.number().moreThan(0),
    Ca: Yup.number().moreThan(0),
    Thal: Yup.number().moreThan(0)
})
const SymptomesForm = () => {
    const currentUser = useAppSelector((state) => state.user.userName)
    const navigate = useNavigate();
  
    useEffect(() => {
      if(currentUser.length == 0) 
        navigate("/login")
    }, [currentUser]);

    return(
        <Formik
            initialValues={initialValues}
            validationSchema={SymptomesSchemaValidation}
            onSubmit={(value: Symptomes, {setSubmitting}:FormikHelpers<Symptomes>) => {
                setSubmitting(false)
            }
        }>
            {({ handleSubmit, isSubmitting, errors, touched }) => (
                <Form
                    onSubmit={handleSubmit}
                    autoComplete="off"
                    className="loginForm"
                >
                    <Header
                        as="h2"
                        icon ="hire a helper"
                        textAlign="center"
                        content="Symptomes form"
                        className="loginFormHeader"
                    />
                    <div className="userAndPassword">
                        <InputField fieldName="Age" placeholder="Type your age"/>
                        <InputField fieldName="Sex" placeholder="Type your sex"/>
                        <InputField fieldName="Trestbps" placeholder="Type your Trestbps"/>
                        <InputField fieldName="Chol" placeholder="Type your Chol"/>
                        <InputField fieldName="Fbs" placeholder="Type your Fbs"/>
                        <InputField fieldName="Restecg" placeholder="Type your Restecg"/>
                        <InputField fieldName="Thalach" placeholder="Type your Thalach"/>
                        <InputField fieldName="Exang" placeholder="Type your Exang"/>
                        <InputField fieldName="Oldpeak" placeholder="Type your Oldpeak"/>
                        <InputField fieldName="Slope" placeholder="Type your Slope"/>
                        <InputField fieldName="Ca" placeholder="Type your Ca"/>
                        <InputField fieldName="Thal" placeholder="Type your Thal"/>
                    </div>
                
                    <Button
                        loading={isSubmitting}
                        className="loginButton"
                        content="Predict"
                        type="submit"
                        fluid
                    />
            </Form>
            )}
        </Formik>
    )
}
export default SymptomesForm;